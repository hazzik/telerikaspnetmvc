// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Web.Mvc;

    /// <summary>
    /// Class used to resolve relative path for virtual path.
    /// </summary>
    public class UrlResolver : IUrlResolver
    {
        private readonly UrlHelper helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlResolver"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public UrlResolver(UrlHelper helper)
        {
            Guard.IsNotNull(helper, "helper");

            this.helper = helper;
        }

        /// <summary>
        /// Returns the relative path for the specified virtual path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string Resolve(string url)
        {
            return helper.Content(url);
        }
    }
}