namespace Telerik.Web.Mvc.UI.Tests.Chart
{
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;

    public class ChartCategoryAxisBuilderTests
    {
        private readonly ChartCategoryAxis<SalesData> axis;
        private readonly ChartCategoryAxisBuilder<SalesData> builder;

        public ChartCategoryAxisBuilderTests()
        {
            var chart = ChartTestHelper.CreateChart<SalesData>();
            axis = new ChartCategoryAxis<SalesData>(chart);
            chart.CategoryAxis = axis;
            builder = new ChartCategoryAxisBuilder<SalesData>(chart);
        }

        [Fact]
        public void Categories_should_bind_categories_with_expression()
        {
            builder.Categories(s => s.DateString);

            var expectedCategories = new Queue<string>();
            expectedCategories.Enqueue("Aug 2010");
            expectedCategories.Enqueue("Sept 2010");

            var categoryStrings = new List<string>();
            foreach (object category in categoryStrings)
            {
                expectedCategories.Dequeue().ShouldEqual(category.ToString());
            }
        }

        [Fact]
        public void Categories_should_set_categories_from_IEnumerable()
        {
            var categories = new string[] { "Aug 2010", "Sept 2010" };

            builder.Categories(categories);

            axis.Categories.ShouldBeSameAs(categories);
        }

        [Fact]
        public void Categories_should_set_categories_from_list()
        {
            builder.Categories("Aug 2010", "Sept 2010");

            AssertCategories(new string[] { "Aug 2010", "Sept 2010" });
        }

        [Fact]
        public void MajorGridLines_should_return_builder()
        {
            builder.MajorGridLines(lines => { }).ShouldBeSameAs(builder);
        }

        [Fact]
        public void MajorGridLines_should_set_Visible()
        {
            builder.MajorGridLines(1, "green");
            axis.MajorGridLines.Visible.ShouldEqual(true);
        }

        [Fact]
        public void MajorGridLines_should_set_Width()
        {
            builder.MajorGridLines(1, "green");
            axis.MajorGridLines.Width.ShouldEqual(1);
        }

        [Fact]
        public void MajorGridLines_should_set_Color()
        {
            builder.MajorGridLines(1, "green");
            axis.MajorGridLines.Color.ShouldEqual("green");
        }

        [Fact]
        public void MinorGridLines_should_return_builder()
        {
            builder.MinorGridLines(lines => { }).ShouldBeSameAs(builder);
        }

        [Fact]
        public void MinorGridLines_should_set_Visible()
        {
            builder.MinorGridLines(1, "green");
            axis.MinorGridLines.Visible.ShouldEqual(true);
        }

        [Fact]
        public void MinorGridLines_should_set_Width()
        {
            builder.MinorGridLines(1, "green");
            axis.MinorGridLines.Width.ShouldEqual(1);
        }

        [Fact]
        public void MinorGridLines_should_set_Color()
        {
            builder.MinorGridLines(1, "green");
            axis.MinorGridLines.Color.ShouldEqual("green");
        }

        [Fact]
        public void Line_should_return_builder()
        {
            builder.Line(line => { }).ShouldBeSameAs(builder);
        }

        [Fact]
        public void Line_should_set_Visible()
        {
            builder.Line(1, "green");
            axis.Line.Visible.ShouldEqual(true);
        }

        [Fact]
        public void Line_should_set_Width()
        {
            builder.Line(1, "green");
            axis.Line.Width.ShouldEqual(1);
        }

        [Fact]
        public void Line_should_set_Color()
        {
            builder.Line(1, "green");
            axis.Line.Color.ShouldEqual("green");
        }

        private void AssertCategories(IEnumerable<string> categories)
        {
            var expectedCategories = new Queue<string>();
            foreach (var category in categories)
            {
                expectedCategories.Enqueue(category);
                                
            }

            var categoryStrings = new List<string>();
            foreach (object category in categoryStrings)
            {
                expectedCategories.Dequeue().ShouldEqual(category.ToString());
            }

        }
    }
}