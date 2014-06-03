﻿// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Globalization;
    using System.Collections.Generic;
    
    using Extensions;
    using Telerik.Web.Mvc.UI.Html;
    using Telerik.Web.Mvc.Resources;

    public class TimePicker : DatePickerBase
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        public TimePicker(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.timepicker.js" });

            defaultEffects.Each(el => Effects.Container.Add(el));

            Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            
            DropDownHtmlAttributes = new Dictionary<string, object>();

            MinValue = DateTime.Today;
            MaxValue = DateTime.Today;
            
            Interval = 30;

            ButtonTitle = "Open the time view";
            ShowButton = true;
        }

        public IDictionary<string, object> DropDownHtmlAttributes { get; private set; }

        public int Interval { get; set; }

        public bool ShowButton { get; set; }

        public string ButtonTitle { get; set; }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tTimePicker", writer);

            objectWriter.Start();
            
            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Append("format", this.Format);
            objectWriter.Append("minValue", this.MinValue);
            objectWriter.Append("maxValue", this.MaxValue);
            objectWriter.Append("interval", this.Interval);
            objectWriter.Append("selectedValue", this.Value);
            objectWriter.Append("enabled", this.Enabled, true);
            objectWriter.Append("openOnFocus", this.OpenOnFocus, false);

            if (DropDownHtmlAttributes.Any())
            {
                objectWriter.Append("dropDownAttr", DropDownHtmlAttributes.ToAttributeString());
            }

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
#if MVC2 || MVC3
            Name = Name ?? ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty);
#endif
            TimePickerHtmlBuilder renderer = new TimePickerHtmlBuilder(this);

            renderer.Build().WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
