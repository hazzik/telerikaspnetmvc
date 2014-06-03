namespace Telerik.Web.Mvc
{
    using System.Diagnostics;

    public class SiteMapBuilder : IHideObjectMembers
    {
        private readonly SiteMapBase siteMap;
        private readonly SiteMapNodeBuilder siteMapNodeBuilder;

        public SiteMapBuilder(SiteMapBase siteMap)
        {
            Guard.IsNotNull(siteMap, "siteMap");

            this.siteMap = siteMap;
            siteMapNodeBuilder = new SiteMapNodeBuilder(this.siteMap.RootNode);
        }

        public SiteMapNodeBuilder RootNode
        {
            [DebuggerStepThrough]
            get
            {
                return siteMapNodeBuilder;
            }
        }

        public static implicit operator SiteMapBase(SiteMapBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            return builder.ToSiteMap();
        }

        public SiteMapBase ToSiteMap()
        {
            return siteMap;
        }

        public SiteMapBuilder CacheDurationInMinutes(float value)
        {
            siteMap.CacheDurationInMinutes = value;

            return this;
        }

        public SiteMapBuilder Compress(bool value)
        {
            siteMap.Compress = value;

            return this;
        }

        public SiteMapBuilder GenerateSearchEngineMap(bool value)
        {
            siteMap.GenerateSearchEngineMap = value;

            return this;
        }
    }
}