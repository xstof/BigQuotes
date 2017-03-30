using QuoteApi.Domain;

namespace QuoteApi.Services
{
    public interface IQuotesService
    {
        Quote GetRandomQuote();
    }
}