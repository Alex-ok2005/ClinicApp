using System;
using System.Globalization;
using System.Windows.Controls;

namespace MaterialDesignDemo.Domain
{
    public class FutureDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            if (DateTime.TryParse((value ?? "").ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return time.Date > DateTime.Now.Date
                ? new ValidationResult(false, "Дата еще не наступила")
                : ValidationResult.ValidResult;
            else return ValidationResult.ValidResult;
        }
    }
}