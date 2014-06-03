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
    /// Base class for jQueryUI component.
    /// </summary>
    public abstract class jQueryViewComponentBase : ViewComponentBase
    {
        private static string hiddenInputSuffix = "hid";

        /// <summary>
        /// Initializes a new instance of the <see cref="jQueryViewComponentBase"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        protected jQueryViewComponentBase(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.Add(jQueryViewComponentDefaultSettings.ScriptFile);
        }

        /// <summary>
        /// Gets or sets the hidden input suffix.
        /// </summary>
        /// <value>The hidden input suffix.</value>
        public static string HiddenInputSuffix
        {
            [DebuggerStepThrough]
            get
            {
                return hiddenInputSuffix;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                hiddenInputSuffix = value;
            }
        }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        public string Theme
        {
            get;
            set;
        }
    }
}