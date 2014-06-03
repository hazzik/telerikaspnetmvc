// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="Grid{T}.ClientEvents"/>.
    /// </summary>
    public class GridClientEventsBuilder : IHideObjectMembers
    {
        private readonly GridClientEvents events;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="viewContext">The view context.</param>
        public GridClientEventsBuilder(GridClientEvents events, ViewContext viewContext)
        {
            this.events = events;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnLoad(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //Error handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnLoad(Action onLoadAction)
        {
            Guard.IsNotNull(onLoadAction, "onLoadAction");

            events.OnLoad = onLoadAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            events.OnLoad = HandlerAction(onLoadHandlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnError client-side event.
        /// </summary>
        /// <param name="onErrorAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnError(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //Error handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnError(Action onErrorAction)
        {
            Guard.IsNotNull(onErrorAction, "onErrorAction");

            events.OnError = onErrorAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnError client-side event.
        /// </summary>
        /// <param name="onErrorHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnError(string onErrorHandlerName)
        {
            Guard.IsNotNullOrEmpty(onErrorHandlerName, "onErrorHandlerName");

            events.OnError = HandlerAction(onErrorHandlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline error handler of the OnDataBinding client-side event.
        /// </summary>
        /// <param name="onErrorAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnDataBinding(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //data binding handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBinding(Action onDataBindingAction)
        {
            Guard.IsNotNull(onDataBindingAction, "onDataBindingAction");

            events.OnDataBinding = onDataBindingAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDataBinding client-side event.
        /// </summary>
        /// <param name="onDataBindingHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnDataBinding("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBinding(string onDataBindingHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBindingHandlerName, "onDataBindingHandlerName");

            events.OnDataBinding = HandlerAction(onDataBindingHandlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline error handler of the OnRowDataBound client-side event.
        /// </summary>
        /// <param name="onRowDataBoundAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnRowDataBound(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     var row = e.row;
        ///                     var dataItem = e.dataItem;
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowDataBound(Action onRowDataBoundAction)
        {
            Guard.IsNotNull(onRowDataBoundAction, "onRowDataBoundAction");

            events.OnRowDataBound = onRowDataBoundAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnRowDataBound client-side event.
        /// </summary>
        /// <param name="onRowDataBoundHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnRowDataBound("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowDataBound(string onRowDataBoundHandlerName)
        {
            Guard.IsNotNullOrEmpty(onRowDataBoundHandlerName, "onRowDataBoundHandlerName");

            events.OnRowDataBound = HandlerAction(onRowDataBoundHandlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }
    }
}