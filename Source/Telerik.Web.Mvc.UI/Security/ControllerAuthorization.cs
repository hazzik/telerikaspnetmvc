namespace Telerik.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public interface IControllerAuthorization
    {
        bool IsAccessibleToUser(RequestContext requestContext, string routeName);

        bool IsAccessibleToUser(RequestContext requestContext, string controllerName, string actionName);
    }

    public class ControllerAuthorization : IControllerAuthorization
    {
        private readonly RouteCollection routes;

        public ControllerAuthorization(RouteCollection routes)
        {
            Guard.IsNotNull(routes, "routes");

            this.routes = routes;
        }

        public ControllerAuthorization() : this(RouteTable.Routes)
        {
        }

        public bool IsAccessibleToUser(RequestContext requestContext, string routeName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(routeName, "routeName");

            RouteBase route = routes[routeName];
            RouteData routeData = route.GetRouteData(requestContext.HttpContext);

            string controllerName = routeData.GetRequiredString("controller");
            string actionName = routeData.GetRequiredString("action");

            return IsAccessibleToUser(requestContext, controllerName, actionName);
        }

        public bool IsAccessibleToUser(RequestContext requestContext, string controllerName, string actionName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            AuthorizationInfo authorizationInfo = ActionAuthorizationCache.GetAuthorization(requestContext, controllerName, actionName);
            bool allowed = true;

            if (authorizationInfo != AuthorizationInfo.Empty)
            {
                CheckAuthorized check = new CheckAuthorized
                                            {
                                                Roles = authorizationInfo.AllowedRoles,
                                                Users = authorizationInfo.AllowedUsers
                                            };

                allowed = check.IsAuthorized(requestContext.HttpContext);
            }

            return allowed;
        }

        private sealed class CheckAuthorized : AuthorizeAttribute
        {
            public bool IsAuthorized(HttpContextBase httpContext)
            {
                return AuthorizeCore(httpContext);
            }
        }
    }
}