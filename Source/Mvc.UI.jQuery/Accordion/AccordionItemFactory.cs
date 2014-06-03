// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for accordion.
    /// </summary>
    public class AccordionItemFactory : IHideObjectMembers
    {
        private readonly IAccordionItemContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionItemFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AccordionItemFactory(IAccordionItemContainer container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        /// <summary>
        /// Add a new accordion item to the container
        /// </summary>
        /// <returns></returns>
        public virtual AccordionItemBuilder Add()
        {
            AccordionItem item = new AccordionItem();

            container.Items.Add(item);

            return new AccordionItemBuilder(item);
        }
    }
}