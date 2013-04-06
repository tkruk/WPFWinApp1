using System;
using System.Linq;
using Caliburn.Micro;

namespace WPFWinApp1.ViewModels
{
    public class AppViewModel : PropertyChangedBase
    {
        private string _caption = "This is a test";
        public string Caption
        {
            get
            { 
                return _caption;
            }
            set
            {
                _caption = value;
                NotifyOfPropertyChange(() => Caption);
            }
        }

        private string _caption2 = "This is a test 2.";
        public string Caption2
        {
            get
            {
                return _caption2;
            }
            set
            {
                _caption2 = value;
                NotifyOfPropertyChange(() => Caption2);
            }
        }
    }
}
