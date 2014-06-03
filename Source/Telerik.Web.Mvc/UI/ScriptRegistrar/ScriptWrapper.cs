// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;

    /// <summary>
    /// Wrap the script for the jQuery ready/unload events.
    /// </summary>
    public class ScriptWrapper : ScriptWrapperBase
    {
        /// <summary>
        /// Gets the on page load start.
        /// </summary>
        /// <value>The on page load start.</value>
        public override string OnPageLoadStart
        {
            [DebuggerStepThrough]
            get
            {
                return "jQuery(document).ready(function(){";
            }
        }

        /// <summary>
        /// Gets the on page load end.
        /// </summary>
        /// <value>The on page load end.</value>
        public override string OnPageLoadEnd
        {
            [DebuggerStepThrough]
            get
            {
                return "});";
            }
        }

        /// <summary>
        /// Gets the on page unload start.
        /// </summary>
        /// <value>The on page unload start.</value>
        public override string OnPageUnloadStart
        {
            [DebuggerStepThrough]
            get
            {
                return "jQuery(window).unload(function(){";
            }
        }

        /// <summary>
        /// Gets the on page unload end.
        /// </summary>
        /// <value>The on page unload end.</value>
        public override string OnPageUnloadEnd
        {
            [DebuggerStepThrough]
            get
            {
                return "});";
            }
        }
    }
}