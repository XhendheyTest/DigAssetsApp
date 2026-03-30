using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetsApp.Application.Interfaces
{
    public interface IBlockchainService
    {
        string GenerateTransactionHash();
        string GetTransactionStatus();
    }
}
