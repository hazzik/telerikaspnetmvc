// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines the <see cref="WebAssetItem"/> group.
    /// </summary>
    public class WebAssetItemGroup : IWebAssetItem
    {
        private string defaultPath;
        private string version;
        private float cacheDurationInDays;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemGroup"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WebAssetItemGroup(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Name = name;
            Version = WebAssetDefaultSettings.Version;
            Compress = WebAssetDefaultSettings.Compress;
            CacheDurationInDays = WebAssetDefaultSettings.CacheDurationInDays;
            Combined = WebAssetDefaultSettings.Combined;
            Items = new InternalAssetItemCollection();
        }

        /// <summary>
        /// Gets or sets the default path.
        /// </summary>
        /// <value>The default path.</value>
        public string DefaultPath
        {
            [DebuggerStepThrough]
            get
            {
                return defaultPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotVirtualPath(value, "value");

                defaultPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the content delivery network URL.
        /// </summary>
        /// <value>The content delivery network URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Its take a URI as a string parameter.")]
        public string ContentDeliveryNetworkUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroup"/> is disabled.
        /// </summary>
        /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
        public bool Disabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            [DebuggerStepThrough]
            get
            {
                return version;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                version = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroup"/> is compress.
        /// </summary>
        /// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
        public bool Compress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cache duration in days.
        /// </summary>
        /// <value>The cache duration in days.</value>
        public float CacheDurationInDays
        {
            [DebuggerStepThrough]
            get
            {
                return cacheDurationInDays;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNegative(value, "value");

                cacheDurationInDays = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebAssetItemGroup"/> is combined.
        /// </summary>
        /// <value><c>true</c> if combined; otherwise, <c>false</c>.</value>
        public bool Combined
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public IList<WebAssetItem> Items
        {
            get;
            private set;
        }

        private sealed class InternalAssetItemCollection : Collection<WebAssetItem>
        {
            protected override void InsertItem(int index, WebAssetItem item)
            {
                Guard.IsNotNull(item, "item");

                if (!AlreadyExists(item))
                {
                    base.InsertItem(index, item);
                }
            }

            protected override void SetItem(int index, WebAssetItem item)
            {
                if (AlreadyExists(item))
                {
                    throw new ArgumentException(Resources.TextResource.ItemWithSpecifiedSourceAlreadyExists, "item");
                }

                base.SetItem(index, item);
            }

            private bool AlreadyExists(WebAssetItem item)
            {
                return this.Any(i => i != item && i.Source.IsCaseInsensitiveEqual(item.Source));
            }
        }
    }
}