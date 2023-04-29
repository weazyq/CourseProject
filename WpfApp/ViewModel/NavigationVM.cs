using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Utils;

namespace WpfApp.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand MathCommand { get; set; }
        public ICommand HelpCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Math(object obj) => CurrentView = new MathVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            MathCommand = new RelayCommand(Math);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
