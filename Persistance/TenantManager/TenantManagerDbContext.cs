using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantAPI.Persistance.TenantManager
{
    public class TenantManagerDbContext : EFCoreStoreDbContext<TenantInfo>
    {
        public TenantManagerDbContext(DbContextOptions<TenantManagerDbContext> options) : base(options)
        {
        }
    }
}