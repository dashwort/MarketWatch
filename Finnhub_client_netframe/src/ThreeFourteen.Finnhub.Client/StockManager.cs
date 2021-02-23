using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ThreeFourteen.Finnhub.Client
{
    public class StockManager
    {
        // 
        public StockManager(string[] stocks)
        {
            this.Stocks = stocks;
            this.Key = LoadKey();

            this.client.DefaultRequestHeaders.Add("X-Finnhub-Token", Key);

            // request timer 
            _requestTimer = new TimerPlus(SearchInterval * 1000) { AutoReset = true };
            _requestTimer.Elapsed += TimerElapsed;
            _requestTimer.Start();
        }

        #region properties

        public HttpClient client = new HttpClient();

        public string Key { get; set; }
        public string[] Stocks { get; set; }
        public bool SearchLimitReached { get; set; } = false;
        public static int SearchInterval { get; set; } = 60;
        public static int MaxRequests { get; set; } = 60;
        #endregion

        #region fields
        public readonly TimerPlus _requestTimer;
        static int _requestCount;
        #endregion

        #region events
        async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Reset();

            if (SearchLimitReached)
            {
                await Task.Run(() => Trace.WriteLine(_requestTimer.TimeLeft));
            }
        }
        #endregion

        #region methods
        internal string LoadKey()
        {
            return File.ReadAllText(@"C:\Temp\stocksapp\key.txt");
        }

        public bool Approved(int reqs)
        {
            bool approval = false;

            var totalReqs = _requestCount + reqs;

            if (totalReqs <= MaxRequests)
            {
                approval = true;
            }

            return approval;
        }

        public void LogRequest(int reqs)
        {
            if (reqs > 0)
                _requestCount += reqs;
        }

        public void Reset()
        {
            Trace.WriteLine($"Max requests in time period {_requestCount}");
            _requestCount = 0;
        }
        #endregion




    }
}
