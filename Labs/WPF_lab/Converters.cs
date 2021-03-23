using System;
using System.Globalization;
using System.Windows.Data;
using Lab;

namespace WPF_lab
{
    [ValueConversion(typeof(V5DataOnGrid), typeof(string))]
    public class DataOnGridConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            V5DataOnGrid data = value as V5DataOnGrid;
            if (data != null) {
                return $"Ox step: {data.Grid.Ox}\n" +
                    $"Number of x-steps: {data.Grid.Nx}\n" +
                    $"Oy step: {data.Grid.Oy}\n" +
                    $"Number of y-steps: {data.Grid.Ny}";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverterCoord : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) {
                DataItem data = (DataItem)value;
                return $"Point = {data.Point}";
            }
            return "null occured";


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverterValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) {
                DataItem data = (DataItem)value;
                return $"EMValue = {data.EMValue} |EMValue| = {data.EMValue.Length()}";
            }
            return "null occured";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class IsUnsavedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) {
                bool isUnsaved = (bool) value;
                if (isUnsaved) {
                    return "(＃＞<>＜) Есть несохраненные изменения";
                } else {
                    return "(=^･ω･^=) Данные сохранены";
                }
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    
}
