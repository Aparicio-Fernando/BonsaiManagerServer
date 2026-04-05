using BonsaiManager.Data.Context;
using BonsaiManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedSpeciesAsync(context);
            await SeedAdminAsync(context);
        }

        private static async Task SeedSpeciesAsync(AppDbContext context)
        {
            if (await context.Species.AnyAsync())
                return;

            var species = new List<Species>
        {
            new() { Id = Guid.NewGuid(), Name = "Ficus Retusa", Description = "Especie tropical muy resistente, ideal para principiantes.", CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Juniperus", Description = "Conífera clásica para bonsai, muy popular en estilo formal.", CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Acer Palmatum", Description = "Arce japonés, conocido por su follaje colorido en otoño.", CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Carmona Retusa", Description = "Árbol de té, produce pequeñas flores blancas.", CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Pinus", Description = "Pino, símbolo clásico del bonsai japonés.", CreatedAt = DateTime.UtcNow },
        };

            await context.Species.AddRangeAsync(species);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAdminAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync(u => u.Role == "Admin"))
                return;

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                Email = "admin@bonsaimanager.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin1234!"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}
