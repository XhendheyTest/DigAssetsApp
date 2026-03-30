using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.Interfaces;

namespace DigitalAssetsApp.Infrastructure.Services
{
    public class BlockchainService : IBlockchainService
    {
        public string GenerateTransactionHash()
        {
            return "0x" + Guid.NewGuid().ToString("N");
        }

        public string GetTransactionStatus()
        {
            return "confirmed";
        }
    }
}
