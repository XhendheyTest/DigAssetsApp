using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.Interfaces;

namespace DigitalAssetsApp.Infrastructure.Services
{
    public class WalletService: IWalletService
    {
        private static Dictionary<string, decimal> _balances = new()
    {
        { "wallet1", 1000 },
        { "wallet2", 500 }
    };

        public Task<decimal> GetBalanceAsync(string address)
        {
            _balances.TryGetValue(address, out var balance);
            return Task.FromResult(balance);
        }

        public Task<bool> HasSufficientBalance(string address, decimal amount)
        {
            _balances.TryGetValue(address, out var balance);
            return Task.FromResult(balance >= amount);
        }

        public Task DeductBalance(string address, decimal amount)
        {
            if (_balances.ContainsKey(address))
                _balances[address] -= amount;

            return Task.CompletedTask;
        }
    }
}
