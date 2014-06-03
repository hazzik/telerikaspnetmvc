// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web.Script.Serialization;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines basic building blocks of Global storage for web assets.
    /// </summary>
    public interface IWebAssetRegistry : IWebAssetLocator
    {
        /// <summary>
        /// Stores the specified web assets with the specified details.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="version">The version.</param>
        /// <param name="compress">if set to <c>true</c> [compress].</param>
        /// <param name="cacheDurationInDays">The cache duration in days.</param>
        /// <param name="items">The items.</param>
        /// <returns>Returns the id.</returns>
        string Store(string contentType, string version, bool compress, float cacheDurationInDays, IList<string> items);

        /// <summary>
        /// Retrieves the web asset by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        WebAsset Retrieve(string id);
    }

    /// <summary>
    /// The default web asset registry.
    /// </summary>
    public class WebAssetRegistry : IWebAssetRegistry
    {
        private const int MinimumLengthToCompress = 384;
        private static readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        private readonly ICacheManager cacheManager;
        private readonly IWebAssetLocator assetLocator;
        private readonly IPathResolver pathResolver;
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetRegistry"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="assetLocator">The asset locator.</param>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="fileSystem">The file system.</param>
        public WebAssetRegistry(ICacheManager cacheManager, IWebAssetLocator assetLocator, IPathResolver pathResolver, IFileSystem fileSystem)
        {
            Guard.IsNotNull(cacheManager, "cacheManager");
            Guard.IsNotNull(assetLocator, "assetLocator");
            Guard.IsNotNull(pathResolver, "pathResolver");
            Guard.IsNotNull(fileSystem, "fileSystem");

            this.cacheManager = cacheManager;
            this.assetLocator = assetLocator;
            this.pathResolver = pathResolver;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Stores the specified web assets with the specified details.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="version">The version.</param>
        /// <param name="compress">if set to <c>true</c> [compress].</param>
        /// <param name="cacheDurationInDays">The cache duration in days.</param>
        /// <param name="items">The items.</param>
        /// <returns>Returns the id.</returns>
        public string Store(string contentType, string version, bool compress, float cacheDurationInDays, IList<string> items)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNullOrEmpty(version, "version");
            Guard.IsNotNegative(cacheDurationInDays, "cacheDurationInDays");
            Guard.IsNotNullOrEmpty(items, "items");
            items.Each(item => Guard.IsNotVirtualPath(item, "item"));

            MergedAsset mergedAsset = CreateMergedAssetWith(contentType, version, compress, cacheDurationInDays, items);
            string id = CreateIdFrom(mergedAsset);

            EnsureAsset(mergedAsset, id);

            return id;
        }

        /// <summary>
        /// Retrieves the web asset by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public WebAsset Retrieve(string id)
        {
            MergedAsset mergedAsset = CreateMergedAssetFrom(id);
            WebAssetHolder assetHolder = EnsureAsset(mergedAsset, id);

            return new WebAsset(assetHolder.Asset.ContentType, assetHolder.Asset.Version, assetHolder.Asset.Compress, assetHolder.Asset.CacheDurationInDays, assetHolder.Content);
        }

        /// <summary>
        /// Returns the correct virtual path based upon the debug mode.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public string Locate(string virtualPath)
        {
            return assetLocator.Locate(virtualPath);
        }

        private static MergedAsset CreateMergedAssetFrom(string id)
        {
            string decompressed = Decompress(Decode(id));
            MergedAsset mergedAsset = Deserialize(decompressed);

            return mergedAsset;
        }

        private static string CreateIdFrom(MergedAsset mergedAsset)
        {
            string serialized = Serialize(mergedAsset);
            string id = Encode(Compress(serialized));

            return id;
        }

        private static MergedAsset CreateMergedAssetWith(string contentType, string version, bool compress, float cacheDurationInDays, IList<string> items)
        {
            Func<string, string> getDirectory = path => path.Substring(2, path.LastIndexOf("/", StringComparison.Ordinal) - 2);
            Func<string, string> getFile = path => path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);

            MergedAsset asset = new MergedAsset
                                    {
                                        ContentType = contentType,
                                        Version = version,
                                        Compress = compress,
                                        CacheDurationInDays = cacheDurationInDays
                                    };

            IEnumerable<string> directories = items.Select(getDirectory).Distinct(StringComparer.OrdinalIgnoreCase);

            directories.Each(directory => asset.Directories.Add(new MergedAssetDirectory { Path = directory }));

            for (int i = 0; i < items.Count; i++)
            {
                string item = items[i];
                string directory = getDirectory(item);
                string file = getFile(item);

                MergedAssetDirectory assetDirectory = asset.Directories.Single(d => d.Path.IsCaseInsensitiveEqual(directory));

                assetDirectory.Files.Add(new MergedAssetFile { Order = i, Name = file });
            }

            return asset;
        }

        private static string Serialize(MergedAsset mergedAsset)
        {
            JavaScriptSerializer serializer = CreateSerializer();

            string json = serializer.Serialize(mergedAsset);

            return json;
        }

        private static MergedAsset Deserialize(string json)
        {
            JavaScriptSerializer serializer = CreateSerializer();

            MergedAsset mergedAsset = serializer.Deserialize<MergedAsset>(json);

            return mergedAsset;
        }

        private static string Encode(string target)
        {
            return target.Replace("/", "_").Replace("+", "-");
        }

        private static string Decode(string target)
        {
            return target.Replace("-", "+").Replace("_", "/");
        }

        private static string Compress(string target)
        {
            return (target.Length > MinimumLengthToCompress) ? target.Compress() : Convert.ToBase64String(Encoding.UTF8.GetBytes(target));
        }

        private static string Decompress(string target)
        {
            return (target.Length > MinimumLengthToCompress) ? target.Decompress() : Encoding.UTF8.GetString(Convert.FromBase64String(target));
        }

        private static JavaScriptSerializer CreateSerializer()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            serializer.RegisterConverters(new JavaScriptConverter[] { new MergedAssetJsonConverter(), new MergedAssetDirectoryJsonConverter(), new MergedAssetFileJsonConverter() });

            return serializer;
        }

        private WebAssetHolder EnsureAsset(MergedAsset asset, string id)
        {
            string key = CreateKey(id);
            WebAssetHolder assetHolder;

            using (syncLock.ReadAndWrite())
            {
                assetHolder = cacheManager.GetItem(key) as WebAssetHolder;

                if (assetHolder == null)
                {
                    using (syncLock.Write())
                    {
                        assetHolder = cacheManager.GetItem(key) as WebAssetHolder;

                        if (assetHolder == null)
                        {
                            IEnumerable<string> items = asset.Directories
                                                             .SelectMany(d => d.Files.Select(f => new { f.Order, path = "~/" + d.Path + "/" + f.Name }))
                                                             .OrderBy(f => f.Order)
                                                             .Select(f => f.path);

                            List<string> physicalPaths = new List<string>();
                            StringBuilder contentBuilder = new StringBuilder();

                            foreach (string item in items)
                            {
                                string virtualPath = assetLocator.Locate(item);
                                string physicalPath = pathResolver.Resolve(virtualPath);

                                string fileContent = fileSystem.ReadAllText(physicalPath);

                                contentBuilder.AppendLine(fileContent);

                                physicalPaths.Add(physicalPath);
                            }

                            assetHolder = new WebAssetHolder { Asset = asset, Content = contentBuilder.ToString() };

                            cacheManager.Insert(key, assetHolder, null, physicalPaths.ToArray());
                        }
                    }
                }
            }

            return assetHolder;
        }

        private string CreateKey(string id)
        {
            return GetType().AssemblyQualifiedName + ":" + id;
        }

        [Serializable]
        private sealed class WebAssetHolder
        {
            public MergedAsset Asset
            {
                get;
                set;
            }

            public string Content
            {
                get;
                set;
            }
        }

        [Serializable]
        private sealed class MergedAsset
        {
            public MergedAsset()
            {
                Directories = new List<MergedAssetDirectory>();
            }

            public string ContentType
            {
                get;
                set;
            }

            public string Version
            {
                get;
                set;
            }

            public bool Compress
            {
                get;
                set;
            }

            public float CacheDurationInDays
            {
                get;
                set;
            }

            public IList<MergedAssetDirectory> Directories
            {
                get;
                private set;
            }
        }

        [Serializable]
        private sealed class MergedAssetDirectory
        {
            public MergedAssetDirectory()
            {
                Files = new List<MergedAssetFile>();
            }

            public string Path
            {
                get;
                set;
            }

            public IList<MergedAssetFile> Files
            {
                get;
                private set;
            }
        }

        [Serializable]
        private sealed class MergedAssetFile
        {
            public int Order
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }
        }

        private sealed class MergedAssetJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAsset);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAsset mergedAsset = (MergedAsset) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "ct", mergedAsset.ContentType },
                                                                 { "v", mergedAsset.Version },
                                                                 { "c", mergedAsset.Compress },
                                                                 { "cd", mergedAsset.CacheDurationInDays },
                                                                 { "d", mergedAsset.Directories }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAsset mergedAsset = new MergedAsset
                                              {
                                                  ContentType = serializer.ConvertToType<string>(dictionary["ct"]),
                                                  Version = serializer.ConvertToType<string>(dictionary["v"]),
                                                  Compress = serializer.ConvertToType<bool>(dictionary["c"]),
                                                  CacheDurationInDays = serializer.ConvertToType<float>(dictionary["cd"])
                                              };

                mergedAsset.Directories.AddRange(serializer.ConvertToType<IList<MergedAssetDirectory>>(dictionary["d"]));

                return mergedAsset;
            }
        }

        private sealed class MergedAssetDirectoryJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAssetDirectory);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAssetDirectory mergedAssetDirectory = (MergedAssetDirectory) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "p", mergedAssetDirectory.Path },
                                                                 { "f", mergedAssetDirectory.Files }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAssetDirectory mergedAssetDirectory = new MergedAssetDirectory
                                                                {
                                                                    Path = serializer.ConvertToType<string>(dictionary["p"])
                                                                };

                mergedAssetDirectory.Files.AddRange(serializer.ConvertToType<IList<MergedAssetFile>>(dictionary["f"]));

                return mergedAssetDirectory;
            }
        }

        private sealed class MergedAssetFileJsonConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                [DebuggerStepThrough]
                get
                {
                    yield return typeof(MergedAssetFile);
                }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                MergedAssetFile mergedAssetFile = (MergedAssetFile) obj;

                IDictionary<string, object> dictionary = new Dictionary<string, object>
                                                             {
                                                                 { "o", mergedAssetFile.Order },
                                                                 { "n", mergedAssetFile.Name }
                                                             };

                return dictionary;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                MergedAssetFile mergedAssetFile = new MergedAssetFile
                                                      {
                                                          Order = serializer.ConvertToType<int>(dictionary["o"]),
                                                          Name = serializer.ConvertToType<string>(dictionary["n"])
                                                      };

                return mergedAssetFile;
            }
        }
    }
}