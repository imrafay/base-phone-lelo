using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PhoneLelo.Project.Pusher.Dto;
using PhoneLelo.Project.Utils;
using PusherServer;
using PhoneLelo.Project.Storage;

namespace PhoneLelo.Project.Pusher
{

    public interface IPusherManager : IDomainService
    {
        Task TriggerPusherEvent(PusherEventInputDto input);
    }

    public class PusherManager : DomainService, IPusherManager
    {
        private readonly PusherServer.Pusher _pusher;
        public string AppId => "1107206";
        public string AppKey => "41230f4f4036d37fed30";
        public string AppSecret => "c964bd1001d8794aee9f";
        public string Cluster => "ap2";


        public PusherManager()
        {
            _pusher = new PusherServer.Pusher(
                appId: AppId,
                appKey: AppKey,
                appSecret: AppSecret,
                options: new PusherOptions
                {
                    Cluster = Cluster,
                    Encrypted = true
                }
            );
        }

        public async Task TriggerPusherEvent(PusherEventInputDto input)
        {
            await _pusher.TriggerAsync(
                        channelName: $"user-{input.UserId}",
                        eventName: input.EventEnum.GetDescription(),
                        data: input.Message
                    );
        }
    }
}
