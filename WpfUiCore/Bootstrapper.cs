using Caliburn.Micro;
using Stocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfUiCore.Models;
using WpfUiCore.ViewModels;

namespace WpfUiCore
{
    public class Bootstrapper : BootstrapperBase
    {
        public static Dependencies Container = new Dependencies();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

    }
}
