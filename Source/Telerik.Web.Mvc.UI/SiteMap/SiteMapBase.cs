namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    public abstract class SiteMapBase
    {
        private static float defaultCacheDurationInMinutes = 60;
        private static bool defaultCompress = true;
        private static bool defaultGenerateSearchEngineMap = true;

        private float cacheDurationInMinutes;

        protected SiteMapBase()
        {
            CacheDurationInMinutes = DefaultCacheDurationInMinutes;
            Compress = DefaultCompress;
            GenerateSearchEngineMap = DefaultGenerateSearchEngineMap;

            RootNode = new SiteMapNode();
        }

        public static float DefaultCacheDurationInMinutes
        {
            [DebuggerStepThrough]
            get
            {
                return defaultCacheDurationInMinutes;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNegative(value, "value");

                defaultCacheDurationInMinutes = value;
            }
        }

        public static bool DefaultCompress
        {
            [DebuggerStepThrough]
            get
            {
                return defaultCompress;
            }

            [DebuggerStepThrough]
            set
            {
                defaultCompress = value;
            }
        }

        public static bool DefaultGenerateSearchEngineMap
        {
            [DebuggerStepThrough]
            get
            {
                return defaultGenerateSearchEngineMap;
            }

            [DebuggerStepThrough]
            set
            {
                defaultGenerateSearchEngineMap = value;
            }
        }

        public SiteMapNode RootNode
        {
            get;
            protected set;
        }

        public float CacheDurationInMinutes
        {
            [DebuggerStepThrough]
            get
            {
                return cacheDurationInMinutes;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNegative(value, "value");

                cacheDurationInMinutes = value;
            }
        }

        public bool Compress
        {
            get;
            set;
        }

        public bool GenerateSearchEngineMap
        {
            get;
            set;
        }

        public static implicit operator SiteMapBuilder(SiteMapBase siteMap)
        {
            Guard.IsNotNull(siteMap, "siteMap");

            return siteMap.ToBuilder();
        }

        public SiteMapBuilder ToBuilder()
        {
            return new SiteMapBuilder(this);
        }

        public virtual void Reset()
        {
            CacheDurationInMinutes = DefaultCacheDurationInMinutes;
            Compress = DefaultCompress;
            GenerateSearchEngineMap = DefaultGenerateSearchEngineMap;

            RootNode = new SiteMapNode();
        }
    }
}