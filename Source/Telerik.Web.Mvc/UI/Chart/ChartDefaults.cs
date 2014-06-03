// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    internal static class ChartDefaults
    {
        public static class Axis
        {
            public static class Category
            {
                public static class MajorGridLines
                {
                    public const int Width = 1;
                    public const string Color = "#000";
                    public const bool Visible = false;
                }

                public static class MinorGridLines
                {
                    public const int Width = 1;
                    public const string Color = "#000";
                    public const bool Visible = false;
                }
            }

            public static class Numeric
            {
                public static class MajorGridLines
                {
                    public const int Width = 1;
                    public const string Color = "#000";
                    public const bool Visible = true;
                }

                public static class MinorGridLines
                {
                    public const int Width = 1;
                    public const string Color = "#000";
                    public const bool Visible = false;
                }
            }

            public static class Line
            {
                public const int Width = 1;
                public const string Color = "#000";
                public const bool Visible = true;
            }

            public const int MinorTickSize = 3;
            public const int MajorTickSize = 4;
            public const ChartAxisTickType MajorTickType = ChartAxisTickType.Outside;
            public const ChartAxisTickType MinorTickType = ChartAxisTickType.None;
            public const int LineWidth = 1;
        }

        public static class Title
        {
            public const string Font = "16px Verdana, sans-serif";
            public const ChartTitlePosition Position = ChartTitlePosition.Top;
            public const ChartTextAlignment Align = ChartTextAlignment.Center;
            public const bool Visible = true;
            public const int Margin = 10;
            public const int Padding = 0;
            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
        }

        public static class Legend
        {
            public const string Font = "12px Verdana, sans-serif";
            public const ChartLegendPosition Position = ChartLegendPosition.Right;
            public const bool Visible = true;
            public const int Margin = 10;
            public const int Padding = 0;
            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
        }

        public static class ChartArea
        {
            public const string background = "#fff";
            public const int Margin = 0;
            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
        }

        public static class PlotArea
        {
            public const string background = "#fff";
            public const int Margin = 0;
            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
        }

        public static class BarSeries
        {
            public const double Gap = 1.5;
            public const double Spacing = 0.4;
            public const ChartBarSeriesOverlay Overlay = ChartBarSeriesOverlay.Glass;

            public static class Labels
            {
                public const ChartBarLabelsPosition Position = ChartBarLabelsPosition.OutsideEnd;
            }

            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
        }

        public static class LineSeries
        {
            public static class Labels
            {
                public const ChartLineLabelsPosition Position = ChartLineLabelsPosition.Above;
            }

            public const double Width = 1;
            public static class Markers
            {
                public const int Size = 6;
                public const string Background = "#fff";
                public const bool Visible = true;
                public const ChartMarkerShape Type = ChartMarkerShape.Square;
                public static class Border
                {
                    public const int Width = 0;
                    public const string Color = "#fff";
                }
            }
        }

        public static class DataLabels
        {
            public const string Font = "16px Verdana, sans-serif";
            public const bool Visible = false;
            public static class Border
            {
                public const int Width = 0;
                public const string Color = "#fff";
            }
            public const int Margin = 0;
            public const int Padding = 0;
            public const string Color = "#000";
        }
    }
}