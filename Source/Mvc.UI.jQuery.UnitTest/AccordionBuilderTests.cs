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

    public class AccordionBuilderTests
    {
        private readonly Accordion _accordion;
        private readonly AccordionBuilder _builder;

        public AccordionBuilderTests()
        {
            _accordion = new Accordion(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new AccordionBuilder(_accordion);
        }

        [Fact]
        public void Should_be_able_to_set_items()
        {
            _builder.Items(
                            factory =>
                            {
                                factory.Add();
                                factory.Add();
                            }
                        );

            Assert.Equal(2, _accordion.Items.Count);
        }

        [Fact]
        public void Should_be_able_to_set_animate()
        {
            _builder.Animate("bounce");

            Assert.Equal("bounce", _accordion.AnimationName);
        }

        [Fact]
        public void Should_be_able_to_set_auto_height()
        {
            _builder.AutoHeight(false);

            Assert.False(_accordion.AutoHeight);
        }

        [Fact]
        public void Should_be_able_to_set_clear_style()
        {
            _builder.ClearStyle(true);

            Assert.True(_accordion.ClearStyle);
        }

        [Fact]
        public void Should_be_able_to_set_open_on()
        {
            _builder.OpenOn("mouseover");

            Assert.Equal("mouseover", _accordion.OpenOn);
        }

        [Fact]
        public void Should_be_able_to_set_collapsible_content()
        {
            _builder.CollapsibleContent(true);

            Assert.True(_accordion.CollapsibleContent);
        }

        [Fact]
        public void Should_be_able_to_set_fill_space()
        {
            _builder.FillSpace(true);

            Assert.True(_accordion.FillSpace);
        }

        [Fact]
        public void Should_be_able_to_set_icon()
        {
            _builder.Icon("customIcon");

            Assert.Equal("customIcon", _accordion.Icon);
        }

        [Fact]
        public void Should_be_able_to_set_selected_icon()
        {
            _builder.SelectedIcon("customSelectedIcon");

            Assert.Equal("customSelectedIcon", _accordion.SelectedIcon);
        }

        [Fact]
        public void Should_be_able_to_set_on_change()
        {
            Action onChange = delegate { };

            _builder.OnChange(onChange);

            Assert.Same(onChange, _accordion.OnChange);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _accordion.Theme);
        }
    }
}