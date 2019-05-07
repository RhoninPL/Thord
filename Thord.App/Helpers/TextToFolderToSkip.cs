using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Thord.App.Helpers
{
    public class TextToFolderToSkip : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> folders)
                return string.Join(";", folders);

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return new List<string> { (string)value };
            var values = ((string)value)?.Split(';');
            return values?.ToList();
        }

        #endregion
    }
}