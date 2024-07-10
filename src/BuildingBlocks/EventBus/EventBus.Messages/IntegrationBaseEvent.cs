namespace EventBus.Messages;

public record IntegrationBaseEvent():IIntegrationEvent
{
    public DateTime CreationDate { get; } = new DateTime();
    public Guid Id { get; set; }
};
