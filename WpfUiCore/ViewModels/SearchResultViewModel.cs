using Caliburn.Micro;
using Stocks;
using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            this.Manager = Bootstrapper.Container.Manager;
            this.Manager.searchResultClient.OnSearchComplete += SearchComplete;
        }

        void SearchComplete(object sender, EventArgs e)
        {
            this.SearchResult = sender as SearchResult;

            if(this.SearchResult != null)
            {
                if (this.SearchResult.Result.Count > 0)
                {
                    Trace.WriteLine($"Search complete with {this.SearchResult.Result.Count} results");
                    this.Results.AddRange(SearchResult.Result);
                }
            }
        }

        BindableCollection<Result> _results = new BindableCollection<Result>();
        SearchResult _searchResult = new SearchResult();
        StockManager _manager;
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

        public StockManager Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
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
            await Manager.searchResultClient.Search(SearchString);
        }

        public bool CanRunSearch()
        {
            return true;
        }

        public async void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedResult != null && !string.IsNullOrEmpty(SelectedResult.Symbol))
            {
                var res = await Manager.companyClient.GetCompany(SelectedResult.Symbol, true);
            }
        }

        public async void SearchString_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchString)) return;

            if(e.Key == Key.Enter)
            {
                await RunSearch();
            }
        }
    }
}
