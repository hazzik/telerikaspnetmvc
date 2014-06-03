// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring child panelbar items.
    /// </summary>
    public class PanelBarItemBuilder : NavigationItemBuilder<PanelBarItem, PanelBarItemBuilder>, IHideObjectMembers
    {
        private readonly PanelBarItem item;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBarItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="viewContext">The context of the View.</param>
        public PanelBarItemBuilder(PanelBarItem item, ViewContext viewContext)
            : base(item)
        {
            Guard.IsNotNull(item, "item");
            Guard.IsNotNull(viewContext, "viewContext");

            this.item = item;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Configures the child items of a <see cref="PanelBarItem"/>.
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item").Items(firstItemChildren => 
        ///                 {
        ///                     firstItemChildren.Add().Text("Child Item 1");
        ///                     firstItemChildren.Add().Text("Child Item 2");
        ///                 });
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarItemBuilder Items(Action<PanelBarItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            var factory = new PanelBarItemFactory(item, viewContext);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Define when the item will be expanded on intial render.
        /// </summary>
        /// <param name="value">If true the item will be expanded.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item").Items(firstItemChildren => 
        ///                 {
        ///                     firstItemChildren.Add().Text("Child Item 1");
        ///                     firstItemChildren.Add().Text("Child Item 2");
        ///                 })
        ///                 .Expanded(true);
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarItemBuilder Expanded(bool value)
        {
            item.Expanded = value;

            return this;
        }

        /// <summary>
        /// Sets the Url, which will be requested to return the content.
        /// </summary>
        /// <param name="value">The url.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///         .Name("PanelBar")
        ///         .Items(parent => {
        ///
        ///              parent.Add()
        ///                    .Text("Completely Open Source")
        ///                    .LoadContentFrom(Url.Action("AjaxView_OpenSource", "PanelBar"));
        ///          })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarItemBuilder LoadContentFrom(string value)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///         .Name("PanelBar")
        ///         .Items(parent => {
        ///
        ///              parent.Add()
        ///                    .Text("Completely Open Source")
        ///                    .LoadContentFrom("AjaxView_OpenSource", "PanelBar");
        ///          })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarItemBuilder LoadContentFrom(string action, string controller)
        {
            if (!string.IsNullOrEmpty(item.Url))
                throw new NotSupportedException(Resources.TextResource.UrlAndContentUrlCannotBeSet);            

            UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);

            Item.ContentUrl = urlHelper.Action(action, controller);

            return this;
        }
    }
}
