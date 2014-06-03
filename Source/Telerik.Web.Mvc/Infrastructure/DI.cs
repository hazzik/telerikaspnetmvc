﻿// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Web;
    using System.Web.Routing;
    using Telerik.Web.Mvc.Infrastructure.Implementation;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Html;
    
    public static class DI
    {
        public static IDependencyInjectionContainer Current
        {
            get;
            set;
        }

        static DI()
        {
            Current = new DependencyInjectionContainer();
            
            RegisterCoreDependencies();

            RegisterAuthorizationDependencies();

            RegisterCacheDependencies();

            RegisterComponentDependencies();

            GridDependencyBootstrapper.Setup();
            ImageBrowserDependencyBootstrapper.Setup();
        }

        private static void RegisterCoreDependencies()
        {
            Current.Register<IPathResolver>(() => new PathResolver());

            Current.Register<IUrlResolver>(() => new UrlResolver());

            Current.Register<IUrlGenerator>(() => new UrlGenerator());

            Current.Register<IUrlResolver>(() => new UrlResolver());

            Current.Register<IConfigurationManager>(() => new ConfigurationManagerWrapper());

            Current.Register<IWebAssetChecker>(() => new WebAssetChecker());

            Current.Register<IVirtualPathProvider>(() => new VirtualPathProviderWrapper());

            Current.Register<ILocalizationServiceFactory>(() => new LocalizationServiceFactory());

            Current.Register<IHttpResponseCacher>(() => new HttpResponseCacher());

            Current.Register<IHttpResponseCompressor>(() => new HttpResponseCompressor());

            Current.Register<IUrlAuthorization>(() => new UrlAuthorization());

            Current.Register<ISliderHtmlBuilderFactory>(() => new SliderHtmlBuilderFactory());

            Current.Register<IRangeSliderHtmlBuilderFactory>(() => new RangeSliderHtmlBuilderFactory());

            Current.Register<ScriptWrapperBase>(() => new ScriptWrapper());
        }
        private static void RegisterAuthorizationDependencies()
        {
            Current.Register<IAuthorizeAttributeBuilder>(() => new AuthorizeAttributeBuilder());


            Current.Register<IControllerAuthorization, IAuthorizeAttributeCache, IReflectedAuthorizeAttributeCache, IObjectCopier>((authorizeAttributeCache,
                reflectedAuthorizeAttributeCache, objectCopier) => new ControllerAuthorization(authorizeAttributeCache, reflectedAuthorizeAttributeCache,
                    objectCopier, RouteTable.Routes));

            Current.Register<INavigationItemAuthorization, IControllerAuthorization, IUrlAuthorization>((controllerAuthorization, urlAuthorization) =>
                new NavigationItemAuthorization(controllerAuthorization, urlAuthorization)); RegisterWebAssetDependencies();
        }
        
        private static void RegisterWebAssetDependencies()
        {
            Current.Register<IWebAssetExtensions>(() =>
            {
                if (HttpContext.Current.IsDebuggingEnabled)
                {
                    return new DebugWebAssetExtensions();
                }

                return new ReleaseWebAssetExtensions();
            });
            
            Current.Register<IWebAssetLocator, ICacheFactory, IVirtualPathProvider, IWebAssetExtensions>((cacheFactory, provider, extensions) =>
                            new WebAssetLocator(cacheFactory.Create("locator"), provider, extensions));

            Current.Register<IWebAssetContentFilter, IVirtualPathProvider, IUrlResolver>((provider, resolver) =>
                new RebaseImagePathContentFilter(provider, resolver));

            Current.Register<IWebAssetGroupSerializer>(() => new WebAssetGroupSerializer());

            Current.Register<IWebAssetGroupReader, IWebAssetLocator, IVirtualPathProvider, IWebAssetContentFilter>((locator, provider, filter) =>
                new WebAssetGroupReader(locator, provider, filter));

            Current.Register<IWebAssetResolverFactory, IWebAssetChecker, IWebAssetLocator, IWebAssetGroupSerializer>((checker, locator, serializer) =>
                new WebAssetResolverFactory(checker, locator, serializer));

            Current.Register<IWebAssetCollectionResolver, IUrlResolver, IWebAssetResolverFactory>((urlResolver, resolverFactory) =>
                new WebAssetCollectionResolver(urlResolver, resolverFactory));
        }
        
        private static void RegisterComponentDependencies()
        {
            Current.Register<INavigationComponentHtmlBuilderFactory<PanelBar, PanelBarItem>, IActionMethodCache>((actionMethodCache) =>
                            new PanelBarHtmlBuilderFactory(actionMethodCache));

            Current.Register<INavigationComponentHtmlBuilderFactory<Menu, MenuItem>, IActionMethodCache>((actionMethodCache) =>
                new MenuHtmlBuilderFactory(actionMethodCache));

            Current.Register<ITabStripHtmlBuilderFactory, IActionMethodCache>((actionMethodCache) =>
                new TabStripHtmlBuilderFactory(actionMethodCache));

            Current.Register<ITreeViewHtmlBuilderFactory, IActionMethodCache>((actionMethodCache) =>
                new TreeViewHtmlBuilderFactory(actionMethodCache));

            Current.Register<IClientSideObjectWriterFactory>(() => new ClientSideObjectWriterFactory());
            
            Current.Register<ICalendarHtmlBuilderFactory>(() => new CalendarHtmlBuilderFactory());

            Current.Register<IWindowHtmlBuilderFactory>(() => new WindowHtmlBuilderFactory());

            Current.Register<IGridActionResultAdapterFactory>(() => new GridActionResultAdapterFactory());
        }
        
        static void RegisterCacheDependencies()
        {
            Current.Register<ICacheProvider>(() => new CacheProvider());
            Current.Register<ICacheFactory, ICacheProvider>((provider) => new CacheFactory(HttpContext.Current.IsDebuggingEnabled, provider));
            Current.Register<IReflectedAuthorizeAttributeCache, ICacheFactory, IAuthorizeAttributeBuilder>((cacheFactory, authorizeAttributeBuilder) =>
                new ReflectedAuthorizeAttributeCache(cacheFactory.Create("authorizeAttribute"), authorizeAttributeBuilder));
            Current.Register<IControllerTypeCache, ICacheFactory>((cacheFactory) => new ControllerTypeCache(cacheFactory.Create("controllerType")));
            Current.Register<IObjectCopier, IFieldCache, IPropertyCache>((fieldCache, propertyCache) => new ObjectCopier(fieldCache, propertyCache));
            Current.Register<IFieldCache, ICacheFactory>((cacheFactory) => new FieldCache(cacheFactory.Create("fields")));
            Current.Register<IPropertyCache, ICacheFactory>((cacheFactory) => new PropertyCache(cacheFactory.Create("properties")));
            Current.Register<IActionMethodCache, ICacheFactory, IControllerTypeCache>((cacheFactory, controllerTypeCache) =>
                new ActionMethodCache(cacheFactory.Create("actionMethod"), controllerTypeCache));
            Current.Register<IAuthorizeAttributeCache, ICacheFactory, IControllerTypeCache, IActionMethodCache>((cacheFactory, controllerTypeCache, actionMethodCache) =>
                new AuthorizeAttributeCache(cacheFactory.Create("authorizeAttribute"), controllerTypeCache, actionMethodCache));
        }
    }
}
