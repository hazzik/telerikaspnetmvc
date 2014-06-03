namespace Telerik.Web.Mvc.UI.Tests.Chart
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;

    public class ChartSeriesDefaultsBuilderTests
    {
        private readonly Chart<object> chart;
        private readonly ChartSeriesDefaultsBuilder<object> builder;

        public ChartSeriesDefaultsBuilderTests()
        {
            chart = ChartTestHelper.CreateChart<object>();
            builder = new ChartSeriesDefaultsBuilder<object>(chart);
        }

        [Fact]
        public void Bar_sets_BarSeries_options()
        {
            builder.Bar().Gap(4);
            chart.SeriesDefaults.Bar.Gap.ShouldEqual(4);
        }

        [Fact]
        public void Column_sets_ColumnSeries_options()
        {
            builder.Column().Gap(4);
            chart.SeriesDefaults.Column.Gap.ShouldEqual(4);
        }

        [Fact]
        public void Line_sets_LineSeries_options()
        {
            builder.Line().Width(4);
            chart.SeriesDefaults.Line.Width.ShouldEqual(4);
        }
    }
}