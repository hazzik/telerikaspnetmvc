namespace Telerik.Web.Mvc.UI.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI.Tests.Chart;
    using Xunit;

    public class ChartSeriesDefaultsSerializerTests
    {
        private readonly ChartSeriesDefaults<SalesData> seriesDefaults;

        public ChartSeriesDefaultsSerializerTests()
        {
            var chart = ChartTestHelper.CreateChart<SalesData>();
            seriesDefaults = new ChartSeriesDefaults<SalesData>(chart);
        }

        [Fact]
        public void Serializes_bar_defaults()
        {
            seriesDefaults.Bar.Stacked = true;
            GetJson(seriesDefaults).ContainsKey("bar");
        }

        [Fact]
        public void Strips_type_from_bar_defaults()
        {
            seriesDefaults.Bar.Stacked = true;
            var barData = GetJson(seriesDefaults)["bar"];
            ((IDictionary<string, object>) barData).ContainsKey("type").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_column_defaults()
        {
            seriesDefaults.Column.Stacked = true;
            GetJson(seriesDefaults).ContainsKey("column");
        }

        [Fact]
        public void Strips_type_from_column_defaults()
        {
            seriesDefaults.Column.Stacked = true;
            var barData = GetJson(seriesDefaults)["column"];
            ((IDictionary<string, object>)barData).ContainsKey("type").ShouldBeFalse();
        }

        private static IDictionary<string, object> GetJson(IChartSeriesDefaults seriesDefaults)
        {
            return seriesDefaults.CreateSerializer().Serialize();
        }
    }
}