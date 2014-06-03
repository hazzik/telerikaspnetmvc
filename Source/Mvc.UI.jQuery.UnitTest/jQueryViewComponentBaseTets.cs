// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class jQueryViewComponentBaseTets
    {
        private readonly Mock<jQueryViewComponentBase> _baseComponent;

        public jQueryViewComponentBaseTets()
        {
            _baseComponent = new Mock<jQueryViewComponentBase>(new ViewContext { HttpContext = TestHelper.CreateMockedHttpContext().Object }, new Mock<IClientSideObjectWriterFactory>().Object);
        }

        [Fact]
        public void Should_be_able_to_set_hidden_input_suffix()
        {
            jQueryViewComponentBase.HiddenInputSuffix = "hdn";

            Assert.Equal("hdn", jQueryViewComponentBase.HiddenInputSuffix);

            jQueryViewComponentBase.HiddenInputSuffix = "hid";
        }

        [Fact]
        public void ScriptFileNames_should_contain_defaut_ascript_file()
        {
            Assert.Contains(jQueryViewComponentDefaultSettings.ScriptFile, _baseComponent.Object.ScriptFileNames);
        }
    }
}