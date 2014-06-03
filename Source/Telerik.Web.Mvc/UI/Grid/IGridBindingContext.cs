﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;
    using System.Web.Mvc;
    using System.Collections.Generic;

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

        ControllerBase Controller
        {
            get;
        }

        IList<GroupDescriptor> GroupDescriptors
        {
            get;
        }

        IList<SortDescriptor> SortDescriptors
        {
            get;
        }

        IList<CompositeFilterDescriptor> FilterDescriptors
        {
            get;
        }

        string Prefix(string parameter);
    }
}