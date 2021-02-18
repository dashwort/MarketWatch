using Newtonsoft.Json;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks
{
    public class QuoteClient
    {
        public string key;
        public HttpClient client;
        public EventHandler OnSearchComplete;

        public QuoteClient(string apikey)
        {
            this.key = apikey;

            if (this.client == null)
                this.client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Finnhub-Token", key);
        }

        public QuoteClient(string apikey, HttpClient webClient)
        {
            this.key = apikey;
            this.client = webClient;
        }

        public async Task<List<Quote>> Search(string[] stocks)
        {
            if (!StockManager.Approved(stocks.Length)) throw new ApplicationException("API Limit Reached");

            List<Task<Quote>> globalQuotes = new List<Task<Quote>>();

            foreach (var stock in stocks)
            {
                globalQuotes.Add(Task.Run(() => GetQuote(stock)));
            }
            
            var results = (await Task.WhenAll(globalQuotes)).ToList();
            OnSearchComplete?.Invoke(results, EventArgs.Empty);
            return results;
        }

        public async Task<Quote> GetQuote(string symbol)
        {
            var watch = Stopwatch.StartNew();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=" + symbol),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                watch.Stop();
                var quote = JsonConvert.DeserializeObject<Quote>(body);
                quote.Latency = watch.ElapsedMilliseconds;
                quote.Symbol = symbol;

                StockManager.LogRequest(1);
                return quote;
            }
        }
    }
}
