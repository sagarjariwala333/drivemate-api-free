using DriveMate.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Context
{
    public class AppDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbSet<Address> Addresses { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        
        public AppDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
    }
}
