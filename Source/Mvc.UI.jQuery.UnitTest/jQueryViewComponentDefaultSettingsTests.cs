// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using Xunit;

    public class jQueryViewComponentDefaultSettingsTests
    {
        [Fact]
        public void Should_be_able_to_set_stylesheet_file()
        {
            jQueryViewComponentDefaultSettings.StyleSheetFile = "foo.css";

            Assert.Equal("foo.css", jQueryViewComponentDefaultSettings.StyleSheetFile);

            jQueryViewComponentDefaultSettings.StyleSheetFile = "jquery-ui-1.7.2.custom.css";
        }

        [Fact]
        public void Should_be_able_to_set_script_file()
        {
            jQueryViewComponentDefaultSettings.ScriptFile = "foo.js";

            Assert.Equal("foo.js", jQueryViewComponentDefaultSettings.ScriptFile);

            jQueryViewComponentDefaultSettings.ScriptFile = "jquery-ui-1.7.2.custom.js";
        }
    }
}