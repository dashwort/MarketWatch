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

            OnViewModelReady += OnStart;
            this.Manager = Bootstrapper.Container.Manager;

            Manager.searchResultClient.OnSearchComplete += SearchComplete;
            Manager.companyClient.OnSearchComplete += CompanySearchComplete;
            Manager.companyClient.OnSingleSearchComplete += SingleSearchCmplete;

            OnViewModelReady?.Invoke(this,EventArgs.Empty);
        }

        async void SingleSearchCmplete(object sender, EventArgs e)
        {
            var company = sender as Company;

            if(company != null)
            {
                company.Quote = await Manager.quoteClient.GetQuote(company.Ticker);
                this.Stocks.Add(company);
            }
        }

        BindableCollection<Company> _stocks = new BindableCollection<Company>();
        BindableCollection<Quote> _quotes = new BindableCollection<Quote>();

        Company _selectedStock = new Company();
        TimerPlus _statusTimer;
        StockManager _manager;

        EventHandler OnViewModelReady;

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
            Trace.WriteLine("Running startup tasks");
            Task.Run(() => Manager.companyClient.Search(_manager.Stocks));
        }

        async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // TODO pull status for review here
            await Task.Run(() => Trace.WriteLine("timer elapsed"));
        }

        async void SearchComplete(object sender, EventArgs e)
        {
            await Task.Run(() => Trace.WriteLine("timer elapsed"));
        }

        async void CompanySearchComplete(object sender, EventArgs e)
        {
            var updatedStocks = new List<Company>();
            var tempStocks = sender as List<Company>;

            var quotes = await Manager.quoteClient.Search(_manager.Stocks);

            foreach (var stock in tempStocks)
            {
                var quote = quotes.Where(x => x.Symbol == stock.Ticker).ToList();

                if (quote.Count == 1)
                {
                    stock.Quote = quote[0];
                    updatedStocks.Add(stock);
                }
            }

            this.Stocks.AddRange(updatedStocks);
        }

        #endregion


    }
}
