using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using GmailAPIDemo.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace GmailAPIDemo.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<GmailAPIDemo.EntityFramework.GmailAPIDemoDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "GmailAPIDemo";
        }

        protected override void Seed(GmailAPIDemo.EntityFramework.GmailAPIDemoDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
