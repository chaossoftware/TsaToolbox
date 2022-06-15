using System;
using System.Globalization;
using System.Windows.Controls;

namespace TsaToolbox
{
    public static class TextBoxExtension
    {
        public static double ReadDouble(this TextBox textBox)
        {
            try
            {
                return Convert.ToDouble(textBox.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Parameter {textBox.Name} should be specified as 'double'");
            }
        }

        public static int ReadInt(this TextBox textBox)
        {
            try
            {
                return Convert.ToInt32(textBox.Text, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Parameter {textBox.Name} should be specified as 'integer'");
            }
        }
    }
}
