// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Moq;
    using Xunit;

    public class ScriptRegistrarBuilderTests
    {
        private readonly ScriptRegistrar _scriptRegistrar;
        private readonly ScriptRegistrarBuilder _builder;

        public ScriptRegistrarBuilderTests()
        {
            ViewContext viewContext = new ViewContext
                                          {
                                              HttpContext = TestHelper.CreateMockedHttpContext().Object,
                                              ViewData = new ViewDataDictionary()
                                          };

            _scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object, new Mock<ScriptWrapperBase>().Object);

            _builder = new ScriptRegistrarBuilder(_scriptRegistrar);
        }

        [Fact]
        public void ToRegistrar_should_return_internal_script_registrar()
        {
            Assert.Same(_scriptRegistrar, _builder.ToRegistrar());
        }

        [Fact]
        public void ScriptRegistrar_operator_should_return_internal_script_registrar()
        {
            ScriptRegistrar scriptRegistrar = _builder;

            Assert.Same(_scriptRegistrar, scriptRegistrar);
        }

        [Fact]
        public void AssetHandlerPath_should_set_script_manager_asset_handler_path()
        {
            const string HandlerPath = "~/assets/scripts/asset.axd";

            _builder.AssetHandlerPath(HandlerPath);

            Assert.Equal(HandlerPath, _scriptRegistrar.AssetHandlerPath);
        }

        [Fact]
        public void Should_be_able_to_configure_default_group()
        {
            int previousCount = _scriptRegistrar.DefaultGroup.Items.Count;

            _builder.DefaultGroup(group => group.Add("foo.js"));

            Assert.Equal((previousCount + 1), _scriptRegistrar.DefaultGroup.Items.Count);
        }

        [Fact]
        public void Should_be_able_to_configure_scripts()
        {
            int previousCount = _scriptRegistrar.Scripts.Count;

            _builder.Scripts(script => script.Add("~/Scripts/script1.js").Add("~/Scripts/script2"));

            Assert.Equal((previousCount + 2), _scriptRegistrar.Scripts.Count);
        }

        [Fact]
        public void OnDocumentReady_should_add_new_action_in_on_document_ready_action_collection()
        {
            Action onLoad = delegate { };

            _builder.OnDocumentReady(onLoad);

            Assert.Same(onLoad, _scriptRegistrar.OnDocumentReadyActions[_scriptRegistrar.OnDocumentReadyActions.Count - 1]);
        }

        [Fact]
        public void OnWindowUnload_should_add_new_action_in_on_window_unload_action_collection()
        {
            Action onUnload = delegate { };

            _builder.OnPageUnload(onUnload);

            Assert.Same(onUnload, _scriptRegistrar.OnWindowUnloadActions[_scriptRegistrar.OnWindowUnloadActions.Count - 1]);
        }

        [Fact]
        public void Render_should_not_throw_exception()
        {
            Assert.DoesNotThrow(() => _builder.Render());
        }
    }
}