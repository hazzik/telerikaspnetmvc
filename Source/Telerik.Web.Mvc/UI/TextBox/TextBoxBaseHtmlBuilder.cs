// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class TextboxBaseHtmlBuilder<T> : ITextBoxBaseHtmlBuilder where T : struct {
        
        public TextboxBaseHtmlBuilder(TextBoxBase<T> input)
        {
            Input = input;
        }

        public TextBoxBase<T> Input
        { 
            get; 
            private set; 
        }

        public IHtmlNode Build(string objectName)
        {
            return new HtmlTag("div")
                   .Attribute("id", Input.Id)
                   .Attributes(Input.HtmlAttributes)
                   .PrependClass(UIPrimitives.Widget, objectName)
                   .ToggleClass("t-state-disabled", !Input.Enabled);
        }

        public IHtmlNode InputTag()
        {
            return new HtmlTag("input", TagRenderMode.SelfClosing)
                   .Attributes(new { name = Input.Name, id = Input.Id + "-input", value = "{0}".FormatWith(GetValue())})
                   .ToggleAttribute("disabled", "disabled", !Input.Enabled)
                   .Attributes(Input.InputHtmlAttributes)
                   .PrependClass(UIPrimitives.Input);
        }

        public IHtmlNode UpButtonTag()
        {
            string title = string.IsNullOrEmpty(Input.ButtonTitleUp) ? "Increase value" : Input.ButtonTitleUp;

            return new HtmlTag("a")
                   .Attributes(new { href = "#", title = title, tabindex = "-1" })
                   .AddClass(UIPrimitives.Link, UIPrimitives.Icon, "t-arrow-up")
                   .Text("Increment");
        }

        public IHtmlNode DownButtonTag()
        {
            string title = string.IsNullOrEmpty(Input.ButtonTitleDown) ? "Decrease value" : Input.ButtonTitleDown;

            return new HtmlTag("a")
                   .Attributes(new { href = "#", title = title, tabindex = "-1" })
                   .AddClass(UIPrimitives.Link, UIPrimitives.Icon, "t-arrow-down")
                   .Text("Decrement");
        }

        private T? GetValue()
        {
            ModelState state;
            T? value = null;
            ViewDataDictionary viewData = Input.ViewContext.ViewData;
            
            if (Input.Value != null)
            {
                value = Input.Value;
            }
            else if (viewData.ModelState.TryGetValue(Input.Id, out state))
            {
                if (state.Errors.Count == 0)
                {
                    value = state.Value.ConvertTo(typeof(T), CultureInfo.CurrentCulture) as T?;
                }
            }
            
            object valueFromViewData = viewData.Eval(Input.Name);

            if (valueFromViewData != null)
            {
                value = (T)Convert.ChangeType(valueFromViewData, typeof(T));
            }

            return value;
        }
    }
}
