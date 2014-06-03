﻿using System.Web.Mvc;

namespace Telerik.Web.Mvc.JavaScriptTest.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index(string testPage, bool autorun)
		{
			ViewData["Message"] = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult TestPage()
		{
			return View();
		}
	
		public ActionResult Suite()
		{
			return View();
		}
	}
}
