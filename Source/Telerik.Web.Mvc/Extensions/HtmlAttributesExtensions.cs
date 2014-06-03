namespace Telerik.Web.Mvc.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public static class HtmlAttributesExtensions
	{
		public static void AppendCssClass(IDictionary<string, object> htmlAttributes, string cssClass)
		{
			htmlAttributes.AppendInValue("class", " ", cssClass);
		}

		public static void PrependCssClass(IDictionary<string, object> htmlAttributes, string cssClass)
		{
			htmlAttributes.PrependInValue("class", " ", cssClass);
		}

		public static void PrependCssClasses(IDictionary<string, object> htmlAttributes, IEnumerable<string> cssClasses)
		{
			var sb = new StringBuilder();

			cssClasses.Each(a =>
			{
				sb.Append(a);
				sb.Append(" ");
			});

			htmlAttributes.PrependInValue("class", " ", sb.ToString().Trim());
		}

		public static void AppendCssClasses(IDictionary<string, object> htmlAttributes, IEnumerable<string> cssClasses)
		{
			var sb = new StringBuilder();

			cssClasses.Each(a =>
			{
				sb.Append(" ");
				sb.Append(a);
			});

			htmlAttributes.AppendInValue("class", " ", sb.ToString().Trim());
		}
	}
}
