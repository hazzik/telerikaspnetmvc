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
    public class TimePickerBuilder : ViewComponentBuilderBase<TimePicker, TimePickerBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePickerBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public TimePickerBuilder(TimePicker component)
            : base(component)
        {
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="configurator">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TimePicker()
        ///             .Name("TimePicker")
        ///             .ClientEvents(events =>
        ///                 events.OnLoad("onLoad").OnChange("onChange")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TimePickerBuilder ClientEvents(Action<TimePickerClientEventsBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new TimePickerClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }

        /// <summary>
        /// Configures the effects of the timepicker.
        /// </summary>
        /// <param name="condigurator">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().TimePicker()
        ///	           .Name("TimePicker")
        ///	           .Effects(fx =>
        ///	           {
        ///		            fx.Height()
        ///			          .Opacity()
        ///					  .OpenDuration(AnimationDuration.Normal)
        ///					  .CloseDuration(AnimationDuration.Normal);
        ///	           })
        /// </code>
        /// </example>
        public TimePickerBuilder Effects(Action<EffectsBuilder> condigurator)
        {
            Guard.IsNotNull(condigurator, "condigurator");

            var factory = new EffectsBuilderFactory();

            condigurator(factory.Create(Component.Effects));

            return this;
        }


        /// <summary>
        /// Sets the time format, which will be used to parse and format the <see cref="DateTime"/>.
        /// </summary>
        public TimePickerBuilder Format(string format)
        {
            Guard.IsNotNullOrEmpty(format, "format");

            Component.Format = format;

            return this;
        }

        /// <summary>
        /// Sets the Input HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        public TimePickerBuilder InputHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            Component.InputHtmlAttributes.Clear();
            Component.InputHtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// Sets whether timepicker to be enabled.
        /// </summary>
        public TimePickerBuilder Enable(bool value)
        {
            Component.Enabled = value;

            return this;
        }

        /// <summary>
        /// Sets the value of the timepicker input
        /// </summary>
        public TimePickerBuilder Value(DateTime? time)
        {
            Component.Value = time;

            return this;
        }

        /// <summary>
        /// Sets the value of the timepicker input
        /// </summary>
        public TimePickerBuilder Value(TimeSpan? time)
        {
            if (time.HasValue)
            {
                Component.Value = new DateTime(time.Value.Ticks);
            }
            else
            {
                Component.Value = null;
            }

            return this;
        }

        /// <summary>
        /// Sets the value of the timepicker input
        /// </summary>
        public TimePickerBuilder Value(string time)
        {
            DateTime result;

            if (DateTime.TryParse(time, out result))
            {
                Component.Value = result;
            }
            else
            {
                Component.Value = null;
            }

            return this;
        }

        /// <summary>
        /// Sets the minimum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Min(DateTime value)
        {
            Component.MinValue = value;

            return this;
        }

        /// <summary>
        /// Sets the minimum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Min(TimeSpan value)
        {
            Component.MinValue = new DateTime(value.Ticks);

            return this;
        }

        /// <summary>
        /// Sets the minimum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Min(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            var time = TimeSpan.Parse(value);
            
            Component.MinValue = new DateTime(time.Ticks);
            
            return this;
        }

        /// <summary>
        /// Sets the maximum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Max(DateTime value)
        {
            Component.MaxValue = value;

            return this;
        }

        /// <summary>
        /// Sets the maximum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Max(TimeSpan value)
        {
            Component.MaxValue = new DateTime(value.Ticks);

            return this;
        }

        /// <summary>
        /// Sets the maximum time, which can be selected in timepicker
        /// </summary>
        public TimePickerBuilder Max(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            TimeSpan time = TimeSpan.Parse(value);
            
            Component.MaxValue = new DateTime(time.Ticks);
            
            return this;
        }
        
        /// <summary>
        /// Sets the interval between hours.
        /// </summary>
        public TimePickerBuilder Interval(int interval) 
        {
            Component.Interval = interval;

            return this;
        }

        /// <summary>
        /// Sets whether timepicker to be rendered with button, which shows timeview on click.
        /// </summary>
        public TimePickerBuilder ShowButton(bool showButton)
        {
            Component.ShowButton = showButton;

            return this;
        }

        /// <summary>
        /// Sets the title of the timepicker button.
        /// </summary>
        public TimePickerBuilder ButtonTitle(string title)
        {
            Guard.IsNotNullOrEmpty(title, "title");

            Component.ButtonTitle = title;

            return this;
        }
    }
}