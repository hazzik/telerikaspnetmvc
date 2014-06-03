// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    /// <summary>
    /// Specifies the animation duration of item.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Need to align with the numeric value of the named animation.")]
    public enum AnimationDuration
    {
        Fast = 200,
        Normal = 400,
        Slow = 600
    }
}