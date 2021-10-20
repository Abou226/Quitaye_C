using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Quitaye.Converters
{
    public class DynamicColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "FourthColor")
            {
                Color color = Color.FromHex("#191616");
                return new SolidColorBrush(color);
            }
            else if(value.ToString() == "ThirdColor")
            {
                Color color = Color.FromHex("#01F9FF");
                return new SolidColorBrush(color);
            }
            else if(value.ToString() == "Secondary")
            {
                Color color = Color.FromHex("#920CFF");
                return new SolidColorBrush(color);
            }
            else
            {
                Color color = Color.FromHex("#e1edf3");
                return new SolidColorBrush(color);
            } 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
