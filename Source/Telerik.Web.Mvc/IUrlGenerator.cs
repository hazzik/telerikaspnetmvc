namespace Telerik.Web.Mvc
{
    using System.Web.Routing;

    public interface IUrlGenerator
    {
        string Generate(RequestContext requestContext, INavigatable navigationItem);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "Should accept url as string.")]
        string Generate(RequestContext requestContext, string url);
    }
}