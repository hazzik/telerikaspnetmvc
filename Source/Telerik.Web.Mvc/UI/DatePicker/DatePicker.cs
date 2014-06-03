﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Resources;
    using Telerik.Web.Mvc.UI.Html;
    using System.Collections.Generic;

    public class DatePicker : DatePickerBase
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        static internal DateTime defaultMinDate = new DateTime(1899, 12, 31);
        static internal DateTime defaultMaxDate = new DateTime(2100, 1, 1);

        public DatePicker(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.calendar.js", "telerik.datepicker.js" });

            defaultEffects.Each(el => Effects.Container.Add(el));

            Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            MinValue = defaultMinDate;
            MaxValue = defaultMaxDate;

            ButtonTitle = "Open the calendar";
            ShowButton = true;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public new string Id
        {
            get
            {
                // Return from htmlattributes if user has specified
                // otherwise build it from name
                return InputHtmlAttributes.ContainsKey("id") ?
                       InputHtmlAttributes["id"].ToString() :
                       (!string.IsNullOrEmpty(Name) ? Name.Replace(".", HtmlHelper.IdAttributeDotReplacement) : null);
            }
        }

        public bool ShowButton
        {
            get;
            set;
        }

        public string ButtonTitle
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

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Append("format", this.Format);
            objectWriter.AppendDateOnly("minValue", this.MinValue);
            objectWriter.AppendDateOnly("maxValue", this.MaxValue);
            objectWriter.AppendDateOnly("selectedValue", this.Value);
            objectWriter.Append("enabled", this.Enabled, true);
            objectWriter.Append("openOnFocus", this.OpenOnFocus, false);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            VerifySettings();

            DatePickerHtmlBuilder renderer = new DatePickerHtmlBuilder(this);

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

            if (MinValue > MaxValue)
            {
                throw new ArgumentException(TextResource.MinPropertyMustBeLessThenMaxProperty.FormatWith("MinValue", "MaxValue"));
            }

            if ((Value != null) && (MinValue > Value || Value > MaxValue))
            {
                throw new ArgumentOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("Date", "MinValue", "MaxValue"));
            }
        }
    }
}