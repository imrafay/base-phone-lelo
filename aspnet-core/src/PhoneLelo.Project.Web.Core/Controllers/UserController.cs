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
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Import.MobilePhone;

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
        private readonly IUserLocationAppService _userLocationAppService;

        public UserController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager, IUserAppService userAppService, IUserLocationAppService userLocationAppService)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _userAppService = userAppService;
            _userLocationAppService = userLocationAppService;
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

        [HttpGet]
        public async Task<List<DropdownOutputDto>> GetStates()
        {
            return await _userLocationAppService
                .GetStates();          
        }

        [HttpGet]
        public async Task<List<DropdownOutputDto>> GetCitiesByStateId(
            long stateId)
        {
            return await _userLocationAppService
                .GetCitiesByStateId(stateId);
        }

        [HttpGet]
        public async Task<List<DropdownOutputDto>> GetNeighbourhoodsByCityId(
            long cityId)
        {
            return await _userLocationAppService
                .GetNeighbourhoodsByCityId(cityId);
        }

        [HttpPost]
        public async Task UpdateUserLocation(
            UserLocationInputDto input)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _userLocationAppService.UpdateUserLocation(input);
            }
        }
    }
}
