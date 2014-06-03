// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Mvc;

    using Infrastructure;

    /// <summary>
    /// Provides the factory methods for creating Telerik View Components.
    /// </summary>
    public class ViewComponentFactory : IHideObjectMembers
    {
        private readonly HtmlHelper htmlHelper;
        private readonly IClientSideObjectWriterFactory clientSideObjectWriterFactory;

        private readonly StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;
        private readonly ScriptRegistrarBuilder scriptRegistrarBuilder;

        [DebuggerStepThrough]
        public ViewComponentFactory(HtmlHelper htmlHelper, IClientSideObjectWriterFactory clientSideObjectWriterFactory, StyleSheetRegistrarBuilder styleSheetRegistrar, ScriptRegistrarBuilder scriptRegistrar)
        {
            Guard.IsNotNull(htmlHelper, "htmlHelper");
            Guard.IsNotNull(clientSideObjectWriterFactory, "clientSideObjectWriterFactory");
            Guard.IsNotNull(styleSheetRegistrar, "styleSheetRegistrar");
            Guard.IsNotNull(scriptRegistrar, "scriptRegistrar");

            this.htmlHelper = htmlHelper;
            this.clientSideObjectWriterFactory = clientSideObjectWriterFactory;

            styleSheetRegistrarBuilder = styleSheetRegistrar;
            scriptRegistrarBuilder = scriptRegistrar;
        }

        private ViewContext ViewContext
        {
            [DebuggerStepThrough]
            get
            {
                return htmlHelper.ViewContext;
            }
        }

        /// <summary>
        /// Creates a <see cref="StyleSheetRegistrar"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().StyleSheetRegistrar()
        ///             .DefaultGroup(group => group
        ///                   group.Add("Site.css")
        ///                        .Add("telerik.common.css")
        ///                        .Add("telerik.vista.css")
        ///                        .Compressed(true)
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            return styleSheetRegistrarBuilder;
        }

        /// <summary>
        /// Creates a <see cref="ScriptRegistrar"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ScriptRegistrar()
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public ScriptRegistrarBuilder ScriptRegistrar()
        {
            return scriptRegistrarBuilder;
        }

        /// <summary>
        /// Creates a <see cref="Menu"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => { /* add items here */ });
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
		public virtual MenuBuilder Menu()
		{
			return new MenuBuilder(Create(() => new Menu(ViewContext, clientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<IMenuRendererFactory>())));
		}

        /// <summary>
        /// Creates a new <see cref="Grid&lt;T&gt;"/> bound to the specified data item type.
        /// </summary>
        /// <example>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Grid")
        ///             .BindTo(Model)
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// Do not forget to bind the grid using the <see cref="GridBuilder{T}.BindTo(System.String)" /> method when using this overload.
        /// </remarks>
        [DebuggerStepThrough]
        public virtual GridBuilder<T> Grid<T>() where T : class
        {
            return new GridBuilder<T>(Create(() => new Grid<T>(ViewContext, clientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<IGridRendererFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="Telerik.Web.UI.Grid&lt;T&gt;"/> bound to the specified data item type. Suitable for custom client-side binding where 
        /// there is no server type corresponding to the data item.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(new {Name = "", Age = 0})
        ///             .Name("Grid")
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(o => o.Name);
        ///                 columns.Add(o => o.Age);
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual GridBuilder<T> Grid<T>(T value) where T : class
        {
            return Grid<T>();
        }

        /// <summary>
        /// Creates a new <see cref="Telerik.Web.UI.Grid&lt;T&gt;"/> bound to the specified data source.
        /// </summary>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Grid<T>(IEnumerable<T> dataSource) where T : class
        {
            GridBuilder<T> builder = Grid<T>();
            
            builder.Component.DataSource = dataSource;

            return builder;
        }

        /// <summary>
        /// Creates a new <see cref="Telerik.Web.UI.Grid&lt;T&gt;"/> bound an item in ViewData.
        /// </summary>
        /// <typeparam name="T">Type of the data item</typeparam>
        /// <param name="dataSourceViewDataKey">The data source view data key.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;("orders")
        ///             .Name("Grid")
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Grid<T>(string dataSourceViewDataKey) where T : class
        {
            GridBuilder<T> builder = Grid<T>();

            builder.Component.DataSource = ViewContext.ViewData.Eval(dataSourceViewDataKey) as IEnumerable<T>;

            return builder;
        }

        /// <summary>
        /// Creates a new <see cref="TabStrip"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TabStrip()
        ///             .Name("TabStrip")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First");
        ///                 items.Add().Text("Second");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual TabStripBuilder TabStrip()
        {
            return new TabStripBuilder(Create(() => new TabStrip(ViewContext, clientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<ITabStripRendererFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="PanelBar"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First");
        ///                 items.Add().Text("Second");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual PanelBarBuilder PanelBar()
        {
            return new PanelBarBuilder(Create(() => new PanelBar(ViewContext, clientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<IPanelBarRendererFactory>())));
        }

        private TViewComponent Create<TViewComponent>(Func<TViewComponent> factory) where TViewComponent : ViewComponentBase
        {
            TViewComponent component = factory();

            scriptRegistrarBuilder.ToRegistrar().Register(component);

            return component;
        }
    }
}