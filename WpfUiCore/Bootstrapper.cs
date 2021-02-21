using Caliburn.Micro;
using Stocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using ThreeFourteen.Finnhub.Client;
using WpfUiCore.Models;
using WpfUiCore.ViewModels;

namespace WpfUiCore
{
    public class Bootstrapper : BootstrapperBase
    {
        public static FinnhubClient Client 
        { 
            get 
            { 
                return _finnhubClient; 
            } 
        }

        static FinnhubClient _finnhubClient;

        public Bootstrapper()
        {
            Initialize();
            _finnhubClient = new FinnhubClient(LoadKey());
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        static string LoadKey()
        {
            return File.ReadAllText(@"C:\Temp\stocksapp\key.txt");
        }
    }
}
