// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Infrastructure;

    public class GridFooterRowHtmlBuilder : HtmlBuilderBase
    {
        private readonly IEnumerable<IGridColumn> columns;
        private readonly Func<IGridColumn, IHtmlBuilder> childBuilderFactory;

        public GridFooterRowHtmlBuilder(IEnumerable<IGridColumn> columns,
            Func<IGridColumn, IHtmlBuilder> childBuilderFactory)
        {
            this.columns = columns;
            this.childBuilderFactory = childBuilderFactory;
        }

        public int RepeatingAdornerCount { get; set; }

        protected override IHtmlNode BuildCore()
        {
            IHtmlNode node = CreateRootNode();

            AppendAdorners();

            columns.Where(c => c.Visible)
                   .Each(c => childBuilderFactory(c)
                                       .Build()
                                       .AppendTo(node));
            return node;
        }

        private void AppendAdorners()
        {
            Adorners.Add(new GridTagRepeatingAdorner(RepeatingAdornerCount)
                             {
                                 TagName = "td",
                                 Nbsp = true,
                                 CssClasses = {UIPrimitives.Grid.GroupCell}
                             });
        }

        protected IHtmlNode CreateRootNode()
        {
            return new HtmlTag("tr")
                .AddClass(UIPrimitives.Grid.FooterTemplateRow);
        }
    }
}