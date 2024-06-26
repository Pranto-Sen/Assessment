using Assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Data
{
    public class AppliationDbContext:DbContext
    {
        public AppliationDbContext(DbContextOptions<AppliationDbContext> options):base(options)
        {
            
        }
        public DbSet<sqlModel> sqlModels { get; set; }
    }
}
