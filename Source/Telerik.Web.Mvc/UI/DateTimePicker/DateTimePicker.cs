// (c) Copyright 2002-2009 Telerik 
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

    public class DateTimePicker : ViewComponentBase, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };
        static internal DateTime defaultMinDate = new DateTime(1899, 12, 31);
        static internal DateTime defaultMaxDate = new DateTime(2100, 1, 1);

        public DateTimePicker(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.datetimepicker.js", "telerik.datepicker.js", "telerik.calendar.js", "telerik.timepicker.js" });

            ClientEvents = new DateTimePickerClientEvents();
            Effects = new Effects();
            defaultEffects.Each(el => Effects.Container.Add(el));

            Enabled = true;

            DateTimeFormatInfo dateTimeFormats = CultureInfo.CurrentCulture.DateTimeFormat;
            Format = dateTimeFormats.ShortDatePattern + " " + dateTimeFormats.ShortTimePattern;
            
            InputHtmlAttributes = new Dictionary<string, object>();
            DropDownHtmlAttributes = new Dictionary<string, object>();

            MinValue = defaultMinDate;
            MaxValue = defaultMaxDate;

            StartTime = DateTime.Today;
            EndTime = DateTime.Today;
            
            Interval = 30;

            CalendarButtonTitle = "Open the calendar";
            TimeButtonTitle = "Open the time view";
        }

        public DateTimePickerClientEvents ClientEvents { get; private set; }

        public Effects Effects { get; set; }

        public string Format { get; set; }

        public bool Enabled { get; set; }

        public IDictionary<string, object> InputHtmlAttributes { get; private set; }

        public IDictionary<string, object> DropDownHtmlAttributes { get; private set; }

        public DateTime? Value { get; set; }

        public DateTime MinValue { get; set; }

        public DateTime MaxValue { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Interval { get; set; }

        public string CalendarButtonTitle { get; set; }
        
        public string TimeButtonTitle { get; set; }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tDateTimePicker", writer);

            objectWriter.Start();
            
            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Append("format", this.Format);
            objectWriter.Append("minValue", this.MinValue);
            objectWriter.Append("maxValue", this.MaxValue);
            objectWriter.Append("startTimeValue", this.StartTime);
            objectWriter.Append("endTimeValue", this.EndTime);
            objectWriter.Append("interval", this.Interval);
            objectWriter.Append("selectedValue", this.Value);

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
            DateTimePickerHtmlBuilder renderer = new DateTimePickerHtmlBuilder(this);

            renderer.Build().WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
