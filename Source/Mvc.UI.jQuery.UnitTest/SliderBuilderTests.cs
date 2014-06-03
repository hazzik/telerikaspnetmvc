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

    public class SliderBuilderTests
    {
        private readonly Slider _slider;
        private readonly SliderBuilder _builder;

        public SliderBuilderTests()
        {
            _slider = new Slider(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new SliderBuilder(_slider);
        }

        [Fact]
        public void Should_be_able_to_set_animate()
        {
            _builder.Animate(true);

            Assert.True(_slider.Animate);
        }

        [Fact]
        public void Should_be_able_to_set_orientation()
        {
            _builder.Orientation(SliderOrientation.Horizontal);

            Assert.Equal(SliderOrientation.Horizontal, _slider.Orientation);
        }

        [Fact]
        public void Should_be_able_to_set_range()
        {
            _builder.Range(SliderRange.ToMaximum);

            Assert.Equal(SliderRange.ToMaximum, _slider.Range);
        }

        [Fact]
        public void Should_be_able_to_set_value()
        {
            _builder.Value(10);

            Assert.Equal(10, _slider.Value);
        }

        [Fact]
        public void Value_should_throw_exception_when_range_is_true()
        {
            _builder.Range(SliderRange.True);

            Assert.Throws<InvalidOperationException>(() => _builder.Value(20));
        }

        [Fact]
        public void Should_be_able_to_set_values()
        {
            _builder.Range(SliderRange.True);
            _builder.Values(20, 30);

            Assert.Equal(20, _slider.GetValues()[0]);
            Assert.Equal(30, _slider.GetValues()[1]);
        }

        [Fact]
        public void Values_should_throw_exception_when_range_is_true()
        {
            _builder.Range(SliderRange.ToMaximum);

            Assert.Throws<InvalidOperationException>(() => _builder.Values(20, 30));
        }

        [Fact]
        public void Should_be_able_to_set_updated_elements()
        {
            _builder.UpdateElements("#foo", "#bar", "#baz", "#yada");

            Assert.Equal(4, _slider.UpdateElements.Count);
        }

        [Fact]
        public void UpdateElements_should_throw_exception_when_range_is_true_and_selectors_is_not_properly_paired()
        {
            _builder.Range(SliderRange.True);

            Assert.Throws<InvalidOperationException>(() => _builder.UpdateElements("#foo", "#bar", "#baz"));
        }

        [Fact]
        public void Should_be_able_to_set_minimum()
        {
            _builder.Minimum(100);

            Assert.Equal(100, _slider.Minimum);
        }

        [Fact]
        public void Should_be_able_to_set_maximum()
        {
            _builder.Maximum(1000);

            Assert.Equal(1000, _slider.Maximum);
        }

        [Fact]
        public void Should_be_able_to_set_steps()
        {
            _builder.Steps(10);

            Assert.Equal(10, _slider.Steps);
        }

        [Fact]
        public void Should_be_able_to_set_on_start()
        {
            Action onStart = delegate { };

            _builder.OnStart(onStart);

            Assert.Same(onStart, _slider.OnStart);
        }

        [Fact]
        public void Should_be_able_to_set_on_slide()
        {
            Action onSlide = delegate { };

            _builder.OnSlide(onSlide);

            Assert.Same(onSlide, _slider.OnSlide);
        }

        [Fact]
        public void Should_be_able_to_set_on_change()
        {
            Action onChange = delegate { };

            _builder.OnChange(onChange);

            Assert.Same(onChange, _slider.OnChange);
        }

        [Fact]
        public void Should_be_able_to_set_on_stop()
        {
            Action onStop = delegate { };

            _builder.OnStop(onStop);

            Assert.Same(onStop, _slider.OnStop);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _slider.Theme);
        }
    }
}