namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;


    public class DatePickerHtmlBuilderFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            DatePickerHtmlBuilderFactory factory = new DatePickerHtmlBuilderFactory();

            IDatePickerHtmlBuilder renderer = factory.Create(DatePickerTestHelper.CreateDatePicker(null, null));

            Assert.IsType<DatePickerHtmlBuilder>(renderer);
        }
    }
}
