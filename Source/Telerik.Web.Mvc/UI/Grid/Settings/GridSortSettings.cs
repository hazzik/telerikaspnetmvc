// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public class GridSortSettings
    {
        public GridSortSettings()
        {
            OrderBy = new List<SortDescriptor>();
        }

        public bool Enabled
        {
            get;
            set;
        }

        public GridSortMode SortMode
        {
            get;
            set;
        }

        public IList<SortDescriptor> OrderBy
        {
            get;
            private set;
        }
    }
}