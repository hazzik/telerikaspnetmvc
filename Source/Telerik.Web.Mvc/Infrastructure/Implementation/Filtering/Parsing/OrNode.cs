// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    public class OrNode : IFilterNode, ILogicalNode
    {
        public IFilterNode First
        {
            get;
            set;
        }

        public IFilterNode Second 
        { 
            get; 
            set; 
        }
		
        public FilterCompositionLogicalOperator LogicalOperator
		{
			get
			{
                return FilterCompositionLogicalOperator.Or;
			}
		}

        public void Accept(IFilterNodeVisitor visitor)
        {
            visitor.StartVisit(this);

            First.Accept(visitor);
            Second.Accept(visitor);

            visitor.EndVisit();
        }
    }
}