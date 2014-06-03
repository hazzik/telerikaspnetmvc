// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using Extensions;

    public class GridGroupDescriptorFactory<TModel> : IHideObjectMembers
        where TModel : class
    {
        private readonly GridGroupingSettings settings;

        public GridGroupDescriptorFactory(GridGroupingSettings settings)
        {
            this.settings = settings;
        }

        public GridGroupDescriptorBuilder<TModel> Add<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return Add<TValue>(expression, ListSortDirection.Ascending);
        }
        
        public GridGroupDescriptorBuilder<TModel> AddDescending<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return Add<TValue>(expression, ListSortDirection.Descending);
        }
        
        private GridGroupDescriptorBuilder<TModel> Add<TValue>(Expression<Func<TModel, TValue>> expression, ListSortDirection sortDirection)
        {
            var descriptor = new GroupDescriptor();
            descriptor.Member = expression.MemberWithoutInstance();
            descriptor.SortDirection = sortDirection;
            descriptor.MemberType = typeof(TValue);

            settings.Groups.Add(descriptor);

            return new GridGroupDescriptorBuilder<TModel>(descriptor);
        }
    }
}
