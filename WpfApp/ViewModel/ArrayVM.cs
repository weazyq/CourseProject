using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Utils;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class ArrayVM : ViewModelBase
    {
        public MathVM MathVM { get; set; }

        public ArrayVM(MathVM mathVM)
        {
            MathVM = mathVM;
        }
    }
}
