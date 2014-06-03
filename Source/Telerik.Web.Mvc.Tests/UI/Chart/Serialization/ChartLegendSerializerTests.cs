namespace Telerik.Web.Mvc.UI.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class ChartLegendSerializerTests
    {
        private readonly ChartLegend legend;

        public ChartLegendSerializerTests()
        {
            legend = new ChartLegend();
        }

        [Fact]
        public void Serializes_font()
        {
            legend.Font = "Font";
            GetJson()["font"].ShouldEqual("Font");
        }

        [Fact]
        public void Does_not_serialize_default_font()
        {
            GetJson().ContainsKey("font").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_position()
        {
            legend.Position = ChartLegendPosition.Bottom;
            GetJson()["position"].ShouldEqual("bottom");
        }

        [Fact]
        public void Does_not_serialize_default_position()
        {
            GetJson().ContainsKey("legend").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_offsetX()
        {
            legend.OffsetX = 100;
            GetJson()["offsetX"].ShouldEqual(100);
        }

        [Fact]
        public void Does_not_serialize_default_offsetX()
        {
            GetJson().ContainsKey("offsetX").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_offsetY()
        {
            legend.OffsetY = 100;
            GetJson()["offsetY"].ShouldEqual(100);
        }

        [Fact]
        public void Does_not_serialize_default_offsetY()
        {
            GetJson().ContainsKey("offsetY").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_visible()
        {
            legend.Visible = false;
            GetJson()["visible"].ShouldEqual(false);
        }

        [Fact]
        public void Does_not_serialize_default_visible()
        {
            GetJson().ContainsKey("visible").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_margin()
        {
            legend.Margin = new ChartSpacing(20);
            GetJson().ContainsKey("margin").ShouldBeTrue();
        }

        [Fact]
        public void Does_not_serialize_default_margin()
        {
            GetJson().ContainsKey("margin").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_padding()
        {
            legend.Padding = new ChartSpacing(20);
            GetJson().ContainsKey("padding").ShouldBeTrue();
        }

        [Fact]
        public void Does_not_serialize_default_padding()
        {
            GetJson().ContainsKey("padding").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_border()
        {
            legend.Border.Color = "red";
            legend.Border.Width = 1;
            ((Dictionary<string, object>)GetJson()["border"])["width"].ShouldEqual(1);
            ((Dictionary<string, object>)GetJson()["border"])["color"].ShouldEqual("red");
        }

        [Fact]
        public void Does_not_serialize_default_border()
        {
            GetJson().ContainsKey("border").ShouldBeFalse();
        }

        private IDictionary<string, object> GetJson()
        {
            return legend.CreateSerializer().Serialize();
        }
    }
}