using Microsoft.EntityFrameworkCore;

namespace PosSystem.Infrastracture.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}