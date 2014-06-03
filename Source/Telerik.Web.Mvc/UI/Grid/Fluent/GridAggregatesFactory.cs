// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using System.Collections.Generic;

    public class GridAggregatesFactory : IHideObjectMembers
    {
        private readonly ICollection<AggregateFunction> aggregates;
        private readonly string member;

        public GridAggregatesFactory(ICollection<AggregateFunction> aggregates, string member)
        {
            this.aggregates = aggregates;
            this.member = member;
        }

        public GridAggregatesFactory Min()
        {
            aggregates.Add(new MinFunction { SourceField = member });
            return this;
        }

        public GridAggregatesFactory Max()
        {
            aggregates.Add(new MaxFunction { SourceField = member });
            return this;
        }

        public GridAggregatesFactory Count()
        {
            aggregates.Add(new CountFunction { SourceField = member });
            return this;
        }

        public GridAggregatesFactory Average()
        {
            aggregates.Add(new AverageFunction { SourceField = member });
            return this;
        }

        public GridAggregatesFactory Sum()
        {
            aggregates.Add(new SumFunction { SourceField = member });
            return this;
        }
    }
}