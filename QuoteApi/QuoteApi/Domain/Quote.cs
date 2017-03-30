using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteApi.Domain
{
    /// <summary>
    /// Represents a "quote" or "saying"
    /// </summary>
    public class Quote
    {
        public Quote(string id, string quoteText, string author)
        {
            this.Id = id;
            this.QuoteText = quoteText;
            this.Author = author;
        }

        /// <summary>
        /// Unique identifier for the quote
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The actual quote or saying
        /// </summary>
        public string QuoteText { get; private set; }

        /// <summary>
        /// The author of the quote or saying
        /// </summary>
        public string Author { get; private set; }
    }
}
