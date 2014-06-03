// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.ComponentModel;

    using Infrastructure;

    /// <summary>
    /// The Builder class for managing script files and statements fluently in ASP.NET MVC View.
    /// </summary>
    public class ScriptRegistrarBuilder : IHideObjectMembers
    {
        private readonly ScriptRegistrar scriptRegistrar;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptRegistrarBuilder"/> class.
        /// </summary>
        /// <param name="scriptRegistrar">The script registrar.</param>
        public ScriptRegistrarBuilder(ScriptRegistrar scriptRegistrar)
        {
            Guard.IsNotNull(scriptRegistrar, "scriptRegistrar");

            this.scriptRegistrar = scriptRegistrar;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ScriptRegistrarBuilder"/> to <see cref="ScriptRegistrar"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ScriptRegistrar(ScriptRegistrarBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToRegistrar();
        }

        /// <summary>
        /// Returns the internal script registrar.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ScriptRegistrar ToRegistrar()
        {
            return scriptRegistrar;
        }

        /// <summary>
        /// Sets the asset handler path. Path must be a virtual path.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ScriptRegistrarBuilder AssetHandlerPath(string value)
        {
            scriptRegistrar.AssetHandlerPath = value;

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register the script files fluently in default group.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual ScriptRegistrarBuilder DefaultGroup(Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(scriptRegistrar.DefaultGroup);
            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register the script files fluently in different groups.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual ScriptRegistrarBuilder Scripts(Action<WebAssetItemCollectionBuilder> configureAction)
        {
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemCollectionBuilder builder = new WebAssetItemCollectionBuilder(scriptRegistrar.Scripts);

            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register on document ready script statements fluently.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ScriptRegistrarBuilder OnDocumentReady(Action javaScript)
        {
            Guard.IsNotNull(javaScript, "javaScript");

            scriptRegistrar.OnDocumentReadyActions.Add(javaScript);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register on window unload script statements fluently.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ScriptRegistrarBuilder OnPageUnload(Action javaScript)
        {
            Guard.IsNotNull(javaScript, "javaScript");

            scriptRegistrar.OnWindowUnloadActions.Add(javaScript);

            return this;
        }

        /// <summary>
        /// Renders the internal script registrar.
        /// </summary>
        public virtual void Render()
        {
            scriptRegistrar.Render();
        }
    }
}