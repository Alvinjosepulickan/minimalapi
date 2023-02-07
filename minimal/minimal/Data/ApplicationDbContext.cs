using Microsoft.EntityFrameworkCore;
using minimal.Models;

namespace minimal.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}
