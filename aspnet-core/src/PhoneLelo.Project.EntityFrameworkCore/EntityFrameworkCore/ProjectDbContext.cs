using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;

namespace PhoneLelo.Project.EntityFrameworkCore
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectDbContext>
    {
        #region Product
        public virtual DbSet<ProductCompany> ProductCompanies { get; set; }
        #endregion

        /* Define a DbSet for each entity of the application */

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }
    }
}
