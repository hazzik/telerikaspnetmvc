// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for slider.
    /// </summary>
    public class SliderBuilder : ViewComponentBuilderBase<Slider, SliderBuilder>, IHideObjectMembers
    {
        public SliderBuilder(Slider component) : base(component)
        {
        }

        /// <summary>
        /// Specify Whether to slide handle smoothly when user click outside handle on the bar.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual SliderBuilder Animate(bool value)
        {
            Component.Animate = value;

            return this;
        }

        /// <summary>
        /// Specify the orientations of the slider.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Orientation(SliderOrientation value)
        {
            Component.Orientation = value;

            return this;
        }

        /// <summary>
        /// Specify the range to slide.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Range(SliderRange value)
        {
            Component.Range = value;

            return this;
        }

        /// <summary>
        /// Specified the slider value explicitly if there's only one handle.
        /// </summary>
        /// <param name="theValue">The explicit value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Value(int theValue)
        {
            Component.Value = theValue;

            return this;
        }

        /// <summary>
        /// Specified the slider value explicitly for multiple handle.
        /// </summary>
        /// <param name="value1">The first value of range.</param>
        /// <param name="value2">The second value for range.</param>
        /// <returns></returns>
        public virtual SliderBuilder Values(int value1, int value2)
        {
            Component.SetValues(value1, value2);

            return this;
        }

        /// <summary>
        /// If set, the current slider numeric value will be displayed in the specified elements
        /// </summary>
        /// <param name="selectors">The selectors.</param>
        /// <returns></returns>
        public virtual SliderBuilder UpdateElements(params string[] selectors)
        {
            Guard.IsNotNullOrEmpty(selectors, "selectors");

            if (Component.Range == SliderRange.True)
            {
                if ((selectors.Length % 2) != 0)
                {
                    throw new InvalidOperationException(Resources.TextResource.TheSelectorsShouldBePairedWhenRangeIsSetToTrue);
                }
            }

            Component.UpdateElements.Clear();
            Component.UpdateElements.AddRange(selectors);

            return this;
        }

        /// <summary>
        /// Specify the minimum value of the slider.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Minimum(int value)
        {
            Component.Minimum = value;

            return this;
        }

        /// <summary>
        /// Specify the maximum value of the slider.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Maximum(int value)
        {
            Component.Maximum = value;

            return this;
        }

        /// <summary>
        /// Specify the size or amount of each interval or step the slider takes between min and max. The full specified value range of the slider (max - min) needs to be evenly divisible by the step.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual SliderBuilder Steps(int value)
        {
            Component.Steps = value;

            return this;
        }

        /// <summary>
        /// This event is triggered when the user starts sliding.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual SliderBuilder OnStart(Action javaScript)
        {
            Component.OnStart = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered on every mouse move during slide.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual SliderBuilder OnSlide(Action javaScript)
        {
            Component.OnSlide = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered when the user stops sliding.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual SliderBuilder OnStop(Action javaScript)
        {
            Component.OnStop = javaScript;

            return this;
        }

        /// <summary>
        /// This event is triggered on slide stop, or if the value is changed programmatically. 
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual SliderBuilder OnChange(Action javaScript)
        {
            Component.OnChange = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme applies to the slider.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual SliderBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}