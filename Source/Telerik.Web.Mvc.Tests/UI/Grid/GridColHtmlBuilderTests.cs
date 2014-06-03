namespace Telerik.Web.Mvc.UI
{
    using Moq;
    using System;
    using Xunit;
    
    public class GridColHtmlBuilderTests
    {
        [Fact]
        public void Should_throw_if_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GridColHtmlBuilder(null));
        }

        [Fact]
        public void Should_return_col_tag()
        {
            var column = new Mock<IGridColumn>();

            var builder = new GridColHtmlBuilder(column.Object);

            Assert.Equal("col", builder.Build().TagName);
        }

        [Fact]
        public void Should_apply_width()
        {
            var column = new Mock<IGridColumn>();
            column.SetupGet(c => c.Width).Returns("100px");

            var builder = new GridColHtmlBuilder(column.Object);
            
            Assert.Equal("width:100px", builder.Build().Attribute("style"));
        }
    }
}
