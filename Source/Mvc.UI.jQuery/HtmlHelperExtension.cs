// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Diagnostics;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Extends a HtmlHelper to create the necessary html elements, emits required stylesheets and javascripts in the view./>
    /// </summary>
    public static class HtmlHelperExtension
    {
        private static readonly IClientSideObjectWriterFactory factory = new ClientSideObjectWriterFactory();

        /// <summary>
        /// Gets the jQuery view components instance.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <returns>jQueryViewComponentFactory</returns>
        [DebuggerStepThrough]
        public static jQueryViewComponentFactory jQuery(this HtmlHelper helper)
        {
            return new jQueryViewComponentFactory(helper, factory);
        }
    }
}