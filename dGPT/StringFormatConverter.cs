﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace dGPT
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string formatParameter = (string)parameter;
            if (formatParameter.Contains("{"))
            {
                return string.Format(formatParameter, value);
            }
            else
            {
                return string.Format(culture, "{0:" + formatParameter + "}", value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}