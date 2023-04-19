using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace MultiTenantAPI.Persistance.Tenant.Entities
{
    public class StoreInfo : ITenantInfo
    {
        public string? Id { get; set; }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }
    }
}