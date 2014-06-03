namespace Telerik.Web.Mvc.UI.Tests.Chart
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;

    public class ChartNumericAxisBuilderTests
    {
        private readonly ChartNumericAxis<SalesData> axis;
        private readonly ChartNumericAxisBuilder builder;

        public ChartNumericAxisBuilderTests()
        {
            var chart = ChartTestHelper.CreateChart<SalesData>();
            axis = new ChartNumericAxis<SalesData>(chart);
            builder = new ChartNumericAxisBuilder(axis);
        }

        [Fact]
        public void Min_should_set_Min()
        {
            builder.Min(10);
            axis.Min.ShouldEqual(10);
        }

        [Fact]
        public void Min_should_return_builder()
        {
            builder.Min(10).ShouldBeSameAs(builder);
        }

        [Fact]
        public void Max_should_set_Max()
        {
            builder.Max(10);
            axis.Max.ShouldEqual(10);
        }

        [Fact]
        public void Max_should_return_builder()
        {
            builder.Max(10).ShouldBeSameAs(builder);
        }

        [Fact]
        public void MajorUnit_should_set_MajorUnit()
        {
            builder.MajorUnit(10);
            axis.MajorUnit.ShouldEqual(10);
        }

        [Fact]
        public void MajorUnit_should_return_builder()
        {
            builder.MajorUnit(10).ShouldBeSameAs(builder);
        }

        [Fact]
        public void AxisCrossingValue_should_set_AxisCrossingValue()
        {
            builder.AxisCrossingValue(10);
            axis.AxisCrossingValue.ShouldEqual(10);
        }

        [Fact]
        public void AxisCrossingValue_should_return_builder()
        {
            builder.AxisCrossingValue(10).ShouldBeSameAs(builder);
        }

        [Fact]
        public void Format_should_set_Format()
        {
            builder.Format("{0:C}");
            axis.Format.ShouldEqual("{0:C}");
        }

        [Fact]
        public void Format_should_return_builder()
        {
            builder.Format("{0:C}").ShouldBeSameAs(builder);
        }
    }
}