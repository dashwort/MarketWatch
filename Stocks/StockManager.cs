using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Stocks
{
    public class StockManager
    {
        public StockManager(string[] stocks)
        {
            this.Stocks = stocks;
            this.Key = LoadKey();

            this.quoteClient = new QuoteClient(Key, client);
            this.companyClient = new CompanyClient(Key, client);
            this.searchResultClient = new SearchResultClient(Key, client);

            this.client.DefaultRequestHeaders.Add("X-Finnhub-Token", Key);

            // request timer 
            _requestTimer = new TimerPlus(SearchInterval * 1000) { AutoReset = true };
            _requestTimer.Elapsed += TimerElapsed;
            _requestTimer.Start();
        }

        // properties
        public QuoteClient quoteClient { get; set; }
        public CompanyClient companyClient { get; set; }
        public SearchResultClient searchResultClient { get; set; }

        public HttpClient client = new HttpClient();

        public string Key { get; set; }
        public string[] Stocks { get; set; }
        public bool SearchLimitReached { get; set; } = false;
        public static int SearchInterval { get; set; } = 60;
        public static int MaxRequests { get; set; } = 60;


        // fields
        public readonly TimerPlus _requestTimer;
        static int _requestCount;

        async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Reset();

            if (SearchLimitReached)
            {
                await Task.Run(() => Console.WriteLine(_requestTimer.TimeLeft));
            }
        }

        string LoadKey()
        {
            return File.ReadAllText(@"C:\Temp\stocksapp\key.txt");
        }

        public static bool Approved(int reqs)
        {
            bool approval = false;

            var totalReqs = _requestCount + reqs;

            if(totalReqs <= MaxRequests)
            {
                approval = true;
            }

            return approval;
        }

        public static void LogRequest(int reqs)
        {
            if (reqs > 0)
                _requestCount += reqs;
        }

        void Reset()
        {
            Console.WriteLine($"Max requests in time period {_requestCount}");
            _requestCount = 0;
        }

    }
}
