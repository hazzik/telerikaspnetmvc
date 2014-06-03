// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartAxisSerializerBase : IChartSerializer
    {
        private readonly IChartAxis axis;

        public ChartAxisSerializerBase(IChartAxis axis)
        {
            this.axis = axis;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("minorTickSize", axis.MinorTickSize, ChartDefaults.Axis.MinorTickSize)
                .Add("majorTickSize", axis.MajorTickSize, ChartDefaults.Axis.MajorTickSize)
                .Add("majorTickType", axis.MajorTickType.ToString(), ChartDefaults.Axis.MajorTickType.ToString())
                .Add("minorTickType", axis.MinorTickType.ToString(), ChartDefaults.Axis.MinorTickType.ToString())
                .Add("majorGridLines", axis.MajorGridLines.CreateSerializer().Serialize(), ShouldSerializeMajorGridlines)
                .Add("minorGridLines", axis.MinorGridLines.CreateSerializer().Serialize(), ShouldSerializeMinorGridlines)
                .Add("line", axis.Line.CreateSerializer().Serialize(), ShouldSerializeLine);

            return result;
        }

        protected virtual bool ShouldSerializeMajorGridlines()
        {
            return false;
        }

        protected virtual bool ShouldSerializeMinorGridlines()
        {
            return false;
        }

        private bool ShouldSerializeLine()
        {
            var line = axis.Line;

            return line.Visible != ChartDefaults.Axis.Line.Visible ||
                    line.Width != ChartDefaults.Axis.Line.Width ||
                    line.Color != ChartDefaults.Axis.Line.Color;
        }
    }
}