// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Web;

    /// <summary>
    /// Defines members that a class must implement in order to compress the response.
    /// </summary>
    public interface IHttpResponseCompressor
    {
        /// <summary>
        /// Compresses the response.
        /// </summary>
        /// <param name="context">The context.</param>
        void Compress(HttpContextBase context);
    }
}