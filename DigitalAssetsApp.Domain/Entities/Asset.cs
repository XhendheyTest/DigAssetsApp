using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetsApp.Domain.Entities
{
    public class Asset
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
