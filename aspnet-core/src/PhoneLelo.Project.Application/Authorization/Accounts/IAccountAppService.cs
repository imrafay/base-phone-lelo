using System.Threading.Tasks;
using Abp.Application.Services;
using PhoneLelo.Project.Authorization.Accounts.Dto;

namespace PhoneLelo.Project.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
