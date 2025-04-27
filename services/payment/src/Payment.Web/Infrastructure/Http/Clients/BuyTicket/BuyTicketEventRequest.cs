namespace Payment.Web.Infrastructure.Http.Clients.BuyTicket;

internal record class BuyTicketEventRequest
{
    public BuyTicketEventRequest(Guid terminalId)
    {
        TerminalId = terminalId;
    }

    public Guid TerminalId { get; }
}