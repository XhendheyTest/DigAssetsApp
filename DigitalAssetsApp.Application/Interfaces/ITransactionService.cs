using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Domain.Entities;

namespace DigitalAssetsApp.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(string from, string to, decimal amount);
        Task<List<Transaction>> GetAllAsync();
    }
}
