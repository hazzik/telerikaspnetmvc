namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;

    using Xunit;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class ComboBoxBindingSettingsBuilderTests
    {
        private readonly AutoCompleteBindingSettings settings;
        private readonly AutoCompleteBindingSettingsBuilder builder;

        public ComboBoxBindingSettingsBuilderTests()
        {
            settings = new AutoCompleteBindingSettings();
            builder = new AutoCompleteBindingSettingsBuilder(settings);
        }

        [Fact]
        public void Builder_should_set_Enabled_property()
        {
            const bool enabled = true;

            builder.Enabled(enabled);
            Assert.Equal(enabled, settings.Enabled);
        }

        [Fact]
        public void Enabled_method_should_return_builder()
        {
            var sameBuilder = builder.Enabled(false);
            Assert.IsType(typeof(AutoCompleteBindingSettingsBuilder), sameBuilder);
        }


        [Fact]
        public void Builder_should_set_Cache_property()
        {
            const bool cached = true;

            builder.Cache(cached);
            Assert.Equal(cached, settings.Cache);
        }

        [Fact]
        public void Cache_method_should_return_builder()
        {
            var sameBuilder = builder.Cache(false);
            Assert.IsType(typeof(AutoCompleteBindingSettingsBuilder), sameBuilder);
        }

        [Fact]
        public void Builder_should_set_Delay_property()
        {
            const int delay = 400;

            builder.Delay(delay);
            Assert.Equal(delay, settings.Delay);
        }

        [Fact]
        public void Delay_method_should_return_builder()
        {
            var sameBuilder = builder.Delay(400);
            Assert.IsType(typeof(AutoCompleteBindingSettingsBuilder), sameBuilder);
        }

        [Fact]
        public void Builder_should_set_Select_properties()
        {
            string action = "action";
            string controller = "controller";
            RouteValueDictionary routeValues = new RouteValueDictionary(new { test = "test" });

            builder.Select(action, controller, routeValues);
            
            Assert.Equal(action, settings.Select.ActionName);
            Assert.Equal(controller, settings.Select.ControllerName);
            Assert.Equal(routeValues["test"], settings.Select.RouteValues["test"]);
        }

        [Fact]
        public void Select_method_should_return_builder()
        {
            string action = "action";
            string controller = "controller";
            RouteValueDictionary routeValues = new RouteValueDictionary(new { test = "test" });

            var sameBuilder = builder.Select(action, controller, routeValues);
            Assert.IsType(typeof(AutoCompleteBindingSettingsBuilder), sameBuilder);
        }
    }
}
