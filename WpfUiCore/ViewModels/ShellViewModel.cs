using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WpfUiCore.Models;

namespace WpfUiCore.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
    {
        public ShellViewModel()
        {
            this.FirstSubViewModel = new StockListViewModel();
            this.SecondSubViewModel = new SearchResultViewModel();
        }

        #region fields
        private List<DisplayContainer> _views;
        private WindowManager WindowManager = new WindowManager();
        private DisplayContainer _primary;
        #endregion

        #region properties
        public Screen FirstSubViewModel { get; private set; }
        public Screen SecondSubViewModel { get; private set; }

        public List<DisplayContainer> AllViews
        {
            get { return _views; }
            set { _views = value; }
        }

        public DisplayContainer Primary
        {
            get { return _primary; }
            set
            {
                _primary = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region ICommandArea
        public ICommand LeftClickCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Trace.WriteLine("LeftClickCommand"),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand ExitApplication
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.Shutdown(0),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }
        #endregion

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
        }
 
        void DisplayViews()
        {
            var defaultView = new DisplayContainer();

            Primary = defaultView;

            foreach (var View in AllViews)
            {
                switch (View.DisplayIndex)
                {
                    case 1:
                        Primary = View;
                        break;
                    default:
                        Primary = View;
                        break;
                }
            }
        }
    }

    public class DelegateCommand : ICommand
    {
        public System.Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}