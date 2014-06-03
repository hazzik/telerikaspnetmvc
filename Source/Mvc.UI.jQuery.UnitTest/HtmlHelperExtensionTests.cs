// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class HtmlHelperExtensionTests
    {
        [Fact]
        public void jQuery_should_return_new_instance()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IWebAssetItemMerger>()).Returns(new Mock<IWebAssetItemMerger>().Object);
            locator.Setup(l => l.Resolve<ScriptWrapperBase>()).Returns(new Mock<ScriptWrapperBase>().Object);
            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IClientSideObjectWriterFactory>()).Returns(new Mock<IClientSideObjectWriterFactory>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            HtmlHelper helper = TestHelper.CreateHtmlHelper();

            jQueryViewComponentFactory factory1 = helper.jQuery();
            jQueryViewComponentFactory factory2 = helper.jQuery();

            Assert.NotSame(factory1, factory2);
        }
    }
}