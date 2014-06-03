// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Extensions;

    public class GridEmptyRowHtmlBuilder : HtmlBuilderBase
    {
        private readonly int colspan;
        private readonly HtmlTemplate noRecordsTemplate;

        public GridEmptyRowHtmlBuilder(int colspan, HtmlTemplate noRecordsTemplate)
        {
            this.colspan = colspan;
            this.noRecordsTemplate = noRecordsTemplate;
        }

        protected override IHtmlNode BuildCore()
        {
            IHtmlNode tr = new HtmlTag("tr")
                .AddClass("t-no-data");

            var td = new HtmlTag("td").Attribute("colspan", colspan.ToString());
            
            noRecordsTemplate.Apply(td);

            td.AppendTo(tr);

            return tr;
        }
    }
}