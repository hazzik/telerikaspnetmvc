// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;

    using Infrastructure;

    public class TabStripRendererFactory : ITabStripRendererFactory
    {
        private readonly IActionMethodCache actionMethodCache;

        public TabStripRendererFactory(IActionMethodCache actionMethodCache)
        {
            Guard.IsNotNull(actionMethodCache, "actionMethodCache");

            this.actionMethodCache = actionMethodCache;
        }

        public ITabStripRenderer Create(TabStrip tabStrip, HtmlTextWriter writer)
        {
            return new TabStripRenderer(tabStrip, writer, actionMethodCache);
        }
    }
}