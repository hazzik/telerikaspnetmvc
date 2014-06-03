// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Extensions;
    using Infrastructure;

    public class UrlGenerator : IUrlGenerator
    {
        public string Generate(RequestContext requestContext, string url)
        {
            return new UrlHelper(requestContext).Content(url);
        }

        public string Generate(RequestContext requestContext, INavigatable navigationItem)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNull(navigationItem, "navigationItem");

            UrlHelper urlHelper = new UrlHelper(requestContext);

            Func<RouteValueDictionary> getRouteValues = () =>
                                                        {
                                                            RouteValueDictionary routeValues = new RouteValueDictionary();

                                                            if (!navigationItem.RouteValues.IsNullOrEmpty())
                                                            {
                                                                routeValues.Merge(navigationItem.RouteValues);
                                                            }

                                                            return routeValues;
                                                        };

            string generatedUrl = null;

            if (!string.IsNullOrEmpty(navigationItem.RouteName))
            {
                generatedUrl = urlHelper.RouteUrl(navigationItem.RouteName, getRouteValues());
            }
            else if (!string.IsNullOrEmpty(navigationItem.ControllerName) && !string.IsNullOrEmpty(navigationItem.ActionName))
            {
                generatedUrl = urlHelper.Action(navigationItem.ActionName, navigationItem.ControllerName, getRouteValues());
            }
            else if (!string.IsNullOrEmpty(navigationItem.Url))
            {
                generatedUrl = navigationItem.Url.StartsWith("~/", StringComparison.Ordinal) ?
                               urlHelper.Content(navigationItem.Url) :
                               navigationItem.Url;
            }

            return generatedUrl;
        }
    }
}