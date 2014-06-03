namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Infrastructure.Implementation;

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AutoPopulateSourceCodeAttribute : FilterAttribute, IResultFilter
    {
        private const string ViewPath = "~/Views/";
        private const string ControllerPath = "~/Controllers/";
        private const string DescriptionPath = "~/Content/";

        private readonly IFileSystem fileSystem;

        public AutoPopulateSourceCodeAttribute(IFileSystem fileSystem)
        {
            Guard.IsNotNull(fileSystem, "fileSystem");

            this.fileSystem = fileSystem;
        }

        public AutoPopulateSourceCodeAttribute()
            : this(new FileSystemWrapper())
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewResult viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                HttpServerUtilityBase server = filterContext.HttpContext.Server;

                string controllerName = filterContext.RouteData.GetRequiredString("controller");

                string commonControllerPath =
                    server.MapPath(ControllerPath + controllerName + "Controller.cs");

                string viewName =
                    !string.IsNullOrEmpty(viewResult.ViewName)
                        ? viewResult.ViewName
                        : filterContext.RouteData.GetRequiredString("action");

                string viewPath =
                    server.MapPath(ViewPath + controllerName + Path.AltDirectorySeparatorChar + viewName + ".aspx");

                string exampleControllerPath =
                    server.MapPath(ControllerPath + controllerName + Path.AltDirectorySeparatorChar + viewName + "Controller.cs");

                string descriptionPath =
                    server.MapPath(DescriptionPath + Path.AltDirectorySeparatorChar + controllerName + 
                    Path.AltDirectorySeparatorChar + "Descriptions" + 
                    Path.AltDirectorySeparatorChar +
                    viewName + ".html");

                var codeFiles = new Dictionary<string, string>();

                var viewData = filterContext.Controller.ViewData;

                if (fileSystem.FileExists(descriptionPath))
                {
                    viewData["Description"] = fileSystem.ReadAllText(descriptionPath);
                }

                codeFiles["View"] = fileSystem.ReadAllText(viewPath);

                string controllerTabTitle = "Controller";

                if (fileSystem.FileExists(exampleControllerPath))
                {
                    controllerTabTitle += " (common)";
                    codeFiles["Controller (example)"] = fileSystem.ReadAllText(exampleControllerPath);
                }

                codeFiles[controllerTabTitle] = fileSystem.ReadAllText(commonControllerPath);

                viewData["codeFiles"] = codeFiles;
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Do Nothing
        }
    }
}