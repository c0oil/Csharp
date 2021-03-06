﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Parcer.Utils
{
    public static class UiHelper
    {
        public static Window FindLastActiveWindow()
        {
            return Application.Current.Windows.Cast<Window>().LastOrDefault(window => window.IsActive);
        }

        public static void HidePreviousWindow(Window formToShow, Window formToHide)
        {
            if (formToHide != null && formToHide != formToShow)
            {
                formToShow.Loaded += (s, e) => formToHide.Visibility = Visibility.Collapsed;
                formToShow.Closed += (s, e) => formToHide.Visibility = Visibility.Visible;
            }
        }

        public static void ExecuteAndCatchException(Action method)
        {
            try
            {
                method?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Stacktrace", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
    }
    
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumVal)
        {
            return GetAttributeOfType<DescriptionAttribute>(enumVal).Description;
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
