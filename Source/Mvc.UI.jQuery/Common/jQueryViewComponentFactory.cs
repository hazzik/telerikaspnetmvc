// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Factory class for creating jQuery UI View Components.
    /// </summary>
    public class jQueryViewComponentFactory : IHideObjectMembers
    {
        private readonly HtmlHelper htmlHelper;
        private readonly IClientSideObjectWriterFactory clientSideObjectWriterFactory;

        private static string defaultAssetKey = "jQueryUI";

        [DebuggerStepThrough]
        public jQueryViewComponentFactory(HtmlHelper htmlHelper, IClientSideObjectWriterFactory clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(htmlHelper, "htmlHelper");
            Guard.IsNotNull(clientSideObjectWriterFactory, "clientSideObjectWriterFactory");

            this.htmlHelper = htmlHelper;
            this.clientSideObjectWriterFactory = clientSideObjectWriterFactory;
        }

        /// <summary>
        /// Gets or sets the component asset key.
        /// </summary>
        /// <value>The component asset key.</value>
        public static string DefaultAssetKey
        {
            [DebuggerStepThrough]
            get
            {
                return defaultAssetKey;
            }

            [DebuggerStepThrough]
            set
            {
                defaultAssetKey = value;
            }
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
        /// Creates a theme switcher for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual ThemeSwitcherBuilder ThemeSwitcher()
        {
            return new ThemeSwitcherBuilder(Create(() => new ThemeSwitcher(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a accordion for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual AccordionBuilder Accordion()
        {
            return new AccordionBuilder(Create(() => new Accordion(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a date picker for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual DatePickerBuilder DatePicker()
        {
            return new DatePickerBuilder(Create(() => new DatePicker(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a message box for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual MessageBoxBuilder MessageBox()
        {
            return new MessageBoxBuilder(Create(() => new MessageBox(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a progress bar for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual ProgressBarBuilder ProgressBar()
        {
            return new ProgressBarBuilder(Create(() => new ProgressBar(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a slider for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual SliderBuilder Slider()
        {
            return new SliderBuilder(Create(() => new Slider(ViewContext, clientSideObjectWriterFactory)));
        }

        /// <summary>
        /// Creates a tab for ASP.NET MVC view.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public virtual TabBuilder Tab()
        {
            return new TabBuilder(Create(() => new Tab(ViewContext, clientSideObjectWriterFactory)));
        }

        private TViewComponent Create<TViewComponent>(Func<TViewComponent> factory) where TViewComponent : ViewComponentBase
        {
            TViewComponent component = factory();

            if (component is jQueryViewComponentBase)
            {
                component.AssetKey = DefaultAssetKey;
            }

            htmlHelper.Telerik().StyleSheetRegistrar().ToRegistrar().Register(component);
            htmlHelper.Telerik().ScriptRegistrar().ToRegistrar().Register(component);

            return component;
        }
    }
}