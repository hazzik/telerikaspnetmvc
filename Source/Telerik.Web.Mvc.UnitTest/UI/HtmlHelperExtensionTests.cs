// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Web.Mvc;

    using Xunit;

    public class HtmlHelperExtensionTests
    {
        [Fact]
        public void Telerik_should_always_return_the_same_instance()
        {
            HtmlHelper helper = TestHelper.CreateHtmlHelper();

            ViewComponentFactory factory1 = helper.Telerik();
            ViewComponentFactory factory2 = helper.Telerik();

            Assert.Same(factory1, factory2);
        }
    }
}