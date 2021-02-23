using Caliburn.Micro;
using Finnhub.ClientCore.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUiCore.ViewModels
{
    public class SearchResultViewModel : Screen
    {
        public SearchResultViewModel()
        {
            Bootstrapper.Client.Stock.OnSymbolSearchComplete += SearchComplete;
        }

        BindableCollection<Result> _results = new BindableCollection<Result>();
        SearchResult _searchResult = new SearchResult();
        Result _result = new Result();
        string _searchString = string.Empty;
        string _searchComboText = string.Empty;
        string[] _searchOptions = new string[] { "Symbol","News" };

        public BindableCollection<Result> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                NotifyOfPropertyChange();
            }
        }

        public SearchResult SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyOfPropertyChange();
            }
        }

        public Result SelectedResult
        {
            get { return _result; }
            set
            {
                _result = value;
                NotifyOfPropertyChange();
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                NotifyOfPropertyChange();
            }
        }

        public string SearchComboText
        {
            get { return _searchComboText; }
            set
            {
                _searchString = value;
                NotifyOfPropertyChange();
            }
        }

        public string[] SearchOptions
        {
            get { return _searchOptions; }
            set
            {
                _searchOptions = value;
                NotifyOfPropertyChange();
            }
        }

        public async Task RunSearch()
        {
            if (string.IsNullOrEmpty(this.SearchString)) 
                return;

            this.SearchResult = new SearchResult();
            this.Results.Clear();
            await Bootstrapper.Client.Stock.SearchSymbolAsync(this.SearchString);
        }

        public async Task AddToPortfolio()
        {
            try
            {
                if (SelectedResult != null && !string.IsNullOrEmpty(SelectedResult.Symbol))
                {
                    await Bootstrapper.Client.Stock.GetCompany2(this.SelectedResult.Symbol, true);
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show($"You need a premium API key for this feature, unable to process request. Error: {ex.Message}", "Permission Exception", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error whilst searching", MessageBoxButton.OK);
            }
        }

        public async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (SelectedResult != null && !string.IsNullOrEmpty(SelectedResult.Symbol))
                {
                    var res = await Bootstrapper.Client.Stock.GetCompany2(this.SelectedResult.Symbol, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error whilst searching", MessageBoxButton.OK);
            }
        }

        public async void SearchString_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchString)) return;

            try
            {
                if (e.Key == Key.Enter)
                {
                    await RunSearch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error whilst searching", MessageBoxButton.OK);
            }
        }

        public void SearchString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchString))
                this.Results.Clear();
        }

        async void SearchComplete(object sender, EventArgs e)
        {
            await Task.Run(() => {
                this.SearchResult = sender as SearchResult;

                if (this.SearchResult != null)
                {
                    if (this.SearchResult.Result.Count > 0)
                    {
                        Trace.WriteLine($"Search complete with {this.SearchResult.Result.Count} results");
                        this.Results.AddRange(SearchResult.Result);
                    }
                }
            });
            
        }
    }
}
