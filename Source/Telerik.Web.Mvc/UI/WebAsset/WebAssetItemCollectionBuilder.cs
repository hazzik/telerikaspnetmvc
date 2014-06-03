// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.ComponentModel;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Builder class for fluently configuring the assets.
    /// </summary>
    public class WebAssetItemCollectionBuilder : IHideObjectMembers
    {
        private readonly WebAssetItemCollection assets;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemCollectionBuilder"/> class.
        /// </summary>
        /// <param name="assets">The assets.</param>
        public WebAssetItemCollectionBuilder(WebAssetItemCollection assets)
        {
            Guard.IsNotNull(assets, "assets");

            this.assets = assets;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Telerik.Web.Mvc.UI.WebAssetItemCollectionBuilder"/> to <see cref="Telerik.Web.Mvc.UI.WebAssetItemCollection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator WebAssetItemCollection(WebAssetItemCollectionBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToCollection();
        }

        /// <summary>
        /// Returns the internal collection.
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WebAssetItemCollection ToCollection()
        {
            return assets;
        }

        /// <summary>
        /// Executes the provided delegate that is used to add item fluently with the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public virtual WebAssetItemCollectionBuilder Add(string source)
        {
            assets.Add(source);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to add the group fluently with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual WebAssetItemCollectionBuilder AddGroup(string name, Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroup itemGroup = assets.FindGroupByName(name);

            if (itemGroup != null)
            {
                throw new ArgumentException(Resources.TextResource.GroupWithSpecifiedNameAlreadyExistsPleaseSpecifyADifferentName.FormatWith(name));
            }

            itemGroup = new WebAssetItemGroup(name) { DefaultPath = assets.DefaultPath };
            assets.Add(itemGroup);

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(itemGroup);
            configureAction(builder);

            return this;
        }

        /// <summary>
        /// Executes the provided delegate that is used to configure the group fluently.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="configureAction">The configure action.</param>
        /// <returns></returns>
        public virtual WebAssetItemCollectionBuilder GetGroup(string name, Action<WebAssetItemGroupBuilder> configureAction)
        {
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotNull(configureAction, "configureAction");

            WebAssetItemGroup itemGroup = assets.FindGroupByName(name);

            if (itemGroup == null)
            {
                throw new ArgumentException(Resources.TextResource.GroupWithSpecifiedNameDoesNotExistPleaseMakeSureYouHaveSpecifiedACorrectName.FormatWith(name));
            }

            WebAssetItemGroupBuilder builder = new WebAssetItemGroupBuilder(itemGroup);

            configureAction(builder);

            return this;
        }
    }
}