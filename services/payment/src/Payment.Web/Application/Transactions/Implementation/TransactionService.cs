using Payment.Web.Application.Transactions.Exceptions;
using Payment.Web.Infrastructure.Http;
using Payment.Web.Infrastructure.Http.Clients.BuyTicket;

namespace Payment.Web.Application.Transactions.Implementation;

internal class TransactionService : ITransactionService
{
    private const int TicketCost = 26;

    private readonly ITransactionServiceReadRepository _transactionServiceReadRepository;
    private readonly ITransactionServiceWriteRepository _transactionServiceWriteRepository;
    private readonly IStatisticHttpClient _statisticHttpClient;

    public TransactionService(
        ITransactionServiceWriteRepository transactionServiceWriteRepository,
        ITransactionServiceReadRepository transactionServiceReadRepository,
        IStatisticHttpClient statisticHttpClient)
    {
        _transactionServiceWriteRepository = transactionServiceWriteRepository;
        _transactionServiceReadRepository = transactionServiceReadRepository;
        _statisticHttpClient = statisticHttpClient;
    }

    public async Task<int> GetCurrentBalance(Guid cardId, CancellationToken cancellationToken)
    {
        var cardExists = await _transactionServiceReadRepository.IsCardExists(cardId, cancellationToken);
        if (!cardExists)
        {
            throw new KeyNotFoundException();
        }

        return await _transactionServiceReadRepository.GetCurrentBalance(cardId, cancellationToken);
    }

    public async Task BuyTicket(Guid cardId, Guid terminalId, CancellationToken cancellationToken)
    {
        var cardExists = await _transactionServiceReadRepository.IsCardExists(cardId, cancellationToken);
        if (!cardExists)
        {
            throw new KeyNotFoundException();
        }

        var balance = await _transactionServiceReadRepository.GetCurrentBalance(cardId, cancellationToken);

        if (balance - TicketCost < 0)
        {
            throw new NotEnoughMoney();
        }

        await _transactionServiceWriteRepository.AddNewTransaction(cardId, -TicketCost, cancellationToken);

        await _statisticHttpClient.SendBuyTicketEventAsync(terminalId, cancellationToken);
    }

    public async Task AddMoney(Guid cardId, int amount, CancellationToken cancellationToken)
    {
        var cardExists = await _transactionServiceReadRepository.IsCardExists(cardId, cancellationToken);
        if (!cardExists)
        {
            throw new KeyNotFoundException();
        }

        await _transactionServiceWriteRepository.AddNewTransaction(cardId, amount, cancellationToken);
    }
}