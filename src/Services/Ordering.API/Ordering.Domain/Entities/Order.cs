using Contracts;
using Ordering.Domain.Enums;

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
        public EOrderStatus Status { get; set; }
    }
}
