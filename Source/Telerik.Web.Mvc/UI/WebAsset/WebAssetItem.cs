// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    /// <summary>
    /// Defines an individual web asset.
    /// </summary>
    public class WebAssetItem : IWebAssetItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItem"/> class.
        /// </summary>
        /// <param name="source">The source. Source must be a virtual path.</param>
        public WebAssetItem(string source)
        {
            Guard.IsNotVirtualPath(source, "source");

            Source = source;
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source
        {
            get;
            private set;
        }
    }
}