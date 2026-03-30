using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Domain.Entities;

namespace DigitalAssetsApp.Infrastructure.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (context.Assets.Any())
                return;

            var assets = new List<Asset>
        {
            new Asset
            {
                Id = Guid.NewGuid(),
                Name = "Bitcoin",
                Symbol = "BTC",
                Price = 60000
            },
            new Asset
            {
                Id = Guid.NewGuid(),
                Name = "Ethereum",
                Symbol = "ETH",
                Price = 3000
            },
            new Asset
            {
                Id = Guid.NewGuid(),
                Name = "Solana",
                Symbol = "SOL",
                Price = 150
            }
        };

            context.Assets.AddRange(assets);
            await context.SaveChangesAsync();
        }
}
}
