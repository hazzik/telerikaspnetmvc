namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;
    using Infrastructure;
    using Moq;
    using System.Web.Caching;
	using Telerik.Web.Mvc.UI;

	public static class MenuTestHelper
	{
		public static Menu CreteMenu(HtmlTextWriter writer, IMenuRenderer renderer)
		{
			Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

			if (writer != null)
			{
				httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(writer);
			}

			Mock<IMenuRendererFactory> menuRendererFactory = new Mock<IMenuRendererFactory>();

			Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();
			var viewDataDinctionary = new ViewDataDictionary();
			viewDataDinctionary.Add("sample", CreateXmlSiteMap());

			viewDataContainer.SetupGet(container => container.ViewData).Returns(viewDataDinctionary);

			// needed for testing serialization
			Mock<ClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<ClientSideObjectWriterFactory>();
			Mock<IUrlGenerator> urlGenerator = new Mock<IUrlGenerator>();
			Mock<INavigationItemAuthorization> authorization = new Mock<INavigationItemAuthorization>();
			ViewContext viewContext = new ViewContext(new ControllerContext(new RequestContext(httpContext.Object, new RouteData()), new Mock<ControllerBase>().Object), new Mock<IView>().Object, viewDataContainer.Object.ViewData, new TempDataDictionary());

			authorization.Setup(a => a.IsAccessibleToUser(viewContext.RequestContext, It.IsAny<INavigatable>())).Returns(true);

			Menu menu = new Menu(viewContext, clientSideObjectWriterFactory.Object, urlGenerator.Object, authorization.Object, menuRendererFactory.Object);

			renderer = renderer ?? new MenuRenderer(menu, writer, new Mock<IActionMethodCache>().Object);
			menuRendererFactory.Setup(f => f.Create(It.IsAny<Menu>(), It.IsAny<HtmlTextWriter>())).Returns(renderer);

			return menu;
		}

        public static SiteMapBase CreateXmlSiteMap()
        {
            Mock<IPathResolver> _pathResolver = new Mock<IPathResolver>();
            Mock<IFileSystem> _fileSystem = new Mock<IFileSystem>();
            Mock<ICacheManager> _cacheManager = new Mock<ICacheManager>();

            const string Xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>" + "\r\n" +
                               @"<siteMap compress=""false"" cacheDurationInMinutes=""120"" generateSearchEngineMap=""true"">" +
                               "\r\n" +
                               @"    <siteMapNode title=""Home"" route=""Home"" foo=""bar"">" + "\r\n" +
                               @"        <siteMapNode title=""Products"" route=""ProductList"" visible=""true"" " +
                               @"lastModifiedAt=""2009/1/3"" changeFrequency=""hourly"" updatePriority=""high"" >" +
                               "\r\n" +
                               @"            <siteMapNode title=""Product 1"" controller=""Product"" action=""Detail"">" +
                               "\r\n" +
                               @"                <routeValues>" + "\r\n" +
                               @"                    <id>1</id>" + "\r\n" +
                               @"                </routeValues>" + "\r\n" +
                               @"            </siteMapNode>" + "\r\n" +
                               @"            <siteMapNode title=""Product 2"" controller=""Product"" action=""Detail"" " +
                               @"includeInSearchEngineIndex=""true"">" + "\r\n" +
                               @"                <routeValues>" + "\r\n" +
                               @"                    <id>2</id>" + "\r\n" +
                               @"                </routeValues>" + "\r\n" +
                               @"            </siteMapNode>" + "\r\n" +
                               @"        </siteMapNode>" + "\r\n" +
                               @"        <siteMapNode title=""Faq"" url=""~/faq"" />" + "\r\n" +
                               @"    </siteMapNode>" + "\r\n" +
                               @"</siteMap>";

            _pathResolver.Setup(pathResolver => pathResolver.Resolve(It.IsAny<string>())).Returns("C:\\Web.sitemap").Verifiable();
            _fileSystem.Setup(fileSystem => fileSystem.ReadAllText(It.IsAny<string>())).Returns(Xml).Verifiable();
            _cacheManager.Setup(cache => cache.Insert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CacheItemRemovedCallback>(), It.IsAny<string>())).Verifiable();

            var siteMap = new XmlSiteMap(_pathResolver.Object, _fileSystem.Object, _cacheManager.Object);
            siteMap.Load();
            return siteMap;
        }
    }
}
