// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Helper class to get currrent and invariant culture.
    /// </summary>
    public static class Culture
    {
        /// <summary>
        /// Gets the System.Globalization.CultureInfo that represents the current culture used by the Resource Manager to look up culture-specific resources at run time.
        /// </summary>
        /// <value>The current.</value>
        public static CultureInfo Current
        {
            [DebuggerStepThrough]
            get
            {
                return CultureInfo.CurrentUICulture;
            }
        }

        /// <summary>
        /// Gets the System.Globalization.CultureInfo that is culture-independent (invariant).
        /// </summary>
        /// <value>The invariant.</value>
        public static CultureInfo Invariant
        {
            [DebuggerStepThrough]
            get
            {
                return CultureInfo.InvariantCulture;
            }
        }
    }
}