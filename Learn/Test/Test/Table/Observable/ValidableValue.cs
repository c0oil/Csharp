using System.ComponentModel;
using System.Linq;
using Test.BaseUI;

namespace Test.Table.Observable
{
    public class ValidableValue<T> : ObservableValue<T>, IDataErrorInfo
    {
        public ValidableValue() { }
        public ValidableValue(T initValue): base(initValue) { }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Value" && (string.IsNullOrEmpty(Value as string)))
                {
                    result = "Please enter Value";
                }
                HasError = result != null;
                return result;
            }
        }

        public string Error
        {
            get { return Value.ToString(); }
        }

        public bool HasError { get; set; }
    }

    public class DoubleValidableValue : ObservableValue<string>, IDataErrorInfo
    {
        public DoubleValidableValue() { }
        public DoubleValidableValue(string initValue) : base(initValue) { }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                double number;
                if (columnName == "Value" && !string.IsNullOrWhiteSpace(Value) && !double.TryParse(Value, out number))
                {
                    result = "Please enter Value";
                }
                HasError = result != null;
                return result;
            }
        }

        public string Error
        {
            get { return "Please enter Value"; }
        }

        public bool HasError { get; set; }
    }

    public class MaskedValidableValue : ObservableValue<string>, IDataErrorInfo
    {
        public MaskedValidableValue() { }
        public MaskedValidableValue(string initValue) : base(initValue) { }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Value" && (string.IsNullOrEmpty(Value) || Value.Contains(TextBoxInputMaskBehavior.DefaultPromptChar)))
                {
                    result = "Please enter Value";
                }
                HasError = result != null;
                return result;
            }
        }

        public static string SkipWrongMaskedValue(string val)
        {
            return val.Contains(TextBoxInputMaskBehavior.DefaultPromptChar) ? string.Empty : val;
        }

        public string Error
        {
            get { return "Please enter Value"; }
        }

        public bool HasError { get; set; }
    }
}