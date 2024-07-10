namespace EventBus.Messages;

public interface IIntegrationEvent
{
     DateTime CreationDate { get; } 
     Guid Id { get; set; }
}