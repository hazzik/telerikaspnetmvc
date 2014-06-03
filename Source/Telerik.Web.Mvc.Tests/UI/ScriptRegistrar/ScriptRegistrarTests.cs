// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Xunit;

    public class ScriptRegistrarTests
    {
        private readonly Mock<HttpContextBase> httpContext;
        private readonly ViewContext viewContext;
        private readonly WebAssetItemCollection scripts;
        private readonly IList<IScriptableComponent> scriptableComponents;
        private readonly Mock<IWebAssetItemMerger> assetMerger;
        private readonly Mock<ScriptWrapperBase> scriptWrapper;

        private readonly ScriptRegistrar scriptRegistrar;

        public ScriptRegistrarTests()
        {
            httpContext = TestHelper.CreateMockedHttpContext();
            scripts = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath);
            scriptableComponents = new List<IScriptableComponent>();
            assetMerger = new Mock<IWebAssetItemMerger>();
            scriptWrapper = new Mock<ScriptWrapperBase>();

            viewContext = new ViewContext
                               {
                                   HttpContext = httpContext.Object,
                                   ViewData = new ViewDataDictionary()
                               };

            scriptRegistrar = new ScriptRegistrar(scripts, scriptableComponents, viewContext, assetMerger.Object, scriptWrapper.Object);
        }

        [Fact]
        public void Should_throw_exception_when_new_instance_is_created_for_the_same_http_context()
        {
            Assert.Throws<InvalidOperationException>(() => new ScriptRegistrar(scripts, scriptableComponents, viewContext, assetMerger.Object, scriptWrapper.Object));
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
            Assert.Equal(WebAssetHttpHandler.DefaultPath, scriptRegistrar.AssetHandlerPath);
        }

        [Fact]
        public void Register_should_add_specified_component_in_scriptable_component_collection()
        {
            Mock<IScriptableComponent> component = new Mock<IScriptableComponent>();

            scriptRegistrar.Register(component.Object);

            Assert.Contains(component.Object, scriptableComponents);
        }

        [Fact]
        public void Register_should_not_add_the_same_component_more_than_once()
        {
            Mock<IScriptableComponent> component = new Mock<IScriptableComponent>();

            scriptRegistrar.Register(component.Object);
            scriptRegistrar.Register(component.Object);

            Assert.Equal(1, scriptableComponents.Count);
        }

        [Fact]
        public void Should_be_able_to_render()
        {
            SetupForRender();
            scriptRegistrar.Render();

            httpContext.Verify();
        }

        [Fact]
        public void Render_should_throw_exception_when_called_more_than_once()
        {
            SetupForRender();
            scriptRegistrar.Render(); // Call once

            Assert.Throws<InvalidOperationException>(() => scriptRegistrar.Render()); // Call Twice
        }

        [Fact]
        public void AssetKey_set_to_default_adds_component_scripts_to_default_group()
        {
            SetupComponent("Default");
            
            scriptRegistrar.Render();

            Assert.Equal("~/Scripts/component.js", scriptRegistrar.DefaultGroup.Items[1].Source);
        }

        [Fact]
        public void Component_asset_group_should_be_added_after_default_group()
        {
            SetupComponent("test");
            scriptRegistrar.Render();

            Assert.Equal(0, scriptRegistrar.Scripts.IndexOf(scriptRegistrar.DefaultGroup));
        }

        [Fact]
        public void Component_scripts_without_asset_group_should_be_added_after_default_group()
        {
            SetupComponent("");
            scriptRegistrar.Render();
            Assert.Equal(0, scriptRegistrar.Scripts.IndexOf(scriptRegistrar.DefaultGroup));
        }

        [Fact]
        public void CollectScriptsFiles_returns_distinct_values()
        {
            assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), 
                It.IsAny<WebAssetItemCollection>())).Returns(new []{"~/foo.js", "~/foo.js"});

            var scriptFiles = scriptRegistrar.CollectScriptFiles();
            Assert.Equal(1, scriptFiles.Count());
        }        
        
        [Fact]
        public void CollectScriptsFiles_keeps_order()
        {
            assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), 
                It.IsAny<WebAssetItemCollection>())).Returns(new []{"~/1.js", "~/2.js", "~/1.js" ,"~/3.js", "~/2.js"});

            var scriptFiles = scriptRegistrar.CollectScriptFiles();
            Assert.Equal(3, scriptFiles.Count());
            Assert.Equal("~/1.js", scriptFiles.ElementAt(0));
            Assert.Equal("~/2.js", scriptFiles.ElementAt(1));
            Assert.Equal("~/3.js", scriptFiles.ElementAt(2));
        }

        private void SetupComponent(string assetKey)
		{
			Mock<IScriptableComponent> component = new Mock<IScriptableComponent>();

            component.SetupGet(c => c.AssetKey).Returns(assetKey);
            component.SetupGet(c=> c.ScriptFilesPath).Returns("~/Scripts");
            component.SetupGet(c=> c.ScriptFileNames).Returns(new [] {"component.js"});
            scriptRegistrar.Register(component.Object);
		}

        private void SetupForRender()
        {
            Mock<IScriptableComponent> component1 = new Mock<IScriptableComponent>();

            component1.SetupGet(c => c.AssetKey).Returns("foo");
            component1.SetupGet(c => c.ScriptFilesPath).Returns(WebAssetDefaultSettings.ScriptFilesPath);
            component1.SetupGet(c => c.ScriptFileNames).Returns(new List<string> { "site1.js", "site2.js" });

            scriptRegistrar.Register(component1.Object);

            Mock<IScriptableComponent> component2 = new Mock<IScriptableComponent>();

            component2.SetupGet(c => c.ScriptFilesPath).Returns(WebAssetDefaultSettings.ScriptFilesPath);
            component2.SetupGet(c => c.ScriptFileNames).Returns(new List<string> { "site3.js", "site4.js" });

            scriptRegistrar.Register(component2.Object);

            assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<WebAssetItemCollection>())).Returns(new List<string> { "/Scripts/site.js", "/Scripts/component1.js" });

            scriptRegistrar.OnDocumentReadyActions.Add(delegate { });
            scriptRegistrar.OnWindowUnloadActions.Add(delegate { });
            scriptRegistrar.OnDocumentReadyActions.Add(delegate { });
            scriptRegistrar.OnWindowUnloadActions.Add(delegate { });

            scriptWrapper.SetupGet(w => w.OnPageLoadStart).Returns(string.Empty);
            scriptWrapper.SetupGet(w => w.OnPageLoadEnd).Returns(string.Empty);
            scriptWrapper.SetupGet(w => w.OnPageUnloadStart).Returns(string.Empty);
            scriptWrapper.SetupGet(w => w.OnPageUnloadEnd).Returns(string.Empty);

            httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();
            httpContext.Setup(context => context.Response.Output.WriteLine(It.IsAny<string>())).Verifiable();
        }
    }
}