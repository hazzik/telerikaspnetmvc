namespace Telerik.Web.Mvc.Tests.Menu
{
    using Telerik.Web.Mvc.UI;
    using Xunit;

    public class MenuItemBuilderTests
    {
        private readonly MenuItem item;
        private readonly MenuItemBuilder builder;

		public MenuItemBuilderTests()
        {
            var viewContext = TestHelper.CreateViewContext();
			item = new MenuItem();
            builder = new MenuItemBuilder(item, viewContext);
        }

        [Fact]
        public void Builder_should_set_expanded_property()
        {
			bool factoryIsMenuItemFactory = false;

			builder.Items(factory => { factoryIsMenuItemFactory = factory is MenuItemFactory; });

			Assert.True(factoryIsMenuItemFactory);
        }
    }
}
