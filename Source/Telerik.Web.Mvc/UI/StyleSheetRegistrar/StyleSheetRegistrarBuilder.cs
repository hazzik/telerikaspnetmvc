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
    /// The Builder class for managing stylesheet files fluently in ASP.NET MVC View.
    /// </summary>
    public class StyleSheetRegistrarBuilder : IHideObjectMembers
    {
        private readonly StyleSheetRegistrar styleSheetRegistrar;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleSheetRegistrarBuilder"/> class.
        /// </summary>
        /// <param name="styleSheetRegistrar">The style sheet registrar.</param>
        public StyleSheetRegistrarBuilder(StyleSheetRegistrar styleSheetRegistrar)
        {
            Guard.IsNotNull(styleSheetRegistrar, "styleSheetRegistrar");

            this.styleSheetRegistrar = styleSheetRegistrar;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="StyleSheetRegistrarBuilder"/> to <see cref="StyleSheetRegistrar"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator StyleSheetRegistrar(StyleSheetRegistrarBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToRegistrar();
        }

        /// <summary>
        /// Returns the internal style sheet registrar.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public StyleSheetRegistrar ToRegistrar()
        {
            return styleSheetRegistrar;
        }

        /// <summary>
        /// Sets the asset handler path. Path must be a virtual path.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual StyleSheetRegistrarBuilder AssetHandlerPath(string value)
        {
            styleSheetRegistrar.AssetHandlerPath = value;

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register the stylesheet files fluently in default group.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual StyleSheetRegistrarBuilder DefaultGroup(Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(styleSheetRegistrar.DefaultGroup);
            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to register the stylesheet files fluently.
        /// </summary>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual StyleSheetRegistrarBuilder StyleSheets(Action<WebAssetItemCollectionBuilder> configureAction)
        {
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemCollectionBuilder builder = new WebAssetItemCollectionBuilder(styleSheetRegistrar.StyleSheets);

            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Renders the internal style sheet registrar.
        /// </summary>
        public virtual void Render()
        {
            styleSheetRegistrar.Render();
        }
    }
}