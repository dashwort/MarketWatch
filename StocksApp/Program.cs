using Stocks;
using Stocks.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StocksApp
{
    class Program
    {
        static StockManager _manager;

        static async Task Main(string[] args)
        {
            await Demo();
            Console.ReadLine();
        }

        static async Task Demo()
        {
            var watch = Stopwatch.StartNew();

            var stonks = new String[] 
            { 
                "TSLA","IBM","AAPL","A","TLRY"
            };

            _manager = new StockManager(stonks);
            _manager.searchResultClient.OnSearchComplete += SearchComplete;

            Task.Run(() => _manager.searchResultClient.Search("tesla"));
            Task.Run(() => _manager.searchResultClient.Search("AAPL"));
            Task.Run(() => _manager.searchResultClient.Search("micro"));
            Task.Run(() => _manager.searchResultClient.Search("ibm"));
            Task.Run(() => _manager.searchResultClient.Search("btc"));

            var quotes = await _manager.quoteClient.Search(_manager.Stocks);
            var companies = await _manager.companyClient.Search(_manager.Stocks);

        }

        private async static void SearchComplete(object sender, EventArgs e)
        {
            var search = sender as SearchResult;

            Console.WriteLine($"Search Complete with {search.Result.Count} results in {search.Latency}ms");
        }
    }


}
