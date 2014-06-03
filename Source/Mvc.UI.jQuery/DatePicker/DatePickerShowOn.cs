// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
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