namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public Cart()
        {

        }
        public Cart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice => Items.Sum(c => c.ItemPrice * c.Quantity);

    }
}
