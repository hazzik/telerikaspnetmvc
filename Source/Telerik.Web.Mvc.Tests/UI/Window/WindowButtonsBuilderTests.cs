namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;
    using Telerik.Web.Mvc.UI.Fluent;

    public class WindowButtonsBuilderTests
    {
        private readonly WindowButtonsBuilder builder;
        private IWindowButtonsContainer buttons;

        public WindowButtonsBuilderTests()
        {
            buttons = new WindowButtons();
            builder = new WindowButtonsBuilder(buttons);
        }

        [Fact]
        public void Close_method_should_add_close_button()
        {
            const string cssClass = "t-close";

            builder.Close();

            Assert.Equal(cssClass, buttons.Container[0].CssClass);
        }

        [Fact]
        public void Close_method_should_return_builder()
        {
            var returnedBuilder = builder.Close();

            Assert.IsType(typeof(WindowButtonsBuilder), returnedBuilder);
        }

        [Fact]
        public void Maximize_method_should_add_maximize_button()
        {
            const string cssClass = "t-maximize";

            builder.Maximize();

            Assert.Equal(cssClass, buttons.Container[0].CssClass);
        }

        [Fact]
        public void Maximize_method_should_return_builder()
        {
            var returnedBuilder = builder.Maximize();

            Assert.IsType(typeof(WindowButtonsBuilder), returnedBuilder);
        }

        [Fact]
        public void Refresh_method_should_add_refresh_button()
        {
            const string cssClass = "t-refresh";

            builder.Refresh();

            Assert.Equal(cssClass, buttons.Container[0].CssClass);
        }

        [Fact]
        public void Refresh_method_should_return_builder()
        {
            var returnedBuilder = builder.Refresh();

            Assert.IsType(typeof(WindowButtonsBuilder), returnedBuilder);
        }
    }
}
