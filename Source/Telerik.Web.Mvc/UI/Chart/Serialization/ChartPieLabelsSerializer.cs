// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartPieLabelsSerializer : ChartLabelsBase
    {
        private readonly ChartPieLabels pieLabels;

        public ChartPieLabelsSerializer(ChartPieLabels pieLabels)
            : base (pieLabels)
        {
            this.pieLabels = pieLabels;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("align", pieLabels.Align.ToString().ToCamelCase(), ChartDefaults.PieSeries.Labels.Align.ToString().ToCamelCase())
                .Add("position", pieLabels.Position.ToString().ToCamelCase(), ChartDefaults.PieSeries.Labels.Position.ToString().ToCamelCase())
                .Add("distance", pieLabels.Distance, ChartDefaults.PieSeries.Labels.Distance);

            return result;
        }
    }
}