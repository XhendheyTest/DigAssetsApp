using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.Interfaces;
using DigitalAssetsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalAssetsApp.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }


}
