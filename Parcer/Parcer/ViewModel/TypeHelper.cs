using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Parcer.ViewModel
{
    public static class TypeHelper
    {
        public static string GetPropertyName<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo.Name;
        }

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

        public static IEnumerable<string> GetProperties(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            return obj.GetType().GetProperties().Select(p => p.Name);
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void VerifyPropertyName(object target, string name)
        {
            if (TypeDescriptor.GetProperties(target)[name] == null)
            {
                throw new ArgumentException($"Property {name} doesn't exists in class {TypeDescriptor.GetClassName(target)}");
            }
        }
    }
}
