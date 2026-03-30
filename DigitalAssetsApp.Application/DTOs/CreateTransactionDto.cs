using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetsApp.Application.DTOS
{
    public class CreateTransactionDto
    {
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
