using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.FileManagement;
using PhoneLelo.Project.Product;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product.Enum;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Storage.FileManagement;
using PhoneLelo.Project.Store;
using PhoneLelo.Project.Store.Dto;
using PhoneLelo.Project.Utils;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ProductStoreAppService : ApplicationService, IProductStoreAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly ProductAdvertManager _productAdvertManager;
        private readonly ProductAdvertViewLogManager _productAdvertViewLogManager;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly ProductStoreManager _productStoreManager;

        public ProductStoreAppService(

            IAbpSession abpSession,
            ProductAdvertManager productAdvertManager,
            ProductAdvertViewLogManager productAdvertViewLogManager,
            IFileStorageManager fileStorageManager,
            ProductStoreManager productStoreManager)
        {
            _abpSession = abpSession;
            _productAdvertManager = productAdvertManager;
            _productAdvertViewLogManager = productAdvertViewLogManager;
            _fileStorageManager = fileStorageManager;
            _productStoreManager = productStoreManager;
        }

        [AbpAllowAnonymous]
        public async Task CreateUserStore(StoreInputDto storeInput)
        {
            if (storeInput.Name.IsNullOrEmpty() ||
                storeInput.Address.IsNullOrEmpty())
            {
                throw new UserFriendlyException(AppConsts.ErrorMessage.MissingFieldMessage);
            }

            var storeCode = Guid.NewGuid();
            var store = new ProductStore()
            {
                Name = storeInput.Name,
                DisplayName = storeInput.Name.ToUpper(),
                StoreCode = Guid.NewGuid(),
                Address = storeInput.Address,
                ImageIconUrl = storeInput.ImageIconUrl,
                IsActive=true
            };
            await _productStoreManager.Create(store);


            var userStore = new UserProductStore()
            {
                UserId = storeInput.UserId,
                ProductStoreId = store.Id
            };
            await _productStoreManager.CreateUserStore(userStore);
        }

        public async Task ToggleStorActivation(
            long storeId,
            bool activeStatus)
        {
            await _productStoreManager.ToggleActivationStore(
                storeId: storeId,
                activeStatus: activeStatus);
        }
    }
}

