namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Routing;

    public class SiteMapNodeBuilder : IHideObjectMembers
    {
        private readonly SiteMapNode siteMapNode;

        public SiteMapNodeBuilder(SiteMapNode siteMapNode)
        {
            Guard.IsNotNull(siteMapNode, "siteMapNode");

            this.siteMapNode = siteMapNode;
        }

        public static implicit operator SiteMapNode(SiteMapNodeBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToNode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public SiteMapNode ToNode()
        {
            return siteMapNode;
        }

        public SiteMapNodeBuilder Title(string value)
        {
            siteMapNode.Title = value;

            return this;
        }

        public SiteMapNodeBuilder Visible(bool value)
        {
            siteMapNode.Visible = value;

            return this;
        }

        public SiteMapNodeBuilder LastModifiedAt(DateTime value)
        {
            siteMapNode.LastModifiedAt = value;

            return this;
        }

        public SiteMapNodeBuilder Route(string routeName, RouteValueDictionary routeValues)
        {
            siteMapNode.RouteName = routeName;

            SetRouteValues(routeValues);
            SetTitleIfEmpty(routeName);

            return this;
        }

        public SiteMapNodeBuilder Route(string routeName, object routeValues)
        {
            Route(routeName, null);

            SetRouteValues(routeValues);

            return this;
        }

        public SiteMapNodeBuilder Route(string routeName)
        {
            return Route(routeName, (object) null);
        }

        public SiteMapNodeBuilder Action(string controllerName, string actionName, RouteValueDictionary routeValues)
        {
            siteMapNode.ControllerName = controllerName;
            siteMapNode.ActionName = actionName;

            SetRouteValues(routeValues);
            SetTitleIfEmpty(actionName);

            return this;
        }

        public SiteMapNodeBuilder Action(string controllerName, string actionName, object routeValues)
        {
            Action(controllerName, actionName, null);

            SetRouteValues(routeValues);

            return this;
        }

        public SiteMapNodeBuilder Action(string controllerName, string actionName)
        {
            return Action(controllerName, actionName, (object) null);
        }

        public SiteMapNodeBuilder Url(string value)
        {
            siteMapNode.Url = value;

            return this;
        }

        public SiteMapNodeBuilder ChangeFrequency(SiteMapChangeFrequency value)
        {
            siteMapNode.ChangeFrequency = value;

            return this;
        }

        public SiteMapNodeBuilder UpdatePriority(SiteMapUpdatePriority value)
        {
            siteMapNode.UpdatePriority = value;

            return this;
        }

        public SiteMapNodeBuilder IncludeInSearchEngineIndex(bool value)
        {
            siteMapNode.IncludeInSearchEngineIndex = value;

            return this;
        }

        public SiteMapNodeBuilder Attributes(IDictionary<string, object> attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            siteMapNode.Attributes.Clear();
            siteMapNode.Attributes.Merge(attributes);

            return this;
        }

        public SiteMapNodeBuilder Attributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            return Attributes(new RouteValueDictionary(attributes));
        }

        public SiteMapNodeBuilder ChildNodes(Action<SiteMapNodeFactory> addActions)
        {
            Guard.IsNotNull(addActions, "addActions");

            SiteMapNodeFactory factory = new SiteMapNodeFactory(siteMapNode);

            addActions(factory);

            return this;
        }

        private void SetRouteValues(ICollection<KeyValuePair<string, object>> values)
        {
            if (!values.IsNullOrEmpty())
            {
                siteMapNode.RouteValues.Clear();
                siteMapNode.RouteValues.AddRange(values);
            }
        }

        private void SetRouteValues(object values)
        {
            if (values != null)
            {
                siteMapNode.RouteValues.Clear();
                siteMapNode.RouteValues.Merge(values);
            }
        }

        private void SetTitleIfEmpty(string value)
        {
            if (string.IsNullOrEmpty(siteMapNode.Title))
            {
                siteMapNode.Title = value;
            }
        }
    }
}