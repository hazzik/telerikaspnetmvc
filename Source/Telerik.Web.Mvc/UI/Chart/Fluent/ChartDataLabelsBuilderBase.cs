// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the chart data labels.
    /// </summary>
    public abstract class ChartDataLabelsBuilderBase<TBuilder> : IHideObjectMembers
        where TBuilder : ChartDataLabelsBuilderBase<TBuilder>
    {
        private readonly ChartDataLabels dataLabels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDataLabelsBuilderBase{TBuilder}" /> class.
        /// </summary>
        /// <param name="chartDataLabels">The data labels configuration.</param>
        public ChartDataLabelsBuilderBase(ChartDataLabels chartDataLabels)
        {
            dataLabels = chartDataLabels;
        }

        /// <summary>
        /// Sets the data labels font
        /// </summary>
        /// <param name="font">The data labels font (CSS format).</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Font("14px Verdana, sans-serif")
        ///                     .Visible(true)
        ///                 );
        ///             )
        ///             .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Font(string font)
        {
            dataLabels.Font = font;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels visibility
        /// </summary>
        /// <param name="visible">The labels visibility.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Bar(s => s.Sales)
        ///               .Labels(labels => labels
        ///                   .Visible(true)
        ///               );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Visible(bool visible)
        {
            dataLabels.Visible = visible;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels background color
        /// </summary>
        /// <param name="background">The data labels background color.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Background("Red")
        ///                     .Visible(true);
        ///                 );
        ///           )          
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Background(string background)
        {
            dataLabels.Background = background;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels text color
        /// </summary>
        /// <param name="color">The data labels text color.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Color("Red")
        ///                     .Visible(true);
        ///                 );
        ///           )    
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Color(string color)
        {
            dataLabels.Color = color;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels margin
        /// </summary>
        /// <param name="top">The data labels top margin.</param>
        /// <param name="right">The data labels right margin.</param>
        /// <param name="bottom">The data labels top margin.</param>
        /// <param name="left">The data labels top margin.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Margin(0, 5, 5, 0)
        ///                     .Visible(true);
        ///                 );
        ///           ) 
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Margin(int top, int right, int bottom, int left)
        {
            dataLabels.Margin.Top = top;
            dataLabels.Margin.Right = right;
            dataLabels.Margin.Bottom = bottom;
            dataLabels.Margin.Left = left;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels margin
        /// </summary>
        /// <param name="margin">The data labels margin.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Margin(20)
        ///                     .Visible(true);
        ///                 );
        ///           ) 
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Margin(int margin)
        {
            dataLabels.Margin = new ChartSpacing(margin);
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels padding
        /// </summary>
        /// <param name="top">The data labels top padding.</param>
        /// <param name="right">The data labels right padding.</param>
        /// <param name="bottom">The data labels top padding.</param>
        /// <param name="left">The data labels top padding.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                .Bar(s => s.Sales)
        ///                .Labels(labels => labels
        ///                     .Padding(0, 5, 5, 0)
        ///                     .Visible(true);
        ///                );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Padding(int top, int right, int bottom, int left)
        {
            dataLabels.Padding.Top = top;
            dataLabels.Padding.Right = right;
            dataLabels.Padding.Bottom = bottom;
            dataLabels.Padding.Left = left;
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels padding
        /// </summary>
        /// <param name="padding">The data labels padding.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                .Bar(s => s.Sales)
        ///                .Labels(labels => labels
        ///                     .Padding(20)
        ///                     .Visible(true);
        ///                );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Padding(int padding)
        {
            dataLabels.Padding = new ChartSpacing(padding);
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels border
        /// </summary>
        /// <param name="width">The data labels border width.</param>
        /// <param name="color">The data labels border color (CSS syntax).</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                .Bar(s => s.Sales)
        ///                .Labels(labels => labels
        ///                     .Border(1, "Red")
        ///                     .Visible(true);
        ///                );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Border(int width, string color)
        {
            dataLabels.Border = new ChartElementBorder(width, color);
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the labels format
        /// </summary>
        /// <param name="format">The data labels format.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                 .Bar(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Format("{0:C}")
        ///                     .Visible(true);
        ///                 );
        ///           )          
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TBuilder Format(string format)
        {
            dataLabels.Format = format;
            return this as TBuilder;
        }
    }
}