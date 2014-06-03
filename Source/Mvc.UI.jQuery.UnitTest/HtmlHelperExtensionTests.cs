// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Web.Mvc;

    using Telerik.Web.Mvc;

    using Xunit;

    public class HtmlHelperExtensionTests
    {
        [Fact]
        public void jQuery_should_return_new_instance()
        {
            HtmlHelper helper = TestHelper.CreateHtmlHelper();

            jQueryViewComponentFactory factory1 = helper.jQuery();
            jQueryViewComponentFactory factory2 = helper.jQuery();

            Assert.NotSame(factory1, factory2);
        }
    }
}