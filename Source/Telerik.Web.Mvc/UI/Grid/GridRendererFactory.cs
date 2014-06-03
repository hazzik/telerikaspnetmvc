// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;

    public class GridRendererFactory : IGridRendererFactory
    {
        public IGridRenderer<T> Create<T>(Grid<T> grid, HtmlTextWriter writer) where T : class
        {
            if (grid.Scrolling.Enabled)
            {
                return new GridScrollableRenderer<T>(grid, writer);
            }

            return new GridRenderer<T>(grid, writer);
        }
    }
}