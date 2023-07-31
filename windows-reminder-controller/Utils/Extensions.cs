using System.ComponentModel;
using System.Reflection;

namespace windows_reminder_controller.Utils
{
  public static class Extensions
  {
    private static string GetDisplayName(Type type, PropertyInfo info, bool hasMetaDataAttribute)
    {
      if (!hasMetaDataAttribute)
      {
        object[] attributes = info.GetCustomAttributes(typeof(DisplayNameAttribute), false);
        if (attributes != null && attributes.Length > 0)
        {
          var displayName = (DisplayNameAttribute)attributes[0];
          return displayName.DisplayName;
        }
        return info.Name;
      }

      var propDesc = TypeDescriptor.GetProperties(type).Find(info.Name, true);

      var displayAttribute =
          propDesc?.Attributes.OfType<DisplayNameAttribute>().FirstOrDefault();

      return displayAttribute?.DisplayName ?? string.Empty;
    }

    public static string DisplayName<T>(this string prop, PropertyInfo[] headers)
      => GetDisplayName(typeof(T), headers.Where(r => r.Name == prop).First(), false);
  }
}
