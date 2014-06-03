namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Script.Serialization;

    public static class MenuExtension
    {
        public static MenuItemsJsonResult ToJsonResult(this IEnumerable<MenuItem> instance, Encoding contentEncoding, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            Guard.IsNotNull(instance, "instance");
            Guard.IsNotNull(jsonConverters, "jsonConverters");

            return new MenuItemsJsonResult(instance, jsonConverters) { ContentEncoding = contentEncoding };
        }

        public static MenuItemsJsonResult ToJsonResult(this IEnumerable<MenuItem> instance, Encoding contentEncoding)
        {
            Guard.IsNotNull(contentEncoding, "contentEncoding");

            return ToJsonResult(instance, contentEncoding, new JavaScriptConverter[] { });
        }

        public static MenuItemsJsonResult ToJsonResult(this IEnumerable<MenuItem> instance, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            return ToJsonResult(instance, null, jsonConverters);
        }

        public static MenuItemsJsonResult ToJsonResult(this IEnumerable<MenuItem> instance)
        {
            return ToJsonResult(instance, null, new JavaScriptConverter[] { });
        }

        public static MenuItemsJsonResult ToJsonResult(this MenuItem instance, Encoding contentEncoding, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            Guard.IsNotNull(instance, "instance");

            return ToJsonResult(new[] { instance }, contentEncoding, jsonConverters);
        }

        public static MenuItemsJsonResult ToJsonResult(this MenuItem instance, Encoding contentEncoding)
        {
            Guard.IsNotNull(contentEncoding, "contentEncoding");

            return ToJsonResult(instance, contentEncoding, new JavaScriptConverter[] { });
        }

        public static MenuItemsJsonResult ToJsonResult(this MenuItem instance, IEnumerable<JavaScriptConverter> jsonConverters)
        {
            return ToJsonResult(instance, null, jsonConverters);
        }

        public static MenuItemsJsonResult ToJsonResult(this MenuItem instance)
        {
            return ToJsonResult(instance, null, new JavaScriptConverter[] { });
        }
    }
}