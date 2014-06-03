// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using System.Web.UI;

    /// <summary>
    /// Displays a slider in an ASP.NET MVC view.
    /// </summary>
    public class Slider : jQueryViewComponentBase
    {
        private int? sliderValue;
        private int[] sliderValues;
        private int? minimum;
        private int? maximum;
        private int? steps;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public Slider(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Orientation = SliderOrientation.Default;
            Range = SliderRange.False;
            UpdateElements = new List<string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Slider"/> is animate.
        /// </summary>
        /// <value><c>true</c> if animate; otherwise, <c>false</c>.</value>
        public bool Animate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public SliderOrientation Orientation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>The range.</value>
        public SliderRange Range
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            [DebuggerStepThrough]
            get
            {
                return sliderValue.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                if (Range == SliderRange.True)
                {
                    throw new InvalidOperationException(Resources.TextResource.ValueIsNotSupportedWhenRangeIsSetToTrue);
                }

                sliderValue = value;
                sliderValues = null;
            }
        }

        /// <summary>
        /// Gets or sets the update elements.
        /// </summary>
        /// <value>The update elements.</value>
        public IList<string> UpdateElements
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            [DebuggerStepThrough]
            get
            {
                return minimum.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                minimum = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            [DebuggerStepThrough]
            get
            {
                return maximum.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>The steps.</value>
        public int Steps
        {
            [DebuggerStepThrough]
            get
            {
                return steps.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");

                steps = value;
            }
        }

        /// <summary>
        /// Gets or sets the on start.
        /// </summary>
        /// <value>The on start.</value>
        public Action OnStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on slide.
        /// </summary>
        /// <value>The on slide.</value>
        public Action OnSlide
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on stop.
        /// </summary>
        /// <value>The on stop.</value>
        public Action OnStop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on change.
        /// </summary>
        /// <value>The on change.</value>
        public Action OnChange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns></returns>
        public int[] GetValues()
        {
            return sliderValues;
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public void SetValues(int value1, int value2)
        {
            if (Range != SliderRange.True)
            {
                throw new InvalidOperationException(Resources.TextResource.ValuesIsOnlySupportedWhenRangeIsSetToTrue);
            }

            sliderValues = new[] { value1, value2 };
            sliderValue = null;
        }

        /// <summary>
        /// Writes the initialization script.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void WriteInitializationScript(TextWriter writer)
        {
            string id = Id;

            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(id, "slider", writer);

            objectWriter.Start()
                        .Append("animate", Animate, false)
                        .Append("orientation", Orientation, SliderOrientation.Default)
                        .Append("range", Range, SliderRange.False);

            if (Range == SliderRange.True)
            {
                objectWriter.Append("values", GetValuesInternal());
            }
            else
            {
                objectWriter.Append("value", GetValueInternal());
            }

            objectWriter.Append("min", minimum)
                        .Append("max", maximum)
                        .Append("step", steps)
                        .Append("start", OnStart)
                        .Append("slide", OnSlide)
                        .Append("stop", OnStop)
                        .Append("change", OnChange)
                        .Complete();

            StringBuilder scriptBuilder = new StringBuilder();

            string updateScript;

            if (Range == SliderRange.True)
            {
                string hidId = id +
                               HtmlHelper.IdAttributeDotReplacement +
                               HiddenInputSuffix +
                               HtmlHelper.IdAttributeDotReplacement;

                updateScript = "function(e,ui){jQuery('#" + hidId + "1').val(ui.values[0]);jQuery('#" + hidId + "2').val(ui.values[1]);}";
            }
            else
            {
                updateScript = "function(e,ui){jQuery('#" + id + HtmlHelper.IdAttributeDotReplacement + HiddenInputSuffix + "').val(ui.value);}";
            }

            scriptBuilder.AppendLine();
            scriptBuilder.Append("jQuery('#{0}').bind('slide',{1}).bind('slidechange',{1});".FormatWith(id, updateScript));

            if (!UpdateElements.IsNullOrEmpty())
            {
                Action<string, string> appendEvent = (eventName, actions) =>
                                                     {
                                                         scriptBuilder.AppendLine();
                                                         scriptBuilder.Append("jQuery('#{0}')".FormatWith(id));
                                                         scriptBuilder.Append(".bind('{0}',function(e,ui)".FormatWith(eventName));
                                                         scriptBuilder.Append("{");
                                                         scriptBuilder.Append(actions);
                                                         scriptBuilder.Append("});");
                                                     };

                StringBuilder eventBuilder = new StringBuilder();
                StringBuilder updateBuilder = new StringBuilder();

                if (Range == SliderRange.True)
                {
                    for (int i = 0; i < UpdateElements.Count; i += 2)
                    {
                        string selector1 = UpdateElements[i];
                        string selector2 = UpdateElements[i + 1];

                        eventBuilder.Append("jQuery('{0}').text(ui.values[0]);jQuery('{1}').text(ui.values[1]);".FormatWith(selector1, selector2));
                        updateBuilder.Append("jQuery('{0}').text(jQuery('#{2}').slider('values',0));jQuery('{1}').text(jQuery('#{2}').slider('values',1));".FormatWith(selector1, selector2, id));
                    }
                }
                else
                {
                    foreach (string selector in UpdateElements)
                    {
                        eventBuilder.Append("jQuery('{0}').text(ui.value);".FormatWith(selector, id));
                        updateBuilder.Append("jQuery('{0}').text(jQuery('#{1}').slider('value'));".FormatWith(selector, id));
                    }
                }

                if (eventBuilder.Length > 0)
                {
                    appendEvent("slide", eventBuilder.ToString());
                    appendEvent("slidechange", eventBuilder.ToString());
                }

                if (updateBuilder.Length > 0)
                {
                    scriptBuilder.AppendLine();
                    scriptBuilder.Append(updateBuilder);
                }
            }

            writer.Write(scriptBuilder.ToString());

            base.WriteInitializationScript(writer);
        }

        // Marked as internal for unit test
        internal int? GetValueInternal()
        {
            object valueAsObject = ViewContext.ViewData.GetConvertedModelStateValue(Name, typeof(int?));

            int? value = (valueAsObject != null) ? (int?)valueAsObject : sliderValue;

            if (!value.HasValue)
            {
                object viewDataValue = ViewContext.ViewData.Eval(Name);

                if (viewDataValue != null)
                {
                    value = (int?)viewDataValue;
                }
            }

            return value;
        }

        // Marked as internal for unit test
        internal IList<int> GetValuesInternal()
        {
            object objectValues = ViewContext.ViewData.GetConvertedModelStateValue(Name, typeof(int[]));
            IList<int> values = null;

            if (objectValues != null)
            {
                int[] intValues = objectValues as int[];

                if ((intValues != null) && (intValues.Length >= 2))
                {
                    values = new List<int> { intValues[0], intValues[1] };
                }
            }

            if ((values == null) && (sliderValues != null))
            {
                values = new List<int>(sliderValues);
            }

            if (values == null)
            {
                int[] intValues = ViewContext.ViewData.Eval(Name) as int[];

                if ((intValues != null) && (intValues.Length >= 2))
                {
                    values = new List<int> { intValues[0], intValues[1] };
                }
            }

            return values;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Func<string, string, string> buildHiddenInput = (hiddenId, hiddenValue) => "<input id=\"{0}\" name=\"{1}\" type=\"hidden\" value=\"{2}\"/>".FormatWith(hiddenId, Name, hiddenValue);

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            string id = Id;

            if (Range == SliderRange.True)
            {
                IList<int> values = GetValues();
                int value1 = 0;
                int value2 = 0;

                if (!values.IsNullOrEmpty())
                {
                    value1 = values[0];

                    if (values.Count > 0)
                    {
                        value2 = values[1];
                    }
                }

                string hidId = id +
                               HtmlHelper.IdAttributeDotReplacement +
                               HiddenInputSuffix +
                               HtmlHelper.IdAttributeDotReplacement;

                writer.Write(buildHiddenInput(hidId + "1", value1.ToString(Culture.Current)));
                writer.Write(buildHiddenInput(hidId + "2", value2.ToString(Culture.Current)));
            }
            else
            {
                int value = GetValueInternal() ?? 0;

                writer.Write(buildHiddenInput(id + HtmlHelper.IdAttributeDotReplacement + HiddenInputSuffix, value.ToString(Culture.Current)));
            }

            HtmlAttributes.Merge("id", id, false);
            writer.Write("<div{0}></div>".FormatWith(HtmlAttributes.ToAttributeString()));
        }
    }
}