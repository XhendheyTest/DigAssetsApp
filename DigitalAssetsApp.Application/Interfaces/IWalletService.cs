using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetsApp.Application.Interfaces
{
    public  interface IWalletService
    {
        Task<decimal> GetBalanceAsync(string address);
        Task<bool> HasSufficientBalance(string address, decimal amount);
        Task DeductBalance(string address, decimal amount);
    }
}
