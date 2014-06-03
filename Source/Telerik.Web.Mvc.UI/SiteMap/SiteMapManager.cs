namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    public static class SiteMapManager
    {
        private static readonly SiteMapDictionary siteMaps = new SiteMapDictionary();

        public static SiteMapDictionary SiteMaps
        {
            [DebuggerStepThrough]
            get
            {
                return siteMaps;
            }
        }

        // Required for Unit Test
        internal static void Clear()
        {
            SiteMaps.Clear();
        }
    }
}