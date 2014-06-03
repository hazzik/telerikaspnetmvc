namespace Telerik.Web.Mvc.UnitTest
{
    using System;
    using System.Collections;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class ControllerAuthorizationTests : IDisposable
    {
        private readonly ControllerAuthorization _controllerAuthorization;

        public ControllerAuthorizationTests()
        {
            TestHelper.RegisterDummyRoutes();
            ControllerTypeCache.ReferencedAssemblies = (() => new ArrayList { GetType().Assembly });

            _controllerAuthorization = new ControllerAuthorization();
        }

        public void Dispose()
        {
            ControllerTypeCache.ReferencedAssemblies = (() => BuildManager.GetReferencedAssemblies());
        }

        [Fact]
        public void IsAccessibleToUser_for_route_should_return_true_for_authorized_user_and_role()
        {
            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            identity.SetupGet(i => i.Name).Returns("Elvis");

            Mock<IPrincipal> user = new Mock<IPrincipal>();
            user.SetupGet(u => u.Identity).Returns(identity.Object);
            user.Setup(u => u.IsInRole("Admin")).Returns(true);

            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();
            httpContext.Setup(context => context.User).Returns(user.Object);

            Assert.True(_controllerAuthorization.IsAccessibleToUser(new RequestContext(httpContext.Object, new RouteData()), "Default"));
        }

        [Fact]
        public void IsAccessibleToUser_for_action_should_return_true_for_authorized_user()
        {
            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            identity.SetupGet(i => i.Name).Returns("Einstein");

            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();
            httpContext.SetupGet(context => context.User.Identity).Returns(identity.Object);

            Assert.True(_controllerAuthorization.IsAccessibleToUser(new RequestContext(httpContext.Object, new RouteData()), "Product", "Detail"));
        }
    }
}