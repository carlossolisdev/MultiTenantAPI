using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantAPI.Persistance.Store;
using MultiTenantAPI.Persistance.Tenant;

namespace MultiTenantAPI.Features.Products;

[ApiController]
[Route("api/stores/{__tenant__}/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly StoreDbContext _storeDbContext;

    public ProductsController(StoreDbContext storeDbContext)
    {
        _storeDbContext = storeDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _storeDbContext.Products.ToListAsync();
        return Ok(new { Products = products });
    }
}