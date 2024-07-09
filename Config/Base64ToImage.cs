using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2App2.Config
{
    public class Base64toImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource image = null;

            if (value != null)
            {
                String Base64Image = (String)value;
                byte[] fotobyte = System.Convert.FromBase64String(Base64Image);
                var stream = new MemoryStream(fotobyte);
                image = ImageSource.FromStream(() => stream);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
