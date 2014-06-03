// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Specifies the slider orientation.
    /// </summary>
    public enum SliderOrientation
    {
        /// <summary>
        /// Lays out the elements of the control in a horizontal manner. 
        /// </summary>
        Default = 0,

        /// <summary>
        /// Lays out the elements of the control in a horizontal manner. 
        /// </summary>
        [ClientSideEnumValue("'horizontal'")]
        Horizontal = 1,

        /// <summary>
        /// Lays out the elements of the control in a vertical manner. 
        /// </summary>
        [ClientSideEnumValue("'vertical'")]
        Vertical = 2
    }
}