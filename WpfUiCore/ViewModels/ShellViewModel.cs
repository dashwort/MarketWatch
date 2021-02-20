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
    public class ShellViewModel : Conductor<object>
    {
        private List<DisplayContainer> _views;
        private WindowManager WindowManager = new WindowManager();

        private DisplayContainer _primary;
        private DisplayContainer _secondary;
        private DisplayContainer _tertiary;
        private string _title;

        public string ToolTipText { get; set; } = "Tool Tip Display";

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
        public DisplayContainer Secondary
        {
            get { return _secondary; }
            set
            {
                _secondary = value;
                NotifyOfPropertyChange();
            }
        }
        public DisplayContainer Tertiary
        {
            get { return _tertiary; }
            set
            {
                _tertiary = value;
                NotifyOfPropertyChange();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange();
            }
        }

        public ICommand LeftClickCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Console.WriteLine("LeftClickCommand"),
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

  
        public ShellViewModel()
        {
            UpdateVersionNumber();
            var view = new StockListViewModel();
            ActivateItem(view);
        }

   
        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
        }
 
        private void UpdateVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Title = $"Outlook Clipboard v.{versionInfo.FileVersion}";
        }

        void DisplayViews()
        {
            var defaultView = new DisplayContainer();

            Primary = defaultView;
            Secondary = defaultView;
            Tertiary = defaultView;

            foreach (var View in AllViews)
            {
                switch (View.DisplayIndex)
                {
                    case 1:
                        Primary = View;
                        break;
                    case 2:
                        Secondary = View;
                        break;
                    case 3:
                        Tertiary = View;
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