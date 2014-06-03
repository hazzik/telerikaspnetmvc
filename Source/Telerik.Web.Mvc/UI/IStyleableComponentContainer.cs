// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Container of styleable component.
    /// </summary>
    public interface IStyleableComponentContainer
    {
        /// <summary>
        /// Registers the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        void Register(IStyleableComponent component);
    }
}