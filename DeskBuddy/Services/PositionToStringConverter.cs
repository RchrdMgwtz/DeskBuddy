using System.Globalization;
using System.Windows.Data;
using DeskBuddy.Domain;
using DeskBuddy.Resources;

namespace DeskBuddy.Services;

public class PositionToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Position enumValue)
        {
            return enumValue switch
            {
                Position.Sitting => Messages.Settings_Sitting,
                Position.Standing => Messages.Settings_Standing,
                _ => enumValue.ToString()
            };
        }

        return value?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var enumValue in Enum.GetValues<Position>())
        {
            if (value?.ToString() == enumValue.ToString())
            {
                return enumValue;
            }
        }

        return null;
    }
}