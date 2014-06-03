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

    public class TabBuilderTests
    {
        private readonly Tab _tab;
        private readonly TabBuilder _builder;

        public TabBuilderTests()
        {
            _tab = new Tab(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new TabBuilder(_tab);
        }

        [Fact]
        public void Should_be_able_to_set_items()
        {
            _builder.Items(item =>
                                   {
                                       item.Add();
                                       item.Add();
                                   }
                          );

            Assert.Equal(2, _tab.Items.Count);
        }

        [Fact]
        public void Should_be_able_to_set_animation_with_numeric_value()
        {
            _builder.Animation("toggle", 700);

            Assert.Equal("toggle", _tab.AnimationOpacity);
            Assert.Equal(700, _tab.AnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_animation_with_enum()
        {
            _builder.Animation("toggle", AnimationDuration.Normal);

            Assert.Equal((int)AnimationDuration.Normal, _tab.AnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_open_animation_with_numeric_value()
        {
            _builder.OpenAnimation("fadeOut", 300);

            Assert.Equal("fadeOut", _tab.OpenAnimationOpacity);
            Assert.Equal(300, _tab.OpenAnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_open_animation_with_enum()
        {
            _builder.OpenAnimation("fadeOut", AnimationDuration.Fast);

            Assert.Equal((int)AnimationDuration.Fast, _tab.OpenAnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_close_animation_with_numeric_value()
        {
            _builder.CloseAnimation("fadeIn", 300);

            Assert.Equal("fadeIn", _tab.CloseAnimationOpacity);
            Assert.Equal(300, _tab.CloseAnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_close_animation_with_enum()
        {
            _builder.CloseAnimation("fadeIn", AnimationDuration.Slow);

            Assert.Equal((int)AnimationDuration.Slow, _tab.CloseAnimationDuration);
        }

        [Fact]
        public void Should_be_able_to_set_open_on()
        {
            _builder.OpenOn("mouseover");

            Assert.Equal("mouseover", _tab.OpenOn);
        }

        [Fact]
        public void Should_be_able_to_set_collapsible_content()
        {
            _builder.CollapsibleContent(true);

            Assert.True(_tab.CollapsibleContent);
        }

        [Fact]
        public void Should_be_able_to_set_cache_ajax_response()
        {
            _builder.CacheAjaxResponse(true);

            Assert.True(_tab.CacheAjaxResponse);
        }

        [Fact]
        public void Should_be_able_to_set_spinner_text()
        {
            _builder.SpinnerText("Please wait...");

            Assert.Equal("Please wait...", _tab.SpinnerText);
        }

        [Fact]
        public void Should_be_able_to_rotate()
        {
            _builder.Rotate(1000);

            Assert.Equal(1000, _tab.RotationDurationInMilliseconds);
            Assert.False(_tab.RotationContinue);
        }

        [Fact]
        public void Should_be_able_to_rotate_with_continue()
        {
            _builder.RotateWithContinue(1000);

            Assert.Equal(1000, _tab.RotationDurationInMilliseconds);
            Assert.True(_tab.RotationContinue);
        }

        [Fact]
        public void Should_be_able_to_set_on_select()
        {
            Action onSelect = delegate { };

            _builder.OnSelect(onSelect);

            Assert.Same(onSelect, _tab.OnSelect);
        }

        [Fact]
        public void Should_be_able_to_set_on_show()
        {
            Action onShow = delegate { };

            _builder.OnShow(onShow);

            Assert.Same(onShow, _tab.OnShow);
        }

        [Fact]
        public void Should_be_able_to_set_on_add()
        {
            Action onAdd = delegate { };

            _builder.OnAdd(onAdd);

            Assert.Same(onAdd, _tab.OnAdd);
        }

        [Fact]
        public void Should_be_able_to_set_on_remove()
        {
            Action onRemove = delegate { };

            _builder.OnRemove(onRemove);

            Assert.Same(onRemove, _tab.OnRemove);
        }

        [Fact]
        public void Should_be_able_to_set_on_enable()
        {
            Action onEnable = delegate { };

            _builder.OnEnable(onEnable);

            Assert.Same(onEnable, _tab.OnEnable);
        }

        [Fact]
        public void Should_be_able_to_set_on_disable()
        {
            Action onDisable = delegate { };

            _builder.OnDisable(onDisable);

            Assert.Same(onDisable, _tab.OnDisable);
        }

        [Fact]
        public void Should_be_able_to_set_on_load()
        {
            Action onLoad = delegate { };

            _builder.OnLoad(onLoad);

            Assert.Same(onLoad, _tab.OnLoad);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _tab.Theme);
        }
    }
}