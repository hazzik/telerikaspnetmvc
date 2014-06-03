namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Web.Mvc.Html;
    using System.Text;
    using System.Collections.Generic;

    public static class HtmlHelpersExtensions
    {
        private static IDictionary<string, int> controllerToProductIdMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"grid", 718},
                {"menu", 719},
                {"panelbar", 720},
                {"tabstrip", 721}
            };

        public static ExampleConfigurator Configurator(this HtmlHelper instance, string title)
        {
            return new ExampleConfigurator(instance)
                        .Title(title);
        }

        public static string ProductMetaTag(this HtmlHelper instance)
        {
            string controller = (string)instance.ViewContext.RouteData.Values["controller"];

            if (!controllerToProductIdMap.ContainsKey(controller))
            {
                return string.Empty;
            }

            return String.Format("<meta name=\"ProductId\" content=\"{0}\" />", controllerToProductIdMap[controller]);
        }

        public static string CheckBox(this HtmlHelper instance, string id, bool isChecked, string labelText)
        {
            return (new StringBuilder())
                        .Append(instance.CheckBox(id, isChecked))
                        .Append("<label for=\"")
                        .Append(id)
                        .Append("\">")
                        .Append(labelText)
                        .Append("</label>")
                    .ToString();
        }

        public static string GetCurrentTheme(this HtmlHelper instance)
        {
            return instance.ViewContext.HttpContext.Request.QueryString["theme"] ?? "vista";
        }
    }

    public class ExampleConfigurator : IDisposable
    {
        private HtmlTextWriter writer;
        private HtmlHelper htmlHelper;
        private string title;
        private MvcForm form;
        public const string CssClass = "configurator";

        public ExampleConfigurator(HtmlHelper instance)
        {
            htmlHelper = instance;
            writer = new HtmlTextWriter(instance.ViewContext.HttpContext.Response.Output);
        }

        public ExampleConfigurator Title(string title)
        {
            this.title = title;

            return this;
        }

        public ExampleConfigurator Begin()
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass + "-legend");
            writer.RenderBeginTag(HtmlTextWriterTag.H4);
            writer.Write(title);
            writer.RenderEndTag();

            return this;
        }

        public ExampleConfigurator End()
        {
            writer.RenderEndTag(); // fieldset

            if (form != null)
                form.EndForm();

            return this;
        }

        public void Dispose()
        {
            End();
        }

        public ExampleConfigurator PostTo(string action, string controller)
        {
            string theme = htmlHelper.ViewContext.HttpContext.Request.Params["theme"] ?? "vista";

            form = htmlHelper.BeginForm(action, controller, new { theme = theme });

            return this;
        }
    }
}