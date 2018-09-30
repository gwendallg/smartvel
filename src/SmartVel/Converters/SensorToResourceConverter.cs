using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartVel.Converters
{
    public class SensorToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value == null ? "dissocie" : "associe";
            return $"resource://SmartVel.Ui.Resources.svg.pictos_appli_capteur_{type}_2.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
