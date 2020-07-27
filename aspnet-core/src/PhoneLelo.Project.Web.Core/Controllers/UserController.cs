using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using PhoneLelo.Project.Authentication.External;
using PhoneLelo.Project.Authentication.JwtBearer;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Models.TokenAuth;
using PhoneLelo.Project.MultiTenancy;
using PhoneLelo.Project.Users;
using PhoneLelo.Project.Users.Dto;
using Abp.Domain.Uow;

namespace PhoneLelo.Project.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ProjectControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IUserAppService _userAppService;

        public UserController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager, IUserAppService userAppService)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _userAppService = userAppService;
        }

        [HttpPost]
        public async Task<long> SignUpUser(
            string phoneNumber,
            string roleName)
        {
           var userId = await _userAppService.SignUpUserByPhoneNumberAsync(
                phoneNumber: phoneNumber,
                roleName: roleName
                );
            return userId;
        }


        [HttpPost]
        public async Task<bool> VerifyUserPhoneNumber(
            long userId,
            string code)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                return await _userAppService.VerifyUserPhoneNumber(
                 userId: userId,
                verificationCode: code
                 );
            }
        }


        [HttpPost]
        public async Task CompleteUserProfile(
            UserDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _userAppService.UpdateUserProfile(input);
            }
        }
        
        
        [HttpPost]
        public List<GsmCsvRow> ImportMobilePhones()
        {
              var result = _userAppService.ImportMobilePhones();
               return result;
        }
    }
}
