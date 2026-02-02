using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClockNest.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayString<TEnum>(this TEnum value) where TEnum : struct, System.Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var display = field?.GetCustomAttribute<DisplayAttribute>();
            return display?.Name ?? value.ToString();
        }
    }
}
