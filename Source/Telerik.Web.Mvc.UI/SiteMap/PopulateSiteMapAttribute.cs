namespace Telerik.Web.Mvc
{
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Sealed in public class is a design smell, we can ignore the little performace benifit of sealed.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PopulateSiteMapAttribute : FilterAttribute, IActionFilter
    {
        private static string defaultViewDataKey = "siteMap";

        public PopulateSiteMapAttribute(SiteMapDictionary siteMaps)
        {
            Guard.IsNotNull(siteMaps, "siteMaps");

            SiteMaps = siteMaps;
        }

        public PopulateSiteMapAttribute() : this(SiteMapManager.SiteMaps)
        {
        }

        public static string DefaultViewDataKey
        {
            [DebuggerStepThrough]
            get
            {
                return defaultViewDataKey;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                defaultViewDataKey = value;
            }
        }

        public string SiteMapName
        {
            get;
            set;
        }

        public string ViewDataKey
        {
            get;
            set;
        }

        public SiteMapDictionary SiteMaps
        {
            get;
            private set;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Guard.IsNotNull(filterContext, "filterContext");

            SiteMapBase siteMap = string.IsNullOrEmpty(SiteMapName) ? SiteMaps.DefaultSiteMap : SiteMaps[SiteMapName];
            string viewDataKey = ViewDataKey ?? DefaultViewDataKey;

            filterContext.Controller.ViewData[viewDataKey] = siteMap;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Do nothing, just sleep.
        }
    }
}