﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartSpacingSerializer : IChartSerializer
    {
        private readonly ChartSpacing spacing;

        public ChartSpacingSerializer(ChartSpacing spacing)
        {
            this.spacing = spacing;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("top", spacing.Top)
                .Add("right", spacing.Right)
                .Add("bottom", spacing.Bottom)
                .Add("left", spacing.Left);

            return result;
        }
    }
}