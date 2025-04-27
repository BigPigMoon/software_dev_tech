namespace Payment.Web.Application.GetCards.Implementation;

internal class GetCardService : IGetCardService
{
    private readonly IGetCardServiceReadRepository _readRepository;
    
    public GetCardService(IGetCardServiceReadRepository readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<IEnumerable<Guid>> GetAllCardsIdAsync()
    {
        var cards = await _readRepository.GetAllCards();  
        return cards.Select(c => c.Id);
    }
}