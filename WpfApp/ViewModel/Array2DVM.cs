using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Utils;

namespace WpfApp.ViewModel
{
    internal class Array2DVM : ViewModelBase
    {
        private Double[,] _array;

        public Double[,] Array
        {
            get { return _array; }
            set
            {
                _array = value;
                OnPropertyChanged();
            }
        }

        public Array2DVM()
        {
        }

        public void SetArray(Double[,] array)
        {
            Array = array;
        }
    }
}
