using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using GmailAPIDemo.EntityFramework;

namespace GmailAPIDemo.Migrator
{
    [DependsOn(typeof(GmailAPIDemoDataModule))]
    public class GmailAPIDemoMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<GmailAPIDemoDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}