// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class ComboBoxHtmlBuilder : IDropDownHtmlBuilder
    {
        public ComboBoxHtmlBuilder(IComboBoxRenderable component)
        {
            this.Component = component;
        }

        public IComboBoxRenderable Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            IHtmlNode root = new HtmlTag("div")
                            .Attribute("id", Component.Id)
                            .Attributes(Component.HtmlAttributes)
                            .PrependClass(UIPrimitives.Widget, "t-combobox", UIPrimitives.Header)
                            .ToggleClass("t-state-disabled", !Component.Enabled);

            this.InnerContentTag().AppendTo(root);
            this.HiddenInputTag().AppendTo(root);

            return root;
        }

        public IHtmlNode InnerContentTag()
        {
            IHtmlNode root = new HtmlTag("div").AddClass("t-dropdown-wrap t-state-default");

            IHtmlNode input = new HtmlTag("input", TagRenderMode.SelfClosing)
                              .Attributes(new { type = "text"})
                              .ToggleAttribute("disabled", "disabled", !Component.Enabled)
                              .Attributes(Component.InputHtmlAttributes)
                              .PrependClass(UIPrimitives.Input)
                              .AppendTo(root);

            if(Component.Items.Any() && Component.SelectedIndex != -1) 
            {
                input.Attribute("value", Component.Items[Component.SelectedIndex].Text);
            }

            if (Component.Id.HasValue())
            {
                input.Attributes(new
                {
                    id = Component.Id + "-input",
                    name = Component.Name + "-input"
                });

                string value = Component.ViewContext.Controller.ValueOf<string>(Component.Name + "-input");
                input.ToggleAttribute("value", value, value.HasValue());
            }

            IHtmlNode link = new HtmlTag("span").AddClass("t-select", UIPrimitives.Header);

            new HtmlTag("span").AddClass(UIPrimitives.Icon, "t-arrow-down").Html("select").AppendTo(link);

            link.AppendTo(root);

            return root;
        }

        public IHtmlNode HiddenInputTag()
        {
            IHtmlNode input = new HtmlTag("input", TagRenderMode.SelfClosing)
                              .Attributes(new { type = "text", 
                                      style="display:none" });
            
            if (Component.Items.Any() && Component.SelectedIndex != -1)
            {
                DropDownItem selectedItem = Component.Items[Component.SelectedIndex];
                input.Attribute("value", selectedItem.Value.HasValue() ? selectedItem.Value : selectedItem.Text);
            }

            if (Component.Name.HasValue()) { 
                input.Attributes(new { name = Component.Name,
                                       id = Component.Id + "-value" });

                string value = Component.ViewContext.Controller.ValueOf<string>(Component.Name);
                input.ToggleAttribute("value", value, value.HasValue());
            }
            
            return input;
        }
    }
}