// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Enum to show how date picker will invoked.
    /// </summary>
    public enum DatePickerShowOn
    {
        /// <summary>
        /// Date picker will show on control focus.
        /// </summary>
        [ClientSideEnumValue("'focus'")]
        Focus = 0,

        /// <summary>
        /// Date picker will show on button click.
        /// </summary>
        [ClientSideEnumValue("'button'")]
        Button = 1,

        /// <summary>
        /// Date picker will show on both control focus and button click.
        /// </summary>
        [ClientSideEnumValue("'both'")]
        Both = 2
    }
}