// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Moq;
    using Xunit;

    public class ViewComponentFactoryTests
    {
        private readonly ViewComponentFactory _factory;

        public ViewComponentFactoryTests()
        {
            ViewContext viewContext = new ViewContext
                                          {
                                              HttpContext = TestHelper.CreateMockedHttpContext().Object,
                                              ViewData = new ViewDataDictionary()
                                          };

            StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), new List<IStyleableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object);
            StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(styleSheetRegistrar);

            ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object, new Mock<ScriptWrapperBase>().Object);
            ScriptRegistrarBuilder scriptRegistrarBuilder = new ScriptRegistrarBuilder(scriptRegistrar);

            _factory = new ViewComponentFactory(styleSheetRegistrarBuilder, scriptRegistrarBuilder);
        }

        [Fact]
        public void StyleSheetManager_should_return_the_same_instace()
        {
            StyleSheetRegistrarBuilder sm1 = _factory.StyleSheetRegistrar();
            StyleSheetRegistrarBuilder sm2 = _factory.StyleSheetRegistrar();

            Assert.Same(sm1, sm2);
        }

        [Fact]
        public void ScriptManager_should_return_the_same_instace()
        {
            ScriptRegistrarBuilder sm1 = _factory.ScriptRegistrar();
            ScriptRegistrarBuilder sm2 = _factory.ScriptRegistrar();

            Assert.Same(sm1, sm2);
        }
    }
}