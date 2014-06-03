namespace Telerik.Web.Mvc.UI
{
    public class MenuItemFactory : IHideObjectMembers
    {
        private readonly IMenuItemContainer container;

        public MenuItemFactory(IMenuItemContainer container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        public MenuItemBuilder Add()
        {
            MenuItem item = new MenuItem();

            container.Items.Add(item);

            return new MenuItemBuilder(item);
        }
    }
}