using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Test.ViewModel
{
    public static class TypeHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> p)
        {
            return GetPropertyNameInternal(p);
        }

        private static string GetPropertyNameInternal(LambdaExpression p)
        {
            return GetPropertyInternal(p).Name;
        }

        private static PropertyInfo GetPropertyInternal(LambdaExpression p)
        {
            MemberExpression memberExpression;

            if (p.Body is UnaryExpression)
            {
                UnaryExpression ue = (UnaryExpression)p.Body;
                memberExpression = (MemberExpression)ue.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)p.Body;
            }
            return (PropertyInfo)(memberExpression).Member;
        }
    }
}
