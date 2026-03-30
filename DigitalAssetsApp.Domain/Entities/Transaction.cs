using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetsApp.Domain.Entities
{
    public class Transaction
    {

        public Guid Id { get; set; }
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TxHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
