// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.ComponentModel;
    using System.Diagnostics;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// View component Builder base class.
    /// </summary>
    public abstract class ViewComponentBuilderBase<TViewComponent, TBuilder> : IHideObjectMembers where TViewComponent : ViewComponentBase where TBuilder : ViewComponentBuilderBase<TViewComponent, TBuilder>
    {
        private readonly TViewComponent component;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentBuilderBase&lt;TViewComponent, TBuilder&gt;"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        protected ViewComponentBuilderBase(TViewComponent component)
        {
            Guard.IsNotNull(component, "component");
            this.component = component;
        }

        /// <summary>
        /// Gets the view component.
        /// </summary>
        /// <value>The component.</value>
        protected internal TViewComponent Component
        {
            [DebuggerStepThrough]
            get
            {
                return component;
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Telerik.Web.Mvc.UI.ViewComponentBuilderBase&lt;TViewComponent,TBuilder&gt;"/> to TViewComponent.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TViewComponent(ViewComponentBuilderBase<TViewComponent, TBuilder> builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToComponent();
        }

        /// <summary>
        /// Returns the internal view component.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TViewComponent ToComponent()
        {
            return Component;
        }

        /// <summary>
        /// Sets the name of the component.
        /// </summary>
        /// <param name="componentName">The name.</param>
        /// <returns></returns>
        public virtual TBuilder Name(string componentName)
        {
            Component.Name = componentName;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the web asset key for the component.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual TBuilder AssetKey(string key)
        {
            Component.AssetKey = key;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the StyleSheets file path. Path must be a virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual TBuilder StyleSheetFilesPath(string path)
        {
            Component.StyleSheetFilesPath = path;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the StyleSheet file names.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        public virtual TBuilder StyleSheetFileNames(params string[] names)
        {
            Guard.IsNotNullOrEmpty(names, "names");

            Component.StyleSheetFileNames.Clear();
            Component.StyleSheetFileNames.AddRange(names);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the Scripts files path.. Path must be a virtual path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual TBuilder ScriptFilesPath(string path)
        {
            Component.ScriptFilesPath = path;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the Script file names.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        public virtual TBuilder ScriptFileNames(params string[] names)
        {
            Guard.IsNotNullOrEmpty(names, "names");

            Component.ScriptFileNames.Clear();
            Component.ScriptFileNames.AddRange(names);

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public virtual TBuilder HtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            Component.HtmlAttributes.Clear();
            Component.HtmlAttributes.Merge(attributes);

            return this as TBuilder;
        }

        /// <summary>
        /// Renders the component.
        /// </summary>
        public virtual void Render()
        {
            Component.Render();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            Render();

            return null;
        }
    }
}