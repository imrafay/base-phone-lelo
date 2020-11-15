using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Threading;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Chat.Dto;
using PhoneLelo.Project.Chat.Enum;
using PhoneLelo.Project.Pusher;
using PhoneLelo.Project.Pusher.Dto;
using PhoneLelo.Project.Utils;

namespace PhoneLelo.Project.Chat.Service
{
    public class ChatAppService : ApplicationService, IChatAppService
    {
        private readonly IPusherManager _pusherManager;
        private readonly ChatMessageManager _chatMessageManager;

        public ChatAppService(
                    IPusherManager pusherManager,
                    ChatMessageManager chatMessageManager)
        {
            _pusherManager = pusherManager;
            _chatMessageManager = chatMessageManager;
        }

        [AbpAllowAnonymous]
        public async Task TestMessage(
            long userId,
            string message)
        {
            var input = new PusherEventInputDto()
            {
                UserId = userId,
                EventEnum = RealTimeEventEnum.TestUser,
                Message = message
            };
            await _pusherManager.TriggerPusherEvent(input);
        }
        public async Task<ListResultDto<ChatMessageDto>> GetAll(
            ChatMessageFilterInputDto filter)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = _chatMessageManager.GetChatMessages(
                    senderId: filter.SenderId,
                    receiverId: filter.ReceiverId);

                var totalCount = query.Count();

                if (query.Any() && filter.pagedAndSort != null)
                {
                    var pagedAndSort = filter.pagedAndSort;
                    var sortBy = pagedAndSort.SortBy.GetDescription();
                    query = query.OrderBy(sortBy).PageResult(
                        pagedAndSort.Page,
                        pagedAndSort.PageSize).Queryable;
                }

                return new PagedResultDto<ChatMessageDto>(
                    totalCount: totalCount,
                    items: query.ToList());
            }
        }

        public async Task Create(ChatMessageInputDto input)
        {
            if (input == null)
            {
                return;
            }

            var chatMessage = ObjectMapper.Map<ChatMessage>(input);
            if (chatMessage == null)
            {
                throw new UserFriendlyException(AppConsts.ErrorMessage.GeneralErrorMessage);
            }

            chatMessage.MessageStatus = MessageStatusEnum.Deliver;
            await _chatMessageManager.CreateAsync(chatMessage);

            #region Pusher Realtime Update
            var pusherEvent = new PusherEventInputDto()
            {
                UserId = input.ReceiverId,
                EventEnum = RealTimeEventEnum.TestUser,
                Message = input.Message
            };
            await _pusherManager.TriggerPusherEvent(pusherEvent);
            #endregion
        }
    }
}

