// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web.Routing;
    using Telerik.Web.Mvc.UI.Html;

    public class GridToolBarCustomCommand<T> : GridToolBarCommandBase<T>, INavigatable where T : class
    {
        public GridToolBarCustomCommand()
        {
            RouteValues = new RouteValueDictionary();
        }

        public string RouteName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public RouteValueDictionary RouteValues { get; private set; }

        public string Url { get; set; }

        public string Text { get; set; }

        public override IEnumerable<IGridButtonBuilder> CreateDisplayButtons(IGridLocalization localization, IGridUrlBuilder urlBuilder, IGridHtmlHelper htmlHelper)
        {
            var factory = new GridButtonFactory();
            var button = factory.CreateButton<GridLinkButtonBuilder>(ButtonType);

            button.Text = Text;
            button.HtmlAttributes = HtmlAttributes;
            button.ImageHtmlAttributes = ImageHtmlAttributes;
            button.Url = delegate { return urlBuilder.Url(this); };

            return new[] { button };
        }
    }
}
