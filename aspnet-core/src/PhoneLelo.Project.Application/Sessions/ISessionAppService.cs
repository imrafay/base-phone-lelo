using System.Threading.Tasks;
using Abp.Application.Services;
using PhoneLelo.Project.Sessions.Dto;

namespace PhoneLelo.Project.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
