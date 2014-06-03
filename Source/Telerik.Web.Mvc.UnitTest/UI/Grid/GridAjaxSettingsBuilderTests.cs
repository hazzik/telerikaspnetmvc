
namespace Telerik.Web.Mvc.UI
{
    using Xunit;
    
    public class GridAjaxSettingsBuilderTests
    {
        [Fact]
        public void Enabled_sets_the_enabled_property()
        {
            GridAjaxSettings settings = new GridAjaxSettings();
            GridAjaxSettingsBuilder builder = new GridAjaxSettingsBuilder(settings);
            builder.Enabled(true);

            Assert.True(settings.Enabled);
        }
    }
}
