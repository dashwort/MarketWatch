using Newtonsoft.Json;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public class SearchResultClient
    {
        public string key;
        public HttpClient client;
        public EventHandler OnSearchComplete;

        public SearchResultClient(string apikey)
        {
            this.key = apikey;

            if (this.client == null)
                this.client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Finnhub-Token", key);
        }

        public SearchResultClient(string apikey, HttpClient webClient)
        {
            this.key = apikey;
            this.client = webClient;
        }

        public async Task<SearchResult> Search(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm)) throw new ApplicationException("Search cannot be empty");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Finnhub-Token", "c0kq75n48v6und6rt5f0");

            var watch = Stopwatch.StartNew();
            Trace.WriteLine($"Running search for '{searchTerm}'");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://finnhub.io/api/v1/search?q=" + searchTerm),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var search = JsonConvert.DeserializeObject<SearchResult>(body);
                watch.Stop();
                search.Latency = watch.ElapsedMilliseconds;

                StockManager.LogRequest(1);
                OnSearchComplete?.Invoke(search, EventArgs.Empty);
                return search;
            }
        }
    }


}
