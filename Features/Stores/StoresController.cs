using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantAPI.Persistance.Tenant;

namespace MultiTenantAPI.Features.Stores
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly TenantDbContext _tenantDbContext;

        public StoresController(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stores = await _tenantDbContext.StoresInfo.ToListAsync();
            return Ok(new { Stores = stores });
        }
    }
}