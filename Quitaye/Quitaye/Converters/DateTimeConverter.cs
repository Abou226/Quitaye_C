using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Quitaye.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;
                if (date.Date == DateTime.Today.Date)
                    return "Aujourd'hui";
                else if (date.Date == DateTime.Today.Date.AddDays(-1))
                    return "Hier";
                else return "Le "+ date.Date.ToString("dd-MM-yyy");
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

