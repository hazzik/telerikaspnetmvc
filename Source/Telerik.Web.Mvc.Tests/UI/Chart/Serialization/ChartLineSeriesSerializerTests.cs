namespace Telerik.Web.Mvc.UI.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI.Tests.Chart;
    using Xunit;

    public class ChartLineSeriesSerializerTests
    {
        private readonly ChartLineSeries<SalesData, decimal> lineSeries;

        public ChartLineSeriesSerializerTests()
        {
            var chart = ChartTestHelper.CreateChart<SalesData>();
            chart.DataSource = SalesDataBuilder.GetCollection();
            lineSeries = new ChartLineSeries<SalesData, decimal>(chart, s => s.RepSales);
        }

        [Fact]
        public void Line_serializes_type()
        {
            GetJson(lineSeries)["type"].ShouldEqual("line");
        }

        [Fact]
        public void Line_serializes_name()
        {
            lineSeries.Name = "Line";
            GetJson(lineSeries)["name"].ShouldEqual("Line");
        }

        [Fact]
        public void Line_serializes_opacity()
        {
            lineSeries.Opacity = 0.5;
            GetJson(lineSeries)["opacity"].ShouldEqual(0.5);
        }

        [Fact]
        public void Line_should_not_serialize_default_opacity()
        {
            lineSeries.Opacity = 1;
            GetJson(lineSeries).ContainsKey("opacity").ShouldBeFalse();
        }

        [Fact]
        public void Line_should_not_serialize_empty_name()
        {
            lineSeries.Name = string.Empty;
            GetJson(lineSeries).ContainsKey("name").ShouldBeFalse();
        }

        [Fact]
        public void Line_serializes_stack()
        {
            lineSeries.Stacked = true;
            GetJson(lineSeries)["stack"].ShouldEqual(true);
        }

        [Fact]
        public void Line_should_not_seriale_default_stack()
        {
            GetJson(lineSeries).ContainsKey("stack").ShouldBeFalse();
        }

        [Fact]
        public void Width_serializes_width()
        {
            lineSeries.Width = 2;
            GetJson(lineSeries)["width"].ShouldEqual(2.0);
        }

        [Fact]
        public void Line_should_not_seriale_default_width()
        {
            GetJson(lineSeries).ContainsKey("width").ShouldBeFalse();
        }

        [Fact]
        public void Line_should_serialize_data()
        {
            (GetJson(lineSeries)["data"] is IEnumerable).ShouldBeTrue();
        }

        [Fact]
        public void Line_should_serialize_member_if_has_no_data()
        {
            lineSeries.Data = null;
            GetJson(lineSeries)["field"].ShouldEqual("RepSales");
        }

        [Fact]
        public void Line_should_serialize_label_settings()
        {
            lineSeries.Labels.Visible = true;
            GetJson(lineSeries).ContainsKey("labels").ShouldEqual(true);
        }

        [Fact]
        public void Line_should_not_serialize_label_settings_by_default()
        {
            GetJson(lineSeries).ContainsKey("labels").ShouldEqual(false);
        }

        [Fact]
        public void Line_should_serialize_marker_settings()
        {
            lineSeries.Markers.Background = "green";
            GetJson(lineSeries).ContainsKey("markers").ShouldEqual(true);
        }

        [Fact]
        public void Line_should_not_serialize_marker_settings_by_default()
        {
            GetJson(lineSeries).ContainsKey("markers").ShouldEqual(false);
        }

        [Fact]
        public void Serializes_color()
        {
            lineSeries.Color = "Blue";
            GetJson(lineSeries)["color"].ShouldEqual("Blue");
        }

        [Fact]
        public void Does_not_serialize_default_color()
        {
            GetJson(lineSeries).ContainsKey("color").ShouldBeFalse();
        }

        private static IDictionary<string, object> GetJson(IChartSeries series)
        {
            return series.CreateSerializer().Serialize();
        }
    }
}
