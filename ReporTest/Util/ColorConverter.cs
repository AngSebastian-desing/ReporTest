using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ReporTest.Util
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estatus)
            {
                if (estatus == "P")
                {
                    return Color.Blue;
                }
                else if (estatus == "A")
                {
                    return Color.Green;
                }
                else if (estatus == "R")
                {
                    return Color.Red;
                }
                else
                {
                    return Color.Gray;
                }
            }

            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
