﻿namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class DropDownListHtmlBuilder : IDropDownHtmlBuilder
    {
        public DropDownListHtmlBuilder(IDropDownRenderable component)
        {
            this.Component = component;
        }

        public IDropDownRenderable Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {           
            IHtmlNode root = new HtmlElement("div")
                                .Attributes(Component.HtmlAttributes)
                                .PrependClass(UIPrimitives.Widget, "t-dropdown", UIPrimitives.Header)
                                .ToggleClass("t-state-disabled", !Component.Enabled)
                                .ToggleAttribute("disabled", "disabled", !Component.Enabled);

            this.InnerContentTag().AppendTo(root);
            
            this.HiddenInputTag().AppendTo(root);

            return root;
        }

        public IHtmlNode InnerContentTag()
        {
            IHtmlNode root = new HtmlElement("div").AddClass("t-dropdown-wrap", UIPrimitives.DefaultState);

            string text = "&nbsp;";
            var items = Component.Items;
            int selectedIndex = Component.SelectedIndex;

            if (items.Count > 0 && !(string.IsNullOrEmpty(items[selectedIndex].Text) || items[selectedIndex].Text.Trim().Length == 0)) 
            {
                text = items[selectedIndex].Text;
            }          

            new HtmlElement("span")
                .AddClass("t-input")
                .Html(text)
                .AppendTo(root);

            IHtmlNode link = new HtmlElement("span").AddClass("t-select");

            new HtmlElement("span")
                .AddClass(UIPrimitives.Icon, "t-arrow-down")
                .Html("select")
                .AppendTo(link);

            link.AppendTo(root);
            
            return root;
        }

        public IHtmlNode HiddenInputTag()
        {
            IHtmlNode input = new HtmlElement("input", TagRenderMode.SelfClosing)
                    .Attributes(new
                    {
                        type = "text",
                        style = "display:none"
                    });

            if (Component.Name.HasValue())
                input.Attributes(Component.GetUnobtrusiveValidationAttributes())
                     .Attributes(new
                     {
                         name = Component.Name,
                         id = Component.Id
                     });

            if (Component.Items.Any())
            {
                DropDownItem selectedItem = Component.Items[Component.SelectedIndex];
                input.Attribute("value", selectedItem.Value.HasValue() ? selectedItem.Value : selectedItem.Text);
            }
           
            return input;
        }
    }
}
