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

    public class TimePickerHtmlBuilder
    {
        public TimePickerHtmlBuilder(TimePicker component)
        {
            Component = component;
        }

        public TimePicker Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            IHtmlNode wrapper = new HtmlTag("div")
                                .Attribute("id", Component.Id)
                                .Attributes(Component.HtmlAttributes)
                                .PrependClass(UIPrimitives.Widget, "t-timepicker");

            IHtmlNode innerWrapper = new HtmlTag("div")
                                    .AddClass("t-picker-wrap")
                                    .AppendTo(wrapper);

            InputTag().AppendTo(innerWrapper);
            
            if(Component.ShowButton)
                ButtonTag().AppendTo(innerWrapper);

            return wrapper;
        }

        public IHtmlNode InputTag()
        {
            ModelState state;
            DateTime? date = null;
            ViewDataDictionary viewData = Component.ViewContext.ViewData;

            if (Component.Value != null && Component.Value != DateTime.MinValue)
            {
                date = Component.Value;
            }
            else if (viewData.ModelState.TryGetValue(Component.Id, out state))
            {
                if (state.Errors.Count == 0)
                {
                    date = state.Value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture) as DateTime?;
                }
            }

            object valueFromViewData = viewData.Eval(Component.Name);

            if (valueFromViewData != null)
            {
                DateTime parsedDate;
                if (DateTime.TryParse(valueFromViewData.ToString(), CultureInfo.CurrentCulture.DateTimeFormat, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    date = parsedDate;
                }
            }

            string value = string.Empty;

            if (date != null)
            {
                if (string.IsNullOrEmpty(Component.Format))
                {
                    value = date.Value.ToShortTimeString();
                }
                else
                {
                    value = date.Value.ToString(Component.Format);
                }
            }

            return new HtmlTag("input", TagRenderMode.SelfClosing)
                .Attributes(new { id = Component.Id + "-input", name = Component.Name })
                .Attributes(Component.InputHtmlAttributes)
                .PrependClass(UIPrimitives.Input)
                .ToggleAttribute("disabled", "disabled", !Component.Enabled)
                .ToggleAttribute("value", value, value.HasValue());
        }

        public IHtmlNode ButtonTag()
        {
            IHtmlNode wrapper = new HtmlTag("span")
                                .AddClass("t-select");

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon, "t-icon-clock")
                .Attribute("title", Component.ButtonTitle)
                .Html(Component.ButtonTitle)
                .AppendTo(wrapper);

            return wrapper;
        }
    }
}