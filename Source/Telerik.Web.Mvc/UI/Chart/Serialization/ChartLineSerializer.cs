// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartLineSerializer : IChartSerializer
    {
        private readonly ChartLine line;

        public ChartLineSerializer(ChartLine line)
        {
            this.line = line;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("width", line.Width)
                .Add("color", line.Color)
                .Add("visible", line.Visible);

            return result;
        }
    }
}