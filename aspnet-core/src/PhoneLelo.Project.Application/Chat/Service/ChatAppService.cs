using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using PhoneLelo.Project.Pusher;
using PhoneLelo.Project.Pusher.Dto;

namespace PhoneLelo.Project.Chat.Service
{
    public class ChatAppService : ApplicationService, IChatAppService
    {
        private readonly IPusherManager _pusherManager; 

        public ChatAppService(
                    IPusherManager pusherManager)
        {
            _pusherManager = pusherManager;
        }

        [AbpAllowAnonymous]
        public async Task TestMessage(
            long userId,
            string message)
        {
            var input = new PusherEventInputDto()
            {
                UserId=userId,
                EventEnum = RealTimeEventEnum.TestUser,
                Message= message
            };
            await _pusherManager.TriggerPusherEvent(input);
        }
    }
}

