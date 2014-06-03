namespace Telerik.Web.Mvc.UnitTest
{
    using System;
    using System.Collections;
    using System.Web.Compilation;
    using System.Web.Routing;

    using Xunit;

    public class ActionAuthorizationCacheTests : IDisposable
    {
        public ActionAuthorizationCacheTests()
        {
            ControllerTypeCache.ReferencedAssemblies = (() => new ArrayList { GetType().Assembly });
        }

        public void Dispose()
        {
            ControllerTypeCache.ReferencedAssemblies = (() => BuildManager.GetReferencedAssemblies());
        }

        [Fact]
        public void GetAuthorization_should_return_correct_users_from_action_attributes()
        {
            AuthorizationInfo info = ActionAuthorizationCache.GetAuthorization(TestHelper.CreateRequestContext(), "Product", "Detail");

            Assert.Equal("Mort, Elvis, Einstein", info.AllowedUsers);
        }

        [Fact]
        public void GetAuthorization_should_return_correct_users_from_controller_attributes()
        {
            AuthorizationInfo info = ActionAuthorizationCache.GetAuthorization(TestHelper.CreateRequestContext(), "Home", "Index");

            Assert.Equal("Mort, Elvis, Einstein", info.AllowedUsers);
        }

        [Fact]
        public void GetAuthorization_should_return_correct_roles_from_namespaced_controller()
        {
            RequestContext context = TestHelper.CreateRequestContext();
            context.RouteData.DataTokens.Add("Namespaces", new[] { "Telerik.Web.Mvc.UnitTest.DummyNamespace" });

            AuthorizationInfo info = ActionAuthorizationCache.GetAuthorization(context, "DuplicateName", "AMethod");

            Assert.Equal("User, Power User, Admin", info.AllowedRoles);
        }
    }
}