// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;

    using Moq;

    public static class TestHelper
    {
        public const string AppPathModifier = "/$(SESSION)";
        public const string ApplicationPath = "/app/";

        [DebuggerStepThrough]
        public static HtmlHelper CreateHtmlHelper()
        {
            Mock<HttpContextBase> httpContext = CreateMockedHttpContext();
            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary());

            ViewContext viewContext = new ViewContext(new ControllerContext(new RequestContext(httpContext.Object, new RouteData()), new Mock<ControllerBase>().Object), new Mock<IView>().Object, viewDataContainer.Object.ViewData, new TempDataDictionary());

            HtmlHelper helper = new HtmlHelper(viewContext, viewDataContainer.Object);

            return helper;
        }

        [DebuggerStepThrough]
        public static Mock<HttpContextBase> CreateMockedHttpContext()
        {
            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();

            httpContext.Setup(context => context.Items).Returns(new Hashtable());
            httpContext.Setup(context => context.Server).Returns(new Mock<HttpServerUtilityBase>().Object);
            httpContext.Setup(context => context.Request.AppRelativeCurrentExecutionFilePath).Returns("~/");
            httpContext.Setup(context => context.Request.ApplicationPath).Returns(ApplicationPath);
            httpContext.Setup(context => context.Request.Url).Returns(new Uri("http://localhost"));
            httpContext.Setup(context => context.Request.PathInfo).Returns(string.Empty);
            httpContext.Setup(context => context.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns((TextWriter tw) => new HtmlTextWriter(tw));
            httpContext.Setup(context => context.Request.Browser.EcmaScriptVersion).Returns(new Version("5.0"));
            httpContext.Setup(context => context.Request.Browser.SupportsCss).Returns(true);
            httpContext.Setup(context => context.Request.Browser.MajorVersion).Returns(7);
            httpContext.Setup(context => context.Request.Browser.IsBrowser("IE")).Returns(false);
            httpContext.Setup(context => context.Request.QueryString).Returns(new NameValueCollection());

            httpContext.Setup(context => context.Response.Output).Returns(new Mock<TextWriter>().Object);
            
            httpContext.Setup(context => context.Response.Filter).Returns(new Mock<Stream>().Object);
            // ReSharper disable AccessToStaticMemberViaDerivedType
            httpContext.Setup(context => context.Response.ContentEncoding).Returns(UTF8Encoding.Default);
            // ReSharper restore AccessToStaticMemberViaDerivedType
            httpContext.Setup(context => context.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => AppPathModifier + r);

            return httpContext;
        }

        [DebuggerStepThrough]
        public static RequestContext CreateRequestContext()
        {
            return new RequestContext(CreateMockedHttpContext().Object, new RouteData());
        }

        [DebuggerStepThrough]
        public static void RegisterDummyRoutes(RouteCollection routes)
        {
            routes.Clear();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ProductList", "Products", new { controller = "Product", action = "List" });
            routes.MapRoute("ProductDetail", "Products/Detail/{id}", new { controller = "Product", action = "Detail", id = string.Empty });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = string.Empty });
        }

        [DebuggerStepThrough]
        public static void RegisterDummyRoutes()
        {
            RegisterDummyRoutes(RouteTable.Routes);
        }
    }
}