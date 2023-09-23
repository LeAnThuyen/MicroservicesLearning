using Contracts;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditBase<long>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
