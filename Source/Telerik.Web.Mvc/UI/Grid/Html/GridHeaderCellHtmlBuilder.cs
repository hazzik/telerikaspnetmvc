// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Extensions;
    using Infrastructure;

    public class GridHeaderCellHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGridColumn column;

        public GridHeaderCellHtmlBuilder(IGridColumn column)
        {
            Guard.IsNotNull(column, "column");

            this.column = column;
        }

        protected override IHtmlNode BuildCore()
        {
            var node = new HtmlTag("th")
                .Attributes(column.HeaderHtmlAttributes)
                .ToggleClass(UIPrimitives.LastHeader, column.IsLast)
                .AddClass(UIPrimitives.Header)
                .Attribute("scope", "col");

            AppendContent(node);

            return node;
        }

        private void AppendContent(IHtmlNode node)
        {
            var headerTemplate = column.HeaderTemplate;
            if (headerTemplate != null && headerTemplate.HasValue())
            {
                headerTemplate.Apply(node);
            }
            else
            {
                node.Html(column.Title.HasValue() ? column.Title : "&nbsp;");
            }
        }
    }
}
