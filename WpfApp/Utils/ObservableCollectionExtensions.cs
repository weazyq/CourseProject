using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Utils
{
    public static class ArrayExtensions
    {
        public static ObservableCollection<ObservableCollection<Double>> ToObservabeCollection(this Double[,] array)
        {
            ObservableCollection<ObservableCollection<Double>> arrayItems = new ObservableCollection<ObservableCollection<double>>();

            for (var i = 0; i < array.GetLength(0); i++)
            {

            }

            return arrayItems;
        }
    }
}
