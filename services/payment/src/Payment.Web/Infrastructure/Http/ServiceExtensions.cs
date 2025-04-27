using Payment.Web.Application.GetCards;
using Payment.Web.Application.Transactions;
using Payment.Web.Infrastructure.Database;
using Payment.Web.Infrastructure.Database.Repositories.GetCards;
using Payment.Web.Infrastructure.Database.Repositories.Transactions;
using Payment.Web.Infrastructure.Http.Clients.BuyTicket;
using Payment.Web.Infrastructure.Http.Clients.GetTerminals;

namespace Payment.Web.Infrastructure.Http;

public static class ServiceExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IStatisticHttpClient, StatisticHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        });
        
        services.AddHttpClient<IStaffHttpClient, FakeStaffHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        });
        
        return services;
    }
}