// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class ThemeSwitcherTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly ThemeSwitcher _themeSwitcher;

        public ThemeSwitcherTests()
        {
            const string AssetKey = "themeSwitcher";

            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _themeSwitcher = new ThemeSwitcher(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = AssetKey };
        }

        [Fact]
        public void CloseOnSelect_should_be_true_when_new_instance_is_created()
        {
            Assert.True(_themeSwitcher.CloseOnSelect);
        }

        [Fact]
        public void Should_be_able_to_set_height()
        {
            _themeSwitcher.Height = 300;

            Assert.Equal(300, _themeSwitcher.Height);
        }

        [Fact]
        public void Should_be_able_to_set_width()
        {
            _themeSwitcher.Width = 600;

            Assert.Equal(600, _themeSwitcher.Width);
        }

        [Fact]
        public void Should_be_able_to_set_button_height()
        {
            _themeSwitcher.ButtonHeight = 40;

            Assert.Equal(40, _themeSwitcher.ButtonHeight);
        }

        [Fact]
        public void GetTheme_should_return_correct_theme_from_view_date()
        {
            _themeSwitcher.Name = "myThemeSwitcher";
            _viewContext.ViewData["myThemeSwitcher"] = "Custom";

            Assert.Equal("Custom", _themeSwitcher.GetTheme());
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts()
        {
            _themeSwitcher.Name = "myThemeSwitcher";
            _themeSwitcher.Height = 300;
            _themeSwitcher.Width = 400;
            _themeSwitcher.InitialTheme = "aTheme";
            _themeSwitcher.InitialText = "Select Theme";
            _themeSwitcher.ButtonPreText = "My Theme";
            _themeSwitcher.CloseOnSelect = false;
            _themeSwitcher.ButtonHeight = 32;
            _themeSwitcher.CookieName = "myTheme";
            _themeSwitcher.OnOpen = delegate { };
            _themeSwitcher.OnSelect = delegate { };
            _themeSwitcher.OnClose = delegate { };

            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int?>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);

            _themeSwitcher.WriteInitializationScript(new Mock<TextWriter>().Object);

            writer.VerifyAll();
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _themeSwitcher.Name = "myThemeSwitcher";
            _themeSwitcher.HtmlAttributes.Add("style", "float:right");

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _themeSwitcher.Render();

            _httpContext.Verify();
        }
    }
}