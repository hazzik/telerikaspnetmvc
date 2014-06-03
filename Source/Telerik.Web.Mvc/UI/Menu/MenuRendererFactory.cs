// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;

    using Infrastructure;

    public class MenuRendererFactory : IMenuRendererFactory
    {
        private readonly IActionMethodCache actionMethodCache;

        public MenuRendererFactory(IActionMethodCache actionMethodCache)
        {
            Guard.IsNotNull(actionMethodCache, "actionMethodCache");

            this.actionMethodCache = actionMethodCache;
        }

        public IMenuRenderer Create(Menu menu, HtmlTextWriter writer)
        {
            return new MenuRenderer(menu, writer, actionMethodCache);
        }
    }
}