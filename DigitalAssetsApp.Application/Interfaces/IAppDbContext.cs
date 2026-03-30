using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace DigitalAssetsApp.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Transaction> Transactions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
