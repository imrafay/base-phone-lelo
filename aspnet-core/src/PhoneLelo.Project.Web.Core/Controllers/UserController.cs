﻿using System;
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
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Store.Dto;

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
        private readonly IProductStoreAppService _productStoreAppService;

        public UserController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IUserAppService userAppService,
            IUserLocationAppService userLocationAppService,
            IProductStoreAppService productStoreAppService)
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
            _productStoreAppService = productStoreAppService;
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
        public async Task CompleteUserProfile(
            UserDto input,
            StoreInputDto storeInput=null)
        {

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                await _userAppService.UpdateUserProfile(input);

                if (storeInput != null)
                {
                    storeInput.UserId = input.Id;
                    await _productStoreAppService.CreateUserStore(storeInput);
                }
            }
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
