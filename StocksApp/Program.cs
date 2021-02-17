using Stocks;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace StocksApp
{
    class Program
    {
        static StockManager _manager;

        static async Task Main(string[] args)
        {
            await Demo();
        }

        static async Task Demo()
        {
            var watch = Stopwatch.StartNew();

            var stonks = new String[] 
            { 
                "TSLA","IBM","AAPL","A","TLRY", "TSLA", "IBM", "BTC"
            };

            _manager = new StockManager(stonks);

            var quotes = await _manager.quoteClient.Search(_manager.Stocks);
            var companies = await _manager.companyClient.Search(_manager.Stocks);

            Console.WriteLine($"Quote Count: {quotes.Count}, Company Count: {companies.Count}");

            watch.Stop();
            Console.WriteLine($"Full search for {stonks.Length} took {watch.ElapsedMilliseconds}");
        }
    }


}
