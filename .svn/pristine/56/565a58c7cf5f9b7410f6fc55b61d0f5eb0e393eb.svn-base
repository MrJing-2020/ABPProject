using ABPProject.EntityFramework;
using EntityFramework.DynamicFilters;

namespace ABPProject.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly ABPProjectDbContext _context;

        public InitialHostDbBuilder(ABPProjectDbContext context)
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
