// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Infrastructure;
    using Infrastructure.Implementation;

    /// <summary>
    /// HTMLHelper extension for providing access to <see cref="ViewComponentFactory"/>.
    /// </summary>
    public static class HtmlHelperExtension
    {
        private static readonly string Key = typeof(ViewComponentFactory).AssemblyQualifiedName;

        /// <summary>
        /// Gets the Telerik View Component Factory
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>The Factory</returns>
        public static ViewComponentFactory Telerik(this HtmlHelper helper)
        {
            Guard.IsNotNull(helper, "helper");

            ViewContext viewContext = helper.ViewContext;
            HttpContextBase httpContext = viewContext.HttpContext;
            ViewComponentFactory factory = httpContext.Items[Key] as ViewComponentFactory;

            if (factory == null)
            {
                IWebAssetItemMerger assetItemMerger = CreateAssetMerger(viewContext);

                StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), new List<IStyleableComponent>(), viewContext, assetItemMerger);
                ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, assetItemMerger, new ScriptWrapper());

                StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(styleSheetRegistrar);
                ScriptRegistrarBuilder scriptRegistrarBuilder = new ScriptRegistrarBuilder(scriptRegistrar);

                factory = new ViewComponentFactory(styleSheetRegistrarBuilder, scriptRegistrarBuilder);

                helper.ViewContext.HttpContext.Items[Key] = factory;
            }

            return factory;
        }

        private static IWebAssetItemMerger CreateAssetMerger(ControllerContext context)
        {
            IPathResolver pathResolver = new PathResolver();
            IFileSystem fileSystem = new FileSystemWrapper();
            IUrlResolver urlResolver = new UrlResolver(new UrlHelper(context.RequestContext));
            IWebAssetLocator assetLocator = new WebAssetLocator(context.HttpContext.IsDebuggingEnabled, pathResolver, fileSystem);
            ICacheManager cacheManager = new CacheManagerWrapper();
            IWebAssetRegistry assetRegistry = new WebAssetRegistry(cacheManager, assetLocator, pathResolver, fileSystem);
            IWebAssetItemMerger assetItemMerger = new WebAssetItemMerger(assetRegistry, urlResolver, context.HttpContext.Server);

            return assetItemMerger;
        }
    }
}