using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public class MathClass
    {
        public static Double Precision = 0.001;

        /// <summary>
        /// Округление числа до точности
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="epsilon">Точность</param>
        /// <returns>Округлённое Double число</returns>
        public static Double RoundToPrecision(Double number)
        {
            Double result = Math.Round(number / Precision) * Precision;
            return result;
        }

        /// <summary>
        /// Нахождение суммы ряда по контрольной формуле
        /// </summary>
        /// <param name="x">Текущая точка</param>
        /// <param name="epsilon">Точность округления</param>
        /// <returns>Возвращает сумму ряда в точке</returns>
        public static Double ControlFormula(Double x)
        {
            Double sumOfSeries = 2 * Math.Pow(Math.Sin(x), 2);

            return RoundToPrecision(sumOfSeries);
        }

        /// <summary>
        /// Нахождение суммы ряда по главной формуле
        /// </summary>
        /// <param name="x">Текущая точка</param>
        /// <param name="epsilon">Точность округления</param>
        /// <returns>Double число</returns>
        public static Double CalcSumOfSeries(Double x)
        {
            Double s1;
            Double sum = 0;
            Int32 i = 1;
            do
            {
                sum += s1 = CalcPartialSumOfSeries(x, i);
                i++;
            } while (Math.Abs(s1) > Precision);

            return RoundToPrecision(sum);
        }

        /// <summary>
        /// Нахождение частичной суммы ряда в точке
        /// </summary>
        /// <param name="x">Текущая точка</param>
        /// <param name="i">Текущая итерация</param>
        /// <returns>Double число</returns>
        public static Double CalcPartialSumOfSeries(Double x, Int32 i)
        {
            Double res = Math.Pow(-1, i + 1) * (Math.Pow(2 * x, 2 * i) / CalcFactorial(2 * i));
            return res;
        }

        

        /// <summary>
        /// Генерация массива "рандомным" способом
        /// </summary>
        /// <param name="countRows">Строки</param>
        /// <param name="countColumns">Столбцы</param>
        /// <param name="lowerBound">Нижняя граница генерации</param>
        /// <param name="upperBound">Верхняя граница генерации</param>
        /// <returns>Double массив значений</returns>
        public static Double[,] GenerateArray(Int32 countRows, Int32 countColumns, Int32 lowerBound, Int32 upperBound)
        {
            Random rand = new Random();
            
            Double[,] array = new Double[countRows, countColumns];

            for (Int32 i = 0; i < array.GetLength(0); i++)
            {
                for (Int32 j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rand.Next(lowerBound, upperBound + 1);
                }
            }

            return array;
        }

        /// <summary>
        /// Фомирование массива суммы ряда
        /// </summary>
        /// <param name="x1">Начальное значение</param>
        /// <param name="x2">Конечное значение</param>
        /// <param name="h">Шаг</param>
        /// <param name="epsilon">Точность</param>
        /// <returns>Double массив значений</returns>
        public static List<Double> FormArrayOfSumSeries(Double x1, Double x2, Double h)
        {
            List<Double> array = new List<Double>();

            while (x1 <= x2)
            {
                Double sum = CalcSumOfSeries(x1);
                array.Add(sum);

                x1 += h;
            }

            return array;
        }

        //Формирование массива с использованием интерполирующей функции Ньютона
        /// <summary>
        /// Формирование массива с использованием интерполирующей функции Ньютона
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="g">Шаг</param>
        /// <returns>Массив </returns>
        public static Double[] FormArrayOfNewtonInterpolation(Double[] array, Double g)
        {
            List<Double> Y = new List<Double>();

            Double[] x = new Double[array.Length];

            for (Int32 i = 0; i < x.Length; i++)
            {
                x[i] = i;
            }

            Double[] coef = GetNewtonPolynom(x, array);

            for (Double fx = x[0]; fx < x[x.Count() - 1] + 0.1; fx += g)
            {
                Double element = Newton(coef, x, fx);
                Y.Add(RoundToPrecision(element));
            }

            return Y.ToArray();
        }

        public static Double CalcFactorial(Double x)
        {
            if (x < 0 ) return Double.NaN;
            
            Int32 sum = 1;

            for (Int32 i = 1; i <= x; i++)
            {
                sum *= i;
            }

            return sum;
        }

        public static Double[,] SquareMatrix(Double[,] matrix)
        {
            Int32 n = matrix.GetLength(0);
            
            Double[,] result = new Double[n, n];

            for (Int32 i = 0; i < n; i++)
            {
                for (Int32 j = 0; j < n; j++)
                {
                    result[i, j] = 0;

                    for (Int32 k = 0; k < n; k++)
                    {
                        result[i, j] += matrix[i, k] * matrix[k, j];
                    }
                }
            }

            return result;
        }

        public static Double[] MultiplyMatrix(Double[,] matrix, Double[] vector)
        {
            Int32 rows = matrix.GetLength(0);
            Int32 columns = matrix.GetLength(1);

            if (columns != vector.Length)
                throw new ArgumentException("Несовместимые размеры двумерного и одномерного массива");

            Double[] result = new Double[rows];

            for (Int32 row = 0; row < rows; row++)
            {
                Double sum = 0;
                for (Int32 col = 0; col < columns; col++)
                {
                    sum += matrix[row, col] * vector[col];
                }
                result[row] = sum;
            }

            return result;
        }

        public static Double[] GetNewtonPolynom(Double[] x, Double[] y)
        {
            Double[,] newton = new Double[x.Count(), x.Count()];
            for (Int32 i = 0; i < x.Count(); i++)
            {
                for (Int32 j = 0; j < x.Count(); j++)
                {
                    if (j == 0)
                    {
                        newton[i, j] = y[i];
                    }
                    else if (j <= i)
                    {
                        newton[i, j] = (newton[i, j - 1] - newton[j - 1, j - 1]) / (x[i] - x[j - 1]);
                    }
                }
            }

            Double[] args = new Double[x.Count()];
            for (Int32 i = 0; i < args.Length; i++)
            {
                args[i] = newton[i, i];
            }
            return args;
        }

        public static Double Newton(Double[] coef, Double[] x, Double t)
        {
            Double S = 0;
            for (Int32 i = 0; i < coef.Length; i++)
            {
                Double P = 1;
                for (Int32 j = 0; j < i; j++)
                {
                    P *= (t - x[j]);
                }
                S += coef[i] * P;
            }
            return S;
        }

        public static Double[] BubbleSort(Double[] array)
        {
            Double[] result = new Double[array.Length];

            for (Int32 i = 0; i < result.Length; i++)
            {
                result[i] = array[i];
            }

            Int32 length = result.Length;
            while (length != 0)
            {
                Int32 max_index = 0;
                for (Int32 i = 1; i < length; i++)
                {
                    if (result[i - 1] < result[i])
                    {
                        Double temp = result[i - 1];
                        result[i - 1] = result[i];
                        result[i] = temp;

                        max_index = i;
                    }
                }
                length = max_index;
            }
            return result;
        }

        public static Double GetMaxElem(Double[] array)
        {
            Double max = Double.MinValue;

            for (Int32 i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }

        public static Double GetMinElem(Double[] array)
        {
            Double minimum = Double.MaxValue;

            for (Int32 i = 0; i < array.Length; i++)
            {
                if (array[i] < minimum)
                {
                    minimum = array[i];
                }
            }

            return minimum;
        }
    }
}
