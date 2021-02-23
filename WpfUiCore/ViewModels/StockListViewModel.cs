using Caliburn.Micro;
using Finnhub.ClientCore;
using Finnhub.ClientCore.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace WpfUiCore.ViewModels
{
    public class StockListViewModel : Screen
    {
        public StockListViewModel()
        {
            _statusTimer = new TimerPlus(200) { AutoReset = true };
            _statusTimer.Elapsed += TimerElapsed;
            _statusTimer.Start();

            OnViewModelReady += OnStart;
            Bootstrapper.Client.Stock.OnCompaniesSearchComplete += Company2SearchComplete;
            Bootstrapper.Client.Stock.OnSingleCompanySearchComplete += SingleSearchComplete;
            OnViewModelReady?.Invoke(this,EventArgs.Empty);
        }



        BindableCollection<Company2> _stocks = new BindableCollection<Company2>();
        BindableCollection<Quote> _quotes = new BindableCollection<Quote>();

        Company2 _selectedStock = new Company2();
        TimerPlus _statusTimer;

        EventHandler OnViewModelReady;

        public BindableCollection<Company2> Stocks
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

        public Company2 SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                NotifyOfPropertyChange();
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }


        #region EventHandlers
        async void OnStart(object sender, EventArgs e)
        {
            Trace.WriteLine("Running startup tasks");
            await Bootstrapper.Client.Stock.SearchCompanies(Bootstrapper.Client.Symbols, true);
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateStatus();
        }

        void UpdateStatus()
        {
            //
        }

        void SearchComplete(object sender, EventArgs e)
        {
            //
        }

        async void Company2SearchComplete(object sender, EventArgs e)
        {
            var updatedStocks = new List<Company2>();
            var tempCompanies = sender as Company2[];

            var quotes = (await Bootstrapper.Client.Stock.SearchQuotes(Bootstrapper.Client.Symbols)).ToList();

            foreach (var company in tempCompanies)
            {
                foreach (var quote in quotes)
                {
                    if (quote.Symbol == company.Ticker)
                    {
                        updatedStocks.Add(company);
                        company.Quote = quote;
                    }  
                }
            }

            Trace.WriteLine($"Search complete with {this.Quotes.Count} quotes and {tempCompanies.Length} companies");
            this.Stocks.AddRange(updatedStocks);
        }

        async void SingleSearchComplete(object sender, EventArgs e)
        {
            try
            {
                var companyTask = sender as Task<Company2>;
                var Company2 = await companyTask;

                if (Company2 != null)
                {
                    Company2.Quote = await Bootstrapper.Client.Stock.GetQuote(Company2.Ticker);
                    this.Stocks.Add(Company2);
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show("You need a premium API key for this feature, unable to process request.", "Permission Exception", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error whilst searching", MessageBoxButton.OK);
            }
        }

        #endregion


    }
}
