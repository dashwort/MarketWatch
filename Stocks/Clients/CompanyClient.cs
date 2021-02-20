using Newtonsoft.Json;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public class CompanyClient
    {
        public string key;
        public HttpClient client;
        public EventHandler OnSearchComplete;

        public CompanyClient(string apikey)
        {
            this.key = apikey;

            if (this.client == null)
                this.client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Finnhub-Token", key);
        }

        public CompanyClient(string apikey, HttpClient webClient)
        {
            this.key = apikey;
            this.client = webClient;
        }

        public async Task<List<Company>> Search(string[] stocks)
        {
            if (!StockManager.Approved(stocks.Length)) throw new ApplicationException("API Limit Reached");

            Console.WriteLine($"Running search for {stocks.Length} Companies");

            List<Task<Company>> globalQuotes = new List<Task<Company>>();

            foreach (var stock in stocks)
            {
                globalQuotes.Add(Task.Run(() => GetCompany(stock)));
            }

            var results = (await Task.WhenAll(globalQuotes)).ToList();
            OnSearchComplete?.Invoke(results, EventArgs.Empty);
            return results;
        }

        public async Task<Company> GetCompany(string symbol)
        {
            var watch = Stopwatch.StartNew();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://finnhub.io/api/v1/stock/profile2?symbol=" + symbol),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                watch.Stop();
                var company = JsonConvert.DeserializeObject<Company>(body);

                StockManager.LogRequest(1);
                return company;
            }
        }
    }
}
