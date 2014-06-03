// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Moq;
    using Xunit;

    public class StyleSheetRegistrarTests
    {
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly ViewContext _viewContext;
        private readonly WebAssetItemCollection _styleSheets;
        private readonly IList<IStyleableComponent> _styleableComponents;
        private readonly Mock<IWebAssetItemMerger> _assetMerger;

        private readonly StyleSheetRegistrar _styleSheetRegistrar;

        public StyleSheetRegistrarTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _styleSheets = new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath);
            _styleableComponents = new List<IStyleableComponent>();
            _assetMerger = new Mock<IWebAssetItemMerger>();

            _viewContext = new ViewContext
                               {
                                   HttpContext = _httpContext.Object,
                                   ViewData = new ViewDataDictionary()
                               };

            _styleSheetRegistrar = new StyleSheetRegistrar(_styleSheets, _styleableComponents, _viewContext, _assetMerger.Object);
        }

        [Fact]
        public void Should_throw_exception_when_new_instance_is_created_for_the_same_http_context()
        {
            Assert.Throws<InvalidOperationException>(() => new StyleSheetRegistrar(_styleSheets, _styleableComponents, _viewContext, _assetMerger.Object));
        }

        [Fact]
        public void AssetHandlerPath_should_be_set_to_default_asset_handler_path_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetHttpHandler.DefaultPath, _styleSheetRegistrar.AssetHandlerPath);
        }

        [Fact]
        public void Register_should_add_specified_component_in_styleable_component_collection()
        {
            Mock<IStyleableComponent> component = new Mock<IStyleableComponent>();

            _styleSheetRegistrar.Register(component.Object);

            Assert.Contains(component.Object, _styleableComponents);
        }

        [Fact]
        public void Register_should_not_add_the_same_component_more_than_once()
        {
            Mock<IStyleableComponent> component = new Mock<IStyleableComponent>();

            _styleSheetRegistrar.Register(component.Object);
            _styleSheetRegistrar.Register(component.Object);

            Assert.Equal(1, _styleableComponents.Count);
        }

        [Fact]
        public void Render_should_write_response()
        {
            SetupForRender();

            _styleSheetRegistrar.Render();

            _httpContext.Verify();
        }

        [Fact]
        public void Render_should_throw_exception_when_called_more_than_once()
        {
            SetupForRender();
            _styleSheetRegistrar.Render(); // Call once

            Assert.Throws<InvalidOperationException>(() => _styleSheetRegistrar.Render()); // Call Twice
        }

        private void SetupForRender()
        {
            Mock<IStyleableComponent> component1 = new Mock<IStyleableComponent>();

            component1.SetupGet(c => c.AssetKey).Returns("foo");
            component1.SetupGet(c => c.StyleSheetFilesPath).Returns(WebAssetDefaultSettings.StyleSheetFilesPath);
            component1.SetupGet(c => c.StyleSheetFileNames).Returns(new List<string> { "site1.css", "site2.css" });

            _styleSheetRegistrar.Register(component1.Object);

            Mock<IStyleableComponent> component2 = new Mock<IStyleableComponent>();

            component2.SetupGet(c => c.StyleSheetFilesPath).Returns(WebAssetDefaultSettings.StyleSheetFilesPath);
            component2.SetupGet(c => c.StyleSheetFileNames).Returns(new List<string> { "site3.css", "site4.css" });

            _styleSheetRegistrar.Register(component2.Object);

            _styleSheetRegistrar.DefaultGroup.Items.Add(new WebAssetItem("~/Content/site.css"));

            _assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<WebAssetItemCollection>())).Returns(new List<string> { "/Content/site.css", "/Content/component1.css" });
            _httpContext.Setup(context => context.Response.Output.WriteLine(It.IsAny<string>())).Verifiable();
        }
    }
}