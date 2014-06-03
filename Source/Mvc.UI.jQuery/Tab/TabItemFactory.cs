// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for tab.
    /// </summary>
    public class TabItemFactory : IHideObjectMembers
    {
        private readonly ITabItemContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItemFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public TabItemFactory(ITabItemContainer container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        /// <summary>
        /// Add a new tab item to the container
        /// </summary>
        /// <returns></returns>
        public virtual TabItemBuilder Add()
        {
            TabItem item = new TabItem();

            container.Items.Add(item);

            return new TabItemBuilder(item);
        }
    }
}