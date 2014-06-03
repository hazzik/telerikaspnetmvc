namespace Mvc.UI.jQuery.Examples.NHaml
{
    using System.Collections.Generic;

    public static class DictionaryExtension
    {
        public static T Get<T>(this IDictionary<string, object> instance, string key)
        {
            return instance.ContainsKey(key) ? (T) instance[key] : default(T);
        }

        public static void Set<T>(this IDictionary<string, object> instance, string key, T value)
        {
            instance[key] = value;
        }
    }
}