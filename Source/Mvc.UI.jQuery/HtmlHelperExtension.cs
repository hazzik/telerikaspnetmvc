// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Diagnostics;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Extends a HtmlHelper to create the necessary html elements, emits required stylesheets and javascripts in the view./>
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Gets the jQuery view components instance.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <returns>jQueryViewComponentFactory</returns>
        [DebuggerStepThrough]
        public static jQueryViewComponentFactory jQuery(this HtmlHelper helper)
        {
            return new jQueryViewComponentFactory(helper, ServiceLocator.Current.Resolve<IClientSideObjectWriterFactory>());
        }
    }
}