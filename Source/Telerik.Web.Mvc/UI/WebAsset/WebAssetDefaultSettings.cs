// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    using Infrastructure;

    /// <summary>
    /// Contains default asset settings.
    /// </summary>
    public static class WebAssetDefaultSettings
    {
        private static string styleSheetFilesPath = "~/Content";
        private static string scriptFilesPath = "~/Scripts";
        private static string version = typeof(WebAssetDefaultSettings).Assembly.GetName().Version.ToString();
        private static bool compress = true;
        private static float cacheDurationInDays = 365f;

        /// <summary>
        /// Gets or sets the style sheet files path. Path must be a virtual path.
        /// </summary>
        /// <value>The style sheet files path.</value>
        public static string StyleSheetFilesPath
        {
            [DebuggerStepThrough]
            get
            {
                return styleSheetFilesPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotVirtualPath(value, "value");

                styleSheetFilesPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the script files path. Path must be a virtual path.
        /// </summary>
        /// <value>The script files path.</value>
        public static string ScriptFilesPath
        {
            [DebuggerStepThrough]
            get
            {
                return scriptFilesPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotVirtualPath(value, "value");

                scriptFilesPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public static string Version
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
        /// Gets or sets a value indicating whether assets should be served as compressed.
        /// </summary>
        /// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
        public static bool Compress
        {
            [DebuggerStepThrough]
            get
            {
                return compress;
            }

            [DebuggerStepThrough]
            set
            {
                compress = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether assets shoule be combined.
        /// </summary>
        /// <value><c>true</c> if combined; otherwise, <c>false</c>.</value>
        public static bool Combined
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cache duration in days.
        /// </summary>
        /// <value>The cache duration in days.</value>
        public static float CacheDurationInDays
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
    }
}