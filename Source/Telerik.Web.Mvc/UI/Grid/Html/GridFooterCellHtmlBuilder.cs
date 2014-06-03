// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Infrastructure;

    public class GridFooterCellHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGridColumn column;

        public GridFooterCellHtmlBuilder(IGridColumn column)
        {
            this.column = column;
        }

        protected override IHtmlNode BuildCore()
        {
            IHtmlNode node = new HtmlTag("td")
                .Attributes(column.FooterHtmlAttributes);

            ApplyContent(node);

            return node;
        }

        private void ApplyContent(IHtmlNode node)
        {
            var footerTemplate = column.FooterTemplate;
            if (footerTemplate != null && footerTemplate.HasValue())
            {
                footerTemplate.Apply(node);
            }
            else
            {
                node.Html("&nbsp;");
            }
        }
    }
}