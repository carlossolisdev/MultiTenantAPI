using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using MultiTenantAPI.Persistance.Tenant.Entities;

namespace MultiTenantAPI.Persistance.Tenant
{
    public class TenantDbContext : EFCoreStoreDbContext<StoreInfo>
    {
        private readonly ITenantInfo? _tenant;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public TenantDbContext(
            DbContextOptions<TenantDbContext> options,
            IWebHostEnvironment env,
            IMultiTenantContextAccessor multiTenantContextAccessor,
            IConfiguration config
        ) : base(options)
        {
            if(multiTenantContextAccessor.MultiTenantContext is not null)
                _tenant = multiTenantContextAccessor.MultiTenantContext.TenantInfo;
            _env = env;
            _config = config;
        }

        // public TenantDbContext(DbContextOptions<TenantDbContext> options, IConfiguration config) : base(options)
        // {
        //     _config = config;
        // }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<StoreInfo> StoresInfo => Set<StoreInfo>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("Tenant");

            if (_tenant is null && _env.IsDevelopment())
            {
                // Init/Dev connection string
                connectionString = _config.GetConnectionString("Tenant");
            }
            else
            {
                // Tenant connection string
                connectionString = _tenant.ConnectionString;
            }

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}