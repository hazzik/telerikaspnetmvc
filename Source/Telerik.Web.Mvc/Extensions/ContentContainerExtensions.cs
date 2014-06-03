namespace Telerik.Web.Mvc.Extensions
{
    using Telerik.Web.Mvc.UI;

    public static class ContentContainerExtensions
    {
        public static void AppendContentCssClass(this IContentContainer container, string cssClass)
        {
            HtmlAttributesExtensions.AppendCssClass(container.ContentHtmlAttributes, cssClass);
        }
    }
}
