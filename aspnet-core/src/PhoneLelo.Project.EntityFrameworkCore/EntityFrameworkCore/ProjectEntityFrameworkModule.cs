using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using PhoneLelo.Project.EntityFrameworkCore.Seed;

namespace PhoneLelo.Project.EntityFrameworkCore
{
    [DependsOn(
        typeof(ProjectCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class ProjectEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<ProjectDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        ProjectDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        ProjectDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            EnsureMigrated();

            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }

        private void EnsureMigrated()
        {
            using (var migrateExecuter = IocManager.ResolveAsDisposable<MultiTenantMigrateExecuter>())
            {
                migrateExecuter.Object.Run();
            }
        }
    }
}
