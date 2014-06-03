namespace Mvc.UI.jQuery.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [AutoPopulateSourceCodes, HandleError]
    public class TabController : Controller
    {
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult Animation()
        {
            return View();
        }

        public ActionResult OpenOnMouseOver()
        {
            return View();
        }

        public ActionResult CollapsibleContent()
        {
            return View();
        }

        public ActionResult LoadViaAjax()
        {
            return View();
        }

        public ActionResult AjaxView1()
        {
            return PartialView();
        }

        public ActionResult AjaxView2()
        {
            return PartialView();
        }

        public ActionResult Roatation()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Dynamic()
        {
            const int MaxCount = 5;

            IList<DynamicTabContent> contents = new List<DynamicTabContent>();
            Random rnd = new Random();

            for(int i = 1; i <= MaxCount; i++)
            {
                DynamicTabContent tabContent = new DynamicTabContent
                                             {
                                                 Text = "Text Text #" + i,
                                                 Content = "This is a dummy content #" + i,
                                                 IsDisabled = ((rnd.Next(1, MaxCount)% 2) == 0)
                                             };
                contents.Add(tabContent);
            }

            contents[rnd.Next(0, contents.Count - 1)].IsSelected = true;

            return View(contents);
        }
    }
}