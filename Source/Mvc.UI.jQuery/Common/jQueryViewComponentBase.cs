// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Diagnostics;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

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
            StyleSheetFileNames.Add(jQueryViewComponentDefaultSettings.StyleSheetFile);
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