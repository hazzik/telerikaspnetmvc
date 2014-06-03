// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System;
    using System.Web;

    /// <summary>
    /// Contains extension methods of <see cref="HttpRequestBase"/>.
    /// </summary>
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// Get the Application root path.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ApplicationRoot(this HttpRequestBase instance)
        {
            string applicationPath = instance.Url.GetLeftPart(UriPartial.Authority) + instance.ApplicationPath;

            // Remove the last /
            if (applicationPath.EndsWith("/", StringComparison.Ordinal))
            {
                applicationPath = applicationPath.Substring(0, applicationPath.Length - 1);
            }

            return applicationPath;
        }
    }
}