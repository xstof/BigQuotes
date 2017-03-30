using QuoteApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteApi.Services
{
    /// <summary>
    /// Service which generates random quotes fron in memory store
    /// </summary>
    public class InMemoryQuotesService : IQuotesService
    {
        private readonly Random rnd;

        /// <summary>
        /// Initializes in memory quotes service
        /// </summary>
        public InMemoryQuotesService()
        {
            this.rnd = new Random();
        }

        /// <summary>
        /// Gets a random quote
        /// </summary>
        /// <returns></returns>
        public Quote GetRandomQuote()
        {
            var quotes = getAllQuotes();
            var randomQuote = quotes[rnd.Next(quotes.Count - 1)];

            return randomQuote;
        }

        private List<Quote> getAllQuotes()
        {
            return new List<Quote>()
            {
                new Quote("0", "first quote", "first author"),
                new Quote("1", "second quote", "second author"),
                new Quote("2", "third quote", "third author"),
                new Quote("3", "fourth quote", "fourth author"),
                new Quote("4", "fifth quote", "fifth author"),
            };
        }
    }
}
