namespace Telerik.Web.Mvc.UnitTest
{
    using System;
    using System.Collections;
    using System.Web.Compilation;
    using System.Web.Routing;

    using Xunit;

    public class ControllerTypeCacheTests : IDisposable
    {
        public ControllerTypeCacheTests()
        {
            ControllerTypeCache.ReferencedAssemblies = (() => new ArrayList { GetType().Assembly });
        }

        public void Dispose()
        {
            ControllerTypeCache.ReferencedAssemblies = (() => BuildManager.GetReferencedAssemblies());
        }

        [Fact]
        public void GetControllerType_should_return_correct_type()
        {
            Type type = ControllerTypeCache.GetControllerType(TestHelper.CreateRequestContext(), "Home");

            Assert.Same(typeof(HomeController), type);
        }

        [Fact]
        public void GetControllerType_should_throw_exception_when_same_controller_exists_in_another_namespace()
        {
            Assert.Throws<InvalidOperationException>(() => ControllerTypeCache.GetControllerType(TestHelper.CreateRequestContext(), "DuplicateName"));
        }

        [Fact]
        public void GetController_with_namespace_should_return_correct_type()
        {
            RequestContext requestContext = TestHelper.CreateRequestContext();

            requestContext.RouteData.DataTokens.Add("Namespaces", new[] { "Telerik.Web.Mvc.UnitTest.DummyNamespace" });

            Type type = ControllerTypeCache.GetControllerType(requestContext, "DuplicateName");

            Assert.Same(typeof(DummyNamespace.DuplicateNameController), type);
        }
    }
}