// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a accordion item container.
    /// </summary>
    public interface IAccordionItemContainer
    {
        IList<AccordionItem> Items
        {
            get;
        }
    }
}