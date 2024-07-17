using Contracts;
using Contracts.Common.Events;
using Ordering.Domain.Enums;
using Ordering.Domain.OrderAggregate.Event;

namespace Ordering.Domain.Entities
{
    public class Order : AuditableEventEntity<long>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string DocumentNo { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public EOrderStatus Status { get; set; }



        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(UserName,FirstName,LastName,EmailAddress,ShippingAddress,InvoiceAddress,Id,TotalPrice,Status,DocumentNo));
            return this;
        }
        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }
    }
}
