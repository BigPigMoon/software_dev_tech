namespace Payment.Web.Infrastructure.Http.Clients.GetTerminals;

internal class FakeStaffHttpClient : BaseHttpClient, IStaffHttpClient
{
    public FakeStaffHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<IEnumerable<Guid>> GetTerminalIdsAsync(CancellationToken cancellationToken)
    {
        var list = new List<Guid>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(Guid.NewGuid());
        }

        return Task.FromResult<IEnumerable<Guid>>(list);
    }
}