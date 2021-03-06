﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuoteApi.Services;
using QuoteApi.Domain;

namespace QuoteApi.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQuotesService quotesService;

        public QuotesController(IQuotesService quotesService)
        {
            this.quotesService = quotesService;
        }

        /// <summary>
        /// Retrieves random quote
        /// </summary>
        /// <returns>Random quote.</returns>
        // GET api/quotes/random
        [HttpGet("/api/quotes/random", Name="GetRandomQuote")]
        [ProducesResponseType(typeof(Quote), 200)]
        public ActionResult Get()
        {
            var quote = quotesService.GetRandomQuote();
            return Ok(quote);
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
