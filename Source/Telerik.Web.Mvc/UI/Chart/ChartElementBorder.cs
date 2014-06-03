// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents chart element border
    /// </summary>
    public class ChartElementBorder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSpacing" /> class.
        /// </summary>
        public ChartElementBorder(int width, string color)
        {
            Width = width;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        public IChartSerializer CreateSerializer()
        {
            return new ChartElementBorderSerializer(this);
        }
    }
}