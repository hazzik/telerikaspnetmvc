// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using Infrastructure;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the fluent interface for configuring child tabstrip items.
    /// </summary>
    public class TabStripItemBuilder : NavigationItemBuilder<TabStripItem, TabStripItemBuilder>, IHideObjectMembers
    {
        private readonly TabStripItem item;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabStripItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewContext">The context of the View.</param>
        public TabStripItemBuilder(TabStripItem item, ViewContext viewContext)
            : base(item)
        {
            Guard.IsNotNull(item, "item");
            Guard.IsNotNull(viewContext, "viewContext");

            this.item = item;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Sets the Url, which will be requested to return the content.
        /// </summary>
        /// <param name="value">The url.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TabStrip()
        ///         .Name("TabStrip")
        ///         .Items(parent => {
        ///
        ///              parent.Add()
        ///                    .Text("Completely Open Source")
        ///                    .LoadContentFrom(Url.Action("AjaxView_OpenSource", "TabStrip"));
        ///          })
        /// %&gt;
        /// </code>
        /// </example>
        public TabStripItemBuilder LoadContentFrom(string value)
        {
            if (!string.IsNullOrEmpty(item.Url))
                throw new NotSupportedException(Resources.TextResource.UrlAndContentUrlCannotBeSet);

            Item.ContentUrl = value;

            return this;
        }

        /// <summary>
        /// Sets the Url, which will be requested to return the content. The method excepts
        /// Action name and Controller name.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="controller">The controller name.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TabStrip()
        ///         .Name("TabStrip")
        ///         .Items(parent => {
        ///
        ///              parent.Add()
        ///                    .Text("Completely Open Source")
        ///                    .LoadContentFrom("AjaxView_OpenSource", "TabStrip");
        ///          })
        /// %&gt;
        /// </code>
        /// </example>
        public TabStripItemBuilder LoadContentFrom(string action, string controller)
        {
            if (!string.IsNullOrEmpty(item.Url))
                throw new NotSupportedException(Resources.TextResource.UrlAndContentUrlCannotBeSet);

            UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);

            Item.ContentUrl = urlHelper.Action(action, controller);
            return this;
        }
    }
}