// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    /// <summary>
    /// Defines a class that is used to store global sitemaps.
    /// </summary>
    public static class SiteMapManager
    {
        private static readonly SiteMapDictionary siteMaps = new SiteMapDictionary();

        /// <summary>
        /// Gets the site maps.
        /// </summary>
        /// <value>The site maps.</value>
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