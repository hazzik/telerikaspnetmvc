// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System.Diagnostics;

    using Telerik.Web.Mvc.Infrastructure;

    public static class jQueryViewComponentDefaultSettings
    {
        private static string styleSheetFile = "jquery-ui-1.7.2.custom.css";
        private static string scriptFile = "jquery-ui-1.7.2.custom.js";

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