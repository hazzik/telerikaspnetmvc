// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Specifies the slider range.
    /// </summary>
    public enum SliderRange
    {
        /// <summary>
        /// No range.
        /// </summary>
        False = 0,

        /// <summary>
        /// Ranged.
        /// </summary>
        [ClientSideEnumValue("true")]
        True = 1,

        /// <summary>
        /// Range to minimum.
        /// </summary>
        [ClientSideEnumValue("'min'")]
        ToMinimum = 2,

        /// <summary>
        /// Range to maximum.
        /// </summary>
        [ClientSideEnumValue("'max'")]
        ToMaximum = 3
    }
}