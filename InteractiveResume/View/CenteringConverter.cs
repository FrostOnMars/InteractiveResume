using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveResume.View
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class CenteringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double coordinate) return value; // Return original value if conversion is not possible
            // Determine the screen's center
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            return parameter switch
            {
                // Check if we are adjusting X or Y based on the parameter
                "X" => (screenWidth / 2) + coordinate,
                "Y" => (screenHeight / 2) + coordinate,
                _ => value // Return original value if conversion is not possible
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
