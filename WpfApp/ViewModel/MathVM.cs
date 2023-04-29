using MathLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Model;
using WpfApp.Utils;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class MathVM : ViewModelBase
    {
        private readonly PageModel _pageModel;

        private MathArrays _mathArrays;

        public Double[] ArrayA
        {
            get { return _mathArrays.ArrayA; }
            set
            {
                _mathArrays.ArrayA = value;
                OnPropertyChanged();
            }
        }

        public Double[,] ArrayB
        {
            get { return _mathArrays.ArrayB; }
            set
            {
                OnPropertyChanged();
            }
        }

        public Double[] ArrayC
        {
            get { return _mathArrays.ArrayC; }
            set
            {
                _mathArrays.ArrayC = value;
                OnPropertyChanged();
            }
        }

        public Double[] ArrayY
        {
            get { return _mathArrays.ArrayY; }
            set
            {
                _mathArrays.ArrayY = value;
                OnPropertyChanged();
            }
        }

        public Double[] ArrayYSort
        {
            get { return _mathArrays.ArrayYSort; }
            set
            {
                _mathArrays.ArrayYSort = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ObservableCollection<Double>> _arrayA { get; set; }
        public ObservableCollection<ObservableCollection<Double>> ArrayAItems
        {
            get { return _arrayA; }
            set { _arrayA = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ObservableCollection<Double>> _arrayC { get; set; }
        public ObservableCollection<ObservableCollection<Double>> ArrayCItems
        {
            get { return _arrayC; }
            set { _arrayC = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ObservableCollection<Double>> _arrayY { get; set; }

        public ObservableCollection<ObservableCollection<Double>> ArrayYItems
        {
            get { return _arrayY; }
            set { _arrayY = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ObservableCollection<Double>> _arrayYSorted { get; set; }
        public ObservableCollection<ObservableCollection<Double>> ArrayYItemsSorted
        {
            get { return _arrayYSorted; }
            set {  _arrayYSorted = value; OnPropertyChanged(); }
        }

        public Double X1 { get; set; } = -0.9;
        public Double X2 { get; set; } = 0.9;
        public Double H { get; set; } = 0.1;
        public Double Precision { get; set; } = 0.001;
        public Int32 LowerBound { get; set; } = -50;
        public Int32 UpperBound { get; set; } = 50;
        public Double G { get; set; } = 0.1;

        public ICommand ArrayACommand { get; }
        public ICommand ArrayBCommand { get; }
        public ICommand ArrayCCommand { get; }
        public ICommand ArrayYCommand { get; }
        public ICommand ShowArrayACommand { get; }
        public ICommand ShowArrayBCommand { get; }
        public ICommand ShowArrayCCommand { get; }
        public ICommand ShowArrayYCommand { get; }
        public ICommand ShowArrayYSortCommand { get; }
        public ICommand ClearArraysCommand { get; }
        public ICommand ShowHelpCommand { get; }
        public ICommand ShowGraphCommand { get; }
        public ICommand SaveDataCommand { get; }

        public MathVM()
        {
            ArrayACommand = new RelayCommand(cmd => FormArrayA(X1,X2,H));
            ArrayBCommand = new RelayCommand(cmd => FormArrayB(LowerBound,UpperBound), canExecute => ArrayAItems?.Count > 0);
            ArrayCCommand = new RelayCommand(cmd => FormArrayC(), canExecute => ArrayB?.Length > 0);
            ArrayYCommand = new RelayCommand(cmd => FormArrayY(G), canExecute => ArrayCItems?.Count > 0);
            ShowArrayACommand = new RelayCommand(cmd => ShowArrayWindow(ArrayAItems), canExecute => ArrayAItems?.Count > 0);
            ShowArrayBCommand = new RelayCommand(cmd => ShowArray2DWindow(ArrayB), canExecute => ArrayB?.Length > 0);
            ShowArrayCCommand = new RelayCommand(cmd => ShowArrayWindow(ArrayCItems), canExecute => ArrayCItems?.Count > 0);
            ShowArrayYCommand = new RelayCommand(cmd => ShowArrayWindow(ArrayYItems), canExecute => ArrayYItems?.Count > 0);
            ShowArrayYSortCommand = new RelayCommand(cmd => ShowArrayWindow(ArrayYItemsSorted), canExecute => ArrayYItems?.Count > 0);
            ClearArraysCommand = new RelayCommand(cmd => ClearArrays());
            ShowHelpCommand = new RelayCommand(cmd => ShowHelp());
            ShowGraphCommand = new RelayCommand(cmd => ShowGraph(), canExecute => ArrayYItems?.Count > 0);
            SaveDataCommand = new RelayCommand(cmd => SaveData(), canExecute => ArrayA?.Length > 0 || ArrayB?.LongLength > 0 || ArrayC?.Length > 0 || ArrayY?.Length > 0);

            _pageModel = new PageModel();
            _mathArrays = new MathArrays();
        }

        #region WINDOW METHODS
        public void ShowArray2DWindow(Double[,] array)
        {
            Array2DView dataView = new Array2DView();
            Array2DVM dataVM = new Array2DVM();
            dataVM.SetArray(array);
            dataView.DataContext = dataVM;
            dataView.Show();
        }

        public void ShowArrayWindow(ObservableCollection<ObservableCollection<Double>> array)
        {
            ArrayView dataView = new ArrayView();
            dataView.DataContext = array;
            dataView.Show();
        }

        private void ShowHelp()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help.chm");
            Process.Start(path);
        }

        private void ShowGraph()
        {
            Double[] X = new Double[ArrayY.Length];

            for (Int32 i = 0; i < X.Length; i++)
            {
                X[i] = i;
            }

            GraphView graphView = new GraphView(X, ArrayY);
            graphView.Show();
        }

        private async void SaveData()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Результат";
            dlg.DefaultExt = ".text";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Boolean? result = dlg.ShowDialog();

            String path = Path.GetFullPath(dlg.FileName); ;

            if (result != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    await WriteArrayAsync(streamWriter, "Массив A", "A", ArrayA);
                    await WriteArrayAsync(streamWriter, "Массив B", ArrayB);
                    await WriteArrayAsync(streamWriter, "Массив C", "C", ArrayC);
                    await WriteArrayAsync(streamWriter, "Массив Y", "Y", ArrayY);
                    await WriteArrayAsync(streamWriter, "Массив Y (сортированный)", "Y", ArrayY);
                }
            }
        }

        private Boolean ArrayIsNotNull(Array array)
        {
            return (array != null && array.Length > 0) ? true : false;
        }

        private async Task WriteArrayAsync(StreamWriter streamWriter, String title, String index, Double[] array)
        {
            if (ArrayIsNotNull(array))
            {
                await streamWriter.WriteLineAsync($"\n{title}");
                Int32 count = 0;
                foreach (var item in array)
                {
                    String str = $"{index}[{count}]: {item} ";
                    await streamWriter.WriteLineAsync(str);
                    count++;
                }
            }
        }

        private async Task WriteArrayAsync(StreamWriter streamWriter, String title, Double[,] array)
        {
            if (ArrayIsNotNull(array))
            {
                await streamWriter.WriteLineAsync($"\nМассив {title}");
                for (Int32 i = 0; i < array.GetLength(0); i++)
                {
                    for (Int32 j = 0; j < array.GetLength(1); j++)
                    {
                        String str = $"{array[i, j]}\t";
                        await streamWriter.WriteAsync(str);
                    }
                    await streamWriter.WriteLineAsync();
                }
            }
        }
        #endregion

        #region ARRAY METHODS
        public void FormArrayA(Double x1, Double x2, Double h)
        {
            ArrayAItems = _mathArrays.FormArrayA(x1, x2, h);
        }

        public void FormArrayB(Int32 lowerBound, Int32 upperBound)
        {
            ArrayB = _mathArrays.FormArrayB(lowerBound, upperBound);
        }
        public void FormArrayC()
        {
            Double[] arrayC = _mathArrays.FormArrayC();

            ArrayCItems = arrayC.ToObservableCollection();
        }
        public void FormArrayY(Double g)
        {
            (Double[] arrayY, Double[] arrayYsort) = _mathArrays.FormArrayY(g);

            ArrayYSort = arrayYsort;
            ArrayYItems = arrayY.ToObservableCollection();
            ArrayYItemsSorted = arrayYsort.ToObservableCollection();
        }
        public void ClearArrays()
        {
            _mathArrays.ClearArrays();
            ArrayAItems?.Clear();
            ArrayB = new Double[,] { };
            ArrayCItems?.Clear();
            ArrayYItems?.Clear();
            ArrayYItemsSorted?.Clear();
        }
        #endregion
    }
}
