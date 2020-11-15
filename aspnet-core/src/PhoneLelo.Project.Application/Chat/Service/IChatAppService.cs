using System.Threading.Tasks;
using Abp.Application.Services;

namespace PhoneLelo.Project.Chat.Service
{
    public interface IChatAppService : IApplicationService
    {    
        Task TestMessage(
            long user,
            string message);
    }
}
