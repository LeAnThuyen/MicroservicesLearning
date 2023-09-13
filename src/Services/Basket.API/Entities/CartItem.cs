using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class CartItem
    {
        [Range(1, double.PositiveInfinity, ErrorMessage = "The filed {0} must be >=1")]
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
    }
}
