// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines the basic building block of web asset merging.
    /// </summary>
    public interface IWebAssetItemMerger
    {
        /// <summary>
        /// Merges the specified assets.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="assetHandlerPath">The asset handler path.</param>
        /// <param name="assets">The assets.</param>
        /// <returns></returns>
        IList<string> Merge(string contentType, string assetHandlerPath, WebAssetItemCollection assets);
    }

    /// <summary>
    /// The default web asset merger.
    /// </summary>
    public class WebAssetItemMerger : IWebAssetItemMerger
    {
        private readonly IWebAssetRegistry assetRegistry;
        private readonly IUrlResolver urlResolver;
        private readonly HttpServerUtilityBase httpServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItemMerger"/> class.
        /// </summary>
        /// <param name="assetRegistry">The asset registry.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="httpServer">The HTTP server.</param>
        public WebAssetItemMerger(IWebAssetRegistry assetRegistry, IUrlResolver urlResolver, HttpServerUtilityBase httpServer)
        {
            Guard.IsNotNull(assetRegistry, "assetRegistry");
            Guard.IsNotNull(urlResolver, "urlResolver");
            Guard.IsNotNull(httpServer, "httpServer");

            this.assetRegistry = assetRegistry;
            this.urlResolver = urlResolver;
            this.httpServer = httpServer;
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

            Func<string, string> getRelativePath = source => urlResolver.Resolve(assetRegistry.Locate(source));

            if (!assets.IsEmpty())
            {
                foreach (IWebAssetItem asset in assets)
                {
                    WebAssetItem item = asset as WebAssetItem;
                    WebAssetItemGroup itemGroup = asset as WebAssetItemGroup;

                    if (item != null)
                    {
                        mergedList.Add(getRelativePath(item.Source));
                    }
                    else if (itemGroup != null)
                    {
                        if (!itemGroup.Disabled)
                        {
                            if (!string.IsNullOrEmpty(itemGroup.ContentDeliveryNetworkUrl))
                            {
                                mergedList.Add(itemGroup.ContentDeliveryNetworkUrl);
                            }
                            else
                            {
                                if (itemGroup.Combined)
                                {
                                    string id = assetRegistry.Store(contentType, itemGroup.Version, itemGroup.Compress, itemGroup.CacheDurationInDays, itemGroup.Items.Select(i => i.Source).ToList());
                                    string virtualPath = "{0}?{1}={2}".FormatWith(assetHandlerPath, httpServer.UrlEncode(WebAssetHttpHandler.IdParameterName), httpServer.UrlEncode(id));
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
                                                                  mergedList.Add(getRelativePath(i.Source));
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