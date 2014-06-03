// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;
    using Infrastructure.Implementation;
    using Telerik.Web.Mvc.Resources;

    /// <summary>
    /// Telerik Grid for ASP.NET MVC is a view component for presenting tabular data.
    /// It supports the following features:
    /// <list type="bullet">
    ///     <item>Flexible databinding - server, ajax and web service</item>
    ///     <item>Paging, sorting and filtering</item>
    ///     <item>Light HTML and JavaScript footprint</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the data item which the grid is bound to.</typeparam>
    public class Grid<T> : ViewComponentBase, IGridBindingContext, IGridColumnContainer<T> where T : class
    {
        private readonly IGridRendererFactory rendererFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        /// <param name="urlGenerator">The URL generator.</param>
        /// <param name="rendererFactory">The renderer factory.</param>
        public Grid(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, IGridRendererFactory rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(rendererFactory, "rendererFactory");

            this.rendererFactory = rendererFactory;
            UrlGenerator = urlGenerator;

            PrefixUrlParameters = true;
            DataProcessor = new GridDataProcessor(this);
            Columns = new List<GridColumn<T>>();
            Paging= new GridPagerSettings();
            Sorting = new GridSortSettings();
            ServerBinding = new GridRequestSettings();
            Scrolling = new GridScrollSettings();
            Ajax = new GridAjaxSettings();
            ClientEvents = new GridClientEvents();
            WebService = new GridWebServiceSettings();
            Filtering = new GridFilterSettings();

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.grid.js" });
        }

        /// <summary>
        /// Gets the client events of the grid.
        /// </summary>
        /// <value>The client events.</value>
        public GridClientEvents ClientEvents
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the filtering configuration.
        /// </summary>
        public GridFilterSettings Filtering
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the web service configuration
        /// </summary>
        public GridWebServiceSettings WebService
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the server binding configuration.
        /// </summary>
        public GridRequestSettings ServerBinding
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the scrolling configuration.
        /// </summary>
        public GridScrollSettings Scrolling
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ajax configuration.
        /// </summary>
        public GridAjaxSettings Ajax
        {
            get;
            private set;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public GridDataProcessor DataProcessor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether custom binding is enabled.
        /// </summary>
        /// <value><c>true</c> if custom binding is enabled; otherwise, <c>false</c>. The default value is <c>false</c></value>
        public bool EnableCustomBinding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the paging configuration.
        /// </summary>
        public GridPagerSettings Paging
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the columns of the grid.
        /// </summary>
        public IList<GridColumn<T>> Columns
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public IEnumerable<T> DataSource
        {
            get;
            set;
        }

        int IGridBindingContext.Total
        {
            get
            {
                return Paging.Total;
            }
        }

        IEnumerable IGridBindingContext.DataSource
        {
            get
            {
                return DataSource;
            }
        }

        IDictionary<string, ValueProviderResult> IGridBindingContext.ValueProvider
        {
            get
            {
                return ViewContext.Controller.ValueProvider;
            }
        }

        /// <summary>
        /// Gets the page size of the grid.
        /// </summary>
        public int PageSize
        {
            get
            {
                if (!Paging.Enabled)
                {
                    return 0;
                }

                return Paging.PageSize;
            }
        }

        /// <summary>
        /// Gets the sorting configuration.
        /// </summary>
        /// <value>The sorting.</value>
        public GridSortSettings Sorting
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to add the <see cref="Name"/> property of the grid as a prefix in url parameters.
        /// </summary>
        /// <value><c>true</c> if prefixing is enabled; otherwise, <c>false</c>. The default value is <c>true</c></value>
        public bool PrefixUrlParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the action executed when rendering a row.
        /// </summary>
        public Action<GridRowRenderingContext<T>> RowAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the action executed when rendering a cell.
        /// </summary>
        public Action<GridCellRenderingContext<T>> CellAction
        {
            get;
            set;
        }

        public string Prefix(string parameter)
        {
            return PrefixUrlParameters ? Id + "-" + parameter : parameter;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tGrid", writer);
            objectWriter.Start();
            List<IDictionary<string, object>> columns = new List<IDictionary<string, object>>();

            Columns.Where(c => c.Visible).Each(c =>
            {
                IDictionary<string, object> column = new Dictionary<string, object>
                {
                    { "name", c.Name },
                    { "type", c.DataType.ToJavaScriptType() }
                };

                if (!string.IsNullOrEmpty(c.Format))
                {
                    column.Add("format", c.Format);
                }

                if (Filtering.Enabled)
                {
                    IList<FilterDescriptor> columnFilters = DataProcessor.FilterDescriptors.SelectRecursive(descriptor =>
                        {
                            CompositeFilterDescriptor compositeDescriptor = descriptor as CompositeFilterDescriptor;

                            if (compositeDescriptor != null)
                            {
                                return compositeDescriptor.FilterDescriptors;
                            }

                            return null;
                        })
                        .Where(descriptor => descriptor is FilterDescriptor)
                        .Cast<FilterDescriptor>()
                        .Where(descriptor => descriptor.Member == c.Name)
                        .ToList();

                    if (columnFilters.Count > 0)
                    {
                        ArrayList filters = new ArrayList();
                        column.Add("filters", filters);
                        columnFilters.Each(filter => filters.Add(new { @operator = filter.Operator.ToToken(), value = filter.Value }));
                    }
                }

                columns.Add(column);
            });

            objectWriter.AppendCollection("columns", columns);

            if (Filtering.Enabled)
            {
                objectWriter.AppendCollection("plugins", new[] { "gridFilter" });

                if (!Ajax.Enabled)
                {
                    RouteValueDictionary routeData = new RouteValueDictionary(ViewContext.RequestContext.RouteData.Values);

                    routeData[Prefix(GridUrlParameters.CurrentPage)] = "{0}";
                    routeData[Prefix(GridUrlParameters.OrderBy)] = "{1}";
                    routeData[Prefix(GridUrlParameters.Filter)] = "{2}";

                    objectWriter.Append("urlFormat", ServerBinding.GenerateUrl(ViewContext, UrlGenerator, routeData));
                }
            }

            if (Paging.Enabled)
            {
                objectWriter.Append("pageSize", PageSize, 10);
                objectWriter.Append("total", DataProcessor.Total);
            }
            else
            {
                objectWriter.Append("pageSize", 0);
            }

            if (Sorting.Enabled)
            {
                objectWriter.Append("sortMode", Sorting.SortMode == GridSortMode.MultipleColumn ? "multi" : "single");
            }

            IList<SortDescriptor> orderBy = DataProcessor.SortDescriptors;

            if (orderBy.Count > 0)
            {
                objectWriter.Append("orderBy", GridSortDescriptorSerializer.Serialize(orderBy));
            }

            if (Ajax.Enabled && !WebService.Enabled)
            {
                objectWriter.Append("ajaxUrl", Ajax.GenerateUrl(ViewContext, UrlGenerator, ViewContext.RequestContext.RouteData.Values));
            }

            if (WebService.Enabled)
            {
                objectWriter.Append("ajaxUrl", UrlGenerator.Generate(ViewContext.RequestContext, WebService.Url));
                objectWriter.Append("ws", true);
            }

            objectWriter.Append("onError", ClientEvents.OnError);
            objectWriter.Append("onDataBinding", ClientEvents.OnDataBinding);
            objectWriter.Append("onRowDataBound", ClientEvents.OnRowDataBound);
            objectWriter.Append("onLoad", ClientEvents.OnLoad);

            objectWriter.Complete();
            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            VerifyWorkingConditions();

            if (Filtering.Enabled)
            {
                ScriptFileNames.Add("telerik.grid.filtering.js");
            }

            if (Columns.IsEmpty())
            {
                Columns.AddRange(new GridColumnGenerator().GetColumns<T>());
            }

            IGridRenderer<T> renderer = rendererFactory.Create(this, writer);

            renderer.GridStart();

            WriteTable(renderer);

            renderer.GridEnd();

            base.WriteHtml(writer);
        }
		
        private void VerifyWorkingConditions()
		{
            if (Ajax.Enabled && WebService.Enabled)
            {
                throw new NotSupportedException(TextResource.CannotUseAjaxAndWebServiceAtTheSameTime);
            }

            if (Ajax.Enabled || WebService.Enabled)
            {
                if (Columns.Where(c => c.Template != null).Count() > 0)
                {
                    throw new NotSupportedException(TextResource.CannotUseTemplatesInAjaxOrWebService);
                }
            }

            if (WebService.Enabled && string.IsNullOrEmpty(WebService.Url))
            {
                throw new ArgumentException(TextResource.WebServiceUrlRequired);
            }
		}

        private static void WriteHeaderCell(IGridRenderer<T> renderer, GridColumn<T> column)
        {
            renderer.HeaderCellStart(column);
            renderer.HeaderCellContent(column);
            renderer.HeaderCellEnd();
        }

        private void WriteTable(IGridRenderer<T> renderer)
        {
            if (!Scrolling.Enabled)
            {
                renderer.TableStart();
                WriteHeader(renderer);
                WriteFooter(renderer);
                WriteRows(renderer);
                renderer.TableEnd();
            }
            else
            {
                WriteHeader(renderer);
                WriteRows(renderer);
                WriteFooter(renderer);
            }
        }

        private void WriteHeader(IGridRenderer<T> renderer)
        {
            renderer.HeaderStart();

            if (Paging.Enabled && (Paging.Position == GridPagerPosition.Top || Paging.Position == GridPagerPosition.Both))
            {
                renderer.HeaderRowStart();
                renderer.FooterCellStart(); // TODO: Need a another version for header
                renderer.LoadingIndicator();
                renderer.Pager();
                renderer.FooterCellEnd(); // TODO: Need a another version for header
                renderer.HeaderRowEnd();
            }

            renderer.HeaderRowStart();

            var columnsToRender = Columns.Where(c => c.Visible);

            int columnIndex = 0;

            columnsToRender.Each(column =>
            {
                if (columnIndex++ == columnsToRender.Count() - 1)
                {
                    column.HeaderHtmlAttributes.AppendInValue("class", " ", UIPrimitives.LastHeader);
                }

                WriteHeaderCell(renderer, column);
            });

            renderer.HeaderRowEnd();

            renderer.HeaderEnd();
        }

        private void WriteRows(IGridRenderer<T> renderer)
        {
            renderer.BodyStart();

            bool empty = true;

            if (DataProcessor.ProcessedDataSource != null)
            {
                IEnumerable<T> dataSource = DataProcessor.ProcessedDataSource.Cast<T>();

                int rowIndex = 0;

                dataSource.Each(row =>
                {
                    WriteRow(renderer, row, rowIndex);
                    rowIndex += 1;
                    empty = false;
                });
            }
            
            if (empty)
            {
                renderer.EmptyRow();
            }

            renderer.BodyEnd();
        }

        private void WriteRow(IGridRenderer<T> renderer, T row, int index)
        {
            GridRowRenderingContext<T> renderingContext = new GridRowRenderingContext<T>(row, index);

            if (RowAction != null)
            {
                RowAction(renderingContext);
            }

            renderer.RowStart(renderingContext);

            Columns.Each(column =>
            {
                if (column.Visible)
                {
                    WriteCell(renderer, column, row, index);
                }
            });

            renderer.RowEnd();
        }

        private void WriteCell(IGridRenderer<T> renderer, GridColumn<T> column, T row, int rowIndex)
        {
            GridCellRenderingContext<T> renderingContext = new GridCellRenderingContext<T>(column, row, rowIndex);

            if (CellAction != null)
            {
                CellAction(renderingContext);
            }

            renderer.RowCellStart(renderingContext);
            renderer.RowCellContent(renderingContext);
            renderer.RowCellEnd();
        }

        private void WriteFooter(IGridRenderer<T> renderer)
        {
            renderer.FooterStart();

            renderer.FooterRowStart();
            renderer.FooterCellStart();
            renderer.LoadingIndicator();
            if (Paging.Enabled && (Paging.Position == GridPagerPosition.Bottom || Paging.Position == GridPagerPosition.Both))
            {
                renderer.Pager();
            }
            renderer.FooterCellEnd();
            renderer.FooterRowEnd();

            renderer.FooterEnd();
        }
    }
}