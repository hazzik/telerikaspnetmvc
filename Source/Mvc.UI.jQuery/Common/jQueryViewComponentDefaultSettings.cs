// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Diagnostics;

    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Default configuration.
    /// </summary>
    public static class jQueryViewComponentDefaultSettings
    {
        private static string styleSheetFile = "jquery-ui-1.7.2.custom.css";
        private static string scriptFile = "jquery-ui-1.7.2.custom.js";

        /// <summary>
        /// Gets or sets the style sheet file.
        /// </summary>
        /// <value>The style sheet file.</value>
        public static string StyleSheetFile
        {
            [DebuggerStepThrough]
            get
            {
                return styleSheetFile;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                styleSheetFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the script file.
        /// </summary>
        /// <value>The script file.</value>
        public static string ScriptFile
        {
            [DebuggerStepThrough]
            get
            {
                return scriptFile;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                scriptFile = value;
            }
        }
    }
}