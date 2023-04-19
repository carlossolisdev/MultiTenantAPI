using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantAPI.Persistance.Store.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }
    }
}