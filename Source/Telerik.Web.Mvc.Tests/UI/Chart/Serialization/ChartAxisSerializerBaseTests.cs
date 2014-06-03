namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using Xunit;

    public class ChartAxisSerializerBaseTests
    {
        private readonly Mock<IChartAxis> axisMock;
        private readonly ChartAxisSerializerBase serializer;

        public ChartAxisSerializerBaseTests()
        {
            axisMock = new Mock<IChartAxis>();
            axisMock.SetupGet(axis => axis.MajorGridLines).Returns(new ChartLine());
            axisMock.SetupGet(axis => axis.MinorGridLines).Returns(new ChartLine());
            axisMock.SetupGet(a => a.Line).Returns(
                new ChartLine(
                    ChartDefaults.Axis.Line.Width,
                    ChartDefaults.Axis.Line.Color,
                    ChartDefaults.Axis.Line.Visible
                )
            );
            serializer = new ChartAxisSerializerBase(axisMock.Object);
        }

        [Fact]
        public void Should_serialize_MinorTickSize()
        {
            axisMock.SetupGet(a => a.MinorTickSize).Returns(1);
            serializer.Serialize()["minorTickSize"].ShouldEqual(1);
        }

        [Fact]
        public void Should_not_serialize_default_MinorTickSize()
        {
            axisMock.SetupGet(a => a.MinorTickSize).Returns(ChartDefaults.Axis.MinorTickSize);
            serializer.Serialize().ContainsKey("minorTickSize").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_MajorTickSize()
        {
            axisMock.SetupGet(a => a.MajorTickSize).Returns(1);
            serializer.Serialize()["majorTickSize"].ShouldEqual(1);
        }

        [Fact]
        public void Should_not_serialize_default_MajorTickSize()
        {
            axisMock.SetupGet(a => a.MajorTickSize).Returns(ChartDefaults.Axis.MajorTickSize);
            serializer.Serialize().ContainsKey("majorTickSize").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_default_MajorTickType()
        {
            axisMock.SetupGet(a => a.MajorTickType).Returns(ChartDefaults.Axis.MajorTickType);
            serializer.Serialize().ContainsKey("majorTickType").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_default_MinorTickType()
        {
            axisMock.SetupGet(a => a.MinorTickType).Returns(ChartDefaults.Axis.MinorTickType);
            serializer.Serialize().ContainsKey("minorTickType").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_Line()
        {
            axisMock.SetupGet(a => a.Line).Returns(new ChartLine(2, "green", true));
            serializer.Serialize().ContainsKey("line").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_default_Line()
        {
            serializer.Serialize().ContainsKey("line").ShouldBeFalse();
        }
    }
}