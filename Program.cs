using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using tenant = MultiTenantAPI.Persistance.Tenant;
using MultiTenantAPI.Persistance.TenantManager;
using MultiTenantAPI.Persistance.Store;

var builder = WebApplication.CreateBuilder(args);

// DbContext register
builder.Services.AddDbContext<TenantManagerDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TenantManager"));
});
builder.Services.AddDbContext<tenant.TenantDbContext>();
builder.Services.AddDbContext<StoreDbContext>();

// Multitenancy support
builder.Services
    .AddMultiTenant<TenantInfo>()
    // .WithHeaderStrategy("X-Tenant")
    .WithHostStrategy()
    .WithEFCoreStore<TenantManagerDbContext, TenantInfo>();

builder.Services
    .AddMultiTenant<tenant.Entities.StoreInfo>()
    .WithRouteStrategy()
    .WithEFCoreStore<tenant.TenantDbContext, tenant.Entities.StoreInfo>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMultiTenant();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.UseEndpoints(endpoint => {
//     endpoint.MapControllerRoute("default", "api/stores/{__tenant__}/{controller}/{action}");
// });

// await SeedTenantData();
app.Run();

async Task SeedTenantData()
{
    using var scope = app.Services.CreateScope();
    var store = scope.ServiceProvider.GetRequiredService<IMultiTenantStore<TenantInfo>>();
    var tenants = await store.GetAllAsync();

    if (tenants.Count() > 0)
    {
        return;
    }

    await store.TryAddAsync(new TenantInfo
    {
        Id = Guid.NewGuid().ToString(),
        Identifier = "tenant01",
        Name = "My Dev Tenant 01",
        ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Tenant_Tenant01;Trusted_Connection=True;MultipleActiveResultSets=true"
    });

    await store.TryAddAsync(new TenantInfo
    {
        Id = Guid.NewGuid().ToString(),
        Identifier = "tenant02",
        Name = "My Dev Tenant 2",
        ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Tenant_Tenant02;Trusted_Connection=True;MultipleActiveResultSets=true"
    });
}