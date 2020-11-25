using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SlippiStats.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum @enum)
        {
            DisplayAttribute attribute = GetDisplayAttribute(@enum);

            return attribute?.Name ?? @enum.ToString();
        }

        public static string GetDescription(this Enum @enum)
        {
            var attribute = GetDisplayAttribute(@enum);

            return attribute?.Description ?? @enum.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            if (value == null)
            {
                return null;
            }

            Type type = value.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException($"Type {type} is not an enum.");
            }

            FieldInfo fieldInfo = type.GetField(value.ToString());

            return fieldInfo?.GetCustomAttribute<DisplayAttribute>();
        }
    }
}