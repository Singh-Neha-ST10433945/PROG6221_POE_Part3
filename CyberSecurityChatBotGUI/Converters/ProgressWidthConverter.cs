using System;
using System.Globalization;
using System.Windows.Data;

namespace CyberSecurityChatBotGUI.Converters
{
    /// <summary>
    /// Converts progress values (current / max) to a width for a progress bar, relative to total available width.
    /// </summary>
    public class ProgressWidthConverter : IMultiValueConverter
    {
        /// <summary>
        /// Calculates the proportional width of a progress bar based on current progress.
        /// </summary>
        /// <param name="values">An array expected to contain: [0] total width (double), [1] current value (double), [2] max value (double)</param>
        /// <param name="targetType">The target type (unused)</param>
        /// <param name="parameter">Optional converter parameter (unused)</param>
        /// <param name="culture">The culture to use in the converter (unused)</param>
        /// <returns>A double representing the computed width for the progress bar; 0 if input is invalid</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Ensure all 3 inputs are valid doubles and max is greater than zero to avoid division by zero
            if (values.Length == 3 &&
                values[0] is double totalWidth &&
                values[1] is double current &&
                values[2] is double max &&
                max > 0)
            {
                // Calculate proportional width: (current / max) * totalWidth
                return (current / max) * totalWidth;
            }

            // Fallback: return 0 if inputs are invalid
            return 0;
        }

        /// <summary>
        /// ConvertBack is not implemented as this converter is intended for one-way binding only.
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
