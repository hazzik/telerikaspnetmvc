// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;
    using System;

    public class NavigationItemFactory<TItem, TBuilder> : IHideObjectMembers
        where TItem : NavigationItem<TItem>, new()
        where TBuilder : NavigationItemBuilder<TItem>, new()
    {
        private readonly INavigationItemContainer<TItem> container;

        public NavigationItemFactory(INavigationItemContainer<TItem> container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        public TBuilder Add(Func<TBuilder> builderFactory)
        {
            TItem instance = new TItem();

            container.Items.Add(instance);

            return builderFactory.Invoke();
        }
    }
}
