using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace KedaiOnline.Infrastructure.Seeders;

internal class KedaiSeeder(KedaiOnlineDbContext dbContext) : IKedaiSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.KedaiOnline.Any())
            {
                var kedaiOnline = GetKedaiOnline();
                dbContext.KedaiOnline.AddRange(kedaiOnline);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }

        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new(UserRoles.Admin),
            new(UserRoles.Owner),
            new(UserRoles.User)
  
        ];

        return roles;
    }

    private IEnumerable<Kedai> GetKedaiOnline()
    {
        List<Kedai> KedaiOnline = [
            new()
            {
                Nama = "Kedai 1",
                Description = "Kedai makan yang menyediakan pelbagai jenis makanan tempatan yang sedap.",
                Category = "Makanan",
                HasDelivery = "Ya",
                ContactEmail = "kedai1@test.com",
                Items = [
                    new()
                    {
                        Name = "Nasi Goreng Kampung",
                        Description = "Nasi goreng dengan sambal belacan.",
                        Price = 10.00m
                    },
                    new()
                    {
                        Name = "Mee Goreng Mamak",
                        Description = "Mee goreng dengan telur dan sayur.",
                        Price = 8.00m
                    }
                ],
                Address = new()
                {
                    Street = "Street 1",
                    City = "Kuala Lumpur",
                    PostalCode = "50000"
                }
            },
            new()
            {
                Nama = "Kedai 2",
                Description = "Kedai runcit yang besar",
                Category = "Runcit",
                HasDelivery = "No",
                ContactEmail = "kedai2@test.com",
                Items = [
                    new()
                    {
                        Name = "Sabun",
                        Description = "Brand Abc",
                        Price = 2.00m
                    },
                    new()
                    {
                        Name = "Maggi",
                        Description = "Satu kotak",
                        Price = 8.00m
                    }
                ],
                 Address = new Address
                {
                    City = "Bachok",
                    Street = "Street 2",
                    PostalCode = "16300"
                }
            }
            ];

        return KedaiOnline;
    }
}
