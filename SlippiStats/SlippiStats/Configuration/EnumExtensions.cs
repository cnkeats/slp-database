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
        public static string GetStockIconPath(this Enum @enum)
        {
            Type enumType = @enum.GetType();

            var attribute = GetAttribute<StockIconAttribute>(@enum);

            return attribute.StockIcon;
        }

        public static int GetTierPlacement(this Enum @enum)
        {
            Type enumType = @enum.GetType();

            var attribute = GetAttribute<TierPlacementAttribute>(@enum);

            return attribute?.TierPlacement ?? 999;
        }

        public static bool GetTournamentLegality(this Enum @enum)
        {
            Type enumType = @enum.GetType();

            var attribute = GetAttribute<TournamentLegalAttribute>(@enum);

            if (attribute == null)
            {
                return true;
            }

            return attribute.TournamentLegal;
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

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

    }

    class StockIconAttribute : Attribute
    {
        public string StockIcon { get; private set; }

        public StockIconAttribute(string path)
        {
            this.StockIcon = path;
        }
    }

    class TournamentLegalAttribute : Attribute
    {
        public bool TournamentLegal { get; private set; }

        public TournamentLegalAttribute(bool legality)
        {
            this.TournamentLegal = legality;
        }
    }

    class TierPlacementAttribute : Attribute
    {
        public int TierPlacement { get; private set; }

        public TierPlacementAttribute(int index)
        {
            this.TierPlacement = index;
        }
    }
}