using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClockNest.Common
{
    public class EmployeeFilterStatusHelper
    {
        public static List<EnumItem<T>> GetEnumList<T>() where T : System.Enum
        {
            return System.Enum.GetValues(typeof(T))
                              .Cast<T>()
                              .Select(e => new EnumItem<T>
                              {
                                  Value = e,
                                  Name = GetDisplayName(e)
                              })
                              .ToList();
        }

        private static string GetDisplayName<T>(T enumValue) where T : System.Enum
        {
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            var displayAttr = memberInfo?.GetCustomAttribute<DisplayAttribute>();
            return displayAttr?.Name ?? enumValue.ToString();
        }
    }

    public class EnumItem<T>
    {
        public T Value { get; set; }
        public string Name { get; set; }
    }
}
