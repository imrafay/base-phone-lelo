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
using PhoneLelo.Project.Product.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
            };

            var payload = new
            {
                Message = message
            };
            await _pusherManager.TriggerPusherEvent(input, payload);
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
                EventEnum = RealTimeEventEnum.ChatMessage
            };

            var payload = new
            {
                Message= input.Message,
                SenderUserId = input.SenderId
            };
            await _pusherManager.TriggerPusherEvent(pusherEvent, payload);

            #endregion
        }

        public async Task<List<UserChatOutputDto>> GetUserChatsList(long userId)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var chatsQuery = _chatMessageManager.GetChatMessages(userId);
                var senderIds = new List<long>();

                senderIds.AddRange(chatsQuery.Where(x => x.SenderId != userId)
                        .Select(x => x.SenderId)
                        .ToList());
                senderIds.AddRange(chatsQuery.Where(x => x.ReceiverId != userId)
                        .Select(x => x.ReceiverId)
                        .ToList());

                var result = new List<UserChatOutputDto>();

                foreach (var senderId in senderIds.Distinct())
                {
                    var chat = await chatsQuery
                        .Where(x =>
                               (x.SenderId == senderId &&
                               x.ReceiverId == userId) ||
                               (x.SenderId == userId &&
                               x.ReceiverId == senderId))
                        .OrderByDescending(x => x.Date)
                        .FirstOrDefaultAsync();
                    var userChat = new UserChatOutputDto()
                    {
                        MessageStatus = chat.MessageStatus,
                        Date = chat.Date,
                        LastMessage = chat.Message,
                        SenderId = senderId,
                        SenderName = (chat.SenderId == senderId) ? chat.SenderName : chat.ReceiverName
                    };

                    result.Add(userChat);
                }

                return result;
            }
        }
    }
}

