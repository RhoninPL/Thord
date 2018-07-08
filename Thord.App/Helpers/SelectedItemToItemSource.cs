using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Thord.App.Helpers
{
    public class SelectedItemToItemSource : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return new List<object>() { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}