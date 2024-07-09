namespace Contracts.Messages;

public interface IMessageProducer
{
    void SendMessages<T>(T message);
}