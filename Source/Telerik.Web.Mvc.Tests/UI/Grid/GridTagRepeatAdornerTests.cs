namespace Telerik.Web.Mvc.UI
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using Xunit;
    
    public class GridTagRepeatAdornerTests
    {
        [Fact]
        public void Should_create_cells_with_predefined_class()
        {
            var row = new HtmlTag("tr");
            var adorner = new GridTagRepeatingAdorner(1)
            {
                TagName = "th",
                CssClasses = {"t-header", "t-group-cell"}
            };

            adorner.ApplyTo(row);
            Assert.Equal(1, row.Children.Count);
            Assert.Equal("th", row.Children[0].TagName);
            Assert.Equal("t-header t-group-cell", row.Children[0].Attribute("class"));
        }

        [Fact]
        public void Should_create_as_many_tags_as_specified()
        {
            var row = new HtmlTag("tr");
            var adorner = new GridTagRepeatingAdorner(2)
            {
                TagName = "th",
                CssClasses = { "t-header", "t-group-cell" }
            };
            adorner.ApplyTo(row);

            Assert.Equal(2, row.Children.Count);
        }
        
        [Fact]
        public void Should_insert_at_the_beginning()
        {
            var row = new HtmlTag("tr");
            new HtmlTag("th").AppendTo(row);

            var adorner = new GridTagRepeatingAdorner(1)
            {
                TagName = "th",
                CssClasses = { "t-header", "t-group-cell" }
            };
            adorner.ApplyTo(row);

            Assert.Equal("t-header t-group-cell", row.Children[0].Attribute("class"));
        }

        [Fact]
        public void Should_set_inner_content_to_nbsp()
        {
            var adorner = new GridTagRepeatingAdorner(1);

            var row = new HtmlTag("tr");

            adorner.ApplyTo(row);

            Assert.Equal("&nbsp;", row.Children[0].InnerHtml);
        }

        [Fact]
        public void Should_throw_on_negative_argument()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GridTagRepeatingAdorner(-1));
        }
    }
}
