using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MathLib;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CalcFactorial_SingleNumber_ReturnsFactorial()
        {
            Assert.AreEqual(Double.NaN, MathClass.CalcFactorial(-1));
            Assert.AreEqual(1, MathClass.CalcFactorial(0));
            Assert.AreEqual(1, MathClass.CalcFactorial(1));
            Assert.AreEqual(2, MathClass.CalcFactorial(2));
            Assert.AreEqual(6, MathClass.CalcFactorial(3));
        }

        [TestMethod]
        public void CalcSumOfSeriesByControlFormula_SingleNumber_ReturnsSumOfSeries()
        {
            Assert.AreEqual(1.227, MathClass.ControlFormula(-0.9));
            Assert.AreEqual(0.02, MathClass.ControlFormula(-0.1));
            Assert.AreEqual(0, MathClass.ControlFormula(0));
            Assert.AreEqual(0.02, MathClass.ControlFormula(0.1));
            Assert.AreEqual(1.227, MathClass.ControlFormula(0.9));
        }

        [TestMethod]
        public void CalcSumOfSeriesByMainFormula_SingleNumber_ReturnsSumOfSeries()
        {
            Assert.AreEqual(1.227, MathClass.ControlFormula(-0.9));
            Assert.AreEqual(0.02, MathClass.ControlFormula(-0.1));
            Assert.AreEqual(0, MathClass.ControlFormula(0));
            Assert.AreEqual(0.02, MathClass.ControlFormula(0.1));
            Assert.AreEqual(1.227, MathClass.ControlFormula(0.9));
        }

        [TestMethod]
        public void GenerateArrayB_Params_ReturnsGeneratedArrayB()
        {
            Int32 lowerBound = -50;
            Int32 upperBound = 50;

            Double[,] array = MathClass.GenerateArray(3, 3, lowerBound, upperBound);

            for (Int32 i = 0; i < array.GetLength(0); i++)
            {
                for (Int32 j = 0; j < array.GetLength(1); j++)
                {
                    Assert.IsTrue(array[i, j] >= lowerBound && array[i, j] <= upperBound);
                }
            }
        }

        [TestMethod]
        public void SquareMatrix_Matrix_ReturnsSquareMatrix()
        {
            Double[,] arrayB =
            {
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 },
            };

            Double[,] expected =
            {
                {30, 66, 102},
                {36, 81, 126},
                {42, 96, 150}
            };

            CollectionAssert.AreEqual(expected, MathClass.SquareMatrix(arrayB));
        }

        [TestMethod]
        public void MultiplyMatrix_MatrixAndVector_ReturnsMultiplyResult()
        {
            Double[] arrayA = { 1, 2, 3 };
            Double[,] arrayB =
            {
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 },
            };

            Double[] expected = { 30, 36, 42 };

            Double[] result = MathClass.MultiplyMatrix(arrayB, arrayA);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BubbleSortArray_Array_ReturnsSumOfSeries()
        {
            Double[] array = { 10, 3, 0, -10 };
            Double[] sortedArray = { 10, 3, 0, -10};

            CollectionAssert.AreEqual(sortedArray, MathClass.BubbleSort(array));
        }

        [TestMethod]
        public void GetMaxElem_Array_ReturnsMaxNumber()
        {
            Double[] array1 = { 10, 3, 0, -10 };
            Double[] array2 = { -10, -3, 0 };

            Assert.AreEqual(10, MathClass.GetMaxElem(array1));
            Assert.AreEqual(0, MathClass.GetMaxElem(array2));
        }

        [TestMethod]
        public void GetMinElem_Array_ReturnsMinNumber()
        {
            Double[] array1 = { 10, 3, 0, -10 };
            Double[] array2 = { -10, -3, 0 };

            Assert.AreEqual(-10, MathClass.GetMinElem(array1));
            Assert.AreEqual(-10, MathClass.GetMinElem(array2));
        }
    }
}
