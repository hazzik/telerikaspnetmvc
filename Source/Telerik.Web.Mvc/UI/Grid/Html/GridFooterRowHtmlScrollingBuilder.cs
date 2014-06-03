// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Infrastructure;

    public class GridFooterRowHtmlScrollingBuilder : HtmlBuilderBase
    {
        private readonly IHtmlBuilder footerRowBuilder;
        private readonly IHtmlBuilder colGroupBuilder;

        public GridFooterRowHtmlScrollingBuilder(IHtmlBuilder footerRowBuilder, IHtmlBuilder colGroupBuilder)
        {
            this.footerRowBuilder = footerRowBuilder;
            this.colGroupBuilder = colGroupBuilder;
        }

        protected override IHtmlNode BuildCore()
        {
            var node = new HtmlTag("div")
                .AddClass(UIPrimitives.Grid.FooterTemplateRowWrap);

            var table = new HtmlTag("table")
                .Attribute("cellspacing", "0");

            table.AppendTo(node);
            colGroupBuilder.Build().AppendTo(table);

            var body = new HtmlTag("tbody");
            body.AppendTo(table);
            footerRowBuilder.Build().AppendTo(body);
            return node;
        }
    }
}