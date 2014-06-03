// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    internal class ChartAxisLabelsSerializer : ChartLabelsBase
    {
        private readonly ChartLabels axisLabels;

        public ChartAxisLabelsSerializer(ChartLabels axisLabels)
            : base(axisLabels)
        {
            this.axisLabels = axisLabels;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            return result;
        }
    }
}