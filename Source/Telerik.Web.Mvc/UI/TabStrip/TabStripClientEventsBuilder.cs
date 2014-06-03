// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;

    using Infrastructure;

    public class TabStripClientEventsBuilder : IHideObjectMembers
    {
        private readonly TabStripClientEvents clientEvents;
        private readonly ViewContext viewContext;

        public TabStripClientEventsBuilder(TabStripClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        public virtual TabStripClientEventsBuilder OnSelect(Action javaScript)
        {
            Guard.IsNotNull(javaScript, "javaScript");

            clientEvents.OnSelect = javaScript;

            return this;
        }

        public virtual TabStripClientEventsBuilder OnSelect(string handlerName)
        {
            Guard.IsNotNullOrEmpty(handlerName, "handlerName");

            clientEvents.OnSelect = () => viewContext.HttpContext.Response.Write(handlerName);

            return this;
        }

        public virtual TabStripClientEventsBuilder OnLoad(Action javaScript)
        {
            Guard.IsNotNull(javaScript, "javaScript");

            clientEvents.OnLoad = javaScript;

            return this;
        }

        public virtual TabStripClientEventsBuilder OnLoad(string handlerName)
        {
            Guard.IsNotNullOrEmpty(handlerName, "handlerName");

            clientEvents.OnLoad = () => viewContext.HttpContext.Response.Write(handlerName);

            return this;
        }

        public virtual TabStripClientEventsBuilder OnError(Action javaScript)
        {
            Guard.IsNotNull(javaScript, "javaScript");

            clientEvents.OnError = javaScript;

            return this;
        }

        public virtual TabStripClientEventsBuilder OnError(string handlerName)
        {
            Guard.IsNotNullOrEmpty(handlerName, "handlerName");

            clientEvents.OnError = () => viewContext.HttpContext.Response.Write(handlerName);

            return this;
        }
    }
}