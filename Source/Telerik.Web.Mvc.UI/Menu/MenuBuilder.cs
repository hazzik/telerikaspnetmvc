namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq.Expressions;

    public class MenuBuilder : ViewComponentBuilderBase<Menu>, IHideObjectMembers
    {
        public MenuBuilder(Menu component) : base(component)
        {
        }

        public MenuBuilder Name(string elementName)
        {
            Guard.IsNotNullOrEmpty(elementName, "elementName");

            Component.Name = elementName;

            return this;
        }

        public MenuBuilder HtmlAttributes(object htmlAttributes)
        {
            Guard.IsNotNull(htmlAttributes, "htmlAttributes");

            Component.HtmlAttributes.Clear();
            Component.HtmlAttributes.Merge(htmlAttributes);

            return this;
        }

        public MenuBuilder Items(Action<MenuItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            MenuItemFactory factory = new MenuItemFactory(Component);

            addAction(factory);

            return this;
        }

        public MenuBuilder BindTo(string viewDataKey)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.ViewDataKey = viewDataKey;

            return this;
        }

        public MenuBuilder BindTo<TModel>(Expression<Func<TModel, object>> expression) where TModel : class
        {
            return this;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "Url might not be a valid uri.")]
        public MenuBuilder LoadChildItemsFromUrl(string url)
        {
            Component.LoadChildItemsFromUrl = url;

            return this;
        }

        public MenuBuilder Theme(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Component.Theme = name;

            return this;
        }
    }
}