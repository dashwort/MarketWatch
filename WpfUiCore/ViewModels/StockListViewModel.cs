using Caliburn.Micro;
using Stocks;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WpfUiCore.ViewModels
{
    public class StockListViewModel : Screen
    {
        public StockListViewModel()
        {
            _statusTimer = new TimerPlus(5000) { AutoReset = true };
            _statusTimer.Elapsed += TimerElapsed;
            _statusTimer.Start();

            Manager = new StockManager(stonks);
            OnViewModelReady += OnStart;
            Manager.searchResultClient.OnSearchComplete += SearchComplete;
            Manager.quoteClient.OnSearchComplete += QuoteSearchComplete;
            Manager.companyClient.OnSearchComplete += CompanySearchComplete;

            //Task.Run(() => _manager.searchResultClient.Search("tesla"));
            OnViewModelReady?.Invoke(this,EventArgs.Empty);
        }

        BindableCollection<Company> _stocks = new BindableCollection<Company>();
        BindableCollection<Quote> _quotes = new BindableCollection<Quote>();

        Company _selectedStock = new Company();
        TimerPlus _statusTimer;
        StockManager _manager;

        EventHandler OnViewModelReady;
        string[] stonks = new String[] { "TSLA","IBM","AAPL","A","TLRY", "TSLA", "IBM" };

        public BindableCollection<Company> Stocks
        {
            get { return _stocks; }
            set 
            {
                _stocks = value;
                NotifyOfPropertyChange();
            }
        }

        public BindableCollection<Quote> Quotes
        {
            get { return _quotes; }
            set
            {
                _quotes = value;
                NotifyOfPropertyChange();
            }
        }

        public Company SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                NotifyOfPropertyChange();
            }
        }

        public StockManager Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                NotifyOfPropertyChange();
            }
        }

        #region EventHandlers
        void OnStart(object sender, EventArgs e)
        {
            Console.WriteLine("Running startup tasks");
            Task.Run(() => Manager.companyClient.Search(_manager.Stocks));
        }

        async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // TODO pull status for review here
            await Task.Run(() => Console.WriteLine("timer elapsed"));
            Console.WriteLine();
        }

        async void SearchComplete(object sender, EventArgs e)
        {
            await Task.Run(() => Console.WriteLine("timer elapsed"));
        }

        async void CompanySearchComplete(object sender, EventArgs e)
        {
            Console.WriteLine("Company search complete");
            this.Stocks.AddRange(sender as List<Company>);

            var quotes = await Manager.quoteClient.Search(_manager.Stocks);

            foreach (var stock in this.Stocks)
            {
                var quote = quotes.Where(x => x.Symbol == stock.Ticker).ToList();

                if (quote.Count == 1)
                    stock.Quote = quote[0];
            }
        }

        async void QuoteSearchComplete(object sender, EventArgs e)
        {
            //Console.WriteLine("Quote Search Complete");
            //var quotes = sender as List<Quote>;
            //List<Company> updatedList = new List<Company>(); 

            //foreach (var stock in Stocks)
            //{
            //    foreach (var quote in quotes)
            //    {
            //        if(stock.Ticker == quote.Symbol)
            //        {
            //            stock.Quote = quote;
            //            updatedList.Add(stock);
            //        }
            //    }
            //}

            //if(updatedList.Count == this.Stocks.Count)
            //{
            //    this.Stocks.AddRange(updatedList);
            //}
        }

        #endregion


    }
}
