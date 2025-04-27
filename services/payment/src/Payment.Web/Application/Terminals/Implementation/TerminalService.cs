using Payment.Web.Infrastructure.Http.Clients.GetTerminals;

namespace Payment.Web.Application.Terminals.Implementation;

internal class TerminalService : ITerminalService
{
    private readonly IStaffHttpClient _staffHttpClient;

    public TerminalService(IStaffHttpClient staffHttpClient)
    {
        _staffHttpClient = staffHttpClient;
    }

    public async Task<IEnumerable<Guid>> GetTerminalIdsAsync(CancellationToken cancellationToken)
    {
        return await _staffHttpClient.GetTerminalIdsAsync(cancellationToken);
    }
}