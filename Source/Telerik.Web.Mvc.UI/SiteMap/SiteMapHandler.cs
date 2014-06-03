namespace Telerik.Web.Mvc
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Xml;

    public class SiteMapHandler : HttpHandlerBase
    {
        private const string SiteMapNameSpace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private readonly SiteMapDictionary siteMaps;
        private readonly IHttpResponseCompressor httpResponseCompressor;
        private readonly IHttpResponseCacher httpResponseCacher;
        private readonly IUrlGenerator urlGenerator;

        private static string defaultPath = "~/sitemap.axd";

        public SiteMapHandler(SiteMapDictionary siteMaps, IHttpResponseCompressor httpResponseCompressor, IHttpResponseCacher httpResponseCacher, IUrlGenerator urlGenerator)
        {
            Guard.IsNotNull(siteMaps, "siteMaps");
            Guard.IsNotNull(httpResponseCompressor, "httpResponseCompressor");
            Guard.IsNotNull(httpResponseCacher, "httpResponseCacher");
            Guard.IsNotNull(urlGenerator, "urlGenerator");

            this.siteMaps = siteMaps;
            this.httpResponseCompressor = httpResponseCompressor;
            this.httpResponseCacher = httpResponseCacher;
            this.urlGenerator = urlGenerator;
        }

        public SiteMapHandler() : this(SiteMapManager.SiteMaps, new HttpResponseCompressor(), new HttpResponseCacher(), new UrlGenerator())
        {
        }

        public static string DefaultPath
        {
            [DebuggerStepThrough]
            get
            {
                return defaultPath;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                defaultPath = value;
            }
        }

        public override void ProcessRequest(HttpContextBase context)
        {
            string name = context.Request.QueryString["name"];
            SiteMapBase siteMap = GetSiteMap(name);

            if ((siteMap != null) && siteMap.GenerateSearchEngineMap)
            {
                HttpResponseBase response = context.Response;

                // Set the content type
                response.ContentType = "text/xml";

                // Compress
                if (siteMap.Compress)
                {
                    httpResponseCompressor.Compress(context);
                }

                // Write
                using (StreamWriter sw = new StreamWriter(response.OutputStream, Encoding.UTF8))
                {
                    using (XmlWriter xtw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = false, Encoding = Encoding.UTF8 }))
                    {
                        WriteSiteMap(xtw, siteMap, context);
                    }
                }

                // Cache
                httpResponseCacher.Cache(context, TimeSpan.FromMinutes(siteMap.CacheDurationInMinutes));
            }
        }

        private static string GetPriority(SiteMapNode node)
        {
            int priority = (int) node.UpdatePriority;
            double actualPriority = priority * .01;

            return actualPriority.ToString("0.0", Culture.Invariant);
        }

        private static string GetApplicationRoot(HttpRequestBase httpRequest)
        {
            string applicationPath = httpRequest.Url.GetLeftPart(UriPartial.Authority) + httpRequest.ApplicationPath;

            // Remove the last /
            if (applicationPath.EndsWith("/", StringComparison.Ordinal))
            {
                applicationPath = applicationPath.Substring(0, applicationPath.Length - 1);
            }

            return applicationPath;
        }

        private void WriteSiteMap(XmlWriter writer, SiteMapBase siteMap, HttpContextBase httpContext)
        {
            string applicationRoot = GetApplicationRoot(httpContext.Request);

            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", SiteMapNameSpace);

            WriteNode(writer, siteMap.RootNode, httpContext, applicationRoot);
            siteMap.RootNode.ChildNodes.Each(node => Iterate(writer, node, httpContext, applicationRoot));

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        private void Iterate(XmlWriter writer, SiteMapNode node, HttpContextBase httpContext, string applicationRoot)
        {
            WriteNode(writer, node, httpContext, applicationRoot);

            node.ChildNodes.Each(childNode => Iterate(writer, childNode, httpContext, applicationRoot));
        }

        private void WriteNode(XmlWriter writer, SiteMapNode node, HttpContextBase httpContext, string applicationRoot)
        {
            if (node.IncludeInSearchEngineIndex)
            {
                string url = GetUrl(node, httpContext, applicationRoot);

                if (!string.IsNullOrEmpty(url))
                {
                    writer.WriteStartElement("url", SiteMapNameSpace);
                    writer.WriteElementString("loc", SiteMapNameSpace, url);

                    if (node.LastModifiedAt.HasValue)
                    {
                        writer.WriteElementString("lastmod", SiteMapNameSpace, node.LastModifiedAt.Value.ToString("yyyy-MM-dd", Culture.Invariant));
                    }

                    if (node.ChangeFrequency != SiteMapChangeFrequency.Automatic)
                    {
                        writer.WriteElementString("changefreq", SiteMapNameSpace, node.ChangeFrequency.ToString().ToLower(Culture.Invariant));
                    }

                    if (node.UpdatePriority != SiteMapUpdatePriority.Automatic)
                    {
                        string priority = GetPriority(node);

                        writer.WriteElementString("priority", SiteMapNameSpace, priority);
                    }

                    writer.WriteEndElement();
                }
            }
        }

        private string GetUrl(INavigationItem node, HttpContextBase httpContext, string applicationRoot)
        {
            string url = urlGenerator.Generate(httpContext.RequestContext(), node);

            if (!string.IsNullOrEmpty(url))
            {
                if (!url.StartsWith("/", StringComparison.Ordinal))
                {
                    url = "/" + url;
                }

                url = applicationRoot + url;
            }

            return url;
        }

        private SiteMapBase GetSiteMap(string name)
        {
            SiteMapBase siteMap = string.IsNullOrEmpty(name) ? siteMaps.DefaultSiteMap : null;

            if (!string.IsNullOrEmpty(name))
            {
                siteMaps.TryGetValue(name, out siteMap);
            }

            return siteMap;
        }
    }
}