namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq.Expressions;

    public class BreadcrumbBuilder : ViewComponentBuilderBase<Breadcrumb>, IHideObjectMembers
    {
        public BreadcrumbBuilder(Breadcrumb component) : base(component)
        {
        }

        public BreadcrumbBuilder Name(string elementName)
        {
            Guard.IsNotNullOrEmpty(elementName, "elementName");

            Component.Name = elementName;

            return this;
        }

        public BreadcrumbBuilder HtmlAttributes(object htmlAttributes)
        {
            Guard.IsNotNull(htmlAttributes, "htmlAttributes");

            Component.HtmlAttributes.Clear();
            Component.HtmlAttributes.Merge(htmlAttributes);

            return this;
        }

        public BreadcrumbBuilder BindTo(string viewDataKey)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.ViewDataKey = viewDataKey;

            return this;
        }

        public BreadcrumbBuilder BindTo<TModel>(Expression<Func<TModel, object>> expression) where TModel : class
        {
            return this;
        }

        public BreadcrumbBuilder SeparatorText(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            Component.SeparatorText = value;
            Component.SeparatorHtmlMarkups = null;

            return this;
        }

        public BreadcrumbBuilder SeparatorTemplate(Action htmlMarkup)
        {
            Guard.IsNotNull(htmlMarkup, "htmlMarkup");

            Component.SeparatorHtmlMarkups = htmlMarkup;
            Component.SeparatorText = null;

            return this;
        }

        public BreadcrumbBuilder Theme(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Component.Theme = name;

            return this;
        }
    }
}