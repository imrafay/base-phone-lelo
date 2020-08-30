using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Users.Dto;

namespace PhoneLelo.Project.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<long> SignUpUserByPhoneNumberAsync(
            string phoneNumber,
            string roleName);

        Task<bool> VerifyUserPhoneNumber(
            long userId,
            string verificationCode);

        Task UpdateUserProfile(UserDto input);

        Task<ListResultDto<RoleDto>> GetRegistrationRoles();

        Task<UserDto> GetUserForView();
    }
}
