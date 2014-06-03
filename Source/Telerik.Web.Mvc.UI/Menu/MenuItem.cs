namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Routing;

    public class MenuItem : LinkedObjectBase<MenuItem>, INavigationItem, IMenuItemContainer, IHideObjectMembers
    {
        private string text;
        private Action content;

        private string routeName;
        private string controllerName;
        private string actionName;
        private string url;
        private bool loadChildItemsAsynchronously;

        public MenuItem()
        {
            Enabled = true;
            Visible = true;
            Items = new LinkedObjectCollection<MenuItem>(this);
            HtmlAttributes = new RouteValueDictionary();
            RouteValues = new RouteValueDictionary();
        }

        public IList<MenuItem> Items
        {
            get;
            private set;
        }

        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        public string Text
        {
            [DebuggerStepThrough]
            get
            {
                return text;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                text = value;
                content = null;
            }
        }

        public Action Content
        {
            [DebuggerStepThrough]
            get
            {
                return content;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNull(value, "value");

                content = value;
                text = null;
            }
        }

        public bool Enabled
        {
            get;
            set;
        }

        public bool Visible
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

        public bool LoadChildItemsAsynchronously
        {
            [DebuggerStepThrough]
            get
            {
                return loadChildItemsAsynchronously;
            }

            [DebuggerStepThrough]
            set
            {
                loadChildItemsAsynchronously = value;
                Items.Clear();
            }
        }
    }
}