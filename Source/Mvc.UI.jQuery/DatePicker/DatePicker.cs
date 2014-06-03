// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using System.Web.UI;

    /// <summary>
    /// Displays a date picker in an ASP.NET MVC view.
    /// </summary>
    public class DatePicker : jQueryViewComponentBase
    {
        private const int DefaultNumberOfMonthsToShow = 1;
        private const int DefaultMonthSteps = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatePicker"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public DatePicker(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            ConstrainInput = true;
            NumberOfMonthsToShow = DefaultNumberOfMonthsToShow;
            MonthSteps = DefaultMonthSteps;
        }

        /// <summary>
        /// Gets or sets the alternate field.
        /// </summary>
        /// <value>The alternate field.</value>
        public string AlternateField
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the alternate field format.
        /// </summary>
        /// <value>The alternate field format.</value>
        public string AlternateFieldFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the append text.
        /// </summary>
        /// <value>The append text.</value>
        public string AppendText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        /// <value>The button text.</value>
        public string ButtonText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the button image path.
        /// </summary>
        /// <value>The button image path.</value>
        public string ButtonImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to button image only.
        /// </summary>
        /// <value><c>true</c> if [button image only]; otherwise, <c>false</c>.</value>
        public bool ButtonImageOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow month change.
        /// </summary>
        /// <value><c>true</c> if [allow month change]; otherwise, <c>false</c>.</value>
        public bool AllowMonthChange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow year change.
        /// </summary>
        /// <value><c>true</c> if [allow year change]; otherwise, <c>false</c>.</value>
        public bool AllowYearChange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the close button text.
        /// </summary>
        /// <value>The close button text.</value>
        public string CloseButtonText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current button text.
        /// </summary>
        /// <value>The current button text.</value>
        public string CurrentButtonText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to go to current.
        /// </summary>
        /// <value><c>true</c> if [go to current]; otherwise, <c>false</c>.</value>
        public bool GoToCurrent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to constrain input.
        /// </summary>
        /// <value><c>true</c> if [constrain input]; otherwise, <c>false</c>.</value>
        public bool ConstrainInput
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>The date format.</value>
        public string DateFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public DateTime? Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public DateTime? MaximumValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public DateTime? MinimumValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the animation.
        /// </summary>
        /// <value>The name of the animation.</value>
        public string AnimationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the duration of the animation.
        /// </summary>
        /// <value>The duration of the animation.</value>
        public int? AnimationDuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the previous text.
        /// </summary>
        /// <value>The previous text.</value>
        public string PreviousText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the next text.
        /// </summary>
        /// <value>The next text.</value>
        public string NextText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide if no previous or next.
        /// </summary>
        /// <value>
        /// <c>true</c> if [hide if no previous or next]; otherwise, <c>false</c>.
        /// </value>
        public bool HideIfNoPreviousOrNext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to navigation as date format.
        /// </summary>
        /// <value>
        /// <c>true</c> if [navigation as date format]; otherwise, <c>false</c>.
        /// </value>
        public bool NavigationAsDateFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of months to show.
        /// </summary>
        /// <value>The number of months to show.</value>
        public int NumberOfMonthsToShow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show month after year.
        /// </summary>
        /// <value><c>true</c> if [show month after year]; otherwise, <c>false</c>.</value>
        public bool ShowMonthAfterYear
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show other months.
        /// </summary>
        /// <value><c>true</c> if [show other months]; otherwise, <c>false</c>.</value>
        public bool ShowOtherMonths
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show button panel.
        /// </summary>
        /// <value><c>true</c> if [show button panel]; otherwise, <c>false</c>.</value>
        public bool ShowButtonPanel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the show on.
        /// </summary>
        /// <value>The show on.</value>
        public DatePickerShowOn ShowOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the month steps.
        /// </summary>
        /// <value>The month steps.</value>
        public int MonthSteps
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DatePicker"/> is localized.
        /// </summary>
        /// <value><c>true</c> if localized; otherwise, <c>false</c>.</value>
        public bool Localized
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on before show.
        /// </summary>
        /// <value>The on before show.</value>
        public Action OnBeforeShow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on before show day.
        /// </summary>
        /// <value>The on before show day.</value>
        public Action OnBeforeShowDay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on change month year.
        /// </summary>
        /// <value>The on change month year.</value>
        public Action OnChangeMonthYear
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on close.
        /// </summary>
        /// <value>The on close.</value>
        public Action OnClose
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on select.
        /// </summary>
        /// <value>The on select.</value>
        public Action OnSelect
        {
            get;
            set;
        }

        /// <summary>
        /// Writes the initialization script.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "datepicker", writer);

            objectWriter.Start()
                        .Append("altField", AlternateField)
                        .Append("altFormat", AlternateFieldFormat)
                        .Append("appendText", AppendText)
                        .Append("changeMonth", AllowMonthChange, false)
                        .Append("changeYear", AllowYearChange, false)
                        .Append("constrainInput", ConstrainInput, true)
                        .Append("gotoCurrent", GoToCurrent, false)
                        .Append("dateFormat", DateFormat)
                        .Append("defaultDate", Value)
                        .Append("maxDate", MaximumValue)
                        .Append("minDate", MinimumValue)
                        .Append("nextText", NextText)
                        .Append("prevText", PreviousText)
                        .Append("hideIfNoPrevNext", HideIfNoPreviousOrNext, false)
                        .Append("navigationAsDateFormat", NavigationAsDateFormat, false)
                        .Append("numberOfMonths", NumberOfMonthsToShow, DefaultNumberOfMonthsToShow)
                        .Append("showMonthAfterYear", ShowMonthAfterYear, false)
                        .Append("showOtherMonths", ShowMonthAfterYear, false)
                        .Append("showButtonPanel", ShowButtonPanel, false)
                        .Append("showOn", ShowOn, DatePickerShowOn.Focus)
                        .Append("stepMonths", MonthSteps, DefaultMonthSteps)
                        .Append("showAnim", AnimationName);

            if (AnimationDuration.HasValue)
            {
                objectWriter.Append("duration:{0}".FormatWith(AnimationDurationConverter.ToString(AnimationDuration.Value)));
            }

            if (ShowOn != DatePickerShowOn.Focus)
            {
                objectWriter.Append("buttonText", ButtonText)
                            .Append("buttonImage", ButtonImagePath)
                            .Append("buttonImageOnly", ButtonImageOnly, false);
            }

            if (ShowButtonPanel)
            {
                objectWriter.Append("closeText", CloseButtonText)
                            .Append("currentText", CurrentButtonText);
            }

            if (Localized)
            {
                DateTimeFormatInfo cultureInfo = Culture.Current.DateTimeFormat;

                IList<string> dayNames = new List<string>(cultureInfo.DayNames);
                IList<string> dayNamesMin = new List<string>(cultureInfo.ShortestDayNames);
                IList<string> dayNamesShort = new List<string>(cultureInfo.AbbreviatedDayNames);

                // Invariant returns one empty element at last
                IList<string> monthNames = new List<string>(cultureInfo.MonthNames.Take(12));
                IList<string> monthNamesShort = new List<string>(cultureInfo.AbbreviatedMonthNames.Take(12));

                objectWriter.Append("dayNames", dayNames)
                            .Append("dayNamesMin", dayNamesMin)
                            .Append("dayNamesShort", dayNamesShort)
                            .Append("monthNames", monthNames)
                            .Append("monthNamesShort", monthNamesShort)
                            .Append("firstDay", (int)cultureInfo.FirstDayOfWeek, 0)
                            .Append("isRTL", Culture.Current.TextInfo.IsRightToLeft, false);

                if (string.IsNullOrEmpty(DateFormat))
                {
                    string format = ConvertToClientFormatString(cultureInfo.ShortDatePattern);

                    objectWriter.Append("dateFormat", format);
                }
            }

            objectWriter.Append("beforeShow", OnBeforeShow)
                        .Append("beforeShowDay", OnBeforeShowDay)
                        .Append("onChangeMonthYear", OnChangeMonthYear)
                        .Append("onChangeMonthYear", OnChangeMonthYear)
                        .Append("onClose", OnClose)
                        .Append("onSelect", OnSelect)
                        .Complete();

            base.WriteInitializationScript(writer);
        }

        // Marked as Internal for Unit Test
        internal static string ConvertToClientFormatString(string serverFormatString)
        {
            string result = serverFormatString;

            // Repalce year
            if (result.IndexOf("yyyy", StringComparison.OrdinalIgnoreCase) > -1)
            {
                result = result.Replace("yyyy", "yy");
            }
            else if (result.IndexOf("yy", StringComparison.OrdinalIgnoreCase) > -1)
            {
                result = result.Replace("yy", "y");
            }

            // Replace Month
            if (result.IndexOf("MMMM", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("MMMM", "MM");
            }
            else if (result.IndexOf("MMM", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("MMM", "M");
            }
            else if (result.IndexOf("MM", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("MM", "mm");
            }
            else if (result.IndexOf("M", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("M", "m");
            }

            // Repalce Day
            if (result.IndexOf("DDDD", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("DDDD", "DD");
            }
            else if (result.IndexOf("DDD", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("DDD", "D");
            }

            return result;
        }

        // Marked as Internal for Unit Test
        internal static string ConvertToServerFormatString(string clientFormatString)
        {
            string result = clientFormatString;

            // Repalce year
            if (result.IndexOf("yy", StringComparison.OrdinalIgnoreCase) > -1)
            {
                result = result.Replace("yy", "yyyy");
            }
            else if (result.IndexOf("y", StringComparison.OrdinalIgnoreCase) > -1)
            {
                result = result.Replace("y", "yy");
            }

            // Replace Month
            if (result.IndexOf("MM", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("MM", "MMMM");
            }
            else if (result.IndexOf("M", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("M", "MMM");
            }
            else if (result.IndexOf("mm", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("mm", "MM");
            }
            else if (result.IndexOf("m", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("m", "M");
            }

            // Repalce Day
            if (result.IndexOf("DD", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("DD", "DDDD");
            }
            else if (result.IndexOf("D", StringComparison.Ordinal) > -1)
            {
                result = result.Replace("D", "DDD");
            }

            return result;
        }

        // Marked as Internal for Unit Test
        internal DateTime? GetValueInternal()
        {
            object valueAsObject = ViewContext.ViewData.GetConvertedModelStateValue(Name, typeof(DateTime?));

            DateTime? value = (valueAsObject != null) ? (DateTime?) valueAsObject : Value;

            if (!value.HasValue)
            {
                object viewDataValue = ViewContext.ViewData.Eval(Name);

                if (viewDataValue != null)
                {
                    value = (DateTime?) viewDataValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            string dateFormat = !string.IsNullOrEmpty(DateFormat) ? 
                                ConvertToServerFormatString(DateFormat) :
                                (Localized ? Culture.Current.DateTimeFormat.ShortDatePattern : "MM/dd/yyyy");

            DateTime? value = GetValueInternal();
            string valueAsString = null;

            if (value.HasValue)
            {
                valueAsString = value.Value.ToString(dateFormat, Culture.Current);
            }

            TagBuilder tagBuilder = new TagBuilder("input");

            tagBuilder.MergeAttributes(HtmlAttributes);

            if (ViewContext.ViewData.HasModeStateError(Name))
            {
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", Name, true);
            tagBuilder.GenerateId(Name);

            if (!string.IsNullOrEmpty(valueAsString))
            {
                tagBuilder.MergeAttribute("value", valueAsString, true);
            }

            writer.Write(tagBuilder.ToString(TagRenderMode.SelfClosing));

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("</div>");
            }
        }
    }
}