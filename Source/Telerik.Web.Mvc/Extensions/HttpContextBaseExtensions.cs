// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
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