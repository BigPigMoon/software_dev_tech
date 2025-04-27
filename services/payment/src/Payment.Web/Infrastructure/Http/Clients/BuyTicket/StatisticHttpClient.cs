namespace Payment.Web.Infrastructure.Http.Clients.BuyTicket;

internal class StatisticHttpClient : BaseHttpClient, IStatisticHttpClient
{
    public StatisticHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task SendBuyTicketEventAsync(Guid terminalId, CancellationToken cancellationToken)
    {
        await PostAsync<BuyTicketEventRequest, BuyTicketEventResponse>("/buy/ticket", new BuyTicketEventRequest(terminalId));
    }
}