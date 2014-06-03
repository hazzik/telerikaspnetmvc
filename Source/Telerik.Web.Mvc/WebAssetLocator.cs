// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Basic building block to locate the correct virtual path.
    /// </summary>
    public interface IWebAssetLocator
    {
        /// <summary>
        /// Returns the correct virtual path based upon the debug mode.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        string Locate(string virtualPath);
    }

    /// <summary>
    /// Default web asset locator.
    /// </summary>
    public class WebAssetLocator : IWebAssetLocator
    {
        private static readonly IDictionary<string, string> cache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        private readonly bool isInDebugMode;
        private readonly IPathResolver pathResolver;
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetLocator"/> class.
        /// </summary>
        /// <param name="isInDebugMode">if set to <c>true</c> [is in debug mode].</param>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="fileSystem">The file system.</param>
        public WebAssetLocator(bool isInDebugMode, IPathResolver pathResolver, IFileSystem fileSystem)
        {
            Guard.IsNotNull(pathResolver, "pathResolver");
            Guard.IsNotNull(fileSystem, "fileSystem");

            this.isInDebugMode = isInDebugMode;
            this.pathResolver = pathResolver;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Locates the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public string Locate(string virtualPath)
        {
            Guard.IsNotNullOrEmpty(virtualPath, "virtualPath");

            string result = virtualPath;

            string extension = Path.GetExtension(virtualPath);

            if (extension.IsCaseInsensitiveEqual(".js"))
            {
                result = isInDebugMode ? ProbePath(virtualPath, new[] { ".debug.js", ".js", ".min.js" }) : ProbePath(virtualPath, new[] { ".min.js", ".js", ".debug.js" });
            }
            else if (extension.IsCaseInsensitiveEqual(".css"))
            {
                result = isInDebugMode ? ProbePath(virtualPath, new[] { ".css", ".min.css" }) : ProbePath(virtualPath, new[] { ".min.css", ".css" });
            }

            return result;
        }

        // Marked as internal for Unit Test
        internal static void ClearCache()
        {
            using (cacheLock.Write())
            {
                cache.Clear();
            }
        }

        private string ProbePath(string virtualPath, IEnumerable<string> extensions)
        {
            string result;

            using (cacheLock.ReadAndWrite())
            {
                if (!cache.TryGetValue(virtualPath, out result))
                {
                    using (cacheLock.Write())
                    {
                        if (!cache.TryGetValue(virtualPath, out result))
                        {
                            foreach (string extension in extensions)
                            {
                                string newVirtualPath = Path.ChangeExtension(virtualPath, extension);
                                string physicalPath = pathResolver.Resolve(newVirtualPath);

                                if (fileSystem.FileExists(physicalPath))
                                {
                                    result = newVirtualPath;
                                    break;
                                }
                            }

                            if (string.IsNullOrEmpty(result))
                            {
                                result = virtualPath;

                                if (!fileSystem.FileExists(pathResolver.Resolve(result)))
                                {
                                    throw new FileNotFoundException(Resources.TextResource.SpecifiedFileDoesNotExist.FormatWith(result));
                                }
                            }

                            cache.Add(virtualPath, result);
                        }
                    }
                }
            }

            return result;
        }
    }
}