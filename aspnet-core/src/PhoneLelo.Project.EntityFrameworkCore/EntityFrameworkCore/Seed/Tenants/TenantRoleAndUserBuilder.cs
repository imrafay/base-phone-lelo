using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly ProjectDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(ProjectDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Admin role

            #region Tenant Admin
            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            } 
            #endregion

            #region Seller
            var sellerRole = _context.Roles
            .IgnoreQueryFilters()
            .FirstOrDefault(r =>
            r.TenantId == _tenantId &&
            r.Name == StaticRoleNames.Tenants.Seller);

            if (sellerRole == null)
            {
                sellerRole = _context.Roles.Add(new Role(
                    _tenantId,
                    StaticRoleNames.Tenants.Seller,
                    StaticRoleNames.Tenants.SellerDisplayName
                    )
                {
                    IsStatic = true
                }).Entity;
                _context.SaveChanges();
            }
            #endregion

            #region Shop Owner
            var shopOwnerRole = _context.Roles.
                   IgnoreQueryFilters()
                   .FirstOrDefault(r =>
                   r.TenantId == _tenantId &&
                   r.Name == StaticRoleNames.Tenants.ShopOwner);

            if (shopOwnerRole == null)
            {
                shopOwnerRole = _context.Roles
                    .Add(new Role(
                        _tenantId,
                        StaticRoleNames.Tenants.ShopOwner,
                        StaticRoleNames.Tenants.ShopOwnerDisplayName)
                    {
                        IsStatic = true
                    }).Entity;

                _context.SaveChanges();
            }
            #endregion

            #region Shop Employee
            var shopEmployeeRole = _context.Roles.
                   IgnoreQueryFilters()
                   .FirstOrDefault(r =>
                   r.TenantId == _tenantId &&
                   r.Name == StaticRoleNames.Tenants.ShopEmployee);

            if (shopEmployeeRole == null)
            {
                shopEmployeeRole = _context.Roles
                    .Add(new Role(
                        _tenantId,
                        StaticRoleNames.Tenants.ShopEmployee,
                        StaticRoleNames.Tenants.ShopEmployeeDisplayName)
                    {
                        IsStatic = true
                    }).Entity;

                _context.SaveChanges();
            }
            #endregion   
                    
            #region Technician
            var technicianRole = _context.Roles.
                   IgnoreQueryFilters()
                   .FirstOrDefault(r =>
                   r.TenantId == _tenantId &&
                   r.Name == StaticRoleNames.Tenants.Technician);

            if (technicianRole == null)
            {
                technicianRole = _context.Roles
                    .Add(new Role(
                        _tenantId,
                        StaticRoleNames.Tenants.Technician,
                        StaticRoleNames.Tenants.TechnicianDisplayName)
                    {
                        IsStatic = true
                    }).Entity;

                _context.SaveChanges();
            }
            #endregion

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new ProjectAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
