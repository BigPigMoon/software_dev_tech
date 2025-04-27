namespace Payment.Web.Infrastructure.Http.Clients.GetTerminals;

internal record class TerminalResponse
{
    public TerminalResponse(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}