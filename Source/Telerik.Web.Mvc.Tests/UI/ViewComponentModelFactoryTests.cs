// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Tests
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Moq;
    using Xunit;
    using Infrastructure;

    public class ViewComponentModelFactoryTests
    {
#if MVC2
        private readonly ViewComponentFactory<TestModel> _factory;
        private readonly HtmlHelper<TestModel> htmlHelper;

        public ViewComponentModelFactoryTests()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            ViewContext viewContext = new ViewContext
                                          {
                                              HttpContext = TestHelper.CreateMockedHttpContext().Object,
                                              ViewData = new ViewDataDictionary()
                                          };

            StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, new Mock<IWebAssetItemMerger>().Object);
            StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(styleSheetRegistrar);

            ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object, new Mock<ScriptWrapperBase>().Object);
            ScriptRegistrarBuilder scriptRegistrarBuilder = new ScriptRegistrarBuilder(scriptRegistrar);

            htmlHelper = TestHelper.CreateHtmlHelper<TestModel>();
            htmlHelper.ViewData.Model = new TestModel { ID = 1, DoubleProperty = 1.0, DecimalProperty= 1.0m, DateTimeProperty = DateTime.Today};

            _factory = new ViewComponentFactory<TestModel>(htmlHelper, new Mock<IClientSideObjectWriterFactory>().Object, styleSheetRegistrarBuilder, scriptRegistrarBuilder);
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.NumericTextBoxFor(m=>m.ID));
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.NumericTextBoxFor(m => m.ID);

            Assert.Equal("ID", builder.ToComponent().Name);
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance_with_set_value()
        {
            var builder = _factory.NumericTextBoxFor(m => m.ID);

            Assert.Equal(1, builder.ToComponent().Value.Value);
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance_with_set_name_even_type_is_null()
        {
            var builder = _factory.NumericTextBoxFor(m => m.NullableProperty);

            Assert.Equal("NullableProperty", builder.ToComponent().Name);
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance_and_value_should_be_null()
        {
            var builder = _factory.NumericTextBoxFor(m => m.NullableProperty);

            Assert.Equal(null, builder.ToComponent().Value);
        }

        [Fact]
        public void NumericTextBoxFor_should_return_new_instance_with_set_min_and_max()
        {
            var builder = _factory.NumericTextBoxFor(m => m.DoubleProperty);

            Assert.Equal(1, builder.ToComponent().MinValue);
            Assert.Equal(30, builder.ToComponent().MaxValue);
        }

        [Fact]
        public void IntegerTextBoxFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.IntegerTextBoxFor(m => m.ID));
        }

        [Fact]
        public void IntegerTextBoxFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.IntegerTextBoxFor(m => m.ID);

            Assert.Equal("ID", builder.ToComponent().Name);
        }

        [Fact]
        public void IntegerTextBoxFor_should_return_new_instance_with_set_value()
        {
            var builder = _factory.IntegerTextBoxFor(m => m.ID);

            Assert.Equal(1, builder.ToComponent().Value.Value);
        }

        [Fact]
        public void IntegerTextBoxFor_should_return_new_instance_with_set_name_even_type_is_null()
        {
            var builder = _factory.IntegerTextBoxFor(m => m.NullableProperty);

            Assert.Equal("NullableProperty", builder.ToComponent().Name);
        }

        [Fact]
        public void IntegerTextBoxFor_should_return_new_instance_and_value_should_be_null()
        {
            var builder = _factory.IntegerTextBoxFor(m => m.NullableProperty);

            Assert.Equal(null, builder.ToComponent().Value);
        }

        [Fact]
        public void PercentTextBoxFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.PercentTextBoxFor(m => m.DoubleProperty));
        }

        [Fact]
        public void PercentTextBoxFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.PercentTextBoxFor(m => m.DoubleProperty);

            Assert.Equal("DoubleProperty", builder.ToComponent().Name);
        }

        [Fact]
        public void PercentTextBoxFor_should_return_new_instance_with_set_value()
        {
            var builder = _factory.PercentTextBoxFor(m => m.DoubleProperty);

            Assert.Equal(1.0, builder.ToComponent().Value.Value);
        }

        [Fact]
        public void PercentTextBoxFor_should_return_new_instance_with_set_name_even_type_is_null()
        {
            var builder = _factory.PercentTextBoxFor(m => m.NullableDouble);

            Assert.Equal("NullableDouble", builder.ToComponent().Name);
        }

        [Fact]
        public void PercentTextBoxFor_should_return_new_instance_and_value_should_be_null()
        {
            var builder = _factory.PercentTextBoxFor(m => m.NullableDouble);

            Assert.Equal(null, builder.ToComponent().Value);
        }

        [Fact]
        public void CurrencyTextBoxFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.CurrencyTextBoxFor(m => m.DecimalProperty));
        }

        [Fact]
        public void CurrencyTextBoxFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.CurrencyTextBoxFor(m => m.DecimalProperty);

            Assert.Equal("DecimalProperty", builder.ToComponent().Name);
        }

        [Fact]
        public void CurrencyTextBoxFor_should_return_new_instance_with_set_value()
        {
            var builder = _factory.CurrencyTextBoxFor(m => m.DecimalProperty);

            Assert.Equal(1.0m, builder.ToComponent().Value.Value);
        }

        [Fact]
        public void CurrencyTextBoxFor_should_return_new_instance_with_set_name_even_type_is_null()
        {
            var builder = _factory.CurrencyTextBoxFor(m => m.NullableDecimal);

            Assert.Equal("NullableDecimal", builder.ToComponent().Name);
        }

        [Fact]
        public void CurrencyTextBoxFor_should_return_new_instance_and_value_should_be_null()
        {
            var builder = _factory.CurrencyTextBoxFor(m => m.NullableDecimal);

            Assert.Equal(null, builder.ToComponent().Value);
        }
        
        [Fact]
        public void DatePickerFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.DatePickerFor(m => m.DateTimeProperty));
        }

        [Fact]
        public void DatePickerFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.DatePickerFor(m => m.DateTimeProperty);

            Assert.Equal("DateTimeProperty", builder.ToComponent().Name);
        }

        [Fact]
        public void DatePickerFor_should_return_new_instance_with_set_value()
        {
            var builder = _factory.DatePickerFor(m => m.DateTimeProperty);

            Assert.Equal(DateTime.Today, builder.ToComponent().Value.Value);
        }

        [Fact]
        public void DatePickerFor_should_return_new_instance_with_set_name_even_type_is_null()
        {
            var builder = _factory.DatePickerFor(m => m.NullableDateTime);

            Assert.Equal("NullableDateTime", builder.ToComponent().Name);
        }

        [Fact]
        public void DatePickerFor_should_return_new_instance_and_value_should_be_null()
        {
            var builder = _factory.DatePickerFor(m => m.NullableDateTime);

            Assert.Equal(null, builder.ToComponent().Value);
        }

        [Fact]
        public void DatePickerFor_should_return_new_instance_with_set_min_and_max()
        {
            var builder = _factory.DatePickerFor(m => m.DateTimeProperty);

            Assert.Equal(new DateTime(2000, 10, 10), builder.ToComponent().MinDate);
            Assert.Equal(new DateTime(2020, 10, 10), builder.ToComponent().MaxDate);
        }

        [Fact]
        public void DropDownListFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.DropDownListFor(m => m.ID));
        }

        [Fact]
        public void DropDownListFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.DropDownListFor(m => m.ID);

            Assert.Equal("ID", builder.ToComponent().Name);
        }

        [Fact]
        public void ComboBoxFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.ComboBoxFor(m => m.ID));
        }

        [Fact]
        public void ComboBoxFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.ComboBoxFor(m => m.ID);

            Assert.Equal("ID", builder.ToComponent().Name);
        }

        [Fact]
        public void AutoCompleteFor_should_return_new_instance()
        {
            Assert.NotNull(_factory.AutoCompleteFor(m => m.ID));
        }

        [Fact]
        public void AutoCompleteFor_should_return_new_instance_with_set_name()
        {
            var builder = _factory.AutoCompleteFor(m => m.ID);

            Assert.Equal("ID", builder.ToComponent().Name);
        }

        public class TestModel
        {
            public int ID { get; set; }

            [Range(1, 30)]
            public double DoubleProperty { get; set; }
            
            public int? NullableProperty { get; set; }
            public double? NullableDouble { get; set; }
            public decimal DecimalProperty { get; set; }
            public decimal? NullableDecimal { get; set; }

            [Range(typeof(DateTime), "10/10/2000", "10/10/2020")]
            public DateTime DateTimeProperty { get; set; }

            public DateTime? NullableDateTime { get; set; }
        }
#endif
    }
}