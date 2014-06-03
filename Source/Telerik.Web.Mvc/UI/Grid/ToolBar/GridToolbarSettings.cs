// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;

    public class GridToolBarSettings<T> where T : class
    {
        public GridToolBarSettings(Grid<T> grid)
        {
            Commands = new List<GridToolBarCommandBase<T>>();
            Grid = grid;
            Template = new HtmlTemplate();
        }

        public Grid<T> Grid
        {
            get;
            private set;
        }

        public bool Enabled
        {
            get
            {
                return Commands.Any() || Template.HasValue();
            }
        }

        public IList<GridToolBarCommandBase<T>> Commands
        {
            get;
            private set;
        }

        public HtmlTemplate Template
        {
            get;
            private set;
        }

        public void AppendTo(IHtmlNode parent)
        {
            if (Template.HasValue())
            {
                Template.Apply(parent);
            }
            else
            {
                Commands.Each(command => command.Html(Grid, parent));
            }
        }
    }
}