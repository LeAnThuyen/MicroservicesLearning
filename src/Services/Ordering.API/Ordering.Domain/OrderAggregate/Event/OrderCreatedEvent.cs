using Contracts.Common.Events;
using Ordering.Domain.Enums;

namespace Ordering.Domain.OrderAggregate.Event;

public class OrderCreatedEvent:BaseEvent
{
    public long Id { get; private set; }
    public string UserName { get;private set; }
    public string FirstName { get; private set; }
    public string LastName { get;private set; }
    public string EmailAddress { get;private set; }
    public string ShippingAddress { get;private set; }
    public string InvoiceAddress { get;private set; }
    public decimal TotalPrice { get;private set; }
    public EOrderStatus Status { get;private set; }
    
    public string DocumnetNo { get;private set; }
    public OrderCreatedEvent(string userName, string firstName, string lastName, string emailAddress, string shippingAddress, string invoiceAddress, long id, decimal totalPrice, EOrderStatus status, string documnetNo)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        ShippingAddress = shippingAddress;
        InvoiceAddress = invoiceAddress;
        Id = id;
        TotalPrice = totalPrice;
        Status = status;
        DocumnetNo = documnetNo;
    }
    
    
}