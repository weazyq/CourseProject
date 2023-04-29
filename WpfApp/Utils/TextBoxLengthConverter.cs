using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfApp.Utils
{
    public class TextBoxLengthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean textBoxesNotEmpty = true;
            if (values.All(v => v is Int32))
            {
                foreach (var item in values)
                {
                    if ((Int32)item == 0)
                    {
                        textBoxesNotEmpty = false;
                    }
                }
            }
            return textBoxesNotEmpty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
