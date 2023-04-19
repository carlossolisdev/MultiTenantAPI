using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using MultiTenantAPI.Persistance.Store.Entities;

namespace MultiTenantAPI.Persistance.Store
{
    public class StoreDbContext : DbContext
    {
        private readonly ITenantInfo? _store;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public StoreDbContext(
            DbContextOptions<StoreDbContext> options,
            IWebHostEnvironment env,
            IMultiTenantContextAccessor multiTenantContextAccessor,
            IConfiguration config
        ) : base(options)
        {
            _store = multiTenantContextAccessor.MultiTenantContext.TenantInfo;
            _env = env;
            _config = config;
        }

        // public StoreDbContext(DbContextOptions<StoreDbContext> options, IConfiguration config) : base(options)
        // {
        //     _config = config;
        // }

        public DbSet<Product> Products => Set<Product>();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("Store");

            if(_store == null && _env.IsDevelopment())
            {
                connectionString = _config.GetConnectionString("Store");
            }
            else
            {
                connectionString = _store.ConnectionString;
            }

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}