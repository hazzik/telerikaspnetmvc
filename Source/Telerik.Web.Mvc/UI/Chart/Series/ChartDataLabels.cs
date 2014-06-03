﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents the options of the chart data labels
    /// </summary>
    public abstract class ChartDataLabels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDataLabels" /> class.
        /// </summary>
        protected ChartDataLabels() 
        {
            Font = ChartDefaults.DataLabels.Font;
            Visible = ChartDefaults.DataLabels.Visible;
            Margin = new ChartSpacing(ChartDefaults.DataLabels.Margin);
            Padding = new ChartSpacing(ChartDefaults.DataLabels.Padding);
            Border = new ChartElementBorder(ChartDefaults.DataLabels.Border.Width, ChartDefaults.DataLabels.Border.Color);
            Color = ChartDefaults.DataLabels.Color;
        }

        /// <summary>
        /// Gets or sets the label font.
        /// </summary>
        /// <value>
        /// Specify a font in CSS format. For example "12px Verdana, sans-serif".
        /// </value>
        public string Font
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating if the label is visible
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label background.
        /// </summary>
        /// <value>
        /// The label background.
        /// </value>
        public string Background
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label border.
        /// </summary>
        /// <value>
        /// The label border.
        /// </value>
        public ChartElementBorder Border
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label margin.
        /// </summary>
        /// <value>
        /// The label margin.
        /// </value>
        public ChartSpacing Margin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label padding.
        /// </summary>
        /// <value>
        /// The label padding.
        /// </value>
        public ChartSpacing Padding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label color.
        /// </summary>
        /// <value>
        /// The label color.
        /// </value>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label format.
        /// </summary>
        /// <value>
        /// The label format.
        /// </value>
        public string Format
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        public abstract IChartSerializer CreateSerializer();
    }
}