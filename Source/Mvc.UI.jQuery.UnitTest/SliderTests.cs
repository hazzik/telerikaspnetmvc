// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class SliderTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly Slider _slider;

        public SliderTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _slider = new Slider(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void Orientation_should_be_default_when_new_instance_is_created()
        {
            Assert.Equal(SliderOrientation.Default, _slider.Orientation);
        }

        [Fact]
        public void Range_should_be_false_when_new_instance_is_created()
        {
            Assert.Equal(SliderRange.False, _slider.Range);
        }

        [Fact]
        public void UpdateElements_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_slider.UpdateElements);
        }

        [Fact]
        public void Should_be_able_to_set_value()
        {
            _slider.Value = 20;

            Assert.Equal(20, _slider.Value);
        }

        [Fact]
        public void Value_should_throw_exception_when_range_is_set_to_true()
        {
            _slider.Range = SliderRange.True;

            Assert.Throws<InvalidOperationException>(() => _slider.Value = 20);
        }

        [Fact]
        public void Value_should_reset_values()
        {
            _slider.Range = SliderRange.True;
            _slider.SetValues(10, 20);

            _slider.Range = SliderRange.False;

            _slider.Value = 20;

            Assert.Null(_slider.GetValues());
        }

        [Fact]
        public void Should_be_able_to_set_minimum()
        {
            _slider.Minimum = -100;

            Assert.Equal(-100, _slider.Minimum);
        }

        [Fact]
        public void Should_be_able_to_set_maximum()
        {
            _slider.Maximum = 1000;

            Assert.Equal(1000, _slider.Maximum);
        }

        [Fact]
        public void Should_be_able_to_set_steps()
        {
            _slider.Steps = 10;

            Assert.Equal(10, _slider.Steps);
        }

        [Fact]
        public void Should_be_able_to_set_values()
        {
            _slider.Range = SliderRange.True;

            _slider.SetValues(10, 20);

            int[] values = _slider.GetValues();

            Assert.Equal(10, values[0]);
            Assert.Equal(20, values[1]);
        }

        [Fact]
        public void SetValues_should_throw_exception_when_range_is_not_set_to_true()
        {
            Assert.Throws<InvalidOperationException>(() => _slider.SetValues(10, 20));
        }

        [Fact]
        public void SetValues_should_reset_value()
        {
            _slider.Value = 20;

            _slider.Range = SliderRange.True;
            _slider.SetValues(10, 20);

            Assert.Equal(0, _slider.Value);
        }

        [Fact]
        public void GetValueInternal_should_return_correct_value_when_value_is_retrieved_from_model_state()
        {
            _viewContext.ViewData.ModelState.Add("mySlider", new ModelState { Value = new ValueProviderResult("10", "10", Culture.Current) });

            _slider.Name = "mySlider";

            int? value = _slider.GetValueInternal();

            Assert.Equal(10, value);
        }

        [Fact]
        public void GetValueInternal_should_return_correct_value_when_value_is_retrieved_from_view_data()
        {
            _viewContext.ViewData["mySlider"] = 10;

            _slider.Name = "mySlider";

            int? value = _slider.GetValueInternal();

            Assert.Equal(10, value);
        }

        [Fact]
        public void GetValuesInternal_should_return_correct_values_when_values_are_retrieved_from_model_state()
        {
            _viewContext.ViewData.ModelState.Add("mySlider", new ModelState { Value = new ValueProviderResult(new[] { "10", "20" }, "10,20", Culture.Current) });

            _slider.Name = "mySlider";

            IList<int> values = _slider.GetValuesInternal();

            Assert.Equal(10, values[0]);
            Assert.Equal(20, values[1]);
        }

        [Fact]
        public void GetValuesInternal_should_return_correct_values_when_values_are_retrieved_from_view_data()
        {
            _viewContext.ViewData["mySlider"] = new[] { 10, 20 };

            _slider.Name = "mySlider";

            IList<int> values = _slider.GetValuesInternal();

            Assert.Equal(10, values[0]);
            Assert.Equal(20, values[1]);
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts_for_non_ranged()
        {
            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();
            SetupForWriteInitializationScript(writer);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int?>())).Returns(writer.Object);

            _slider.Range = SliderRange.False;
            _slider.Value = 60;

            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Setup(w => w.Write(It.IsAny<string>()));

            _slider.WriteInitializationScript(textWriter.Object);

            writer.VerifyAll();
            textWriter.VerifyAll();
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts_for_for_ranged()
        {
            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();
            SetupForWriteInitializationScript(writer);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<IList<int>>())).Returns(writer.Object);

            _slider.Range = SliderRange.True;
            _slider.SetValues(20, 40);

            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Setup(w => w.Write(It.IsAny<string>()));

            _slider.WriteInitializationScript(textWriter.Object);

            writer.VerifyAll();
            textWriter.VerifyAll();
        }

        [Fact]
        public void Render_for_value_should_write_in_response()
        {
            SetupForWriteHtml();

            _slider.Value = 20;
            _slider.Render();

            _httpContext.Verify();
        }

        [Fact]
        public void Render_for_values_and_theme_should_write_in_response()
        {
            SetupForWriteHtml();

            _slider.Theme = "custom";
            _slider.Range = SliderRange.True;
            _slider.SetValues(30, 80);

            _slider.Render();

            _httpContext.Verify();
        }

        private void SetupForWriteInitializationScript(Mock<IClientSideObjectWriter> writer)
        {
            _slider.Name = "mySlider";
            _slider.Animate = true;
            _slider.Orientation = SliderOrientation.Horizontal;
            _slider.UpdateElements.AddRange(new[] { "#foo", "#bar" });
            _slider.Minimum = 10;
            _slider.Maximum = 200;
            _slider.Steps = 5;
            _slider.OnStart = delegate { };
            _slider.OnSlide = delegate { };
            _slider.OnStop = delegate { };
            _slider.OnChange = delegate { };

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int?>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<SliderOrientation>(), It.IsAny<SliderOrientation>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<SliderRange>(), It.IsAny<SliderRange>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);
        }

        private void SetupForWriteHtml()
        {
            _slider.Name = "mySlider";
            _slider.HtmlAttributes.Add("style", "border:1px solid #222");

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();
        }
    }
}