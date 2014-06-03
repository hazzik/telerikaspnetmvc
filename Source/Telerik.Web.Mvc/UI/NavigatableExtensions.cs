// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// INavigatable extension for providing access to <see cref="INavigatable"/>.
    /// </summary>
    public static class NavigatableExtensions
    {
        /// <summary>
        /// Sets the action and controller name, along with Route values of <see cref="INavigatable"/> object.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="actionName">Action name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="routeValues">Route values as an object</param>
        public static void Action(this INavigatable navigatable, string actionName, string controllerName, object routeValues)
        {
            navigatable.ControllerName = controllerName;
            navigatable.ActionName = actionName;
            navigatable.SetRouteValues(routeValues);
        }

        /// <summary>
        /// Sets the action, controller name and route values of <see cref="INavigatable"/> object.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="actionName">Action name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="routeValues">Route values as <see cref="RouteValueDictionary"/></param>
        public static void Action(this INavigatable navigatable, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            navigatable.ControllerName = controllerName;
            navigatable.ActionName = actionName;
            navigatable.SetRouteValues(routeValues);
        }

        /// <summary>
        /// Sets the url property of <see cref="INavigatable"/> object.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="actionName">The Url.</param>
        public static void Url(this INavigatable navigatable, string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            navigatable.Url = value;
        }

        /// <summary>
        /// Sets the route name and route values of <see cref="INavigatable"/> object.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="routeName">Route name.</param>
        /// <param name="routeValues">Route values as an object.</param>
        public static void Route(this INavigatable navigatable, string routeName, object routeValues)
        {
            Guard.IsNotNullOrEmpty(routeName, "routeName");

            navigatable.RouteName = routeName;
            navigatable.SetRouteValues(routeValues);
        }

        /// <summary>
        /// Sets the route name and route values of <see cref="INavigatable"/> object.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="routeName">Route name.</param>
        /// <param name="routeValues">Route values as <see cref="RouteValueDictionary"/>.</param>
        public static void Route(this INavigatable navigatable, string routeName, RouteValueDictionary routeValues)
        {
            Guard.IsNotNullOrEmpty(routeName, "routeName");

            navigatable.RouteName = routeName;
            navigatable.SetRouteValues(routeValues);
        }

        /// <summary>
        /// Generating url depending on the ViewContext and the <see cref="IUrlGenerator"/> generator.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="viewContext">The <see cref="ViewContext"/> object</param>
        /// <param name="urlGenerator">The <see cref="IUrlGenerator"/> generator.</param>
        public static string GenerateUrl(this INavigatable navigatable, ViewContext viewContext, IUrlGenerator urlGenerator)
        {
            return urlGenerator.Generate(viewContext.RequestContext, navigatable);
        }

        /// <summary>
        /// Generating url depending on the ViewContext and the <see cref="IUrlGenerator"/> generator.
        /// </summary>
        /// <param name="navigatable">The <see cref="INavigatable"/> object.</param>
        /// <param name="viewContext">The <see cref="ViewContext"/> object</param>
        /// <param name="urlGenerator">The <see cref="IUrlGenerator"/> generator.</param>
        /// <param name="routeValues">The route values as <see cref="RouteValueDictionary"/>.</param>
        public static string GenerateUrl(this INavigatable navigatable, ViewContext viewContext, IUrlGenerator urlGenerator, RouteValueDictionary routeValues)
        {
            string url = urlGenerator.Generate(viewContext.RequestContext, navigatable) ??
                         new UrlHelper(viewContext.RequestContext).RouteUrl(routeValues);

            return url;
        }

        /// <summary>
        /// Verify whether the <see cref="INavigatable"/> object is accessible.
        /// </summary>
        /// <param name="item">The <see cref="INavigatable"/> object.</param>
        /// <param name="authorization">The <see cref="INavigationItemAuthorization"/> object.</param>
        /// <param name="viewContext">The <see cref="ViewContext"/> object</param>
        public static bool IsAccessible(this INavigatable item, INavigationItemAuthorization authorization, ViewContext viewContext)
        {
            return authorization.IsAccessibleToUser(viewContext.RequestContext, item);
        }

        /// <summary>
        /// Verifies whether collection of <see cref="INavigatable"/> objects is accessible.
        /// </summary>
        /// <typeparam name="T">Object of <see cref="INavigatable"/> type.</typeparam>
        /// <param name="item">The <see cref="INavigatable"/> object.</param>
        /// <param name="authorization">The <see cref="INavigationItemAuthorization"/> object.</param>
        /// <param name="viewContext">The <see cref="ViewContext"/> object</param>
        public static bool IsAccessible<T>(this IEnumerable<T> items, INavigationItemAuthorization authorization, ViewContext viewContext)
        {
            return items.Any(item => authorization.IsAccessibleToUser(viewContext.RequestContext, (INavigatable)item));
        }

        private static void SetRouteValues(this INavigatable navigatable, object values)
        {
            if (values != null)
            {
                navigatable.RouteValues.Clear();
                navigatable.RouteValues.Merge(values);
            }
        }

        private static void SetRouteValues(this INavigatable navigatable, IDictionary<string, object> values)
        {
            if (values != null)
            {
                navigatable.RouteValues.Clear();
                navigatable.RouteValues.Merge(values);
            }
        }
    }
}