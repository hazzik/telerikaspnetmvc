// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;

    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="TimePicker"/> component.
    /// </summary>
    public class DateTimePickerBuilder : ViewComponentBuilderBase<DateTimePicker, DateTimePickerBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePickerBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public DateTimePickerBuilder(DateTimePicker component)
            : base(component)
        {
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="configurator">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DateTimePicker()
        ///             .Name("DateTimePicker")
        ///             .ClientEvents(events =>
        ///                 events.OnLoad("onLoad").OnChange("onChange")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public DateTimePickerBuilder ClientEvents(Action<DateTimePickerClientEventsBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new DateTimePickerClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }

        //TODO: write tests for it
        /// <summary>
        /// Configures the effects of the dateTimePicker.
        /// </summary>
        /// <param name="configurator">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().DateTimePicker()
        ///	           .Name("DateTimePicker")
        ///	           .Effects(fx =>
        ///	           {
        ///		            fx.Height()
        ///			          .Opacity()
        ///					  .OpenDuration(AnimationDuration.Normal)
        ///					  .CloseDuration(AnimationDuration.Normal);
        ///	           })
        /// </code>
        /// </example>
        public DateTimePickerBuilder Effects(Action<EffectsBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            EffectsBuilderFactory factory = new EffectsBuilderFactory();

            configurator(factory.Create(Component.Effects));

            return this;
        }


        /// <summary>
        /// Sets the time format, which will be used to parse and format the <see cref="DateTime"/>.
        /// </summary>
        public DateTimePickerBuilder Format(string format)
        {
            Guard.IsNotNullOrEmpty(format, "format");

            Component.Format = format;

            return this;
        }

        /// <summary>
        /// Sets the Input HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        public DateTimePickerBuilder InputHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            Component.InputHtmlAttributes.Clear();
            Component.InputHtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// Sets whether dateTimePicker to be enabled.
        /// </summary>
        public DateTimePickerBuilder Enable(bool value)
        {
            Component.Enabled = value;

            return this;
        }

        /// <summary>
        /// Sets the value of the dateTimePicker input
        /// </summary>
        public DateTimePickerBuilder Value(DateTime? date)
        {
            Component.Value = date;

            return this;
        }

        /// <summary>
        /// Sets the value of the dateTimePicker input
        /// </summary>
        public DateTimePickerBuilder Value(string date)
        {
            DateTime parsedDate;

            if (DateTime.TryParse(date, out parsedDate))
            {
                Component.Value = parsedDate;
            }
            else
            {
                Component.Value = null;
            }
            return this;
        }

        /// <summary>
        /// Sets the minimal date, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder Min(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.MinValue = date;

            return this;
        }

        /// <summary>
        /// Sets the minimal date, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder Min(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            var date = DateTime.Parse(value);
            
            Component.MinValue = date;

            return this;
        }

        /// <summary>
        /// Sets the maximal date, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder Max(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.MaxValue = date;

            return this;
        }

        /// <summary>
        /// Sets the maximal date, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder Max(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            var date = DateTime.Parse(value);

            Component.MaxValue = date;

            return this;
        }

        /// <summary>
        /// Sets the minimal time, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder StartTime(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.StartTime = date;

            return this;
        }

        /// <summary>
        /// Sets the minimal time, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder StartTime(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            var date = DateTime.Parse(value);

            Component.StartTime = date;

            return this;
        }

        /// <summary>
        /// Sets the maximal time, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder EndTime(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.EndTime = date;

            return this;
        }

        /// <summary>
        /// Sets the maximal time, which can be selected in DateTimePicker.
        /// </summary>
        public DateTimePickerBuilder EndTime(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            var date = DateTime.Parse(value);

            Component.EndTime = date;

            return this;
        }
        
        /// <summary>
        /// Sets the interval between hours.
        /// </summary>
        public DateTimePickerBuilder Interval(int interval) 
        {
            Component.Interval = interval;

            return this;
        }

        /// <summary>
        /// Sets the title of the DateTimePicker button.
        /// </summary>
        public DateTimePickerBuilder CalendarButtonTitle(string title)
        {
            Guard.IsNotNullOrEmpty(title, "title");

            Component.CalendarButtonTitle = title;

            return this;
        }

        /// <summary>
        /// Sets the title of the DateTimePicker button.
        /// </summary>
        public DateTimePickerBuilder TimeButtonTitle(string title)
        {
            Guard.IsNotNullOrEmpty(title, "title");

            Component.TimeButtonTitle = title;

            return this;
        }
    }
}