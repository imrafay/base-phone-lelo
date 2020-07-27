using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Authorization.Accounts;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Uow;
using System;
using System.Reflection;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace PhoneLelo.Project.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly int _tenantId = 1;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;

        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }


        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        [AbpAllowAnonymous]
        public async Task UpdateUserProfile(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        [AbpAllowAnonymous]
        public async Task<long> SignUpUserByPhoneNumberAsync(
            string phoneNumber,
            string roleName)
        {
            //TODO : phone number validation(1 to 1)
            var user = new User()
            {
                Name = AppConsts.DefaultUserName,
                Surname = string.Empty,
                UserName = phoneNumber,
                EmailAddress = phoneNumber,
                IsActive = true,
                PhoneNumber = phoneNumber,
                TenantId = _tenantId
            };

            //TODO:Change this dummy confirmation
            user.IsEmailConfirmed = true;
            user.IsPhoneNumberConfirmed = false;

            await _userManager.InitializeOptionsAsync(_tenantId);
            CheckErrors(await _userManager.CreateAsync(user, AppConsts.DefaultUserPassword));

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                if (roleName != null)
                {
                    var roleNames = new List<string>() { roleName };
                    using (AbpSession.Use(_tenantId, user.Id))
                    {
                        await _userManager.SetRolesAsync(user, roleNames.ToArray());
                    }
                }

                user.PhoneNumberCode = GenerateAndSendVerificationCode(phoneNumber);
                await _userManager.UpdateAsync(user);
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            return user.Id;
        }

        [AbpAllowAnonymous]
        public async Task<bool> VerifyUserPhoneNumber(
            long userId,
            string verificationCode)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            if (user != null && user.PhoneNumberCode == verificationCode)
            {
                user.IsPhoneNumberConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }

        private string GenerateAndSendVerificationCode(string phoneNumber)
        {
            //TODO : SMS verfication implementation here.
            var code = AppConsts.DefaultPhoneNumberCode;
            return code;
        }

        [AbpAllowAnonymous]
        public List<GsmCsvRow> ImportMobilePhones()
        {
            var folderPath = "PhoneLelo.Project.Users";
            var fileContent = GetFileContent($"{folderPath}.gsm.csv");

            using (var reader = new StringReader(fileContent))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;

                var records = csv.GetRecords<GsmCsvRow>().ToList();
                if (!records.Any())
                {
                    return records;
                }

                return records;
            }
        }

        private string GetFileContent(string fileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(fileName))
                {
                    using (StreamReader reader = new StreamReader(stream))

                        return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
                throw;
            }
        }
    }
    
    public class GsmCsvRow
    {
        [Name("oem")]
        public string Brand { get; set; }

        [Name("model")]
        public string Model { get; set; }

        [Name("network_technology")]
        public string NetworkTechnology { get; set; }

        [Name("display_size")]
        public string DisplaySize { get; set; }

        [Name("display")]
        public string Display { get; set; }

        [Name("features")]
        public string Features { get; set; }

        [Name("memory_internal")]
        public string MemoryInternal { get; set; }

        [Name("main_camera_single")]
        public string MainCameraSingle { get; set; }
        
        
        [Name("body")]
        public string Body { get; set; }
        
        [Name("platform_os")]
        public string PlatformOS { get; set; }
        
        [Name("selfie_camera_features")]
        public string SelfieCameraFeature { get; set; }
        
        [Name("sound")]
        public string Sound { get; set; }
        
        [Name("battery")]
        public string Battery { get; set; }
        
        [Name("battery_talk_time")]
        public string BatteryTalkTime { get; set; }
        
        [Name("launch_announced")]
        public string LaunchAnnouncedYear { get; set; }
        
        [Name("display_resolution")]
        public string DisplayResolution { get; set; }
        
        [Name("features_sensors")]
        public string FeaturesSensors { get; set; }
    }
}

