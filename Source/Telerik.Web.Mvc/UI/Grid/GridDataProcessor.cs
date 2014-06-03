// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    public class GridDataProcessor
    {
        private readonly IGridBindingContext bindingContext;

        private bool dataSourceIsProcessed;
        private int totalCount;
        private IEnumerable processedDataSource;
        private IList<SortDescriptor> sortDescriptors;
        private IList<IFilterDescriptor> filterDescriptors;

        public GridDataProcessor(IGridBindingContext bindingContext)
        {
            Guard.IsNotNull(bindingContext, "bindingContext");

            this.bindingContext = bindingContext;
        }

        public int Total
        {
            get
            {
                EnsureDataSourceIsProcessed();
                return totalCount;
            }
        }

        public IList<SortDescriptor> SortDescriptors
        {
            get
            {
                if (sortDescriptors == null)
                {
                    sortDescriptors = GridSortDescriptorSerializer.Deserialize(bindingContext.ValueOf<string>(GridUrlParameters.OrderBy));
                }

                return sortDescriptors;
            }
        }

        public IList<IFilterDescriptor> FilterDescriptors
        {
            get
            {
                if (filterDescriptors == null)
                {
                    filterDescriptors = FilterDescriptorFactory.Create(bindingContext.ValueOf<string>(GridUrlParameters.Filter));
                }

                return filterDescriptors;
            }
        }
        public int PageCount
        {
            get
            {
                EnsureDataSourceIsProcessed();

                int pageSize = bindingContext.PageSize;

                if ((totalCount == 0) || (pageSize == 0))
                {
                    return 1;
                }

                return (totalCount + pageSize - 1)/pageSize;
            }
        }

        public IEnumerable ProcessedDataSource
        {
            get
            {
                EnsureDataSourceIsProcessed();

                return processedDataSource;
            }
        }

        public int CurrentPage
        {
            get
            {
                return bindingContext.ValueOf<int?>(GridUrlParameters.CurrentPage) ?? 1;
            }
        }

        private void EnsureDataSourceIsProcessed()
        {
            if (dataSourceIsProcessed)
            {
                return;
            }

            if (bindingContext.DataSource == null)
            {
                dataSourceIsProcessed = true;
                return;
            }

            if (!bindingContext.EnableCustomBinding)
            {
                IQueryable dataSource = bindingContext.DataSource.AsQueryable();
                GridModel model = dataSource.ToGridModel(CurrentPage, bindingContext.PageSize, SortDescriptors, FilterDescriptors);
                totalCount = model.Total;
                processedDataSource = model.Data;
            }
            else
            {
                processedDataSource = bindingContext.DataSource;
                totalCount = bindingContext.Total;
            }

            dataSourceIsProcessed = true;
        }
    }
}