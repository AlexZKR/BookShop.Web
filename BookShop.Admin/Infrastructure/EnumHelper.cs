using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Admin.Infrastructure;
public static class EnumHelper<T> where T : struct, Enum
{
    public static IEnumerable<T> GetValues(Enum value)
    {
        var enumValues = new List<T>();

        foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
        {
            enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
        }
        return enumValues;
    }

    public static T? Parse(string value)
    {
        if (value != null)
            return (T)Enum.Parse(typeof(T), value, true);
        else
            return null;
    }

    public static T GetEnumValueFromString(string value)
{
    T result;
    if (Enum.TryParse(value, true, out result))
    {
        if (Enum.IsDefined(typeof(T), result))
        {
            return result;
        }
    }
    throw new ArgumentException("Invalid enum value: " + value);
}

    public static IEnumerable<string> GetNames(Enum value)
    {
        return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
    }
    public static string GetName(Enum value)
    {
        return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).FirstOrDefault()!;
    }

    public static IEnumerable<string?> GetDisplayValues(Enum value)
    {
        return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
    }

    private static string lookupResource(Type resourceManagerProvider, string resourceKey)
    {
        var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
            BindingFlags.Static | BindingFlags.Public, null, typeof(string),
            new Type[0], null);
        if (resourceKeyProperty != null)
        {
            return (string)resourceKeyProperty!.GetMethod!.Invoke(null, null)!;
        }

        return resourceKey; // Fallback with the key name
    }

    public static string? GetDisplayValue(T? value)
    {
        if (value != null)
        {


            var fieldInfo = value.GetType().GetField(value.ToString()!);

            var descriptionAttributes = fieldInfo!.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes![0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType!, descriptionAttributes[0].Name!);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name! : value!.ToString()!;
        }
        else
        {
            return null;
        }
    }

    public static IEnumerable<SelectListItem> GetStaticDataFromEnum(Enum value)
    {
        var allValues = Enum.GetValues(value.GetType());
        List<SelectListItem> selects = new List<SelectListItem>();
        Type genericType = typeof(T);
        if (genericType.IsEnum)
        {
            foreach (T e in allValues)
            {
                selects.Add
                (new SelectListItem(EnumHelper<T>.GetDisplayValue(EnumHelper<T>.Parse(e.ToString()!)),
                EnumHelper<T>.ConvertToInt(e).ToString()));
            }
        }
        return selects;
    }

    public static int ConvertToInt(T enumValue) => Unsafe.As<T, int>(ref enumValue);

}