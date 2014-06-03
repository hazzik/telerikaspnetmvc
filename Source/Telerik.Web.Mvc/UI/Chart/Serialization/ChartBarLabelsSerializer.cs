﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartBarLabelsSerializer : ChartLabelsBase
    {
        private readonly ChartBarLabels barLabels;

        public ChartBarLabelsSerializer(ChartBarLabels barLabels)
            : base(barLabels)
        {
            this.barLabels = barLabels;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();
            
            FluentDictionary.For(result)
                .Add("position", barLabels.Position.ToString().ToCamelCase(), ChartDefaults.BarSeries.Labels.Position.ToString().ToCamelCase());

            return result;
        }
    }
}