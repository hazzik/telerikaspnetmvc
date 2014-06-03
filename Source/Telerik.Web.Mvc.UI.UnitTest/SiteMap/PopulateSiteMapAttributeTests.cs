namespace Telerik.Web.Mvc.UnitTest
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class PopulateSiteMapAttributeTests
    {
        private readonly PopulateSiteMapAttribute _attribute;
        private readonly Mock<SiteMapBase> _siteMap;

        public PopulateSiteMapAttributeTests()
        {
            _siteMap = new Mock<SiteMapBase>();

            SiteMapDictionary siteMaps = new SiteMapDictionary
                                                {
                                                    { "mySiteMap", _siteMap.Object }
                                                };

            _attribute = new PopulateSiteMapAttribute(siteMaps)
                             {
                                 SiteMapName = "mySiteMap"
                             };
        }

        [Fact]
        public void Default_constructor_should_not_throw_exception()
        {
            Assert.DoesNotThrow(() => new PopulateSiteMapAttribute());
        }

        [Fact]
        public void Should_be_able_to_set_Default_ViewData_Key()
        {
            PopulateSiteMapAttribute.DefaultViewDataKey = "mySiteMap";

            Assert.Equal("mySiteMap", PopulateSiteMapAttribute.DefaultViewDataKey);

            PopulateSiteMapAttribute.DefaultViewDataKey = "siteMap";
        }

        [Fact]
        public void OnExecuting_should_fill_view_data()
        {
            Mock<ControllerBase> controller = new Mock<ControllerBase>();

            ControllerContext controllerContext = new ControllerContext(TestHelper.CreateMockedHttpContext().Object, new RouteData(), controller.Object);
            ActionExecutingContext executingContext = new ActionExecutingContext(controllerContext, new Mock<ActionDescriptor>().Object, new Dictionary<string, object>());

            _attribute.OnActionExecuting(executingContext);

            Assert.Same(_siteMap.Object, controller.Object.ViewData[PopulateSiteMapAttribute.DefaultViewDataKey]);
        }

        [Fact]
        public void OnActionExecuted_does_nothing()
        {
            _attribute.OnActionExecuted(new ActionExecutedContext());
        }
    }
}