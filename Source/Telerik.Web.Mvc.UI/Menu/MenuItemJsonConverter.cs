namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using UI;

    public class MenuItemJsonConverter : JsonConverterBase
    {
        public MenuItemJsonConverter(ControllerContext controllerContext, IUrlGenerator urlGenerator) : base(controllerContext)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");

            UrlGenerator = urlGenerator;
        }

        public MenuItemJsonConverter(ControllerContext controllerContext) : this(controllerContext, new UrlGenerator())
        {
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            [DebuggerStepThrough]
            get
            {
                yield return typeof(MenuItem);
            }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            IDictionary<string, object> dictionary = null;

            if (obj != null)
            {
                MenuItem menuItem = obj as MenuItem;

                if (menuItem == null)
                {
                    throw new ArgumentException("Unsupported type.", "obj");
                }

                dictionary = new Dictionary<string, object>();

                if (menuItem.Content != null)
                {
                    IResponseCapturer capturer = new ResponseCapturer(ControllerContext.HttpContext);

                    string content = capturer.Capture(menuItem.Content);

                    if (!string.IsNullOrEmpty(content))
                    {
                        dictionary.Add("content", content);
                    }
                }
                else
                {
                    string url = UrlGenerator.Generate(ControllerContext.RequestContext, menuItem);

                    if (!string.IsNullOrEmpty(url))
                    {
                        dictionary.Add("url", url);
                    }

                    dictionary.Add("text", menuItem.Text);
                }

                if (!menuItem.Enabled)
                {
                    dictionary.Add("enabled", false);
                }

                if (!menuItem.Visible)
                {
                    dictionary.Add("visible", false);
                }

                if (!menuItem.Items.IsNullOrEmpty())
                {
                    dictionary.Add("items", menuItem.Items);
                }
                else if (menuItem.LoadChildItemsAsynchronously)
                {
                    dictionary.Add("loadAync", true);
                }
            }

            return dictionary;
        }
    }
}