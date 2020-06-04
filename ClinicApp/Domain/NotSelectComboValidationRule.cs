using System.Globalization;
using System.Windows.Controls;

namespace ClinicApp.Domain
{
    public class NotSelectComboValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((value != null) 
                && ((value.GetType() == typeof(string) 
                && (string)value != "") 
                || (value.GetType() == typeof(int) 
                && (int)value > 0)))
                return ValidationResult.ValidResult;
            else return new ValidationResult(false, "Поле не может быть пустым");
        }
    }
}
