﻿namespace Telerik.Web.Mvc.UI.Tests.Chart
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;

    public class ChartLineLabelsBuilderTests
    {
        private readonly ChartLineLabels labels;
        private readonly ChartLineLabelsBuilder builder;

        public ChartLineLabelsBuilderTests()
        {
            labels = new ChartLineLabels();
            builder = new ChartLineLabelsBuilder(labels);
        }

        [Fact]
        public void Font_sets_Font()
        {
            builder.Font("Font");
            labels.Font.ShouldEqual("Font");
        }

        [Fact]
        public void Visible_sets_Visible()
        {
            builder.Visible(false);
            labels.Visible.ShouldBeFalse();
        }

        [Fact]
        public void Position_sets_Position()
        {
            builder.Position(ChartLineLabelsPosition.Left);
            labels.Position.ShouldEqual(ChartLineLabelsPosition.Left);
        }

        [Fact]
        public void Color_sets_color()
        {
            builder.Color("Red");
            labels.Color.ShouldEqual("Red");
        }

        [Fact]
        public void Background_sets_background()
        {
            builder.Background("Blue");
            labels.Background.ShouldEqual("Blue");
        }

        [Fact]
        public void Margin_sets_margins()
        {
            builder.Margin(20);
            labels.Margin.Top.ShouldEqual(20);
            labels.Margin.Right.ShouldEqual(20);
            labels.Margin.Bottom.ShouldEqual(20);
            labels.Margin.Left.ShouldEqual(20);

            builder.Margin(1, 2, 3, 4);
            labels.Margin.Top.ShouldEqual(1);
            labels.Margin.Right.ShouldEqual(2);
            labels.Margin.Bottom.ShouldEqual(3);
            labels.Margin.Left.ShouldEqual(4);
        }

        [Fact]
        public void Padding_sets_paddings()
        {
            builder.Padding(20);
            labels.Padding.Top.ShouldEqual(20);
            labels.Padding.Right.ShouldEqual(20);
            labels.Padding.Bottom.ShouldEqual(20);
            labels.Padding.Left.ShouldEqual(20);

            builder.Padding(1, 2, 3, 4);
            labels.Padding.Top.ShouldEqual(1);
            labels.Padding.Right.ShouldEqual(2);
            labels.Padding.Bottom.ShouldEqual(3);
            labels.Padding.Left.ShouldEqual(4);
        }

        [Fact]
        public void Border_sets_width_and_color()
        {
            builder.Border(1, "red");
            labels.Border.Color.ShouldEqual("red");
            labels.Border.Width.ShouldEqual(1);
        }

        [Fact]
        public void Format_sets_format()
        {
            builder.Format("{0:C}");
            labels.Format.ShouldEqual("{0:C}");
        }
    }
}