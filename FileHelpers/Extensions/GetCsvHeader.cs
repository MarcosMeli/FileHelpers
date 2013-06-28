using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
 
// ReSharper disable CheckNamespace
namespace FileHelpers
    // ReSharper restore CheckNamespace
{
    public static class FileHelpersTypeExtensions {
        public static IEnumerable<string> GetFieldTitles(this Type type) {
            var fields = from field in type.GetFields(
            BindingFlags.GetField |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance)
                         where field.IsFileHelpersField()
                         select field;

            return from field in fields
                   let attrs = field.GetCustomAttributes(true)
                   let order = attrs.OfType<FieldOrderAttribute>().Single().GetOrder()
                   let title = attrs.OfType<FieldTitleAttribute>().Single().Name
                   orderby order
                   select title;
        }

        public static string GetCsvHeader(this Type type) {
            // using nasty, nasty hack because the original delimiter is not public
            return String.Join(type.GetCustomAttributes(true).OfType<Joiner>().Single().Delimiter, type.GetFieldTitles());
        }

        static bool IsFileHelpersField(this FieldInfo field) {
            return field.GetCustomAttributes(true)
            .OfType<FieldOrderAttribute>()
            .Any();
        }

        static int GetOrder(this FieldOrderAttribute attribute) {
            // Hack cos FieldOrderAttribute.Order is internal (why?)
            var pi = typeof(FieldOrderAttribute)
            .GetProperty("Order",
            BindingFlags.GetProperty |
            BindingFlags.Instance |
            BindingFlags.NonPublic);

            return (int)pi.GetValue(attribute, null);
        }
    }
}
