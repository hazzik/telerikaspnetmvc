// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    public class MenuItemFactory : IHideObjectMembers
    {
        private readonly INavigationItemContainer<MenuItem> container;

        public MenuItemFactory(INavigationItemContainer<MenuItem> container)
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