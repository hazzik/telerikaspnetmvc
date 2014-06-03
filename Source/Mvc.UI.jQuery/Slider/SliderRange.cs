// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Specifies the slider range.
    /// </summary>
    public enum SliderRange
    {
        False = 0,
        [ClientSideEnumValue("true")]
        True = 1,
        [ClientSideEnumValue("'min'")]
        ToMinimum = 2,
        [ClientSideEnumValue("'max'")]
        ToMaximum = 3
    }
}