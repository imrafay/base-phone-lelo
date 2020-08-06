using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;
using PhoneLelo.Project.Location;

namespace PhoneLelo.Project.EntityFrameworkCore
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectDbContext>
    {
        #region Product
        public virtual DbSet<ProductCompany> ProductCompanies { get; set; }

        public virtual DbSet<ProductModel> ProductModels { get; set; }
        #endregion

        #region Location
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; } 
        #endregion

        /* Define a DbSet for each entity of the application */

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }
    }
}
