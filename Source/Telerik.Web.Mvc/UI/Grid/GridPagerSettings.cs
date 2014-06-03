// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;

    using Infrastructure;

    public class GridPagerSettings
    {
        private int pageSize = 10;

        public GridPagerSettings()
        {
            Style = GridPagerStyles.NextPreviousAndNumeric;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public int PageSize 
        {
            [DebuggerStepThrough]
            get
            {
                return pageSize;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");
                pageSize = value;
            }
        }

        public GridPagerStyles Style
        {
            get;
            set;
        }

        public GridPagerPosition Position 
        { 
            get; 
            set; 
        }

        public int Total
        {
            get;
            set;
        }
    }
}