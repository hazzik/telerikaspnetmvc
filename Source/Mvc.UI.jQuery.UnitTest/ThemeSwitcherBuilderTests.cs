// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class ThemeSwitcherBuilderTests
    {
        private readonly ThemeSwitcher _themeSwitcher;
        private readonly ThemeSwitcherBuilder _builder;

        public ThemeSwitcherBuilderTests()
        {
            _themeSwitcher = new ThemeSwitcher(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new ThemeSwitcherBuilder(_themeSwitcher);
        }

        [Fact]
        public void Should_be_able_to_set_to_initial_theme()
        {
            _builder.InitialTheme("UI lightness");

            Assert.Equal("UI lightness", _themeSwitcher.InitialTheme);
        }

        [Fact]
        public void Should_be_able_to_set_height()
        {
            _builder.Height(300);

            Assert.Equal(300, _themeSwitcher.Height);
        }

        [Fact]
        public void Should_be_able_to_set_width()
        {
            _builder.Width(400);

            Assert.Equal(400, _themeSwitcher.Width);
        }

        [Fact]
        public void Should_be_able_to_set_initial_text()
        {
            _builder.InitialText("Please select");

            Assert.Equal("Please select", _themeSwitcher.InitialText);
        }

        [Fact]
        public void Should_be_able_to_set_button_pre_text()
        {
            _builder.ButtonPreText("My Theme:");

            Assert.Equal("My Theme:", _themeSwitcher.ButtonPreText);
        }

        [Fact]
        public void Should_be_able_to_set_close_on_select()
        {
            _builder.CloseOnSelect(false);

            Assert.False(_themeSwitcher.CloseOnSelect);
        }

        [Fact]
        public void Should_be_able_to_set_button_height()
        {
            _builder.ButtonHeight(20);

            Assert.Equal(20, _themeSwitcher.ButtonHeight);
        }

        [Fact]
        public void Should_be_able_to_set_cookie_name()
        {
            _builder.CookieName("myTheme");

            Assert.Equal("myTheme", _themeSwitcher.CookieName);
        }

        [Fact]
        public void Should_be_able_to_set_on_open()
        {
            Action onOpen = delegate { };

            _builder.OnOpen(onOpen);

            Assert.Same(onOpen, _themeSwitcher.OnOpen);
        }

        [Fact]
        public void Should_be_able_to_set_on_close()
        {
            Action onClose = delegate { };

            _builder.OnClose(onClose);

            Assert.Same(onClose, _themeSwitcher.OnClose);
        }

        [Fact]
        public void Should_be_able_to_set_on_select()
        {
            Action onSelect = delegate { };

            _builder.OnSelect(onSelect);

            Assert.Same(onSelect, _themeSwitcher.OnSelect);
        }
    }
}