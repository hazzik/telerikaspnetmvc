// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.ComponentModel;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Class used by the Accordion to build HTML tags for accordion item.
    /// </summary>
    public class AccordionItemBuilder : IHideObjectMembers
    {
        private readonly AccordionItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public AccordionItemBuilder(AccordionItem item)
        {
            Guard.IsNotNull(item, "item");

            this.item = item;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Mvc.UI.jQuery.AccordionItemBuilder"/> to <see cref="Mvc.UI.jQuery.AccordionItem"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator AccordionItem(AccordionItemBuilder builder)
        {
            return builder.ToItem();
        }

        /// <summary>
        /// Convert to a AccordionItem.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public AccordionItem ToItem()
        {
            return item;
        }

        /// <summary>
        /// Specify the extra header HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual AccordionItemBuilder HtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            item.HtmlAttributes.Clear();
            item.HtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// Specify the header text displayed for the accordion item in a accordion.
        /// </summary>
        /// <param name="theText">The text.</param>
        /// <returns></returns>
        public virtual AccordionItemBuilder Text(string theText)
        {
            item.Text = theText;

            return this;
        }

        /// <summary>
        /// Specify the extras content HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual AccordionItemBuilder ContentHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            item.ContentHtmlAttributes.Clear();
            item.ContentHtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// HTML markups for the accordion item.
        /// </summary>
        /// <param name="markups">The HTML markups.</param>
        /// <returns></returns>
        public virtual AccordionItemBuilder Content(Action markups)
        {
            item.Content = markups;

            return this;
        }

        /// <summary>
        /// Sets a value indicating whether the item is selected.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual AccordionItemBuilder Selected(bool value)
        {
            item.Selected = value;

            return this;
        }
    }
}