// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IGridBindingContext
    {
        IEnumerable DataSource
        {
            get;
        }

        int PageSize
        {
            get;
        }

        int Total
        {
            get;
        }

        bool EnableCustomBinding
        {
            get;
        }

        IDictionary<string, ValueProviderResult> ValueProvider
        {
            get;
        }

        string Prefix(string parameter);
    }
}