// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;

    public class GridRenderer<T> : IGridRenderer<T> where T : class
    {
        public GridRenderer(Grid<T> grid, HtmlTextWriter htmlWriter)
        {
            Guard.IsNotNull(grid, "grid");
            Guard.IsNotNull(htmlWriter, "htmlWriter");

            Grid = grid;
            Writer = htmlWriter;
            UrlGenerator = new GridUrlGenerator<T>(grid);
        }

        protected GridUrlGenerator<T> UrlGenerator
        {
            get;
            private set;
        }

        protected Grid<T> Grid
        {
            get;
            private set;
        }

        protected HtmlTextWriter Writer
        {
            get;
            private set;
        }

        public virtual void GridStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Id, Grid.Id);
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-widget t-grid");
            Writer.AddAttributes(Grid.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        public virtual void GridEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void TableStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            Writer.RenderBeginTag(HtmlTextWriterTag.Table);
            Colgroup();
        }

        public virtual void TableEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void EmptyRow()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-no-data");
            Writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            Writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Grid.Columns.Count().ToString(Culture.Current));
            Writer.RenderBeginTag(HtmlTextWriterTag.Td);
            
            Writer.RenderEndTag();
            Writer.RenderEndTag();
        }

        public virtual void HeaderStart()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Thead);
        }

        public virtual void HeaderEnd()
        {
            Writer.RenderEndTag(); //</thead>
        }

        public virtual void HeaderRowStart()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Tr);
        }

        public virtual void HeaderRowEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void HeaderCellStart(GridColumn<T> column)
        {
            Guard.IsNotNull(column, "column");

            column.HeaderHtmlAttributes.PrependInValue("class", " ", "t-header");

            column.HeaderHtmlAttributes.Merge("scope", "col", false);

            Writer.AddAttributes(column.HeaderHtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Th);
        }

        public virtual void HeaderCellEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void HeaderCellContent(GridColumn<T> column)
        {
            Guard.IsNotNull(column, "column");

            if ((column.Sortable) && (Grid.Sorting.Enabled))
            {
                IList<SortDescriptor> orderBy = new List<SortDescriptor>(Grid.DataProcessor.SortDescriptors);
                SortDescriptor descriptor = orderBy.SingleOrDefault(c => c.Member.IsCaseInsensitiveEqual(column.Name));

                ListSortDirection? oldDirection = null;

                if (descriptor != null)
                {
                    oldDirection = descriptor.SortDirection;

                    ListSortDirection? newDirection = NextSortDirection(oldDirection);

                    if (newDirection == null)
                    {
                        orderBy.Remove(descriptor);
                    }
                    else
                    {
                        if (Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                        {
                            orderBy.Clear();
                            orderBy.Add(new SortDescriptor
                            {
                                SortDirection = newDirection.Value,
                                Member = descriptor.Member
                            });
                        }
                        else
                        {
                            orderBy[orderBy.IndexOf(descriptor)] = new SortDescriptor
                            {
                                SortDirection = newDirection.Value,
                                Member = descriptor.Member
                            };
                        }
                    }
                }
                else
                {
                    if (Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                    {
                        orderBy.Clear();
                    }
                    orderBy.Add(new SortDescriptor { Member = column.Name, SortDirection = ListSortDirection.Ascending });
                }

                string url = UrlGenerator.SortingUrl(Grid.UrlGenerator, orderBy);

                Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-link");
                Writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
                Writer.RenderBeginTag(HtmlTextWriterTag.A);
                Writer.Write(column.Title);

                if (oldDirection.HasValue)
                {
                    string iconCssClass = oldDirection == ListSortDirection.Ascending ? "t-arrow-up" : "t-arrow-down";
                    Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon " + iconCssClass);
                    Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    Writer.RenderEndTag();
                }

                Writer.RenderEndTag();
            }
            else
            {
                Writer.Write(column.Title);
            }

            if (Grid.Filtering.Enabled && column.Filterable)
            {
                Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-grid-filter t-state-default");
                Writer.RenderBeginTag(HtmlTextWriterTag.Div);
                Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-filter");
                Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                Writer.RenderEndTag();
                Writer.RenderEndTag();
            }
        }

        public virtual void Pager()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-pager t-reset");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            bool shouldRenderNextPrev = ((Grid.Paging.Style & GridPagerStyles.NextPrevious) != 0);

            bool shouldRenderNumeric = ((Grid.Paging.Style & GridPagerStyles.Numeric) != 0);

            bool shouldRenderPageInput = ((Grid.Paging.Style & GridPagerStyles.PageInput) != 0);

            Action<int, string> writeEnabledArrow = (page, text) => WritePagingArrow(Writer, text, true, page);
            Action<string> writeDisabledArrow = text => WritePagingArrow(Writer, text, false, null);

            if (shouldRenderNextPrev)
            {
                if (Grid.DataProcessor.CurrentPage > 1)
                {
                    writeEnabledArrow(1, "first");
                }
                else
                {
                    writeDisabledArrow("first");
                }

                if (Grid.DataProcessor.CurrentPage > 1)
                {
                    writeEnabledArrow(Grid.DataProcessor.CurrentPage - 1, "prev");
                }
                else
                {
                    writeDisabledArrow("prev");
                }
            }

            if (shouldRenderNumeric)
            {
                Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-numeric");
                Writer.RenderBeginTag(HtmlTextWriterTag.Div);

                const int NumericLinkSize = 10;
                int pageCount = Grid.DataProcessor.PageCount;
                int currentPage = Grid.DataProcessor.CurrentPage;

                int numericStart = 1;

                if (currentPage > NumericLinkSize)
                {
                    int reminder = (currentPage % NumericLinkSize);

                    numericStart = (reminder == 0) ?
                                   (currentPage - NumericLinkSize) + 1 :
                                   (currentPage - reminder) + 1;
                }

                int numericEnd = (numericStart + NumericLinkSize) - 1;

                if (numericEnd > pageCount)
                {
                    numericEnd = pageCount;
                }

                if (numericStart > 1)
                {
                    WriteNumericLink(Writer, "...", numericStart - 1);
                }

                for (int page = numericStart; page <= numericEnd; page++)
                {
                    if (page == currentPage)
                    {
                        Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.ActiveState);
                        Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        Writer.Write(page.ToString(Culture.Current));
                        Writer.RenderEndTag();
                    }
                    else
                    {
                        WriteNumericLink(Writer, page.ToString(Culture.Current), page);
                    }
                }

                if (numericEnd < pageCount)
                {
                    WriteNumericLink(Writer, "...", numericEnd + 1);
                }

                Writer.RenderEndTag();
            }

            if (shouldRenderPageInput)
            {
                Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-page-i-of-n");
                Writer.RenderBeginTag(HtmlTextWriterTag.Div);
                Writer.Write("Page ");
                Writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                Writer.AddAttribute(HtmlTextWriterAttribute.Value, Grid.DataProcessor.CurrentPage.ToString(Culture.Current));
                Writer.RenderBeginTag(HtmlTextWriterTag.Input);
                Writer.RenderEndTag();
                Writer.Write(" of {0}".FormatWith(Grid.DataProcessor.PageCount));
                Writer.RenderEndTag();
            }

            if (shouldRenderNextPrev)
            {
                if (Grid.DataProcessor.CurrentPage < Grid.DataProcessor.PageCount)
                {
                    writeEnabledArrow(Grid.DataProcessor.CurrentPage + 1, "next");
                }
                else
                {
                    writeDisabledArrow("next");
                }

                if (Grid.DataProcessor.CurrentPage < Grid.DataProcessor.PageCount)
                {
                    writeEnabledArrow(Grid.DataProcessor.PageCount, "last");
                }
                else
                {
                    writeDisabledArrow("last");
                }
            }

            Writer.RenderEndTag();

            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-status-text");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);
            int start = ((Grid.DataProcessor.CurrentPage - 1) * Grid.Paging.PageSize) + 1;
            int end = start + Grid.Paging.PageSize - 1;
            Writer.Write("Displaying items {0} - {1} of {2}".FormatWith(start, end, Grid.DataProcessor.Total));
            Writer.RenderEndTag();
        }
        
        public virtual void FooterStart()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
        }

        public virtual void FooterEnd()
        {
            Writer.RenderEndTag(); // </tfoot>
        }

        public virtual void FooterRowStart()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Tr);
        }

        public virtual void FooterRowEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void FooterCellStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-footer");
            Writer.AddAttribute(HtmlTextWriterAttribute.Colspan, Grid.Columns.Count.ToString(Culture.Current));
            Writer.RenderBeginTag(HtmlTextWriterTag.Td);
        }

        public virtual void FooterCellEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void BodyStart()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
        }

        public virtual void BodyEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void RowStart(GridRowRenderingContext<T> renderingContext)
        {
            if (renderingContext.IsAlternate)
            {
                renderingContext.HtmlAttributes.AppendInValue("class", " ", "t-alt");
            }

            Writer.AddAttributes(renderingContext.HtmlAttributes);

            Writer.RenderBeginTag(HtmlTextWriterTag.Tr);
        }

        public virtual void RowEnd()
        {
            Writer.RenderEndTag();
        }

        public void RowCellStart(GridCellRenderingContext<T> renderingContext)
        {
            Guard.IsNotNull(renderingContext, "renderingContext");

            GridColumn<T> column = renderingContext.Column;

            IDictionary<string, object> htmlAttributes = new Dictionary<string, object>(column.HtmlAttributes, StringComparer.OrdinalIgnoreCase);

            htmlAttributes.Merge(renderingContext.HtmlAttributes, true);

            Writer.AddAttributes(htmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Td);
        }

        public void RowCellContent(GridCellRenderingContext<T> renderingContext)
        {
            Guard.IsNotNull(renderingContext, "renderingContext");

            GridColumn<T> column = renderingContext.Column;
            T row = renderingContext.DataItem;

            if (renderingContext.Content != null)
            {
                renderingContext.Content(row);
            }
            else if (!string.IsNullOrEmpty(renderingContext.Text))
            {
                Writer.Write(renderingContext.Text);
            }
            else
            {
                if (column.Template != null)
                {
                    column.Template(row);
                }
                else
                {
                    Func<string, string> encode = value => column.Encoded ? HttpUtility.HtmlEncode(value) : value;
                    
                    object content = column.Value(row) ?? string.Empty;
                    
                    if (string.IsNullOrEmpty(column.Format))
                    {
                        Writer.Write(content.ToString());
                    }
                    else
                    {
                        Writer.Write(encode(column.Format.FormatWith(content)));
                    }
                }
            }
        }

        public virtual void RowCellEnd()
        {
            Writer.RenderEndTag();
        }

        public virtual void LoadingIndicator()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-status");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Icon + " t-refresh");
            Writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            Writer.RenderBeginTag(HtmlTextWriterTag.A);
            Writer.RenderEndTag();

            Writer.RenderEndTag();
        }

        public virtual void Colgroup()
        {
            Writer.RenderBeginTag(HtmlTextWriterTag.Colgroup);

            Grid.Columns.Where(column => column.Visible).Each(column =>
            {
                if (!string.IsNullOrEmpty(column.Width))
                {
                    Writer.AddStyleAttribute(HtmlTextWriterStyle.Width, column.Width);
                }

                Writer.RenderBeginTag(HtmlTextWriterTag.Col);
                Writer.RenderEndTag();
            });

            Writer.RenderEndTag();
        }

        private static ListSortDirection? NextSortDirection(ListSortDirection? direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                return ListSortDirection.Descending;
            }

            if (direction == ListSortDirection.Descending)
            {
                return null;
            }

            return ListSortDirection.Ascending;
        }

        private void WritePagingArrow(HtmlTextWriter writer, string text, bool enabled, int? page)
        {
            string cssClass = UIPrimitives.Link;
            string url = "javascript:void(0)";

            if (!enabled)
            {
                cssClass += " " + UIPrimitives.DisabledState;
            }
            else if (page.HasValue)
            {
                url = UrlGenerator.PagingUrl(Grid.UrlGenerator, page.Value);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-{0}".FormatWith(text));
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(text);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void WriteNumericLink(HtmlTextWriter writer, string text, int page)
        {
            string url = UrlGenerator.PagingUrl(Grid.UrlGenerator, page);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Link);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(text);
            writer.RenderEndTag();
        }
    }
}