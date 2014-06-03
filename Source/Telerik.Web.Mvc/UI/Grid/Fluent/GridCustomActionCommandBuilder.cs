// (c) Copyright 2002-2011 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Web.Routing;
    using Telerik.Web.Mvc.UI;
    public class GridCustomActionCommandBuilder<T> : GridActionCommandBuilderBase<GridCustomActionCommand<T>, GridCustomActionCommandBuilder<T>>
        where T : class
    {
        public GridCustomActionCommandBuilder(GridCustomActionCommand<T> command) : base(command)
        {

        }

        public GridCustomActionCommandBuilder<T> Text(string text)
        {
            Command.Text = text;

            return this;
        }

        public GridCustomActionCommandBuilder<T> Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            Command.Action(actionName, controllerName, routeValues);
            
            return this;
        }

        public GridCustomActionCommandBuilder<T> Action(string actionName, string controllerName, object routeValues)
        {
            Command.Action(actionName, controllerName, routeValues);
            
            return this;
        }

        public GridCustomActionCommandBuilder<T> Action(string actionName, string controllerName)
        {
            Command.Action(actionName, controllerName, (object)null);

            return this;
        }

        public GridCustomActionCommandBuilder<T> SendDataKeys(bool value)
        {
            Command.SendDataKeys = value;

            return this;
        }

        public GridCustomActionCommandBuilder<T> SendState(bool value)
        {
            Command.SendState = value;

            return this;
        }

        public GridCustomActionCommandBuilder<T> Ajax(bool enabled)
        {
            Command.Ajax = enabled;

            return this;
        }

        public GridCustomActionCommandBuilder<T> DataRouteValues(Action<GridDataKeyFactory<T>> factory)
        {
            factory(new GridDataKeyFactory<T>(Command.DataRouteValues, true));

            return this;
        }
    }
}