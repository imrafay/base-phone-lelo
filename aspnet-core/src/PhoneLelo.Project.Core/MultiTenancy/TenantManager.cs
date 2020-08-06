using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Editions;

namespace PhoneLelo.Project.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
