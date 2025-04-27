namespace Payment.Web.Infrastructure.Http.Clients.GetTerminals;

public interface IStaffHttpClient
{
    Task<IEnumerable<Guid>> GetTerminalIdsAsync(CancellationToken cancellationToken);
}