// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class UrlResolverTests
    {
        private readonly UrlResolver _resolver;

        public UrlResolverTests()
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();
            UrlHelper urlHelper = new UrlHelper(new RequestContext(httpContext.Object, new RouteData()));

            _resolver = new UrlResolver(urlHelper);
        }

        [Fact]
        public void Resolve_should_return_correct_relative_path()
        {
            string path = _resolver.Resolve("~/scripts/jquery-1.3.2.min.js");

            Assert.NotEqual(string.Empty, path);
        }
    }
}