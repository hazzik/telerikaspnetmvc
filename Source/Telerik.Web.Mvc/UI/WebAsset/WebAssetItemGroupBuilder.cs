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
    /// Builder class for fluently configuring the group.
    /// </summary>
    public class WebAssetItemGroupBuilder : IHideObjectMembers
    {
        private readonly WebAssetItemGroup assetItemGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemGroupBuilder"/> class.
        /// </summary>
        /// <param name="assetItemGroup">The asset item group.</param>
        public WebAssetItemGroupBuilder(WebAssetItemGroup assetItemGroup)
        {
            Guard.IsNotNull(assetItemGroup, "assetItemGroup");

            this.assetItemGroup = assetItemGroup;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Telerik.Web.Mvc.UI.WebAssetItemGroupBuilder"/> to <see cref="Telerik.Web.Mvc.UI.WebAssetItemGroup"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WebAssetItemGroup(WebAssetItemGroupBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToGroup();
        }

        /// <summary>
        /// Returns the internal group.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WebAssetItemGroup ToGroup()
        {
            return assetItemGroup;
        }

        /// <summary>
        /// Sets the content delivery network URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder ContentDeliveryNetworkUrl(string value)
        {
            assetItemGroup.ContentDeliveryNetworkUrl = value;

            return this;
        }

        /// <summary>
        /// Marks the group as disabled.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder Disabled(bool value)
        {
            assetItemGroup.Disabled = value;

            return this;
        }

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder Version(string value)
        {
            assetItemGroup.Version = value;

            return this;
        }

        /// <summary>
        /// Sets whether the groups will be served as compressed.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder Compress(bool value)
        {
            assetItemGroup.Compress = value;

            return this;
        }

        /// <summary>
        /// Sets the caches the duration of this group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder CacheDurationInDays(float value)
        {
            assetItemGroup.CacheDurationInDays = value;

            return this;
        }

        /// <summary>
        /// Sets whether the groups items will be served as combined.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder Combined(bool value)
        {
            assetItemGroup.Combined = value;

            return this;
        }

        /// <summary>
        /// Sets the defaults path of the containing <see cref="WebAssetItem"/>.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder DefaultPath(string path)
        {
            assetItemGroup.DefaultPath = path;

            return this;
        }

        /// <summary>
        /// Adds the specified source as <see cref="WebAssetItem"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual WebAssetItemGroupBuilder Add(string value)
        {
            assetItemGroup.Items.Add(CreateItem(value));

            return this;
        }

        private WebAssetItem CreateItem(string source)
        {
            Guard.IsNotNullOrEmpty(source, "source");

            string itemSource = source.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ? source : PathHelper.CombinePath(assetItemGroup.DefaultPath, source);

            return new WebAssetItem(itemSource);
        }
    }
}