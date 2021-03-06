using Microsoft.EntityFrameworkCore;
using Authn.Models;

namespace Authn.Data
{
    public class AuthDbContext:DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBulider)
        {
            modelBulider.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Provider).HasMaxLength(250);
                entity.Property(e => e.NameIdentifier).HasMaxLength(250);
                entity.Property(e => e.UserName).HasMaxLength(250);
                entity.Property(e => e.Password).HasMaxLength(250);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.FirstName).HasMaxLength(250);
                entity.Property(e => e.LastName).HasMaxLength(250);
                entity.Property(e => e.Mobile).HasMaxLength(250);
                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.City).HasMaxLength(250);
                entity.Property(e => e.Country).HasMaxLength(250);
                entity.Property(e => e.Roles).HasMaxLength(1000);

                entity.HasData(new AppUser
                {
                    Provider = "Cookies",
                    UserId = 1,
                    NameIdentifier = "bob",
                    Email= "bob@donet.pl",
                    UserName = "bob",
                    Password = "pizza",
                    FirstName = "Bob",
                    LastName = "Tester",
                    Mobile = "333 333 333",
                    Address = "Rzebika 1/3",
                    City = "Krakow",
                    Country = "Poland",
                    Roles ="Admin"

                });
            }
            );
        }


        public DbSet<Authn.Models.Repair> Repair { get; set; }


        public DbSet<Authn.Models.Delivery> Delivery { get; set; }


        public DbSet<Authn.Models.PartsTypes> PartsTypes { get; set; }


        public DbSet<Authn.Models.Part> Part { get; set; }
    }
}
