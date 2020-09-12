using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Location;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneLelo.Project.Authorization
{
    public class ProductStoreManager : DomainService
    {
        private readonly IRepository<City, long> _cityRepository;
        private readonly IRepository<State, long> _stateRepository;
        private readonly IRepository<Neighbourhood, long> _neighbourhoodRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<ProductStore, long> _productStoreRepository;
        private readonly IRepository<UserProductStore, long> _userProductStoreRepository;

        public ProductStoreManager(
            IRepository<City, long> cityRepository,
            IRepository<State, long> stateRepository,
            IRepository<Neighbourhood, long> neighbourhoodRepository,
            IRepository<User, long> userRepository,
            IRepository<ProductStore, long> productStoreRepository,
            IRepository<UserProductStore, long> userProductStoreRepository)


        {
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _neighbourhoodRepository = neighbourhoodRepository;
            _userRepository = userRepository;
            _productStoreRepository = productStoreRepository;
            _userProductStoreRepository = userProductStoreRepository;
        }

        public async Task Create(ProductStore store)
        {
            await _productStoreRepository.InsertAsync(store);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task Update(ProductStore store)
        {
            await _productStoreRepository.UpdateAsync(store);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<ProductStore> GetStoreByIdAsync(long storeId)
        {
            var store = await _productStoreRepository.GetAll()
                .Include(x => x.UserProductStores)
                .Where(x => 
                    x.Id == storeId &&
                    x.IsActive == true)
                .FirstOrDefaultAsync();

            return store;
        }
        
        public async Task<ProductStore> GetStoreByUserIdAsync(long userId)
        {
            var store = await _userProductStoreRepository.GetAll()
                .Include(x => x.ProductStoreFk)
                .Where(x => 
                    x.UserId == userId &&
                    x.ProductStoreFk.IsActive == true)
                .Select(x=>x.ProductStoreFk)
                .FirstOrDefaultAsync();

            return store;
        }

        public async Task ToggleActivationStore(
            long storeId,
            bool activeStatus)
        {
            var store =  await GetStoreByIdAsync(storeId);
            if (store != null)
            {
                store.IsActive = activeStatus;
                await _productStoreRepository.UpdateAsync(store);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
           
        }

        public async Task CreateUserStore(UserProductStore userStore)
        {

            await _userProductStoreRepository.InsertAsync(userStore);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        
        public async Task UpdateUserStore(UserProductStore userStore)
        {
            await _userProductStoreRepository.UpdateAsync(userStore);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
