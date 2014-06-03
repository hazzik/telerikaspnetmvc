namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;

    using Xunit;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class DropDownListBindingSettingsBuilderTests
    {
        private readonly DropDownBindingSettings settings;
        private readonly DropDownListBindingSettingsBuilder builder;

        public DropDownListBindingSettingsBuilderTests()
        {
            settings = new DropDownBindingSettings();
            builder = new DropDownListBindingSettingsBuilder(settings);
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
            Assert.IsType(typeof(DropDownListBindingSettingsBuilder), sameBuilder);
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
            Assert.IsType(typeof(DropDownListBindingSettingsBuilder), sameBuilder);
        }
    }
}
