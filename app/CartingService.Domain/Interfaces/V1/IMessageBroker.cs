namespace CartingService.Domain.Interfaces.V1;

public interface IMessageBroker
{
    Task ReceiveAsync(CancellationToken cancellationToken = default);
}