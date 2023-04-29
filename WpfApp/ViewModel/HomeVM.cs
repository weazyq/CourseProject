using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.ViewModel
{
    public class HomeVM : Utils.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public HomeVM()
        {
            _pageModel = new PageModel();
        }
    }
}
