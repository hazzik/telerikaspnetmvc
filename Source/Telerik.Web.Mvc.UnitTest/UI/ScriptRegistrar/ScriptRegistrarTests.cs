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

    public class ScriptRegistrarTests
    {
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly ViewContext _viewContext;
        private readonly WebAssetItemCollection _scripts;
        private readonly IList<IScriptableComponent> _scriptableComponents;
        private readonly Mock<IWebAssetItemMerger> _assetMerger;
        private readonly Mock<ScriptWrapperBase> _scriptWrapper;

        private readonly ScriptRegistrar _scriptRegistrar;

        public ScriptRegistrarTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _scripts = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath);
            _scriptableComponents = new List<IScriptableComponent>();
            _assetMerger = new Mock<IWebAssetItemMerger>();
            _scriptWrapper = new Mock<ScriptWrapperBase>();

            _viewContext = new ViewContext
                               {
                                   HttpContext = _httpContext.Object,
                                   ViewData = new ViewDataDictionary()
                               };

            _scriptRegistrar = new ScriptRegistrar(_scripts, _scriptableComponents, _viewContext, _assetMerger.Object, _scriptWrapper.Object);
        }

        [Fact]
        public void Should_throw_exception_when_new_instance_is_created_for_the_same_http_context()
        {
            Assert.Throws<InvalidOperationException>(() => new ScriptRegistrar(_scripts, _scriptableComponents, _viewContext, _assetMerger.Object, _scriptWrapper.Object));
        }

        [Fact]
        public void Should_be_able_to_set_framework_script_path()
        {
            ScriptRegistrar.FrameworkScriptPath = "~/JavaScripts";

            Assert.Equal("~/JavaScripts", ScriptRegistrar.FrameworkScriptPath);

            ScriptRegistrar.FrameworkScriptPath = "~/Scripts";
        }

        [Fact]
        public void Should_be_able_to_change_framework_script_file_names()
        {
            ScriptRegistrar.FrameworkScriptFileNames.Add("foo.js");

            Assert.Equal(2, ScriptRegistrar.FrameworkScriptFileNames.Count);

            ScriptRegistrar.FrameworkScriptFileNames.RemoveAt(1);
        }

        [Fact]
        public void AssetHandlerPath_should_be_set_to_default_asset_handler_path_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetHttpHandler.DefaultPath, _scriptRegistrar.AssetHandlerPath);
        }

        [Fact]
        public void Register_should_add_specified_component_in_scriptable_component_collection()
        {
            Mock<IScriptableComponent> component = new Mock<IScriptableComponent>();

            _scriptRegistrar.Register(component.Object);

            Assert.Contains(component.Object, _scriptableComponents);
        }

        [Fact]
        public void Register_should_not_add_the_same_component_more_than_once()
        {
            Mock<IScriptableComponent> component = new Mock<IScriptableComponent>();

            _scriptRegistrar.Register(component.Object);
            _scriptRegistrar.Register(component.Object);

            Assert.Equal(1, _scriptableComponents.Count);
        }

        [Fact]
        public void Should_be_able_to_render()
        {
            SetupForRender();
            _scriptRegistrar.Render();

            _httpContext.Verify();
        }

        [Fact]
        public void Render_should_throw_exception_when_called_more_than_once()
        {
            SetupForRender();
            _scriptRegistrar.Render(); // Call once

            Assert.Throws<InvalidOperationException>(() => _scriptRegistrar.Render()); // Call Twice
        }

        private void SetupForRender()
        {
            Mock<IScriptableComponent> component1 = new Mock<IScriptableComponent>();

            component1.SetupGet(c => c.AssetKey).Returns("foo");
            component1.SetupGet(c => c.ScriptFilesPath).Returns(WebAssetDefaultSettings.ScriptFilesPath);
            component1.SetupGet(c => c.ScriptFileNames).Returns(new List<string> { "site1.js", "site2.js" });

            _scriptRegistrar.Register(component1.Object);

            Mock<IScriptableComponent> component2 = new Mock<IScriptableComponent>();

            component2.SetupGet(c => c.ScriptFilesPath).Returns(WebAssetDefaultSettings.ScriptFilesPath);
            component2.SetupGet(c => c.ScriptFileNames).Returns(new List<string> { "site3.js", "site4.js" });

            _scriptRegistrar.Register(component2.Object);

            _assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<WebAssetItemCollection>())).Returns(new List<string> { "/Scripts/site.js", "/Scripts/component1.js" });

            _scriptRegistrar.OnDocumentReadyActions.Add(delegate { });
            _scriptRegistrar.OnWindowUnloadActions.Add(delegate { });
            _scriptRegistrar.OnDocumentReadyActions.Add(delegate { });
            _scriptRegistrar.OnWindowUnloadActions.Add(delegate { });

            _scriptWrapper.SetupGet(w => w.OnPageLoadStart).Returns(string.Empty);
            _scriptWrapper.SetupGet(w => w.OnPageLoadEnd).Returns(string.Empty);
            _scriptWrapper.SetupGet(w => w.OnPageUnloadStart).Returns(string.Empty);
            _scriptWrapper.SetupGet(w => w.OnPageUnloadEnd).Returns(string.Empty);

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();
            _httpContext.Setup(context => context.Response.Output.WriteLine(It.IsAny<string>())).Verifiable();
        }
    }
}