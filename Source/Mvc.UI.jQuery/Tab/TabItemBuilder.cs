// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.ComponentModel;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Class used by the Tab to build HTML tags for tab item.
    /// </summary>
    public class TabItemBuilder : IHideObjectMembers
    {
        private readonly TabItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public TabItemBuilder(TabItem item)
        {
            Guard.IsNotNull(item, "item");

            this.item = item;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Mvc.UI.jQuery.TabItemBuilder"/> to <see cref="Mvc.UI.jQuery.TabItem"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TabItem(TabItemBuilder builder)
        {
            return builder.ToItem();
        }

        /// <summary>
        /// Convert to a TabItem.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TabItem ToItem()
        {
            return item;
        }

        /// <summary>
        /// Specify the extra header HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual TabItemBuilder HtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            item.HtmlAttributes.Clear();
            item.HtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// Specify the theText displayed for the tab item in a tab.
        /// </summary>
        /// <param name="theText">The text.</param>
        /// <returns></returns>
        public virtual TabItemBuilder Text(string theText)
        {
            item.Text = theText;

            return this;
        }

        /// <summary>
        /// Specify whether loads the content from specific url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "Its take a URI as a string parameter.")]
        public virtual TabItemBuilder LoadContentFrom(string url)
        {
            item.LoadContentFromUrl = url;

            return this;
        }

        /// <summary>
        /// Specify the extras content HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual TabItemBuilder ContentHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            item.ContentHtmlAttributes.Clear();
            item.ContentHtmlAttributes.Merge(attributes);

            return this;
        }

        /// <summary>
        /// HTML markups for the tab item.
        /// </summary>
        /// <param name="markups">The HTML markups.</param>
        /// <returns></returns>
        public virtual TabItemBuilder Content(Action markups)
        {
            item.Content = markups;

            return this;
        }

        /// <summary>
        /// Sets a value indicating whether the item is selected.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual TabItemBuilder Selected(bool value)
        {
            item.Selected = value;

            return this;
        }

        /// <summary>
        /// Sets a value indicating whether the item is disabled.
        /// </summary>
        /// <param name="value">if set to <c>true</c> value.</param>
        /// <returns></returns>
        public virtual TabItemBuilder Disabled(bool value)
        {
            item.Disabled = value;

            return this;
        }
    }
}