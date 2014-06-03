namespace Telerik.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public interface IUrlGenerator
    {
        string Generate(RequestContext requestContext, INavigationItem navigationItem);
    }

    public class UrlGenerator : IUrlGenerator
    {
        public string Generate(RequestContext requestContext, INavigationItem navigationItem)
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