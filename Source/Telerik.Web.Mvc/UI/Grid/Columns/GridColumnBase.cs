
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI.Html;

    /// <summary>
    /// Represents a column in the <see cref="Grid{T}"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public abstract class GridColumnBase<T> : IGridColumn where T : class
    {

        public string Format
        {
            get
            {
                return Settings.Format;
            }
            set
            {
                Settings.Format = value;
            }
        }
        
        public string EditorHtml
        {
            get;
            set;
        }

        protected GridColumnBase(Grid<T> grid)
        {
            Grid = grid;
            Settings = new GridColumnSettings();
            Visible = true;
            HeaderTemplate = new HtmlTemplate();
            FooterTemplate = new HtmlTemplate<GridAggregateResult>();
        }

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid<T> Grid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the member of the column.
        /// </summary>
        /// <value>The member.</value>
        public string Member
        {
            get
            {
                return Settings.Member;
            }
            
            set
            {
                Settings.Member = value;
            }
        }

        /// <summary>
        /// Gets the template of the column.
        /// </summary>
        public virtual Action<T> Template
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the header template of the column.
        /// </summary>
        public HtmlTemplate HeaderTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the footer template of the column.
        /// </summary>
        public HtmlTemplate<GridAggregateResult> FooterTemplate
        {
            get; 
            set;
        }

        public virtual Func<T, object> InlineTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the column.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get
            {
                return Settings.Title;
            }
            set
            {
                Settings.Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get
            {
                return Settings.Width;
            }
            set
            {
                Settings.Width = value;
            }
        }

        public string ClientTemplate
        {
            get
            {
                return Settings.ClientTemplate;
            }
            set
            {
                Settings.ClientTemplate = value;
            }
        }

        public string ClientFooterTemplate
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether this column is hidden.
        /// </summary>
        /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Hidden columns are output as HTML but are not visible by the end-user.
        /// </remarks>
        public virtual bool Hidden
        {
            get
            {
                return Settings.Hidden;
            }
            set
            {
                Settings.Hidden = value;
            }
        }

        public virtual bool Encoded
        {
            get
            {
                return Settings.Encoded;
            }
            
            set
            {
                Settings.Encoded = value;
            }
        }

        /// <summary>
        /// Gets the header HTML attributes.
        /// </summary>
        /// <value>The header HTML attributes.</value>
        public IDictionary<string, object> HeaderHtmlAttributes
        {
            get
            {
                return Settings.HeaderHtmlAttributes;
            }
        }

        /// <summary>
        /// Gets the footer HTML attributes.
        /// </summary>
        /// <value>The footer HTML attributes.</value>
        public IDictionary<string, object> FooterHtmlAttributes
        {
            get
            {
                return Settings.FooterHtmlAttributes;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        /// <remarks>
        /// Invisible columns are not output in the HTML.
        /// </remarks>
        public bool Visible
        {
            get
            {
                return Settings.Visible;
            }
            set
            {
                Settings.Visible = value;
            }
        }

        /// <summary>
        /// Gets the HTML attributes of the cell rendered for the column
        /// </summary>
        /// <value>The HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get
            {
                return Settings.HtmlAttributes;
            }
        }

        public virtual IGridColumnSerializer CreateSerializer()
        {
            return new GridColumnSerializer(this);
        }

        IGrid IGridColumn.Grid
        {
            get
            {
                return Grid;
            }
        }

        public bool IsLast
        {
            get
            {
                return Grid.VisibleColumns.LastOrDefault() == this;
            }
        }

        internal GridColumnSettings Settings
        {
            get;
            set;
        }

        protected void Decorate(IGridDecoratableCellBuilder cellBuilder, string lastCssClass)
        {
            if (IsLast && lastCssClass.HasValue())
            {
                cellBuilder.Decorators.Add(new GridLastCellBuilderDecorator(lastCssClass));
            }

            if (Hidden)
            {
                cellBuilder.Decorators.Add(new GridHiddenCellBuilderDecorator());
            }
        }

        private Action<object> CreateCallback(IGridDataCellBuilder builder, bool insert, bool edit)
        {
            return (dataItem) =>
            {
                if (Grid.CellAction != null)
                {
                    var cell = new GridCell<T>(this, (T)dataItem);
#if MVC2 || MVC3                    
                    cell.InEditMode = edit;
                    cell.InInsertMode = insert;
#endif
                    if (Template != null)
                    {
                        cell.Template.CodeBlockTemplate = Template;
                    }

                    if (InlineTemplate != null)
                    {
                        cell.Template.InlineTemplate = InlineTemplate;
                    }

                    Grid.CellAction(cell);

                    if (builder.HtmlAttributes != null)
                    {
                        builder.HtmlAttributes.Merge(cell.HtmlAttributes);
                    }
                    else
                    {
                        builder.HtmlAttributes = cell.HtmlAttributes;
                    }
                    
                    builder.Html = cell.Text;
                }
            };
        }

        public virtual IGridDataCellBuilder CreateDisplayBuilder(IGridHtmlHelper htmlHelper)
        {
            var builder = CreateDisplayBuilderCore(htmlHelper);
            
            Decorate(builder, UIPrimitives.Last);

            builder.Callback = CreateCallback(builder, false, false);
            
            return builder;
        }

        protected virtual IGridDataCellBuilder CreateDisplayBuilderCore(IGridHtmlHelper htmlHelper)
        {
            var template = new HtmlTemplate<T>();

            if (Template != null)
            {
                template.CodeBlockTemplate = Template;
            }

            if (InlineTemplate != null)
            {
                template.InlineTemplate = InlineTemplate;
            }

            return new GridTemplateCellBuilder<T>(template)
            {
                HtmlAttributes = HtmlAttributes
            };
        }

        public IGridDataCellBuilder CreateEditBuilder(IGridHtmlHelper htmlHelper)
        {
            var builder = CreateEditBuilderCore(htmlHelper);

            Decorate(builder, UIPrimitives.Last);

            builder.Callback = CreateCallback(builder, false, true);

            return builder;
        }

        protected abstract IGridDataCellBuilder CreateEditBuilderCore(IGridHtmlHelper htmlHelper);
        
        protected abstract IGridDataCellBuilder CreateInsertBuilderCore(IGridHtmlHelper htmlHelper);

        public IGridDataCellBuilder CreateInsertBuilder(IGridHtmlHelper htmlHelper)
        {
            var builder = CreateInsertBuilderCore(htmlHelper);

            Decorate(builder, UIPrimitives.Last);

            builder.Callback = CreateCallback(builder, true, false);

            return builder;
        }
        
        public IGridCellBuilder CreateHeaderBuilder()
        {
            var builder = CreateHeaderBuilderCore();
            
            Decorate(builder, UIPrimitives.LastHeader);

            return builder;
        }

        protected virtual IGridCellBuilder CreateHeaderBuilderCore()
        {
            return new GridHeaderCellBuilder(HeaderHtmlAttributes, AppendHeaderContent);
        }

        public IGridCellBuilder CreateFooterBuilder(IEnumerable<AggregateResult> aggregateResults)
        {
            var builder = CreateFooterBuilderCore(aggregateResults);
            
            Decorate(builder, string.Empty);

            return builder;
        }
        
        public IGridCellBuilder CreateGroupFooterBuilder(IEnumerable<AggregateResult> aggregateResults)
        {
            var builder = CreateGroupFooterBuilderCore(aggregateResults);

            Decorate(builder, UIPrimitives.Last);

            return builder;
        }

        protected virtual IGridCellBuilder CreateFooterBuilderCore(IEnumerable<AggregateResult> aggregateResults)
        {
            return new GridFooterCellBuilder(FooterHtmlAttributes, FooterTemplate);
        }
        
        protected virtual IGridCellBuilder CreateGroupFooterBuilderCore(IEnumerable<AggregateResult> aggregateResults)
        {
            return new GridFooterCellBuilder(FooterHtmlAttributes, FooterTemplate);
        }

        protected void AppendHeaderContent(IHtmlNode container)
        {
            if (HeaderTemplate != null && HeaderTemplate.HasValue())
            {
                HeaderTemplate.Apply(container);
            }
            else
            {
                container.Html(Title.HasValue() ? Title : "&nbsp;");
            }
        }
    }
}