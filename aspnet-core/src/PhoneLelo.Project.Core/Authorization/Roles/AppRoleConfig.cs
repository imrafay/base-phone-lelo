using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace PhoneLelo.Project.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            // Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host
                )
            );

            // Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant
                )
            );
            
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.ShopOwner,
                    MultiTenancySides.Tenant
                )
            );
            
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Seller,
                    MultiTenancySides.Tenant
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.ShopEmployee,
                    MultiTenancySides.Tenant
                )
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Technician,
                    MultiTenancySides.Tenant
                )
            );
        }
    }
}
