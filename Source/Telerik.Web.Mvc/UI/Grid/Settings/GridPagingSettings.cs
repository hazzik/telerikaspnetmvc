// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Globalization;
    using System.Linq;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridPagingSettings : IClientSerializable
    {
        private readonly IGrid grid;

        private int pageSize = 10;
        private int total = 0;

        public GridPagingSettings(IGrid grid)
        {
            this.grid = grid;
            Style = GridPagerStyles.NextPreviousAndNumeric;
            CurrentPage = 1;
            PageSizesInDropDown = new[] {5, 10, 20, 50};
        }
        
        public int CurrentPage
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public int PageSize 
        {
            get
            {
                return pageSize;
            }

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

        public int[] PageSizesInDropDown
        {
            get; set;
        }

        public int Total
        {
            get
            {
                return total;
            }
            set
            {
                Guard.IsNotNegative(value, "value");

                total = value;
            }
        }

        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (Enabled)
            {
                writer.Append("pageSize", PageSize, 10);
                writer.Append("total", grid.DataProcessor.Total);
                writer.Append("currentPage", grid.DataProcessor.CurrentPage);
                writer.AppendCollection("pageSizesInDropDown", PageSizesInDropDown.Select(v => v.ToString(CultureInfo.InvariantCulture)));
            }
            else
            {
                writer.Append("pageSize", 0);
            }
        }
    }
}