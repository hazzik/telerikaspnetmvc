// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;

    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="Grid"/> component.
    /// </summary>
    public class GridBuilder<T> : ViewComponentBuilderBase<Grid<T>, GridBuilder<T>> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public GridBuilder(Grid<T> component) : base(component)
        {
        }


        /// <summary>
        /// Binds the grid to a list of objects
        /// </summary>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid<Order>()
        ///             .Name("Orders")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"]);
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> BindTo(IEnumerable<T> dataSource)
        {
            Component.DataSource = dataSource;

            return this;
        }

        /// <summary>
        /// Callback for each row.
        /// </summary>
        /// <param name="callback">Action, which will be executed for each row. 
        /// You can format the entire row</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .RowAction(row =>
        ///             {
        ///                 // "DataItem" is the Order object to which the current row is bound to
        ///                 if (row.DataItem.Freight > 10)
        ///                 {
        ///                     //Set the background of the entire row
        ///                     row.HtmlAttributes["style"] = "background:red;";
        ///                 }
        ///             });
        /// %&gt;
        /// </code>
        /// </example>
        public GridBuilder<T> RowAction(Action<GridRowRenderingContext<T>> callback)
        {
            Guard.IsNotNull(callback, "callback");

            Component.RowAction = callback;

            return this;
        }

        /// <summary>
        /// Callback for each cell.
        /// </summary>
        /// <param name="callback">Action, which will be executed for each cell. 
        /// You can format a concrete cell.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .CellAction(cell =>
        ///             {
        ///                if (cell.Column.Name == "Freight")
        ///                {
        ///                    if (cell.DataItem.Freight > 10)
        ///                    {
        ///                        //Set the background of this cell only
        ///                        cell.HtmlAttributes["style"] = "background:red;";
        ///                    }
        ///                }
        ///             });
        /// %&gt;
        /// </code>
        /// </example>
        public GridBuilder<T> CellAction(Action<GridCellRenderingContext<T>> callback)
        {
            Guard.IsNotNull(callback, "callback");

            Component.CellAction = callback;

            return this;
        }

        /// <summary>
        /// Enables or disables the custom binding of the grid.
        /// </summary>
        /// <param name="value">If true enables custom binding.</param>
        /// <returns></returns>
        public virtual GridBuilder<T> EnableCustomBinding(bool value)
        {
            Component.EnableCustomBinding = value;

            return this;
        }

        /// <summary>
        /// Defines the columns of the grid.
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"]);
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Columns(Action<GridColumnFactory<T>> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            GridColumnFactory<T> factory = new GridColumnFactory<T>(Component);

            addAction(factory);

            return this;
        }

        /// <summary>
        /// Allows sorting of the columns.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Sortable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Sortable()
        {
            Component.Sorting.Enabled = true;

            return this;
        }

        /// <summary>
        /// Allows sorting of the columns.
        /// </summary>
        /// <param name="sortSettingsAction">Use builder to define sort settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Sortable(sorting => sorting.SortMode(GridSortMode.MultipleColumn)
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Sortable(Action<GridSortSettingsBuilder> sortSettingsAction)
        {
            Guard.IsNotNull(sortSettingsAction, "sortSettingsAction");

            Component.Sorting.Enabled = true;

            sortSettingsAction(new GridSortSettingsBuilder(Component.Sorting));
            
            return this;
        }

        /// <summary>
        /// Put grid name as a prefix.
        /// </summary>
        public virtual GridBuilder<T> PrefixUrlParameters(bool prefix)
        {
            Component.PrefixUrlParameters = prefix;

            return this;
        }

        /// <summary>
        /// Allows paging of the data.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Pageable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Pageable()
        {
            return Pageable(delegate { });
        }

        /// <summary>
        /// Allows paging of the data.
        /// </summary>
        /// <param name="sortSettingsAction">Use builder to define paging settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Pageable(paging => 
        ///                        paging.PageSize(20)
        ///                              .Style(GridPagerStyles.NextPreviousAndNumeric)
        ///                              .Position(GridPagerPosition.Bottom)
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Pageable(Action<GridPagerSettingsBuilder> pagerAction)
        {
            Guard.IsNotNull(pagerAction, "pagerAction");

            Component.Paging.Enabled = true;
            pagerAction(new GridPagerSettingsBuilder(Component.Paging));

            return this;
        }
        
        /// <summary>
        /// Use it to configure Server binding.
        /// </summary>
        /// <param name="operationSettingsAction">Use builder to set different server binding settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .ServerBinding(serverBinding => serverBinding
        ///                 .Action("Index", "Home", new {id = (string)ViewData["id"]})
        ///             )
        ///             .Pagealbe()
        ///             .Sortable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> ServerBinding(Action<GridRequestSettingsBuilder> operationSettingsAction)
        {
            Guard.IsNotNull(operationSettingsAction, "operationSettingsAction");

            operationSettingsAction(new GridRequestSettingsBuilder(Component.ServerBinding));

            return this;
        }

        /// <summary>
        /// Use it to configure Ajax binding.
        /// </summary>
        /// <param name="operationSettingsAction">Use builder to set different ajax binding settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_AjaxBinding", "Home"))
        ///             .Pagealbe()
        ///             .Sortable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Ajax(Action<GridAjaxSettingsBuilder> ajaxSettingsAction)
        {
            Guard.IsNotNull(ajaxSettingsAction, "ajaxSettingsAction");
            
            Component.Ajax.Enabled = true;

            ajaxSettingsAction(new GridAjaxSettingsBuilder(Component.Ajax));
            
            return this;
        }

        /// <summary>
        /// Allows filtering of the columns.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Filterable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Filterable()
        {
            Component.Filtering.Enabled = true;
            return this;
        }

        /// <summary>
        /// Allows filtering of the columns.
        /// </summary>
        /// <param name="sortSettingsAction">Use builder to define filtering settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Filterable(filtering => filtering.Enabled(true);
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Filterable(Action<GridFilterSettingsBuilder> filteringAction)
        {
            Guard.IsNotNull(filteringAction, "filteringAction");

            Component.Filtering.Enabled = true;

            filteringAction(new GridFilterSettingsBuilder(Component.Filtering));

            return this;
        }

        /// <summary>
        /// Show scrollbar if there are many items.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Scrollable();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Scrollable()
        {
            Component.Scrolling.Enabled = true;

            return this;
        }

        /// <summary>
        /// Show scrollbar if there are many items.
        /// </summary>
        /// <param name="sortSettingsAction">Use builder to define scrolling settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        ///             .BindTo((IEnumerable<Order>)ViewData["Orders"])
        ///             .Scrollable(scrolling => scrolling.Enabled(true);
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Scrollable(Action<GridScrollSettingsBuilder> scrollingAction)
        {
            Guard.IsNotNull(scrollingAction, "scrollingAction");
            
            Component.Scrolling.Enabled = true;

            scrollingAction(new GridScrollSettingsBuilder(Component.Scrolling));
            
            return this;
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="eventsAction">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .ClientEvents(events => events
        ///                 .OnDataBinding("onDataBinding")
        ///                 .OnRowDataBound("onRowDataBound")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> ClientEvents(Action<GridClientEventsBuilder> eventsAction)
        {
            Guard.IsNotNull(eventsAction, "eventsAction");
            
            eventsAction(new GridClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }
        

        /// <summary>
        /// Use it to configure web service binding.
        /// </summary>
        /// <param name="webServiceAction">Use builder to set different web service binding settings.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid()
        ///             .Name("Grid")
        ///             .WebService(webService => webService.Url("~/Models/Orders.asmx/GetOrders"))
        ///             .Columns(columns=>
        ///             {
        ///                 columns.Add(c => c.OrderID).Width(100);
        ///                 columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
        ///                 columns.Add(c => c.ShipAddress);
        ///                 columns.Add(c => c.ShipCity).Width(200);
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> WebService(Action<GridWebServiceSettingsBuilder> webServiceAction)
        {
            Guard.IsNotNull(webServiceAction, "webServiceAction");

            Component.WebService.Enabled = true;

            webServiceAction(new GridWebServiceSettingsBuilder(Component.WebService));
            
            return this;
        }
    }
}