// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
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