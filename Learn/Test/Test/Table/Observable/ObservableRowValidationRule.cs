using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Test.Table.Observable
{
    public class ObservableRowValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
                        CultureInfo cultureInfo)
        {
            BindingGroup group = (BindingGroup)value;

            bool hasError = false;
            var observableRow = group.Items.OfType<ObservableRow>().FirstOrDefault();
            if (observableRow != null)
            {
                hasError = observableRow.HasError;
            }
            else
            {
                throw new ArgumentNullException();
            }

            return hasError ? new ValidationResult(false, "Have Error") : ValidationResult.ValidResult;
        }
    }
}
