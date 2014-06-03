// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class DatePickerBuilderTests
    {
        private readonly DatePicker _datePicker;
        private readonly DatePickerBuilder _builder;

        public DatePickerBuilderTests()
        {
            _datePicker = new DatePicker(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new DatePickerBuilder(_datePicker);
        }

        [Fact]
        public void Should_be_able_to_set_alternate_field()
        {
            _builder.AlternateField("hidDate");

            Assert.Equal("hidDate", _datePicker.AlternateField);
        }

        [Fact]
        public void Should_be_able_to_set_alternate_field_format()
        {
            _builder.AlternateFieldFormat("ddd MMM YYYY");

            Assert.Equal("ddd MMM YYYY", _datePicker.AlternateFieldFormat);
        }

        [Fact]
        public void Should_be_able_to_set_animation_with_numeric_value()
        {
            _builder.Animation("show", 700);

            Assert.Equal("show", _datePicker.AnimationName);
            Assert.Equal(700, _datePicker.AnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_animation_with_enum()
        {
            _builder.Animation("show", AnimationDuration.Normal);

            Assert.Equal((int)AnimationDuration.Normal, _datePicker.AnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_append_text()
        {
            _builder.AppendText("dd/MM/YYYY");

            Assert.Equal("dd/MM/YYYY", _datePicker.AppendText);
        }

        [Fact]
        public void Should_be_able_to_set_button_image_path()
        {
            _builder.ButtonImagePath("/images/calendarIcon.png");

            Assert.Equal("/images/calendarIcon.png", _datePicker.ButtonImagePath);
        }

        [Fact]
        public void Should_be_able_to_set_button_image_only()
        {
            _builder.ButtonImageOnly(true);

            Assert.True(_datePicker.ButtonImageOnly);
        }

        [Fact]
        public void Should_be_able_to_set_allow_month_change()
        {
            _builder.AllowMonthChange(false);

            Assert.False(_datePicker.AllowMonthChange);
        }

        [Fact]
        public void Should_be_able_to_set_allow_year_change()
        {
            _builder.AllowYearChange(false);

            Assert.False(_datePicker.AllowYearChange);
        }

        [Fact]
        public void Should_be_able_to_set_close_button_text()
        {
            _builder.CloseButtonText("Go");

            Assert.Equal("Go", _datePicker.CloseButtonText);
        }

        [Fact]
        public void Should_be_able_to_set_Current_Button_Text()
        {
            _builder.CurrentButtonText("Today");

            Assert.Equal("Today", _datePicker.CurrentButtonText);
        }

        [Fact]
        public void Should_be_able_to_set_go_to_current()
        {
            _builder.GoToCurrent(true);

            Assert.True(_datePicker.GoToCurrent);
        }

        [Fact]
        public void Should_be_able_to_set_constrain_input()
        {
            _builder.ConstrainInput(false);

            Assert.False(_datePicker.ConstrainInput);
        }

        [Fact]
        public void Should_be_able_to_set_date_format()
        {
            _builder.DateFormat("ddd MMM YYYY");

            Assert.Equal("ddd MMM YYYY", _datePicker.DateFormat);
        }

        [Fact]
        public void Should_be_able_to_set_value()
        {
            DateTime value = DateTime.Now;

            _builder.Value(value);

            Assert.Equal(value, _datePicker.Value);
        }

        [Fact]
        public void Should_be_able_to_set_minimum_value()
        {
            DateTime value = DateTime.Now.AddYears(-10);

            _builder.MinimumValue(value);

            Assert.Equal(value, _datePicker.MinimumValue);
        }

        [Fact]
        public void Should_be_able_to_set_maximum_value()
        {
            DateTime value = DateTime.Now.AddYears(10);

            _builder.MaximumValue(value);

            Assert.Equal(value, _datePicker.MaximumValue);
        }

        [Fact]
        public void Should_be_able_to_set_previous_text()
        {
            _builder.PreviousText("<");

            Assert.Equal("<", _datePicker.PreviousText);
        }

        [Fact]
        public void Should_be_able_to_set_next_text()
        {
            _builder.NextText(">");

            Assert.Equal(">", _datePicker.NextText);
        }

        [Fact]
        public void Should_be_able_to_set_hide_if_no_previous_or_next()
        {
            _builder.HideIfNoPreviousOrNext(true);

            Assert.True(_datePicker.HideIfNoPreviousOrNext);
        }

        [Fact]
        public void Should_be_able_to_set_navigation_as_dateformat()
        {
            _builder.NavigationAsDateFormat(true);

            Assert.True(_datePicker.NavigationAsDateFormat);
        }

        [Fact]
        public void Should_be_able_to_set_number_of_months_to_show()
        {
            _builder.NumberOfMonthsToShow(6);

            Assert.Equal(6, _datePicker.NumberOfMonthsToShow);
        }

        [Fact]
        public void Should_be_able_to_set_show_month_after_year()
        {
            _builder.ShowMonthAfterYear(true);

            Assert.True(_datePicker.ShowMonthAfterYear);
        }

        [Fact]
        public void Should_be_able_to_set_show_other_months()
        {
            _builder.ShowOtherMonths(true);

            Assert.True(_datePicker.ShowOtherMonths);
        }

        [Fact]
        public void Should_be_able_to_set_show_button_panel()
        {
            _builder.ShowButtonPanel(true);

            Assert.True(_datePicker.ShowButtonPanel);
        }

        [Fact]
        public void Should_be_able_to_set_show_on()
        {
            _builder.ShowOn(DatePickerShowOn.Button);

            Assert.Equal(DatePickerShowOn.Button, _datePicker.ShowOn);
        }

        [Fact]
        public void Should_be_abale_to_set_month_steps()
        {
            _builder.MonthSteps(3);

            Assert.Equal(3, _datePicker.MonthSteps);
        }

        [Fact]
        public void Should_be_able_to_set_localized()
        {
            _builder.Localized(true);

            Assert.True(_datePicker.Localized);
        }

        [Fact]
        public void Should_be_able_to_set_on_before_show()
        {
            Action onBeforeShow = delegate { };

            _builder.OnBeforeShow(onBeforeShow);

            Assert.Same(onBeforeShow, _datePicker.OnBeforeShow);
        }

        [Fact]
        public void Should_be_able_to_set_on_before_show_day()
        {
            Action onBeforeShowDay = delegate { };

            _builder.OnBeforeShowDay(onBeforeShowDay);

            Assert.Same(onBeforeShowDay, _datePicker.OnBeforeShowDay);
        }

        [Fact]
        public void Should_be_able_to_set_on_change_month_year()
        {
            Action onChangeMonthYear = delegate { };

            _builder.OnChangeMonthYear(onChangeMonthYear);

            Assert.Same(onChangeMonthYear, _datePicker.OnChangeMonthYear);
        }

        [Fact]
        public void Should_be_able_to_set_on_close()
        {
            Action onClose = delegate { };

            _builder.OnClose(onClose);

            Assert.Same(onClose, _datePicker.OnClose);
        }

        [Fact]
        public void Should_be_able_to_set_on_select()
        {
            Action onSelect = delegate { };

            _builder.OnSelect(onSelect);

            Assert.Same(onSelect, _datePicker.OnSelect);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _datePicker.Theme);
        }
    }
}