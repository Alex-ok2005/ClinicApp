using System;
using System.Globalization;
using System.Windows.Controls;

namespace ClinicApp.Domain
{
    public class FutureDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            if (DateTime.TryParse((value ?? "").ToString(), CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return ((time.Date > DateTime.Now.Date)||(time.Date < new DateTime(1753, 1, 1)))
                ? new ValidationResult(false, "Некорректная дата")
                : ValidationResult.ValidResult;
            else return ValidationResult.ValidResult;
        }
    }
}