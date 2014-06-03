namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class ActionAuthorizationCache
    {
        private static readonly ReaderWriterLockSlim controllerLock = new ReaderWriterLockSlim();
        private static readonly Dictionary<string, AuthorizationInfo> controllerCache = new Dictionary<string, AuthorizationInfo>(StringComparer.OrdinalIgnoreCase);

        private static readonly ReaderWriterLockSlim actionLock = new ReaderWriterLockSlim();
        private static readonly Dictionary<string, Dictionary<string, AuthorizationInfo>> actionCache = new Dictionary<string, Dictionary<string, AuthorizationInfo>>(StringComparer.OrdinalIgnoreCase);

        public static AuthorizationInfo GetAuthorization(RequestContext requestContext, string controllerName, string actionName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            AuthorizationInfo controllerAuthorizationInfo;

            using (controllerLock.ReadAndWrite())
            {
                if (!controllerCache.TryGetValue(controllerName, out controllerAuthorizationInfo))
                {
                    using (controllerLock.Write())
                    {
                        if (!controllerCache.TryGetValue(controllerName, out controllerAuthorizationInfo))
                        {
                            Type controllerType = ControllerTypeCache.GetControllerType(requestContext, controllerName);

                            AuthorizeAttribute authorizeAttribute = controllerType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().SingleOrDefault();

                            if (authorizeAttribute != null)
                            {
                                controllerAuthorizationInfo = new AuthorizationInfo(authorizeAttribute.Users, authorizeAttribute.Roles);
                            }

                            controllerCache.Add(controllerName, controllerAuthorizationInfo);
                        }
                    }
                }
            }

            Func<MethodInfo, bool> isActionMethod = method => typeof(ActionResult).IsAssignableFrom(method.ReturnType) && (method.GetCustomAttributes(true).OfType<NonActionAttribute>().SingleOrDefault() == null);

            Dictionary<string, AuthorizationInfo> actionAuthorizations;

            using (actionLock.ReadAndWrite())
            {
                if (!actionCache.TryGetValue(controllerName, out actionAuthorizations))
                {
                    using (actionLock.Write())
                    {
                        if (!actionCache.TryGetValue(controllerName, out actionAuthorizations))
                        {
                            Type controllerType = ControllerTypeCache.GetControllerType(requestContext, controllerName);
                            actionAuthorizations = new Dictionary<string, AuthorizationInfo>(StringComparer.OrdinalIgnoreCase);

                            IEnumerable<MethodInfo> actionMethods = controllerType.GetMethods().Where(isActionMethod);

                            foreach (MethodInfo method in actionMethods)
                            {
                                ActionNameAttribute actionNameAttribute = method.GetCustomAttributes(true).OfType<ActionNameAttribute>().SingleOrDefault();

                                string action = (actionNameAttribute != null) ? actionNameAttribute.Name : method.Name;
                                AuthorizationInfo authorizationInfo = null;

                                AuthorizeAttribute authorizeAttribute = method.GetCustomAttributes(true).OfType<AuthorizeAttribute>().SingleOrDefault();

                                if (authorizeAttribute != null)
                                {
                                    authorizationInfo = new AuthorizationInfo(authorizeAttribute.Users, authorizeAttribute.Roles);
                                }

                                actionAuthorizations.Add(action, authorizationInfo);
                            }

                            actionCache.Add(controllerName, actionAuthorizations);
                        }
                    }
                }
            }

            AuthorizationInfo actionAuthorizationInfo;

            actionAuthorizations.TryGetValue(actionName, out actionAuthorizationInfo);

            return actionAuthorizationInfo ?? controllerAuthorizationInfo ?? AuthorizationInfo.Empty;
        }
    }
}