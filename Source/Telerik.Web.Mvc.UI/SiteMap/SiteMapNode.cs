namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Routing;

    public class SiteMapNode : LinkedObjectBase<SiteMapNode>, INavigationItem
    {
        private string title;
        private string routeName;
        private string controllerName;
        private string actionName;
        private string url;

        public SiteMapNode()
        {
            Visible = true;
            RouteValues = new RouteValueDictionary();
            IncludeInSearchEngineIndex = true;
            Attributes = new RouteValueDictionary();
            ChildNodes = new LinkedObjectCollection<SiteMapNode>(this);
        }

        public string Title
        {
            [DebuggerStepThrough]
            get
            {
                return title;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                title = value;
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public DateTime? LastModifiedAt
        {
            get;
            set;
        }

        public string RouteName
        {
            [DebuggerStepThrough]
            get
            {
                return routeName;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                routeName = value;
                controllerName = actionName = url = null;
            }
        }

        public string ControllerName
        {
            [DebuggerStepThrough]
            get
            {
                return controllerName;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                controllerName = value;

                routeName = url = null;
            }
        }

        public string ActionName
        {
            [DebuggerStepThrough]
            get
            {
                return actionName;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                actionName = value;

                routeName = url = null;
            }
        }

        public IDictionary<string, object> RouteValues
        {
            get;
            private set;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Url might not be a valid uri.")]
        public string Url
        {
            [DebuggerStepThrough]
            get
            {
                return url;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                url = value;

                routeName = controllerName = actionName = null;
                RouteValues.Clear();
            }
        }

        public SiteMapChangeFrequency ChangeFrequency
        {
            get;
            set;
        }

        public SiteMapUpdatePriority UpdatePriority
        {
            get;
            set;
        }

        public bool IncludeInSearchEngineIndex
        {
            get;
            set;
        }

        public IDictionary<string, object> Attributes
        {
            get;
            private set;
        }

        public IList<SiteMapNode> ChildNodes
        {
            get;
            private set;
        }
    }
}