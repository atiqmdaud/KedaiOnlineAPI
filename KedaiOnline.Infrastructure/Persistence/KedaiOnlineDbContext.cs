using KedaiOnline.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KedaiOnline.Infrastructure.Persistence;

internal class KedaiOnlineDbContext(DbContextOptions<KedaiOnlineDbContext> options) :
    IdentityDbContext<User>(options)
{
    internal DbSet<Kedai> KedaiOnline { get; set; }
    internal DbSet<Item> Items { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  {
    //      //optionsBuilder.UseSqlServer("Server=localhost;Database=KedaiOnline;User Id=sa;Password=yourStrong(!)Password;");
    //      //optionsBuilder.UseSqlServer("Data Source=DESKTOP-35KEQ7S\\SQLEXPRESS;Database=KedaiOnlineDb;Trusted_Connection=True;Encrypt=False;Trust Server Certificate=False;");
    //      optionsBuilder.UseSqlServer("");
    //  }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Kedai>().ToTable("KedaiOnline");
    //    modelBuilder.Entity<Item>().ToTable("Items");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Kedai>().OwnsOne(r => r.Address);

        modelBuilder.Entity<Kedai>()
            .HasMany(r => r.Items)
            .WithOne()
            .HasForeignKey(d=>d.KedaiId);


    }
}
