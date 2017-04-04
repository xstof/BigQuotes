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
                new Quote("0", "You can put wings on a pig but that doesn't make it an eagle.", "William J Clinton"),
                new Quote("1", "I learned long ago never to wrestle with a pig.  You get dirty and besides... the pig likes it.", "George Bernard Shaw"),
                new Quote("2", "Intellectual property has the shelf live of a banana.", "Bill Gates"),
                new Quote("3", "Someone's sitting in the shade today because someone planted a tree long ago.", "Warren Buffet"),
                new Quote("4", "Shoot for the moon and if you miss you will still be amongst the stars", "Les Brown"),
            };
        }
    }
}
