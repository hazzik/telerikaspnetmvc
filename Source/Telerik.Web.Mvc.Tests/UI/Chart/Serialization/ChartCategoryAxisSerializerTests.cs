namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System.Collections;
    using Xunit;

    public class ChartCategoryAxisSerializerTests
    {
        private readonly Mock<IChartCategoryAxis> axisMock;
        private readonly ChartCategoryAxisSerializer serializer;

        public ChartCategoryAxisSerializerTests()
        {
            axisMock = new Mock<IChartCategoryAxis>();
            serializer = new ChartCategoryAxisSerializer(axisMock.Object);

            axisMock.SetupGet(a => a.MajorGridLines).Returns(
                new ChartLine(
                    ChartDefaults.Axis.Category.MajorGridLines.Width,
                    ChartDefaults.Axis.Category.MajorGridLines.Color,
                    ChartDefaults.Axis.Category.MajorGridLines.Visible
                )
            );

            axisMock.SetupGet(a => a.MinorGridLines).Returns(
                new ChartLine(
                    ChartDefaults.Axis.Category.MinorGridLines.Width,
                    ChartDefaults.Axis.Category.MinorGridLines.Color,
                    ChartDefaults.Axis.Category.MinorGridLines.Visible
                )
            );

            axisMock.SetupGet(a => a.Line).Returns(new ChartLine());
        }

        [Fact]
        public void Should_serialize_categories()
        {
            axisMock.SetupGet(a => a.Categories).Returns(new string[] { "A", "B" });
            (serializer.Serialize()["categories"] is IEnumerable).ShouldBeTrue();
        }

        [Fact]
        public void Should_serialize_field()
        {
            axisMock.SetupGet(a => a.Member).Returns("RepName");
            axisMock.SetupGet(a => a.Categories).Returns((IEnumerable) null);
            serializer.Serialize()["field"].ShouldEqual("RepName");
        }

        [Fact]
        public void Should_not_serialize_field_if_not_set()
        {
            axisMock.SetupGet(a => a.Member).Returns((string) null);
            axisMock.SetupGet(a => a.Categories).Returns((IEnumerable)null);
            serializer.Serialize().ContainsKey("field").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_field_if_has_categories()
        {
            axisMock.SetupGet(a => a.Member).Returns("RepName");
            axisMock.SetupGet(a => a.Categories).Returns(new string[] { "A", "B" });
            serializer.Serialize().ContainsKey("field").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_majorGridLines_if_not_set()
        {
            serializer.Serialize().ContainsKey("majorGridLines").ShouldBeFalse();
        }
        
        [Fact]
        public void Should_serialize_majorGridLines_if_set()
        {
            axisMock.SetupGet(a => a.MajorGridLines).Returns(
                new ChartLine(1, "white", true)
            );

            serializer.Serialize().ContainsKey("majorGridLines").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_minorGridLines_if_not_set()
        {
            serializer.Serialize().ContainsKey("minorGridLines").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_minorGridLines_if_set()
        {
            axisMock.SetupGet(a => a.MinorGridLines).Returns(
                new ChartLine(1, "white", true)
            );

            serializer.Serialize().ContainsKey("minorGridLines").ShouldBeTrue();
        }
    }
}
