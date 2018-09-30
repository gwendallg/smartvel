using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace SmartVel.Converters
{
    public class ByteToPhotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is byte[] model))
            {
                return ImageSource.FromResource($"SmartVel.Resources.png.ic_camera.png");
            }

            return ImageSource.FromStream(() => new MemoryStream(model));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
