namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;
    using System.Web.Mvc;

    public class ViewComponentFactory : ViewComponentFactoryBase, IHideObjectMembers
    {
        private static string defaultAssetKey = "rad";

        private readonly IUrlGenerator urlGenerator;

        [DebuggerStepThrough]
        public ViewComponentFactory(HtmlHelper htmlHelper, IUrlGenerator urlGenerator) : base(htmlHelper.ViewContext)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");

            this.urlGenerator = urlGenerator;
        }

        [DebuggerStepThrough]
        public ViewComponentFactory(HtmlHelper htmlHelper) : this(htmlHelper, new UrlGenerator())
        {
        }

        /// <summary>
        /// Gets or sets the componenet asset key.
        /// </summary>
        /// <value>The componenet asset key.</value>
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
                Guard.IsNotNullOrEmpty(value, "value");

                defaultAssetKey = value;
            }
        }

        [DebuggerStepThrough]
        public MenuBuilder Menu()
        {
            return new MenuBuilder(new Menu(ViewContext, InternalStyleSheetRegistrar, InternalScriptRegistrar, ResponseCapturer, urlGenerator));
        }

        [DebuggerStepThrough]
        public BreadcrumbBuilder Breadcrumb()
        {
            return new BreadcrumbBuilder(new Breadcrumb(ViewContext, InternalStyleSheetRegistrar, InternalScriptRegistrar, ResponseCapturer));
        }
    }
}