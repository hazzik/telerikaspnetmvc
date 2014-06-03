namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Web.Mvc;

    public class Menu : ViewComponentBase, IMenuItemContainer
    {
        private string viewDatakey;

        public Menu(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator) : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");

            UrlGenerator = urlGenerator;
            Items = new LinkedObjectCollection<MenuItem>(null);
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public IList<MenuItem> Items
        {
            get;
            private set;
        }

        public string ViewDataKey
        {
            [DebuggerStepThrough]
            get
            {
                return viewDatakey;
            }

            [DebuggerStepThrough]
            set
            {
                if (string.Compare(viewDatakey, value, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    viewDatakey = value;
                }
            }
        }

        public string Theme
        {
            get;
            set;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "It might not be a valid uri.")]
        public string LoadChildItemsFromUrl
        {
            get;
            set;
        }

        protected override void WriteHtml()
        {
            TagBuilder parentBuilder = new TagBuilder("ul");
            StringBuilder innerHtml = new StringBuilder();

            foreach (MenuItem item in Items)
            {
                BuilItem(item, innerHtml);
            }

            parentBuilder.InnerHtml = innerHtml.ToString();

            TagBuilder menuBuilder = new TagBuilder("div");

            menuBuilder.MergeAttributes(HtmlAttributes);
            menuBuilder.MergeAttribute("id", Id, true);

            menuBuilder.InnerHtml = parentBuilder.ToString();

            ViewContext.HttpContext.Response.Write(menuBuilder.ToString());
        }

        private void BuilItem(MenuItem item, StringBuilder output)
        {
            TagBuilder itemBuilder = new TagBuilder("li");
            itemBuilder.MergeAttributes(item.HtmlAttributes);

            StringBuilder innerHtml = new StringBuilder();

            if (item.Content != null)
            {
                string content = ResponseCapturer.Capture(item.Content);

                if (!string.IsNullOrEmpty(content))
                {
                    innerHtml.Append(content);
                }
            }
            else
            {
                string url = UrlGenerator.Generate(ViewContext.RequestContext, item) ?? "#";

                TagBuilder linkBuilder = new TagBuilder("a");
                linkBuilder.Attributes.Add("href", url);
                linkBuilder.SetInnerText(item.Text);

                innerHtml.Append(linkBuilder.ToString());
            }

            if (!item.Items.IsNullOrEmpty())
            {
                TagBuilder childrenBuilder = new TagBuilder("ul");
                StringBuilder itemOutput = new StringBuilder();

                foreach (MenuItem childItem in item.Items)
                {
                    BuilItem(childItem, itemOutput);
                }

                itemBuilder.InnerHtml = itemOutput.ToString();
                innerHtml.Append(childrenBuilder.ToString());
            }

            itemBuilder.InnerHtml = innerHtml.ToString();

            output.Append(itemBuilder.ToString());
        }
    }
}