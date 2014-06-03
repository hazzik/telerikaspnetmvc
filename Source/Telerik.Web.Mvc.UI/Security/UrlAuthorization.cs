namespace Telerik.Web.Mvc
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Routing;

    public interface IUrlAuthorization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "Url might not be a valid uri.")]
        bool IsAccessibleToUser(RequestContext requestContext, string url);
    }

    public class UrlAuthorization : IUrlAuthorization
    {
        private static readonly SiteMapProvider provider = CreateProvider();

        public bool IsAccessibleToUser(RequestContext requestContext, string url)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(url, "url");

            InternalSiteMapNode node = new InternalSiteMapNode(provider, url.ToLower(Culture.Invariant), url);
            bool allowed = node.IsAccessibleToUser(requestContext.HttpContext);

            return allowed;
        }

        private static SiteMapProvider CreateProvider()
        {
            SiteMapProvider xmlProvider = new XmlSiteMapProvider();

            xmlProvider.Initialize("internal", new NameValueCollection { { "securityTrimmingEnabled", "true" } });

            return xmlProvider;
        }

        private sealed class InternalSiteMapNode : System.Web.SiteMapNode
        {
            public InternalSiteMapNode(SiteMapProvider provider, string key, string url) : base(provider, key, url)
            {
            }

            /// ReSharper disable UnusedParameter.Local
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "httpContext", Justification = "To align with the HttpContextbase design.")]
            public bool IsAccessibleToUser(HttpContextBase httpContext)
            /// ReSharper restore UnusedParameter.Local
            {
                /// The New HttpContextBase/HttpContextWrapper does not expose the inner HttpContext
                /// which is a design smell of the ASP.NET Team, so we need have to use the
                /// untestable HttpContext.Current.

                return IsAccessibleToUser(HttpContext.Current);
            }
        }
    }
}