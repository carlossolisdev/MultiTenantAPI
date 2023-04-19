using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantAPI.Persistance.TenantManager;

namespace MultiTenantAPI.Features.Tenants
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly TenantManagerDbContext _tenantManagerDbContext;

        public TenantsController(TenantManagerDbContext tenantManagerDbContext)
        {
            _tenantManagerDbContext = tenantManagerDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tenants = await _tenantManagerDbContext.TenantInfo.ToListAsync();
            return Ok(new { Tenants = tenants });
        }
    }
}