// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Routing;

    using Extensions;
    using Infrastructure;

    public class GridUrlGenerator<T> where T : class
    {
        private readonly Grid<T> grid;

        public GridUrlGenerator(Grid<T> grid)
        {
            Guard.IsNotNull(grid, "grid");

            this.grid = grid;
        }

        public string SortingUrl(IUrlGenerator urlGenerator, IList<SortDescriptor> orderBy)
        {
            return Url(orderBy, urlGenerator, grid.DataProcessor.CurrentPage);
        }

        public string PagingUrl(IUrlGenerator urlGenerator, int page)
        {
            return Url(grid.DataProcessor.SortDescriptors, urlGenerator, page);
        }

        private string Url(IEnumerable<SortDescriptor> orderBy, IUrlGenerator urlGenerator, int page)
        {
            RouteValueDictionary routeValues = grid.ServerBinding.RouteValues;

            routeValues.Merge(grid.ViewContext.RouteData.Values);

            string orderBykey = grid.Prefix(GridUrlParameters.OrderBy);

            string serializedOrderBy = GridSortDescriptorSerializer.Serialize(orderBy);
			routeValues[orderBykey] = serializedOrderBy;

            string pagekey = grid.Prefix(GridUrlParameters.CurrentPage);

            routeValues[pagekey] = page;
            
            // `orderby` should be always if `filter` is present
            string filterKey = grid.Prefix(GridUrlParameters.Filter);
			if (routeValues.ContainsKey(filterKey) && string.IsNullOrEmpty(serializedOrderBy))
            {
                if (!string.IsNullOrEmpty((string)routeValues[filterKey]))
                {
                    routeValues[orderBykey] = "~";
                }
            }

            // We have to carry the query string for the next request.
            // so we need to copy it as well.
            foreach (string key in grid.ViewContext.HttpContext.Request.QueryString)
            {
                if (!routeValues.ContainsKey(key))
                {
                    routeValues[key] = grid.ViewContext.HttpContext.Request.QueryString[key];
                }
            }

            return grid.ServerBinding.GenerateUrl(grid.ViewContext, urlGenerator, routeValues);
        }
    }
}