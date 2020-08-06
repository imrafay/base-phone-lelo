using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PhoneLelo.Project.EntityFrameworkCore;
using PhoneLelo.Project.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PhoneLelo.Project.Web.Tests
{
    [DependsOn(
        typeof(ProjectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ProjectWebTestModule : AbpModule
    {
        public ProjectWebTestModule(ProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ProjectWebMvcModule).Assembly);
        }
    }
}