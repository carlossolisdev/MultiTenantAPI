namespace MultiTenantAPI.Persistance.Tenant.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }
    }
}