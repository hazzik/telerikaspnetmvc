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
    /// Defines the fluent interface for configuring the <see cref="Menu"/> component.
    /// </summary>
    public class MenuBuilder : ViewComponentBuilderBase<Menu, MenuBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public MenuBuilder(Menu component) : base(component)
        {
        }

        /// <summary>
        /// Defines the items in the menu
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder Items(Action<MenuItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            MenuItemFactory factory = new MenuItemFactory(Component);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="clientEventsAction">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ClientEvents(events =>
        ///                 events.OnOpen("onOpen").OnClose("onClose")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder ClientEvents(Action<MenuClientEventsBuilder> clientEventsAction)
        {
            Guard.IsNotNull(clientEventsAction, "clientEventsAction");

            clientEventsAction(new MenuClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }

        /// <summary>
        /// Sets the menu orientation.
        /// </summary>
        /// <param name="value">The desired orientation.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Orientation(MenuOrientation.Vertical)
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder Orientation(MenuOrientation value)
        {
            Component.Orientation = value;

            return this;
        }

        /// <summary>
        /// Enables or disables the "open-on-click" feature.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .OpenOnClick(true)
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder OpenOnClick(bool value)
        {
            Component.OpenOnClick = value;

            return this;
        }

        /// <summary>
        /// Sets the theme of the menu
        /// </summary>
        public MenuBuilder Theme(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Component.Theme = name;

            return this;
        }

        /// <summary>
        /// Binds the menu to a sitemap
        /// </summary>
        /// <param name="viewDataKey">The view data key.</param>
        /// <param name="siteMapAction">The action to configure the item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .BindTo("examples", (item, siteMapNode) =>
        ///             {
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder BindTo(string viewDataKey, Action<MenuItem, SiteMapNode> siteMapAction)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.BindTo(viewDataKey, Component.ViewContext, siteMapAction);

            return this;
        }
        /// <summary>
        /// Binds the menu to a sitemap.
        /// </summary>
        /// <param name="viewDataKey">The view data key.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .BindTo("examples")
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder BindTo(string viewDataKey)
        {
            Guard.IsNotNullOrEmpty(viewDataKey, "viewDataKey");

            Component.BindTo(viewDataKey, Component.ViewContext);

            return this;
        }

        /// <summary>
        /// Binds the menu to a list of objects
        /// </summary>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <param name="itemDataBound">The action executed for every data bound item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .BindTo(new []{"First", "Second"}, (item, value)
        ///             {
        ///                item.Text = value;
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder BindTo<T>(IEnumerable<T> dataSource, Action<MenuItem, T> itemDataBound)
        {
            Guard.IsNotNull(itemDataBound, "itemDataBound");

            Component.BindTo(dataSource, itemDataBound);

            return this;
        }

        /// <summary>
        /// Configures the effects of the menu.
        /// </summary>
        /// <param name="effectsAction">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().Menu()
		///	           .Name("Menu")
		///	           .Effects(fx =>
		///	           {
		///		            fx.Slide(properties => properties
		///					    .OpenDuration(AnimationDuration.Normal)
		///					    .CloseDuration(AnimationDuration.Normal))
		///			          .Opacity(properties => properties
		///					    .OpenDuration(AnimationDuration.Normal)
		///					    .CloseDuration(AnimationDuration.Normal));
        ///	           })
        /// </code>
        /// </example>
        public MenuBuilder Effects(Action<EffectFactory> effectsAction)
        {
            Guard.IsNotNull(effectsAction, "effectsAction");

            effectsAction(new EffectFactory(Component));

            return this;
        }

        /// <summary>
        /// Selects the item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        ///             .SelectedIndex(1)
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder SelectedIndex(int index)
        {
            Guard.IsNotNull(index, "index");
            Guard.IsNotNegative(index, "index");

            Component.SelectedIndex = index;

            return this;
        }

        /// <summary>
        /// Callback for each item.
        /// </summary>
        /// <param name="action">Action, which will be executed for each item.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ItemAction(item =>
        ///             {
        ///                 item
        ///                     .Text(...)
        ///                     .HtmlAttributes(...);
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder ItemAction(Action<MenuItem> action)
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
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .HighlightPath(true)
        /// %&gt;
        /// </code>
        /// </example>
        public MenuBuilder HighlightPath(bool value)
        {
            Component.HighlightPath = value;

            return this;
        }
    }
}