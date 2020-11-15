using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Chat.Dto;

namespace PhoneLelo.Project.Chat.Service
{
    public interface IChatAppService : IApplicationService
    {    
        Task TestMessage(
            long user,
            string message);

        Task<ListResultDto<ChatMessageDto>> GetAll(
            ChatMessageFilterInputDto filter);

        Task Create(ChatMessageInputDto input);
    }
}
