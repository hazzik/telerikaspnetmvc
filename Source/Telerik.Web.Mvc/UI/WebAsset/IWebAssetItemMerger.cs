// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the basic building block of web asset merging.
    /// </summary>
    public interface IWebAssetItemMerger
    {
        /// <summary>
        /// Merges the specified assets.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetHandlerPath">The asset handler path.</param>
        /// <param name="assets">The assets.</param>
        /// <returns></returns>
        IList<string> Merge(string contentType, string assetHandlerPath, WebAssetItemCollection assets);
    }
}