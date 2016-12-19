using System.Linq;
using GmailAPIDemo.EntityFramework;
using GmailAPIDemo.MultiTenancy;

namespace GmailAPIDemo.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly GmailAPIDemoDbContext _context;

        public DefaultTenantCreator(GmailAPIDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
