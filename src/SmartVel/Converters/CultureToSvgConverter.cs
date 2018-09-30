using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartVel.Converters
{
    public class CultureToSvgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as CultureInfo;
            if (item == null) return null;
            return ImageSource.FromResource(
                $"resource://SmartVel.Resources.Svg.Culture_{item.Name.ToUpperInvariant()}.svg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
