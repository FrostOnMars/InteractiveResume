using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InteractiveResume.View
{
    public class GeometryToPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as PathGeometry;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
