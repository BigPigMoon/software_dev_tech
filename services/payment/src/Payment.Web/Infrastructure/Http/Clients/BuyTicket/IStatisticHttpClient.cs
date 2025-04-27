namespace Payment.Web.Infrastructure.Http.Clients.BuyTicket;

public interface IStatisticHttpClient
{
    public Task SendBuyTicketEventAsync(Guid terminalId, CancellationToken cancellationToken);
}