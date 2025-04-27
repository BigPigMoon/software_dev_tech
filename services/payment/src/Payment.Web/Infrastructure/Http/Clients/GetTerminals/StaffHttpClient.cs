namespace Payment.Web.Infrastructure.Http.Clients.GetTerminals;

internal class StaffHttpClient : BaseHttpClient, IStaffHttpClient
{
    public StaffHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<Guid>> GetTerminalIdsAsync(CancellationToken cancellationToken)
    {
        var terminals = await GetAsync<IEnumerable<TerminalResponse>>("/terminals");

        return terminals.Select(t => t.Id);
    }
}