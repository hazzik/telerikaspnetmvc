namespace Telerik.Web.Mvc.Examples
{
	using System;
	using System.Collections.Generic;
	using System.Web;
	using System.Web.Mvc;
	using Telerik.Web.Mvc.Infrastructure;
	using Telerik.Web.Mvc.Infrastructure.Implementation;

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public class SourceCodeFileAttribute : FilterAttribute, IResultFilter
	{
		private readonly IFileSystem fileSystem;
		private readonly string caption;
		private readonly string filename;

		public SourceCodeFileAttribute(IFileSystem fileSystem, string caption, string filename)
		{
			Guard.IsNotNull(fileSystem, "fileSystem");
			Guard.IsNotNullOrEmpty(caption, "caption");
			Guard.IsNotNullOrEmpty(filename, "filename");

			this.fileSystem = fileSystem;
			this.caption = caption;
			this.filename = filename;
		}

		public SourceCodeFileAttribute(string caption, string filename)
			: this(new FileSystemWrapper(), caption, filename)
		{
		}

		public void OnResultExecuting(ResultExecutingContext filterContext)
		{
			HttpServerUtilityBase server = filterContext.HttpContext.Server;

			ViewDataDictionary viewData = filterContext.Controller.ViewData;

			var codeFiles = viewData.Get<Dictionary<string, string>>("codeFiles");

			codeFiles[caption] = fileSystem.ReadAllText(server.MapPath(filename));
		}

		public void OnResultExecuted(ResultExecutedContext filterContext)
		{
			//Do Nothing
		}
	}
}