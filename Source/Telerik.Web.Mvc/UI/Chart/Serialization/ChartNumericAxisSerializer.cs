// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartNumericAxisSerializer : ChartAxisSerializerBase
    {
        private readonly IChartNumericAxis axis;

        public ChartNumericAxisSerializer(IChartNumericAxis axis)
            : base(axis)
        {
            this.axis = axis;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("min", axis.Min, () => { return axis.Min.HasValue; })
                .Add("max", axis.Max, () => { return axis.Max.HasValue; })
                .Add("majorUnit", axis.MajorUnit, () => { return axis.MajorUnit.HasValue; })
                .Add("axisCrossingValue", axis.AxisCrossingValue, () => { return axis.AxisCrossingValue.HasValue; });

            var labelsFormat = new Dictionary<string, object>();
            FluentDictionary.For(labelsFormat).Add("format", axis.Format, string.Empty);
            if (labelsFormat.Count > 0)
            {
                result.Add("labels", labelsFormat);
            }

            return result;
        }

        protected override bool ShouldSerializeMajorGridlines()
        {
            var majorGridLines = axis.MajorGridLines;

            return majorGridLines.Visible != ChartDefaults.Axis.Numeric.MajorGridLines.Visible ||
                   majorGridLines.Width != ChartDefaults.Axis.Numeric.MajorGridLines.Width ||
                   majorGridLines.Color != ChartDefaults.Axis.Numeric.MajorGridLines.Color;
        }

        protected override bool ShouldSerializeMinorGridlines()
        {
            var minorGridLines = axis.MinorGridLines;

            return minorGridLines.Visible != ChartDefaults.Axis.Numeric.MinorGridLines.Visible ||
                   minorGridLines.Width != ChartDefaults.Axis.Numeric.MinorGridLines.Width ||
                   minorGridLines.Color != ChartDefaults.Axis.Numeric.MinorGridLines.Color;
        }
    }
}
