// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartElementBorderSerializer : IChartSerializer
    {
        private readonly ChartElementBorder border;

        public ChartElementBorderSerializer(ChartElementBorder border)
        {
            this.border = border;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();

            FluentDictionary.For(result)
                .Add("width", border.Width)
                .Add("dashType", border.DashType.ToString().ToLowerInvariant())
                .Add("color", border.Color);

            return result;
        }
    }
}