using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Utils
{
    public static class ArrayExtensions
    {
        public static ObservableCollection<ObservableCollection<Double>> ToObservableCollection(this Double[,] array)
        {

            ObservableCollection<ObservableCollection<Double>> collection = new ObservableCollection<ObservableCollection<Double>>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                ObservableCollection<Double> item = new ObservableCollection<Double>();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        item.Add(j);
                    }
                    else if (i != 0 && j == 0)
                    {
                        item.Add(i);
                    }
                    else
                    {
                        item.Add(array[i - 1, j - 1]);
                    }
                }
                collection.Add(item);
            }

            return collection;
        }

        public static ObservableCollection<ObservableCollection<Double>> ToObservableCollection(this Double[] array)
        {
            ObservableCollection<ObservableCollection<Double>> collection = new ObservableCollection<ObservableCollection<Double>>();

            for (Int32 i = 0; i < array.Length; i++)
            {
                ObservableCollection<Double> item = new ObservableCollection<Double> { i, array[i] };
                collection.Add(item);
            }

            return collection;
        }
    }
}
