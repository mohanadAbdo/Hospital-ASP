using Microsoft.EntityFrameworkCore;

namespace Mohanad_Hospital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }
    }
}
