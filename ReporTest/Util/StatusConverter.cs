using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ReporTest.Util
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string status)
            {
                if (status == "A")
                    return "Aceptado";
                else if (status == "P")
                    return "Pendiente";
                else if (status == "R")
                    return "Rechazado";
            }

            return "Desconocido";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
