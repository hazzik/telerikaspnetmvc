namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using UI;

    public class MenuItemsJsonResult : JsonResultBase
    {
        private static readonly IList<Type> defaultJsonConverterTypes = new List<Type> { typeof(MenuItemJsonConverter) };

        public MenuItemsJsonResult(IEnumerable<MenuItem> items, IEnumerable<JavaScriptConverter> jsonConverters) : base(jsonConverters)
        {
            Guard.IsNotNull(items, "items");

            Items = new List<MenuItem>(items);
        }

        public MenuItemsJsonResult(IEnumerable<MenuItem> items) : this(items, new List<JavaScriptConverter>())
        {
        }

        public MenuItemsJsonResult() : this(new List<MenuItem>())
        {
        }

        public static IEnumerable<Type> DefaultJsonConverterTypes
        {
            [DebuggerStepThrough]
            get
            {
                return defaultJsonConverterTypes;
            }
        }

        public IEnumerable<MenuItem> Items
        {
            get;
            private set;
        }

        protected override void Serialize(JavaScriptSerializer serializer, ControllerContext context)
        {
            Guard.IsNotNull(serializer, "serializer");
            Guard.IsNotNull(context, "context");

            IList<JavaScriptConverter> converters = new List<JavaScriptConverter>();

            foreach (Type type in DefaultJsonConverterTypes)
            {
                if (type.IsAssignableFrom(typeof(JavaScriptConverter)))
                {
                    bool exists = false;

                    foreach (JavaScriptConverter converter in JsonConverters)
                    {
                        if (type.IsAssignableFrom(converter.GetType()))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        JavaScriptConverter converter = (type.IsAssignableFrom(typeof(JsonConverterBase)) ?
                                                        Activator.CreateInstance(type, new object[] { context }) :
                                                        Activator.CreateInstance(type))
                                                        as JavaScriptConverter;

                        if (converter != null)
                        {
                            converters.Add(converter);
                        }
                    }
                }
            }

            if (!converters.IsNullOrEmpty())
            {
                serializer.RegisterConverters(converters);
            }

            string json = serializer.Serialize(Items);

            context.HttpContext.Response.Write(json);
        }
    }
}