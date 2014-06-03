// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartCategoryAxisSerializer : ChartAxisSerializerBase
    {
        private readonly IChartCategoryAxis axis;

        public ChartCategoryAxisSerializer(IChartCategoryAxis axis)
            : base(axis)
        {
            this.axis = axis;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();
            FluentDictionary.For(result)
                .Add("categories", axis.Categories, () => { return axis.Categories != null; } )
                .Add("field", axis.Member, () => { return axis.Categories == null && axis.Member != null; });

            return result;
        }

        protected override bool ShouldSerializeMajorGridlines()
        {
            var majorGridLines = axis.MajorGridLines;

            return  majorGridLines.Visible != ChartDefaults.Axis.Category.MajorGridLines.Visible ||
                    majorGridLines.Width != ChartDefaults.Axis.Category.MajorGridLines.Width ||
                    majorGridLines.Color != ChartDefaults.Axis.Category.MajorGridLines.Color;
        }

        protected override bool ShouldSerializeMinorGridlines()
        {
            var minorGridLines = axis.MinorGridLines;

            return minorGridLines.Visible != ChartDefaults.Axis.Category.MinorGridLines.Visible ||
                   minorGridLines.Width != ChartDefaults.Axis.Category.MinorGridLines.Width ||
                   minorGridLines.Color != ChartDefaults.Axis.Category.MinorGridLines.Color;
        }
    }
}
