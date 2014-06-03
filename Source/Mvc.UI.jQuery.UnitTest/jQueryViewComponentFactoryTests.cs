// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class jQueryViewComponentFactoryTests
    {
        private readonly jQueryViewComponentFactory _factory;

        public jQueryViewComponentFactoryTests()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IWebAssetItemMerger>()).Returns(new Mock<IWebAssetItemMerger>().Object);
            locator.Setup(l => l.Resolve<ScriptWrapperBase>()).Returns(new Mock<ScriptWrapperBase>().Object);
            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IClientSideObjectWriterFactory>()).Returns(new Mock<IClientSideObjectWriterFactory>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            _factory = new jQueryViewComponentFactory(TestHelper.CreateHtmlHelper(), new Mock<IClientSideObjectWriterFactory>().Object);
        }

        [Fact]
        public void Should_be_able_to_set_default_asset_key()
        {
            jQueryViewComponentFactory.DefaultAssetKey = "xxx";

            Assert.Equal("xxx", jQueryViewComponentFactory.DefaultAssetKey);

            jQueryViewComponentFactory.DefaultAssetKey = "jQueryUI";
        }

        [Fact]
        public void ThemeSwitcher_should_return_new_instance()
        {
            Assert.NotNull(_factory.ThemeSwitcher());
        }

        [Fact]
        public void ThemeSwitcher_should_not_have_any_asset_key()
        {
            ThemeSwitcher themeSwitcher = _factory.ThemeSwitcher();

            Assert.Null(themeSwitcher.AssetKey);
        }

        [Fact]
        public void Accordion_should_return_new_instance()
        {
            Assert.NotNull(_factory.Accordion());
        }

        [Fact]
        public void DatePicker_should_return_new_instance()
        {
            Assert.NotNull(_factory.DatePicker());
        }

        [Fact]
        public void MessageBox_should_return_new_instance()
        {
            Assert.NotNull(_factory.MessageBox());
        }

        [Fact]
        public void ProgressBar_should_return_new_instance()
        {
            Assert.NotNull(_factory.ProgressBar());
        }

        [Fact]
        public void Slider_should_return_new_instance()
        {
            Assert.NotNull(_factory.Slider());
        }

        [Fact]
        public void Tab_should_return_new_instance()
        {
            Assert.NotNull(_factory.Tab());
        }
    }
}