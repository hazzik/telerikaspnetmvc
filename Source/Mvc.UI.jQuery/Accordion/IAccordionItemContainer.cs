// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a accordion item container.
    /// </summary>
    public interface IAccordionItemContainer
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        IList<AccordionItem> Items
        {
            get;
        }
    }
}