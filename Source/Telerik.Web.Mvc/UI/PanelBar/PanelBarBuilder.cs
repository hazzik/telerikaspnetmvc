// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="PanelBar"/> component.
    /// </summary>
    public class PanelBarBuilder : ViewComponentBuilderBase<PanelBar, PanelBarBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBarBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public PanelBarBuilder(PanelBar component) : base(component)
        {
        }

        /// <summary>
        /// Defines the items in the panelbar
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder Items(Action<PanelBarItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            var factory = new PanelBarItemFactory(Component, Component.ViewContext);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Sets the theme of the panelbar
        /// </summary>
        public PanelBarBuilder Theme(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Component.Theme = name;

            return this;
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="clientEventsAction">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events =>
        ///                 events.OnExpand("onExpand").OnCollapse("onCollapse")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder ClientEvents(Action<PanelBarClientEventsBuilder> clientEventsAction)
        {
            Guard.IsNotNull(clientEventsAction, "clientEventsAction");

            clientEventsAction(new PanelBarClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }

        /// <summary>
        /// Binds the panelbar to a sitemap
        /// </summary>
        /// <param name="viewDataKey">The view data key.</param>
        /// <param name="siteMapAction">The action to configure the item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .BindTo("examples", (item, siteMapNode) =>
        ///             {
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder BindTo(string viewDataKey, Action<PanelBarItem, SiteMapNode> siteMapAction)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.isBindToSiteMap = true;

            Component.BindTo(viewDataKey, Component.ViewContext, siteMapAction);

            return this;
        }

        /// <summary>
        /// Binds the panelbar to a sitemap.
        /// </summary>
        /// <param name="viewDataKey">The view data key.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .BindTo("examples")
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder BindTo(string viewDataKey)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.isBindToSiteMap = true;

            Component.BindTo(viewDataKey, Component.ViewContext);

            return this;
        }

        /// <summary>
        /// Binds the panelbar to a list of objects
        /// </summary>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <param name="itemDataBound">The action executed for every data bound item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .BindTo(new []{"First", "Second"}, (item, value)
        ///             {
        ///                item.Text = value;
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder BindTo<T>(IEnumerable<T> dataSource, Action<PanelBarItem, T> itemDataBound)
        {
            Guard.IsNotNull(itemDataBound, "itemDataBound");
            Component.BindTo(dataSource, itemDataBound);

            return this;
        }

        /// <summary>
        /// Configures the effects of the panelbar.
        /// </summary>
        /// <param name="effectsAction">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().PanelBar()
        ///	           .Name("PanelBar")
        ///	           .Effects(fx =>
        ///	           {
        ///		            fx.Height(properties => properties
        ///					    .OpenDuration(AnimationDuration.Normal)
        ///					    .CloseDuration(AnimationDuration.Normal))
        ///			          .Opacity(properties => properties
        ///					    .OpenDuration(AnimationDuration.Normal)
        ///					    .CloseDuration(AnimationDuration.Normal));
        ///	           })
        /// </code>
        /// </example>
        public PanelBarBuilder Effects(Action<EffectFactory> addEffects)
        {
            Guard.IsNotNull(addEffects, "addAction");

            addEffects(new EffectFactory(Component));

            return this;
        }

        /// <summary>
        /// Callback for each item.
        /// </summary>
        /// <param name="itemAction">Action, which will be executed for each item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ItemAction(item =>
        ///             {
        ///                 item
        ///                     .Text(...)
        ///                     .HtmlAttributes(...);
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder ItemAction(Action<PanelBarItem> action)
        {
            Guard.IsNotNull(action, "action");

            Component.ItemAction = action;

            return this;
        }

        /// <summary>
        /// Select item depending on the current URL.
        /// </summary>
        /// <param name="value">If true the item will be highlighted.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .HighlightPath(true)
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder HighlightPath(bool value)
        {
            Component.HighlightPath = value;

            return this;
        }

        /// <summary>
        /// Renders the panelbar with expanded items.
        /// </summary>
        /// <param name="value">If true the panelbar will be expanded.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ExpandAll(true)
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder ExpandAll(bool value)
        {
            Component.ExpandAll = value;

            return this;
        }

        /// <summary>
        /// Sets the expand mode of the panelbar.
        /// </summary>
        /// <param name="value">The desired expand mode.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ExpandMode(PanelBarExpandMode.Multiple)
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder ExpandMode(PanelBarExpandMode value)
        {
            Component.ExpandMode = value;
            
            return this;
        }

        /// <summary>
        /// Selects the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        ///             .SelectedIndex(1)
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarBuilder SelectedIndex(int index)
        {
            Guard.IsNotNull(index, "index");
            Guard.IsNotNegative(index, "index");

            Component.SelectedIndex = index;

            return this;
        }
    }
}