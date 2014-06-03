// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for date picker.
    /// </summary>
    public class DatePickerBuilder : ViewComponentBuilderBase<DatePicker, DatePickerBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatePickerBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public DatePickerBuilder(DatePicker component) : base(component)
        {
        }

        /// <summary>
        /// The jQuery selector for another field that is to be updated with the selected date of the datepicker. 
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder AlternateField(string name)
        {
            Component.AlternateField = name;

            return this;
        }

        /// <summary>
        /// This allows one date format to be shown to the user for selection purposes, while a different format is actually sent behind the scenes.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder AlternateFieldFormat(string format)
        {
            Component.AlternateFieldFormat = format;

            return this;
        }

        /// <summary>
        /// Configures the animation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder Animation(string name, int duration)
        {
            Component.AnimationName = name;
            Component.AnimationDuration = duration;

            return this;
        }

        /// <summary>
        /// Configures the animation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder Animation(string name, AnimationDuration duration)
        {
            return Animation(name, (int) duration);
        }

        /// <summary>
        /// The text to display after each date field, e.g. to show the required format.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder AppendText(string text)
        {
            Component.AppendText = text;

            return this;
        }

        /// <summary>
        /// The URL for the popup button image. If set, button text becomes the alt value and is not directly displayed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ButtonImagePath(string path)
        {
            Component.ButtonImagePath = path;

            return this;
        }

        /// <summary>
        /// Set to true to place an image after the field to use as the trigger without it appearing on a button.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ButtonImageOnly(bool value)
        {
            Component.ButtonImageOnly = value;

            return this;
        }

        /// <summary>
        /// Allows you to change the month by selecting from a drop-down list. You can enable this feature by setting the value to true.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder AllowMonthChange(bool value)
        {
            Component.AllowMonthChange = value;

            return this;
        }

        /// <summary>
        /// Allows you to change the year by selecting from a drop-down list. You can enable this feature by setting the value to true.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder AllowYearChange(bool value)
        {
            Component.AllowYearChange = value;

            return this;
        }

        /// <summary>
        /// The text to display for the close link.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder CloseButtonText(string text)
        {
            Component.CloseButtonText = text;

            return this;
        }

        /// <summary>
        /// The text to display for the current day link.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder CurrentButtonText(string text)
        {
            Component.CurrentButtonText = text;

            return this;
        }

        /// <summary>
        /// If true, the current day link moves to the currently selected date instead of today.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder GoToCurrent(bool value)
        {
            Component.GoToCurrent = value;

            return this;
        }

        /// <summary>
        /// True if the input field is constrained to the current date format.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ConstrainInput(bool value)
        {
            Component.ConstrainInput = value;

            return this;
        }

        /// <summary>
        /// The format for parsed and displayed dates.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder DateFormat(string format)
        {
            Component.DateFormat = format;

            return this;
        }

        /// <summary>
        /// Set the date to highlight on first opening if the field is blank.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder Value(DateTime theValue)
        {
            Component.Value = theValue;

            return this;
        }

        /// <summary>
        /// Set a minimum selectable date via a DateaTime object. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder MinimumValue(DateTime value)
        {
            Component.MinimumValue = value;

            return this;
        }

        /// <summary>
        /// Set a maximum selectable date via a DateaTime object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder MaximumValue(DateTime value)
        {
            Component.MaximumValue = value;

            return this;
        }

        /// <summary>
        /// The text to display for the previous month link.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder PreviousText(string text)
        {
            Component.PreviousText = text;

            return this;
        }

        /// <summary>
        /// The text to display for the next month link.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder NextText(string text)
        {
            Component.NextText = text;

            return this;
        }

        /// <summary>
        /// Normally the previous and next links are disabled when not applicable. You can hide them altogether by setting this attribute to true.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder HideIfNoPreviousOrNext(bool value)
        {
            Component.HideIfNoPreviousOrNext = value;

            return this;
        }

        /// <summary>
        /// When true the formatDate function is applied to the PrevText, NextText, and currentText values before display, allowing them to display the target month names for example.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder NavigationAsDateFormat(bool value)
        {
            Component.NavigationAsDateFormat = value;

            return this;
        }

        /// <summary>
        /// Set how many months to show at once.
        /// </summary>
        /// <param name="numberOfMonths">The number of months.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder NumberOfMonthsToShow(int numberOfMonths)
        {
            Component.NumberOfMonthsToShow = numberOfMonths;

            return this;
        }

        /// <summary>
        /// Whether to show the month after the year in the header.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ShowMonthAfterYear(bool value)
        {
            Component.ShowMonthAfterYear = value;

            return this;
        }

        /// <summary>
        /// Display dates in other months (non-selectable) at the start or end of the current month.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ShowOtherMonths(bool value)
        {
            Component.ShowOtherMonths = value;

            return this;
        }

        /// <summary>
        /// Whether to show the button panel.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ShowButtonPanel(bool value)
        {
            Component.ShowButtonPanel = value;

            return this;
        }

        /// <summary>
        /// Have the datepicker appear automatically when the field receives focus, appear only when a button is clicked, or appear when either event takes place.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder ShowOn(DatePickerShowOn value)
        {
            Component.ShowOn = value;

            return this;
        }

        /// <summary>
        /// Set how many months to move when clicking the Previous/Next links.
        /// </summary>
        /// <param name="steps">The steps.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder MonthSteps(int steps)
        {
            Component.MonthSteps = steps;

            return this;
        }

        /// <summary>
        /// Localize the DatePicker.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual DatePickerBuilder Localized(bool value)
        {
            Component.Localized = value;

            return this;
        }

        /// <summary>
        /// Can be a function that takes an input field and current datepicker instance and returns an options object to update the datepicker with. It is called just before the datepicker is displayed.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder OnBeforeShow(Action javaScript)
        {
            Component.OnBeforeShow = javaScript;

            return this;
        }

        /// <summary>
        /// Supply a callback function to handle the beforeShowDay event as an init option. 
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder OnBeforeShowDay(Action javaScript)
        {
            Component.OnBeforeShowDay = javaScript;

            return this;
        }

        /// <summary>
        /// Supply a callback function to handle the onChangeMonthYear event as an init option.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder OnChangeMonthYear(Action javaScript)
        {
            Component.OnChangeMonthYear = javaScript;

            return this;
        }

        /// <summary>
        /// Supply a callback function to define your own event when the datepicker is closed, whether or not a date is selected.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder OnClose(Action javaScript)
        {
            Component.OnClose = javaScript;

            return this;
        }

        /// <summary>
        /// Supply a callback function to define your own event when the datepicker is selected.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder OnSelect(Action javaScript)
        {
            Component.OnSelect = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme apply to the date picker.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual DatePickerBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}