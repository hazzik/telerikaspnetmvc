// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    
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
            IHtmlNode root = new HtmlElement("div")
                            .Attributes(Component.HtmlAttributes)
                            .PrependClass(UIPrimitives.Widget, "t-combobox", UIPrimitives.Header)
                            .ToggleClass("t-state-disabled", !Component.Enabled);

            this.InnerContentTag().AppendTo(root);
            this.HiddenInputTag().AppendTo(root);

            return root;
        }

        public IHtmlNode InnerContentTag()
        {
            IHtmlNode root = new HtmlElement("div").AddClass("t-dropdown-wrap t-state-default");

            IHtmlNode input = new HtmlElement("input", TagRenderMode.SelfClosing)
                              .Attributes(new { type = "text"})
                              .ToggleAttribute("disabled", "disabled", !Component.Enabled)
                              .PrependClass(UIPrimitives.Input)
                              .AppendTo(root);

            string text = string.Empty;
            if (Component.Items.Any())
            {
                text = Component.Value;
                if (Component.SelectedIndex != -1)
                {
                    text = Component.Items[Component.SelectedIndex].Text;
                    if (Component.Encoded)
                    {
                        text = System.Web.HttpUtility.HtmlDecode(text);
                    }
                }
            }

            if (Component.Name.HasValue())
            {
                input.Attributes(new { id = Component.Id + "-input", name = Component.Name + "-input" });
                text = Component.ViewContext.Controller.ValueOf<string>(Component.Name + "-input") ?? text;
            }

            input.ToggleAttribute("value", text, text.HasValue())
                 .Attributes(Component.InputHtmlAttributes);

            IHtmlNode link = new HtmlElement("span").AddClass("t-select", UIPrimitives.Header);

            new HtmlElement("span").AddClass(UIPrimitives.Icon, "t-arrow-down").Html("select").AppendTo(link);

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

            string value = string.Empty;
            if (Component.Items.Any()) 
            {
                value = Component.Value;
                if (string.IsNullOrEmpty(value) && Component.SelectedIndex != -1) 
                {
                    DropDownItem selectedItem = Component.Items[Component.SelectedIndex];
                    value = selectedItem.Value.HasValue() ? selectedItem.Value : selectedItem.Text;
                }
            }

            if (Component.Name.HasValue()) { 
                input.Attributes(Component.GetUnobtrusiveValidationAttributes())
                     .Attributes(new 
                     { 
                         name = Component.Name,              
                         id = Component.Id
                     });

                value = Component.ViewContext.Controller.ValueOf<string>(Component.Name) ?? value;
            }

            input.ToggleAttribute("value", value, value.HasValue());
            
            return input;
        }
    }
}