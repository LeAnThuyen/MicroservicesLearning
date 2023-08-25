using Contracts;

namespace Product.API.Entities
{
    public class CatalogProduct : EntityAuditBase<long>
    {

        public string No { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public int StockQuantity { get; set; }
    }
}
