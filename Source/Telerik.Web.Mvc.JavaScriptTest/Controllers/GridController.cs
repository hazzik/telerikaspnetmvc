using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Telerik.Web.Mvc.JavaScriptTest.Controllers
{
    public class GridController : Controller
    {
        public ActionResult Paging()
		{
			ViewData["lessData"] = TestData(2);
			ViewData["moreData"] = TestData(20);

            return View();
        }
        
        public ActionResult Sorting()
        {
			var data = TestData(2);

			return View(data);
        }

        public ActionResult Binding()
        {
            return View(TestData(20));
        }

        public ActionResult Filtering()
        {
            return View(TestData(20));
        }

		private List<Customer> TestData(int rowCount)
		{
			List<Customer> data = new List<Customer>();
            DateTime date = new DateTime(1980, 1, 1);

			for (int i = 0; i < rowCount; i++)
            {
                data.Add(new Customer{ Name = "Customer" + (i + 1), BirthDate = date.AddDays(i) });
            }
			return data;
		}
    }
}
