using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Quitaye.Converters
{
    public class ValueProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if 30 sec is your maximum time
            // return (double)value/30;

            //if 60 sec if your maximum time
            return (decimal)value / 100;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
