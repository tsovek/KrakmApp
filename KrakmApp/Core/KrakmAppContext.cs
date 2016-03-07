using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

using KrakmApp.Entities;

namespace KrakmApp.Core
{
    public class KrakmAppContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Monument> Monuments { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Entertainment> Entertainments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public KrakmAppContext(DbContextOptions options) 
            : base (options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Property(e => e.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Client>().HasOne(e => e.Hotel).WithMany(e => e.Clients);
            modelBuilder.Entity<Client>().HasOne(e => e.Localization).WithOne();

            modelBuilder.Entity<Hotel>().Property(e => e.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Hotel>().Property(e => e.Adress).HasMaxLength(100);
            modelBuilder.Entity<Hotel>().Property(e => e.Phone).HasMaxLength(14);
            modelBuilder.Entity<Hotel>().Property(e => e.Email).HasMaxLength(100);
            modelBuilder.Entity<Hotel>().HasMany(e => e.Partners).WithOne(e => e.Hotel);
            modelBuilder.Entity<Hotel>().HasMany(e => e.Localizations).WithOne();
            modelBuilder.Entity<Hotel>().HasMany(e => e.Partners).WithOne(e => e.Hotel);

            modelBuilder.Entity<Partner>().Property(e => e.Adress).HasMaxLength(100);
            modelBuilder.Entity<Partner>().Property(e => e.Phone).HasMaxLength(100);
            modelBuilder.Entity<Partner>().Property(e => e.Name).HasMaxLength(200);
            modelBuilder.Entity<Partner>().HasOne(e => e.Localization).WithOne();
            modelBuilder.Entity<Partner>().HasOne(e => e.Marker).WithOne();

            modelBuilder.Entity<Entertainment>().Property(e => e.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Entertainment>().HasOne(e => e.Localization).WithOne();

            modelBuilder.Entity<Marker>().Property(e => e.Name).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Salt).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<UserRole>().Property(ur => ur.UserId).IsRequired();
            modelBuilder.Entity<UserRole>().Property(ur => ur.RoleId).IsRequired();

            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }
}
