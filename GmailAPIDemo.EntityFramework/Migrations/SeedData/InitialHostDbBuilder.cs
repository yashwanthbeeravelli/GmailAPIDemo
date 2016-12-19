using GmailAPIDemo.EntityFramework;
using EntityFramework.DynamicFilters;

namespace GmailAPIDemo.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly GmailAPIDemoDbContext _context;

        public InitialHostDbBuilder(GmailAPIDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
