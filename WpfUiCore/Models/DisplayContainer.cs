using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUiCore.ViewModels;
using WpfUiCore.Views;

namespace WpfUiCore.Models
{
    public class DisplayContainer : Screen
    {
        private int _index;
        private bool _toLoad;
        private string _displayName;

        public int DisplayIndex
        {
            get { return _index; }
            set
            {
                _index = value;
                NotifyOfPropertyChange();
            }
        }

        public bool ToLoad
        {
            get { return _toLoad; }
            set
            {
                _toLoad = value;
                NotifyOfPropertyChange();
            }
        }

        public string DisplayNameView
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyOfPropertyChange();
            }
        }

        public DisplayContainer()
        {
            ToLoad = false;
        }
    }
}
