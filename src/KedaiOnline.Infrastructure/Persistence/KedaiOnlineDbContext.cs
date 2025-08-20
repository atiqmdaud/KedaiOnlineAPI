using KedaiOnline.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KedaiOnline.Infrastructure.Persistence;

internal class KedaiOnlineDbContext(DbContextOptions<KedaiOnlineDbContext> options) :
    IdentityDbContext<User>(options)
{
    internal DbSet<Kedai> KedaiOnline { get; set; }
    internal DbSet<Item> Items { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Kedai>().OwnsOne(r => r.Address);

        modelBuilder.Entity<Kedai>()
            .HasMany(r => r.Items)
            .WithOne()
            .HasForeignKey(d=>d.KedaiId);

        modelBuilder.Entity<User>()
            .HasMany(o => o.OwnedKedaiOnline)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);


    }
}
