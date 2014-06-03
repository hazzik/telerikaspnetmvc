// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// Contains extension methods of <see cref="HttpContextBase"/>.
    /// </summary>
    public static class HttpContextBaseExtensions
    {
        /// <summary>
        /// Requests the context.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static RequestContext RequestContext(this HttpContextBase instance)
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(instance) ?? new RouteData();
            RequestContext requestContext = new RequestContext(instance, routeData);

            return requestContext;
        }
    }
}