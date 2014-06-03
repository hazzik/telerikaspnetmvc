// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Resources;

    public class DatePicker : ViewComponentBase, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        private readonly IDatePickerHtmlBuilderFactory rendererFactory;

        static internal DateTime defaultMinDate = new DateTime(1899, 12, 31);
        static internal DateTime defaultMaxDate = new DateTime(2100, 1, 1);

        public DatePicker(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IDatePickerHtmlBuilderFactory rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.calendar.js", "telerik.datepicker.js" });

            InputHtmlAttributes = new RouteValueDictionary();

            ClientEvents = new DatePickerClientEvents();
            this.Effects = new Effects();

            defaultEffects.Each(el => Effects.Container.Add(el));

            Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            this.rendererFactory = rendererFactory;

            Min = defaultMinDate;
            Max = defaultMaxDate;
            Value = null;

            EnableButton = true;
            Enabled = true;
        }

        public IDictionary<string, object> InputHtmlAttributes
        {
            get;
            set;
        }

        public Effects Effects
        {
            get;
            set;
        }

        public DatePickerClientEvents ClientEvents
        {
            get;
            private set;
        }

        public bool EnableButton
        {
            get;
            set;
        }

        public string ButtonTitle
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        public DateTime? Value
        {
            get;
            set;
        }

        public DateTime Min
        {
            get;
            set;
        }

        public DateTime Max
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tDatePicker", writer);

            objectWriter.Start();

            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            objectWriter.Append("format", this.Format);
            objectWriter.AppendDateOnly("selectedValue", this.Value);
            objectWriter.AppendDateOnly("minDate", this.Min);
            objectWriter.AppendDateOnly("maxDate", this.Max);
            objectWriter.Append("enabled", this.Enabled, true);
             
            objectWriter.AppendClientEvent("onLoad", ClientEvents.OnLoad);
            objectWriter.AppendClientEvent("onChange", ClientEvents.OnChange);
            objectWriter.AppendClientEvent("onOpen", ClientEvents.OnOpen);
            objectWriter.AppendClientEvent("onClose", ClientEvents.OnClose);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            VerifySettings();

            IDatePickerHtmlBuilder renderer = rendererFactory.Create(this);

            IHtmlNode rootTag = renderer.Build();

            rootTag.WriteTo(writer);
            base.WriteHtml(writer);
        }

        public override void VerifySettings()
        {
#if MVC2 || MVC3
            Name = Name ?? ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty);
#endif
            base.VerifySettings();

            if (Min > Max)
            {
                throw new ArgumentException(TextResource.MinDateShouldBeLessThanMaxDate);
            }

            if ((Value != null) && (Min > Value || Value > Max))
            {
                throw new ArgumentOutOfRangeException(TextResource.DateOutOfRange);
            }
        }
    }
}