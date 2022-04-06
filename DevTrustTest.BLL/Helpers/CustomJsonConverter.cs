using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevTrustTest.BLL.Helpers
{
    public static class CustomJsonConverter
    {
        public static T Deserialize<T>(string json) where T : class, new()
        {
            if (string.IsNullOrEmpty(json))
                return null;
            T person = (T)SetValues(typeof(T), json);
            return person;
        }
        public static object SetValues(Type type, string json)
        {
            object obj = null;
            var _json = Regex.Replace(json, @"\s+", "");
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (_json.Contains($"{property.Name}:", StringComparison.OrdinalIgnoreCase))
                {
                    if ((property.PropertyType.IsValueType || property.PropertyType == typeof(string)))
                    {
                        if (obj == null)
                            obj = Activator.CreateInstance(type);
                        var results = Regex.Match(json, $"{property.Name}\\s*:(.*?),", RegexOptions.IgnoreCase);
                        Regex rgx = new Regex("[^a-zA-Z0-9]");
                        var charArray = rgx.Matches(results.Groups[1].Value).Select(g => char.Parse(g.Value)).ToArray();
                        string value = results.Groups[1].Value.Trim(charArray);
                        if (!string.IsNullOrEmpty(value) && value != "null")
                        {
                            var converter = TypeDescriptor.GetConverter(property.PropertyType);
                            var propertyValue = converter.ConvertFrom(value);
                            property.SetValue(obj, propertyValue);
                        }
                    }
                    else if (property.PropertyType.IsClass)
                    {
                        var results = Regex.Match(json, $"{property.Name}: \\s*{{(.*?)}}", RegexOptions.IgnoreCase);
                        if (!string.IsNullOrEmpty(results.Groups[1].Value))
                        {
                            property.SetValue(obj, SetValues(property.PropertyType, results.Groups[1].Value));
                        }
                    }
                }
            }
            return obj;
        }
        public static string Serialize<T>(T obj) where T : class
        {
            string result = "{";
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                string temp = null;
                if (property.PropertyType.IsValueType)
                {
                    if (property.PropertyType.Name == "Nullable`1" && property.GetValue(obj) == null)
                        temp = $"{property.Name}: null";
                    else
                        temp = $"{property.Name}: {property.GetValue(obj)}";
                }
                if (property.PropertyType == typeof(string))
                {
                    if (property.GetValue(obj) == null)
                        temp = $"{property.Name}: null";
                    else
                        temp = $"{property.Name}: \"{property.GetValue(obj)}\"";
                }
                else if (property.PropertyType.IsClass)
                {
                    if (property.GetValue(obj) == null)
                        temp = $"{property.Name}: null";
                    else
                    {
                        dynamic prop = property.GetValue(obj);
                        temp = $"{property.Name}: {Serialize(prop)}";
                    }
                }
                result += temp + ",";
            }
            if (result[result.Length - 2] == '}')
                result = result.Remove(result.LastIndexOf(','), 1);
            return result + "}";
        }
    }
}
