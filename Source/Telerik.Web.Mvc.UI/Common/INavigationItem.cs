namespace Telerik.Web.Mvc
{
    using System.Collections.Generic;

    public interface INavigationItem
    {
        string RouteName
        {
            get;
        }

        string ControllerName
        {
            get;
        }

        string ActionName
        {
            get;
        }

        IDictionary<string, object> RouteValues
        {
            get;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Url might not be a valid uri.")]
        string Url
        {
            get;
        }
    }
}