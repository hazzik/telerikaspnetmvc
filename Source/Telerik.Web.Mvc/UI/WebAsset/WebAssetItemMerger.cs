// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// The default web asset merger.
    /// </summary>
    public class WebAssetItemMerger : IWebAssetItemMerger
    {
        private readonly IWebAssetRegistry assetRegistry;
        private readonly IUrlResolver urlResolver;
        private readonly IUrlEncoder urlEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemMerger"/> class.
        /// </summary>
        /// <param name="assetRegistry">The asset registry.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="urlEncoder">The URL encoder.</param>
        public WebAssetItemMerger(IWebAssetRegistry assetRegistry, IUrlResolver urlResolver, IUrlEncoder urlEncoder)
        {
            Guard.IsNotNull(assetRegistry, "assetRegistry");
            Guard.IsNotNull(urlResolver, "urlResolver");
            Guard.IsNotNull(urlEncoder, "urlEncoder");

            this.assetRegistry = assetRegistry;
            this.urlResolver = urlResolver;
            this.urlEncoder = urlEncoder;
        }

        /// <summary>
        /// Merges the specified assets.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetHandlerPath">The asset handler path.</param>
        /// <param name="assets">The assets.</param>
        /// <returns></returns>
        public IList<string> Merge(string contentType, string assetHandlerPath, WebAssetItemCollection assets)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNullOrEmpty(assetHandlerPath, "assetHandlerPath");
            Guard.IsNotNull(assets, "assets");

            IList<string> mergedList = new List<string>();

            Func<string, string, string> getRelativePath = (source, version) => urlResolver.Resolve(assetRegistry.Locate(source, version));

            if (!assets.IsEmpty())
            {
                foreach (IWebAssetItem asset in assets)
                {
                    WebAssetItem item = asset as WebAssetItem;
                    WebAssetItemGroup itemGroup = asset as WebAssetItemGroup;

                    if (item != null)
                    {
                        mergedList.Add(getRelativePath(item.Source, null));
                    }
                    else if (itemGroup != null)
                    {
                        if (itemGroup.Enabled)
                        {
                            if (!string.IsNullOrEmpty(itemGroup.ContentDeliveryNetworkUrl))
                            {
                                mergedList.Add(itemGroup.ContentDeliveryNetworkUrl);
                            }
                            else
                            {
                                if (itemGroup.Combined)
                                {
                                    string id = assetRegistry.Store(contentType, itemGroup);
                                    string virtualPath = "{0}?{1}={2}".FormatWith(assetHandlerPath, urlEncoder.Encode(WebAssetHttpHandler.IdParameterName), urlEncoder.Encode(id));
                                    string relativePath = urlResolver.Resolve(virtualPath);

                                    if (!mergedList.Contains(relativePath, StringComparer.OrdinalIgnoreCase))
                                    {
                                        mergedList.Add(relativePath);
                                    }
                                }
                                else
                                {
                                    itemGroup.Items.Each(i => 
                                                          {
                                                              if (!mergedList.Contains(i.Source, StringComparer.OrdinalIgnoreCase))
                                                              {
                                                                  mergedList.Add(getRelativePath(i.Source, itemGroup.Version));
                                                              }
                                                          });
                                }
                            }
                        }
                    }
                }
            }

            return mergedList.ToList();
        }
    }
}