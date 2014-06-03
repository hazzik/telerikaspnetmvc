namespace Telerik.Web.Mvc.Extensions
{
	using Telerik.Web.Mvc.UI;
	using System.Collections.Generic;
	using System.Text;

	public static class HtmlAttributesContainerExtensions
	{
		public static void AppendCssClass(this IHtmlAttributesContainer container, string cssClass)
		{
			HtmlAttributesExtensions.AppendCssClass(container.HtmlAttributes, cssClass);
		}

		public static void PrependCssClass(this IHtmlAttributesContainer container, string cssClass)
		{
			HtmlAttributesExtensions.PrependCssClass(container.HtmlAttributes, cssClass);
		}

		public static void PrependCssClasses(this IHtmlAttributesContainer container, IEnumerable<string> cssClasses)
		{
			HtmlAttributesExtensions.PrependCssClasses(container.HtmlAttributes, cssClasses);
		}

		public static void AppendCssClasses(this IHtmlAttributesContainer container, IEnumerable<string> cssClasses)
		{
			HtmlAttributesExtensions.AppendCssClasses(container.HtmlAttributes, cssClasses);
		}
	}
}
