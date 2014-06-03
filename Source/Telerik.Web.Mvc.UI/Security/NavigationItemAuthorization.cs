namespace Telerik.Web.Mvc
{
    using System.Web.Routing;

    public interface INavigationItemAuthorization
    {
        bool IsAccessibleToUser(RequestContext requestContext, INavigationItem navigationItem);
    }

    public class NavigationItemAuthorization : INavigationItemAuthorization
    {
        private readonly IControllerAuthorization controllerAuthorization;
        private readonly IUrlAuthorization urlAuthorization;

        public NavigationItemAuthorization(IControllerAuthorization controllerAuthorization, IUrlAuthorization urlAuthorization)
        {
            Guard.IsNotNull(controllerAuthorization, "controllerAuthorization");
            Guard.IsNotNull(urlAuthorization, "urlAuthorization");

            this.controllerAuthorization = controllerAuthorization;
            this.urlAuthorization = urlAuthorization;
        }

        public NavigationItemAuthorization() : this(new ControllerAuthorization(), new UrlAuthorization())
        {
        }

        public bool IsAccessibleToUser(RequestContext requestContext, INavigationItem navigationItem)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNull(navigationItem, "navigationItem");

            bool isAllowed = true;

            if (!string.IsNullOrEmpty(navigationItem.RouteName))
            {
                isAllowed = controllerAuthorization.IsAccessibleToUser(requestContext, navigationItem.RouteName);
            }
            else if (!string.IsNullOrEmpty(navigationItem.ControllerName) && !string.IsNullOrEmpty(navigationItem.ActionName))
            {
                isAllowed = controllerAuthorization.IsAccessibleToUser(requestContext, navigationItem.ControllerName, navigationItem.RouteName);
            }
            else if (!string.IsNullOrEmpty(navigationItem.Url))
            {
                isAllowed = urlAuthorization.IsAccessibleToUser(requestContext, navigationItem.Url);
            }

            return isAllowed;
        }
    }
}