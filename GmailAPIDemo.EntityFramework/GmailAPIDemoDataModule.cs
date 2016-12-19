using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using GmailAPIDemo.EntityFramework;

namespace GmailAPIDemo
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(GmailAPIDemoCoreModule))]
    public class GmailAPIDemoDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<GmailAPIDemoDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
