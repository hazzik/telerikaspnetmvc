namespace Telerik.Web.Mvc.UI.Tests
{
    using System;
    using System.Globalization;
    using Xunit;
    using Telerik.Web.Mvc.UI.Html;

    public class DateTimePickerHtmlBuilderTests
    {
        private IDateTimePickerHtmlBuilder renderer;
        private DateTimePicker dateTimePicker;
        private DateTime date;

        public DateTimePickerHtmlBuilderTests()
        {
            date = new DateTime(2009, 12, 3);

            dateTimePicker = DateTimePickerTestHelper.CreateDateTimePicker(null, null);
            dateTimePicker.Name = "DatePicker";
            renderer = new DateTimePickerHtmlBuilder(dateTimePicker);
        }

        [Fact]
        public void Input_should_render_input_control()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Contains(UIPrimitives.Input, tag.Attribute("class"));
            Assert.Equal("input", tag.TagName);
        }

        [Fact]
        public void Input_should_render_id_and_name()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(dateTimePicker.Id, tag.Attribute("id"));
            Assert.Equal(dateTimePicker.Name, tag.Attribute("name"));
        }

        [Fact]
        public void Input_should_render_type_text_attribute()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Equal("text", tag.Attribute("type"));
        }

        [Fact]
        public void Input_should_render_html_attributes()
        {
            dateTimePicker.InputHtmlAttributes.Add("class", "t-test");

            IHtmlNode tag = renderer.InputTag();
            
            Assert.Equal(UIPrimitives.Input + " t-test", tag.Attribute("class"));
        }

    }
}