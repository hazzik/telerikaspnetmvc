// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class GridActionBindingContext : IGridBindingContext
    {
        public GridActionBindingContext(bool enableCustomBinding, IDictionary<string, ValueProviderResult> valueProvider, IEnumerable dataSource, int total)
        {
            EnableCustomBinding = enableCustomBinding;
            ValueProvider = valueProvider;
            DataSource = dataSource;
            Total = total;
        }

        public bool EnableCustomBinding
        {
            get;
            private set;
        }

        public IDictionary<string, ValueProviderResult> ValueProvider
        {
            get;
            private set;
        }

        public IEnumerable DataSource
        {
            get;
            set;
        }

        public int PageSize
        {
            get 
            {
                return ValueProvider.ValueOf<int>(Prefix(GridUrlParameters.PageSize)); 
            }
        }

        public int Total
        {
            get;
            set;
        }

        public string Prefix(string parameter)
        {
            return parameter;
        }
    }
}