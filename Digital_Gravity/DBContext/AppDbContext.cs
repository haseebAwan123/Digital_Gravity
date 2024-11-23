using Digital_Gravity.Models;
using Microsoft.EntityFrameworkCore;


namespace Digital_Gravity.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscriptions> UserSubscriptions { get; set; }

    }
}
