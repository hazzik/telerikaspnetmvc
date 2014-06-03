namespace Telerik.Web.Mvc.UI.Html
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridPagerTextBoxHtmlBuilder : HtmlBuilderBase
    {
        private readonly GridLocalization localization;

        public GridPagerTextBoxHtmlBuilder(GridLocalization localization)
        {
            this.localization = localization;
        }

        public string Value
        {
            get;
            set;
        }

        public int TotalPages
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var div = new HtmlTag("div")
                        .AddClass("t-page-i-of-n");
            
            var page = new LiteralNode(localization.Page);

            page.AppendTo(div);

            var input = new HtmlTag("input", TagRenderMode.SelfClosing)
                            .Attribute("type", "text")
                            .Attribute("value", Value);
            
            input.AppendTo(div);

            var of = new LiteralNode(string.Format(localization.PageOf, TotalPages));

            of.AppendTo(div);

            return div;
        }
    }
}
