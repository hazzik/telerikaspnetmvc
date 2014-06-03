// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class DatePickerTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly DatePicker _datePicker;

        public DatePickerTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _datePicker = new DatePicker(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void ConstrainInput_should_be_true_when_new_instance_is_created()
        {
            Assert.True(_datePicker.ConstrainInput);
        }

        [Fact]
        public void NumberOfMonthsToShow_should_be_one_when_new_instance_is_created()
        {
            Assert.Equal(1, _datePicker.NumberOfMonthsToShow);
        }

        [Fact]
        public void MonthSteps_should_be_one_when_new_instance_is_created()
        {
            Assert.Equal(1, _datePicker.MonthSteps);
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts()
        {
            _datePicker.Name = "myDatePicker";
            _datePicker.Value = DateTime.Today;
            _datePicker.AnimationDuration = (int) AnimationDuration.Slow;
            _datePicker.ShowButtonPanel = true;
            _datePicker.ShowOn = DatePickerShowOn.Both;
            _datePicker.Localized = true;
            _datePicker.OnBeforeShow = delegate { };

            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<DateTime?>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<IList<string>>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<DatePickerShowOn>(), It.IsAny<DatePickerShowOn>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);

            _datePicker.WriteInitializationScript(new Mock<TextWriter>().Object);

            writer.VerifyAll();
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_four_y_with_two_y()
        {
            string result = DatePicker.ConvertToClientFormatString("mm/dd/yyyy");

            Assert.Equal("mm/dd/yy", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_two_y_with_one_y()
        {
            string result = DatePicker.ConvertToClientFormatString("mm/dd/yy");

            Assert.Equal("mm/dd/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_four_capital_M_with_two_capital_M()
        {
            string result = DatePicker.ConvertToClientFormatString("MMMM/dd/y");

            Assert.Equal("MM/dd/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_three_capital_M_with_one_capital_M()
        {
            string result = DatePicker.ConvertToClientFormatString("MMM/dd/y");

            Assert.Equal("M/dd/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_two_capital_M_with_two_small_M()
        {
            string result = DatePicker.ConvertToClientFormatString("MM/dd/y");

            Assert.Equal("mm/dd/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_one_capital_M_with_one_small_M()
        {
            string result = DatePicker.ConvertToClientFormatString("M/dd/y");

            Assert.Equal("m/dd/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_four_capital_D_with_two_capital_D()
        {
            string result = DatePicker.ConvertToClientFormatString("mm/DDDD/y");

            Assert.Equal("mm/DD/y", result);
        }

        [Fact]
        public void ConvertToClientFormatString_should_replace_three_capital_D_with_one_capital_D()
        {
            string result = DatePicker.ConvertToClientFormatString("mm/DDD/y");

            Assert.Equal("mm/D/y", result);
        }

        [Fact]
        public void GetValueInternal_should_return_correct_value_when_value_is_retrieved_from_model_state()
        {
            DateTime today = DateTime.Today;
            string todayAsString = today.ToShortDateString();

            _viewContext.ViewData.ModelState.Add("myDatePicker", new ModelState { Value = new ValueProviderResult(todayAsString, todayAsString, Culture.Current) });

            _datePicker.Name = "myDatePicker";

            DateTime? value = _datePicker.GetValueInternal();

            Assert.Equal(today, value.Value);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_two_y_with_four_y()
        {
            string result = DatePicker.ConvertToServerFormatString("m/dd/yy");

            Assert.Equal("M/dd/yyyy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_one_y_with_two_y()
        {
            string result = DatePicker.ConvertToServerFormatString("m/dd/y");

            Assert.Equal("M/dd/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_two_capital_M_with_four_capital_M()
        {
            string result = DatePicker.ConvertToServerFormatString("MM/dd/y");

            Assert.Equal("MMMM/dd/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_one_capital_M_with_three_capital_M()
        {
            string result = DatePicker.ConvertToServerFormatString("M/dd/y");

            Assert.Equal("MMM/dd/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_two_small_m_with_two_capital_M()
        {
            string result = DatePicker.ConvertToServerFormatString("mm/dd/y");

            Assert.Equal("MM/dd/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_one_small_m_with_one_capital_M()
        {
            string result = DatePicker.ConvertToServerFormatString("m/dd/y");

            Assert.Equal("M/dd/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_two_capital_D_with_four_capital_D()
        {
            string result = DatePicker.ConvertToServerFormatString("m/DD/y");

            Assert.Equal("M/DDDD/yy", result);
        }

        [Fact]
        public void ConvertToServerFormatString_should_replace_one_capital_D_with_three_capital_D()
        {
            string result = DatePicker.ConvertToServerFormatString("m/D/y");

            Assert.Equal("M/DDD/yy", result);
        }

        [Fact]
        public void GetValueInternal_should_return_correct_value_when_value_is_retrieved_from_view_data()
        {
            DateTime today = DateTime.Today;

            _viewContext.ViewData["myDatePicker"] = today;

            _datePicker.Name = "myDatePicker";

            DateTime? value = _datePicker.GetValueInternal();

            Assert.Equal(today, value.Value);
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _datePicker.Name = "myDatePicker";
            _datePicker.Theme = "custom";
            _datePicker.DateFormat = "mm/dd/yy";

            _viewContext.ViewData.ModelState[_datePicker.Name] = new ModelState{ Value = new ValueProviderResult(DateTime.Today, DateTime.Today.ToShortDateString(), Culture.Current) };
            _viewContext.ViewData.ModelState.AddModelError(_datePicker.Name, "Error");

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _datePicker.Render();

            _httpContext.Verify();
        }
    }
}