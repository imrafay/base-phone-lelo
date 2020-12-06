using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;
using PhoneLelo.Project.Location;
using PhoneLelo.Project.Storage;
using PhoneLelo.Project.Chat;

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

        #region Product Advert
        public virtual DbSet<ProductAdvert> ProductAdverts { get; set; }

        public virtual DbSet<ProductAdvertBatteryUsage> ProductAdvertBatteryUsages { get; set; }

        public virtual DbSet<ProductAdvertImage> ProductAdvertImages { get; set; }

        public virtual DbSet<ProductAdvertAccessory> ProductAdvertAccessories { get; set; }

        public virtual DbSet<ProductAdvertViewLog> ProductAdvertViewLogs { get; set; }
        #endregion

        #region Feeb Back
        public virtual DbSet<UserProfileReview> UserProfileReviews { get; set; } 
        public virtual DbSet<UserProfileReviewLike> UserProfileReviewLikes { get; set; }
        #endregion

        #region Chate
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        #endregion

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }


        /* Define a DbSet for each entity of the application */

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });
        }
    }
}
