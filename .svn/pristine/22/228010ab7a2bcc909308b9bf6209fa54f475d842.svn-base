using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using ABPProject.EntityFramework;

namespace ABPProject.Migrator
{
    [DependsOn(typeof(ABPProjectDataModule))]
    public class ABPProjectMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ABPProjectDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}