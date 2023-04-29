using MathLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    public class MathArrays
    {
        private static Double[] A;
        private static Double[,] B;
        private static Double[] C;
        private static Double[] Y;
        private static Double[] YSort;

        public Double[] ArrayA
        {
            get { return A; }
            set { A = value; }
        }
        public Double[,] ArrayB
        {
            get { return B; }
            set { B = value; }
        }
        public Double[] ArrayC
        {
            get { return C; }
            set { C = value; }

        }
        public Double[] ArrayY
        {
            get { return Y; }
            set { Y = value; }
        }

        public Double[] ArrayYSort
        {
            get { return YSort; }
            set { YSort = value; }
        }

        #region ARRAY METHODS
        public ObservableCollection<ObservableCollection<Double>> FormArrayA(Double x1, Double x2, Double h)
        {
            List<Double> arrayA = new List<Double>();
            ObservableCollection<ObservableCollection<Double>> arrayAItems = new ObservableCollection<ObservableCollection<Double>>();

            while (x1 <= x2)
            {
                ObservableCollection<Double> arrayAItem = new ObservableCollection<Double>();
                Double fx = MathClass.CalcSumOfSeries(x1);
                Double kfx = MathClass.ControlFormula(x1);

                arrayAItem.Add(x1);
                arrayAItem.Add(fx);
                arrayAItem.Add(kfx);

                arrayA.Add(fx);
                arrayAItems.Add(arrayAItem);

                x1 += h;
            }

            ArrayA = arrayA.ToArray();
            return arrayAItems;
        }
        public Double[,] FormArrayB(Int32 lowerBound, Int32 upperBound)
        {
            Int32 length = ArrayA.Length;

            return ArrayB = MathClass.GenerateArray(length, length, lowerBound, upperBound);
        }
        public Double[] FormArrayC()
        {
            ArrayC = MathClass.MultiplyMatrix(MathClass.SquareMatrix(ArrayB), ArrayA);
            return ArrayC;
        }
        public (Double[], Double[]) FormArrayY(Double g)
        {
            ArrayY = MathClass.FormArrayOfNewtonInterpolation(ArrayC, g);
            ArrayYSort = MathClass.BubbleSort(ArrayY);
            
            return (ArrayY, ArrayYSort);
        }
        public void ClearArrays()
        {
            ArrayB = new Double[,] { };
            ArrayC = new Double[] { };
            ArrayY = new Double[] { };
            ArrayA = new Double[] { };
        }
        #endregion
    }
}
