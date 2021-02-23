using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ThreeFourteen.Finnhub.Client.Model;
using ThreeFourteen.Finnhub.Client.Serialisation;

namespace ThreeFourteen.Finnhub.Client
{
    public class StockClient
    {
        private readonly FinnhubClient _finnhubClient;

        public EventHandler OnCompaniesSearchComplete;
        public EventHandler OnQuotesSearchComplete;
        public EventHandler OnSymbolSearchComplete;
        public EventHandler OnSingleCompanySearchComplete;
        public EventHandler OnSingleQuoteSearchComplete;

        internal StockClient(FinnhubClient finnhubClient)
        {
            _finnhubClient = finnhubClient;
        }

        public async Task<Company2[]> SearchCompanies(string[] stocks, bool raiseEventOnComplete = false)
        {
            if (!_finnhubClient.Manager.Approved(stocks.Length)) throw new ApplicationException("API Limit Reached");
            if (stocks.Length == 0) throw new NullReferenceException("Passed in stocks cannot be empty");

            Trace.WriteLine($"Running search for {stocks.Length} Companies");

            var c = new List<Task<Company2>>();

            foreach (var stock in stocks)
            {
                c.Add(Task.Run(() => GetCompany2(stock)));
            }

            var results = await (Task.WhenAll(c));

            if (raiseEventOnComplete)
                OnCompaniesSearchComplete?.Invoke(results, EventArgs.Empty);

            return results;
        }

        public async Task<Quote[]> SearchQuotes(string[] stocks, bool raiseEventOnComplete = false)
        {
            if (!_finnhubClient.Manager.Approved(stocks.Length)) throw new ApplicationException("API Limit Reached");
            if (stocks.Length == 0) throw new NullReferenceException("Passed in stocks cannot be empty");

            Trace.WriteLine($"Running search for {stocks.Length} Quotes");

            var q = new List<Task<Quote>>();

            foreach (var stock in stocks)
            {
                q.Add(Task.Run(() => GetQuoteAsync(stock)));
            }

            var results = await (Task.WhenAll(q));

            if (raiseEventOnComplete)
                OnQuotesSearchComplete?.Invoke(results, EventArgs.Empty);

            return results;
        }

        public async Task<SearchResult> SearchSymbolsAsync(string symbol, bool raiseEventOnComplete = false)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var result = await _finnhubClient.SendAsync<SearchResult>("search", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));

            if (raiseEventOnComplete)
            {
                Trace.WriteLine("Raising on symbol search complete event");
                OnSymbolSearchComplete?.Invoke(result, EventArgs.Empty);
            }
                
            return result;
        }

        public async Task<SearchResult> SearchSymbolAsync(string searchTerm)
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

                OnSymbolSearchComplete?.Invoke(search, EventArgs.Empty);
                return search;
            }
        }

        public Task<SearchResult> SearchSymbols(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<SearchResult>("search", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<Company> GetCompany(string symbol, bool raiseEventOnComplete = false)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var result = _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));

            if (raiseEventOnComplete)
                OnSingleQuoteSearchComplete?.Invoke(result, EventArgs.Empty);

            return result;
        }

        public Task<Company2> GetCompany2(string symbol, bool raiseEventOnComplete = false)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var result = _finnhubClient.SendAsync<Company2>("stock/profile2", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));

            if (raiseEventOnComplete)
                OnSingleCompanySearchComplete?.Invoke(result, EventArgs.Empty);

            return result;
        }

        public Task<Company> GetCompanyByIsin(string isin)
        {
            if (string.IsNullOrWhiteSpace(isin)) throw new ArgumentException(nameof(isin));

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Isin, isin));
        }

        public Task<Company> GetCompanyByCusip(string cusip)
        {
            if (string.IsNullOrWhiteSpace(cusip)) throw new ArgumentException(nameof(cusip));

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Cusip, cusip));
        }

        public Task<Compensation> GetCompensation(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<Compensation>("stock/ceo-compensation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<RecommendationTrend[]> GetRecommendationTrends(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<RecommendationTrend[]>("stock/recommendation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<PriceTarget> GetPriceTarget(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<PriceTarget>("stock/price-target", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<string[]> GetPeers(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<string[]>("stock/peers", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<Earnings[]> GetEarnings(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<Earnings[]>("stock/earnings", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<StockExchange[]> GetExchanges()
        {
            return _finnhubClient.SendAsync<StockExchange[]>("stock/exchange", JsonDeserialiser.Default);
        }

        public Task<Symbol[]> GetSymbols(string exchange)
        {
            return _finnhubClient.SendAsync<Symbol[]>("stock/symbol", JsonDeserialiser.Default,
                new Field(FieldKeys.Exchange, exchange));
        }

        public Task<Quote> GetQuote(string symbol, bool raiseEventOnComplete = false)
        {
            var result = _finnhubClient.SendAsync<Quote>("quote", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));

            if (raiseEventOnComplete)
                OnSingleQuoteSearchComplete?.Invoke(result, EventArgs.Empty);

            return result;
        }

        public async Task<Quote> GetQuoteAsync(string symbol, bool raiseEventOnComplete = false)
        {
            var result = await _finnhubClient.SendAsync<Quote>("quote", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));

            if (raiseEventOnComplete)
                OnSingleQuoteSearchComplete?.Invoke(result, EventArgs.Empty);

            result.Symbol = symbol;

            return result;
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, int count)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.Count, count.ToString()))
                .ConfigureAwait(false);

            return data.Map();
        }

        public Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from)
        {
            return GetCandles(symbol, resolution, from, DateTime.UtcNow);
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from, DateTime to)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.From, new DateTimeOffset(from).ToUnixTimeSeconds().ToString()),
                new Field(FieldKeys.To, new DateTimeOffset(to).ToUnixTimeSeconds().ToString()))
                .ConfigureAwait(false);

            return data.Map();
        }
    }
}
