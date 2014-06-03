namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Routing;

    public class MenuItemBuilder : IHideObjectMembers
    {
        private readonly MenuItem item;

        public MenuItemBuilder(MenuItem item)
        {
            Guard.IsNotNull(item, "item");

            this.item = item;
        }

        public static implicit operator MenuItem(MenuItemBuilder builder)
        {
            return builder.ToItem();
        }

        public MenuItem ToItem()
        {
            return item;
        }

        public MenuItemBuilder HtmlAttributes(object htmlAttributes)
        {
            Guard.IsNotNull(htmlAttributes, "htmlAttributes");

            item.HtmlAttributes.Clear();
            item.HtmlAttributes.Merge(htmlAttributes);

            return this;
        }

        public MenuItemBuilder Text(string value)
        {
            item.Text = value;

            return this;
        }

        public MenuItemBuilder Content(Action htmlMarkups)
        {
            Guard.IsNotNull(htmlMarkups, "htmlMarkups");

            item.Content = htmlMarkups;

            return this;
        }

        public MenuItemBuilder Enabled(bool value)
        {
            item.Enabled = value;

            return this;
        }

        public MenuItemBuilder Visible(bool value)
        {
            item.Visible = value;

            return this;
        }

        public MenuItemBuilder Route(string routeName, RouteValueDictionary routeValues)
        {
            Guard.IsNotNullOrEmpty(routeName, "routeName");

            item.RouteName = routeName;

            SetRouteValues(routeValues);
            SetTextIfEmpty(routeName);

            return this;
        }

        public MenuItemBuilder Route(string routeName, object routeValues)
        {
            Route(routeName, null);

            SetRouteValues(routeValues);

            return this;
        }

        public MenuItemBuilder Route(string routeName)
        {
            return Route(routeName, (object) null);
        }

        public MenuItemBuilder Action(string controllerName, string actionName, RouteValueDictionary routeValues)
        {
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            item.ControllerName = controllerName;
            item.ActionName = actionName;

            SetRouteValues(routeValues);
            SetTextIfEmpty(actionName);

            return this;
        }

        public MenuItemBuilder Action(string controllerName, string actionName, object routeValues)
        {
            Action(controllerName, actionName, null);

            SetRouteValues(routeValues);

            return this;
        }

        public MenuItemBuilder Action(string controllerName, string actionName)
        {
            return Action(controllerName, actionName, (object) null);
        }

        public MenuItemBuilder Url(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            item.Url = value;

            return this;
        }

        public MenuItemBuilder ChildItems(Action<MenuItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            MenuItemFactory factory = new MenuItemFactory(item);

            addAction(factory);

            item.LoadChildItemsAsynchronously = false;

            return this;
        }

        public MenuItemBuilder LoadChildItemsAsynchronously(bool value)
        {
            item.LoadChildItemsAsynchronously = value;

            return this;
        }

        private void SetRouteValues(ICollection<KeyValuePair<string, object>> values)
        {
            if (!values.IsNullOrEmpty())
            {
                item.RouteValues.Clear();
                item.RouteValues.Merge(values);
            }
        }

        private void SetRouteValues(object values)
        {
            if (values != null)
            {
                item.RouteValues.Clear();
                item.RouteValues.Merge(values);
            }
        }

        private void SetTextIfEmpty(string value)
        {
            if (string.IsNullOrEmpty(item.Text))
            {
                item.Text = value;
            }
        }
    }
}