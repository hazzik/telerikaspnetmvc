// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;

    using Infrastructure;

    /// <summary>
    /// Provides the factory methods for creating Telerik View Components.
    /// </summary>
    public class ViewComponentFactory : IHideObjectMembers
    {
        private readonly StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;
        private readonly ScriptRegistrarBuilder scriptRegistrarBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentFactory"/> class.
        /// </summary>
        /// <param name="styleSheetRegistrar">The style sheet registrar.</param>
        /// <param name="scriptRegistrar">The script registrar.</param>
        [DebuggerStepThrough]
        public ViewComponentFactory(StyleSheetRegistrarBuilder styleSheetRegistrar, ScriptRegistrarBuilder scriptRegistrar)
        {
            Guard.IsNotNull(styleSheetRegistrar, "styleSheetRegistrar");
            Guard.IsNotNull(scriptRegistrar, "scriptRegistrar");

            styleSheetRegistrarBuilder = styleSheetRegistrar;
            scriptRegistrarBuilder = scriptRegistrar;
        }

        /// <summary>
        /// Gets the style sheet registrar.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            return styleSheetRegistrarBuilder;
        }

        /// <summary>
        /// Gets the script registrar.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public ScriptRegistrarBuilder ScriptRegistrar()
        {
            return scriptRegistrarBuilder;
        }
    }
}