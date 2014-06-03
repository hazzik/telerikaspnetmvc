// (c) Copyright Telerik Corp. 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    using Resources;

    public static class NavigationItemContainerExtensions
    {
        public static string GetItemUrl<TComponent, TItem>(this TComponent component, TItem item, ViewContext viewContext, IUrlGenerator generator) where TComponent : ViewComponentBase, INavigationItemContainer<TItem> where TItem : NavigationItem<TItem>, IContentContainer
        {
            IAsyncContentContainer asyncContentContainer = item as IAsyncContentContainer;

            if (asyncContentContainer != null && !string.IsNullOrEmpty(asyncContentContainer.ContentUrl))
            {
                return asyncContentContainer.ContentUrl;
            }

            if (item.Content != null)
            {
                if (string.IsNullOrEmpty(item.Url))
                    return component.GetItemContentId(item);
                else
                    return item.Url;
            }

            return item.GenerateUrl(viewContext, generator) ?? "#";
        }

        public static string GetImageUrl<T>(this T item, ViewContext viewContext) where T : NavigationItem<T>
        {
            var urlHelper = new UrlHelper(viewContext.RequestContext);

            return urlHelper.Content(item.ImageUrl);
        }

        private static string GetItemContentId<TComponent, TItem>(this TComponent component, TItem item) where TComponent : ViewComponentBase, INavigationItemContainer<TItem> where TItem : NavigationItem<TItem>, IContentContainer
        {
            return item.ContentHtmlAttributes.ContainsKey("id") ?
                   "#{0}".FormatWith(item.ContentHtmlAttributes["id"].ToString()) :
                   "#{0}-{1}".FormatWith(component.Id, (component.Items.IndexOf(item) + 1).ToString(Culture.Invariant));
        }

        public static void BindTo<T>(this INavigationItemContainer<T> component, string sitemapViewDataKey, ViewContext viewContext, Action<T, SiteMapNode> siteMapAction) where T : NavigationItem<T>, new()
        {
            var siteMap = viewContext.ViewData.Eval(sitemapViewDataKey) as SiteMapBase;

            if (siteMap == null)
            {
                throw new NotSupportedException(TextResource.SiteMapShouldBeDefinedInViewData.FormatWith(sitemapViewDataKey));
            }

            component.Items.Clear();

            foreach (SiteMapNode node in siteMap.RootNode.ChildNodes)
            {
                LoadItemsFromSiteMapNode(node, component, siteMapAction);
            }
        }

        public static void BindTo<T>(this INavigationItemContainer<T> component, string sitemapViewDataKey, ViewContext viewContext) where T : NavigationItem<T>, new()
        {
            BindTo(component, sitemapViewDataKey, viewContext, null);
        }

        public static void BindTo<TNavigationItem, TDataItem>(this INavigationItemContainer<TNavigationItem> component, IEnumerable<TDataItem> dataSource, Action<TNavigationItem, TDataItem> action) where TNavigationItem : NavigationItem<TNavigationItem>, new()
        {
            foreach (TDataItem dataItem in dataSource)
            {
                var item = new TNavigationItem();

                component.Items.Add(item);

                action(item, dataItem);
            }
        }

        private static bool isPathHighlighted;
        public static bool HighlightItem<T>(this INavigationItemContainer<T> component, ViewContext viewContext) where T : NavigationItem<T>
        {
            isPathHighlighted = false;
            component.Items.Each(item =>
            {
                HighlightSelectedItem(item, viewContext);
            });
            return isPathHighlighted;
        }

        private static void HighlightSelectedItem<T>(T item, ViewContext viewContext)
            where T : NavigationItem<T>
        {
            string controllerName = viewContext.RouteData.Values["controller"] as string;
            string actionName = viewContext.RouteData.Values["action"] as string;

            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(item.ControllerName) &&
                !string.IsNullOrEmpty(item.Text) && (string.Equals(controllerName.ToLower(), item.Text.ToLower()) ||
                 string.Equals(controllerName.ToLower(), item.ControllerName.ToLower())))
            {
                if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(item.ActionName) 
                    && string.Equals(actionName.ToLower(), item.ActionName.ToLower()))
                {
                    item.Selected = true;
                    isPathHighlighted = true;
                }
            }
            if (item is INavigationItemContainer<T>)
            {
                ((INavigationItemContainer<T>)item).Items.Each(subItem => { HighlightSelectedItem(subItem, viewContext); });
            }
        }

        private static void LoadItemsFromSiteMapNode<T>(SiteMapNode node, INavigationItemContainer<T> parent, Action<T, SiteMapNode> siteMapAction) where T : NavigationItem<T>, new()
        {
            var item = new T {Text = node.Title, Visible = node.Visible };

            if (!string.IsNullOrEmpty(node.RouteName))
            {
                item.RouteName = node.RouteName;
            }

            if (!string.IsNullOrEmpty(node.ControllerName) && !string.IsNullOrEmpty(node.ActionName))
            {
                item.ControllerName = node.ControllerName;
                item.ActionName = node.ActionName;
            }

            if (!string.IsNullOrEmpty(node.Url))
            {
                item.Url = node.Url;
            }

            item.RouteValues.AddRange(node.RouteValues);

            if (siteMapAction != null)
            {
                siteMapAction(item, node);
            }

            parent.Items.Add(item);

            if (item is INavigationItemContainer<T>)
            {
                node.ChildNodes.Each(childNode => LoadItemsFromSiteMapNode(childNode, (INavigationItemContainer<T>) item, siteMapAction));
            }
        }
    }
}