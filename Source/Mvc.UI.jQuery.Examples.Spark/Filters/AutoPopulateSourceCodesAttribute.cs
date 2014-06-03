namespace Mvc.UI.jQuery.Examples.SparkView
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Infrastructure.Implementation;

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AutoPopulateSourceCodesAttribute : FilterAttribute, IResultFilter
    {
        private const string BaseViewPath = "~/Views/";
        private const string BaseControllerPath = "~/Controllers/";

        private readonly IFileSystem fileSystem;

        public AutoPopulateSourceCodesAttribute(IFileSystem fileSystem)
        {
            Guard.IsNotNull(fileSystem, "fileSystem");

            this.fileSystem = fileSystem;
        }

        public AutoPopulateSourceCodesAttribute() : this(new FileSystemWrapper())
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewResult viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                HttpServerUtilityBase server = filterContext.HttpContext.Server;

                string controllerName = filterContext.RouteData.GetRequiredString("controller");

                string controllerVirtualPath = BaseControllerPath + controllerName + "Controller.cs";
                string controllerPhysicalPath = server.MapPath(controllerVirtualPath);

                string viewName = !string.IsNullOrEmpty(viewResult.ViewName) ? viewResult.ViewName : filterContext.RouteData.GetRequiredString("action");
                string viewVirtualPath = BaseViewPath + controllerName + Path.AltDirectorySeparatorChar + viewName + ".spark";
                string viewPhysicalPath = server.MapPath(viewVirtualPath);

                string masterPageVirtualPath = BaseViewPath + "Shared" + Path.AltDirectorySeparatorChar + "application.spark";
                string masterPagePhysicalPath = server.MapPath(masterPageVirtualPath);

                string descriptionVirtualPath = BaseViewPath + controllerName + Path.AltDirectorySeparatorChar + viewName + ".txt";
                string descriptionPhysicalPath = server.MapPath(descriptionVirtualPath);

                string controllerSourceCode = fileSystem.ReadAllText(controllerPhysicalPath);
                string viewSourceCode = fileSystem.ReadAllText(viewPhysicalPath);
                string masterPageSourceCode = fileSystem.ReadAllText(masterPagePhysicalPath);

                string description = fileSystem.FileExists(descriptionPhysicalPath) ?
                                     fileSystem.ReadAllText(descriptionPhysicalPath) :
                                     "Description file does not exist for \"{0}\".".FormatWith(viewName);

                ViewDataDictionary viewData = filterContext.Controller.ViewData;

                viewData.Set("viewSourceCode", viewSourceCode);
                viewData.Set("controllerSourceCode", controllerSourceCode);
                viewData.Set("masterPageSourceCode", masterPageSourceCode);
                viewData.Set("description", description);
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Do Nothing
        }
    }
}