namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;

    using System.Web.Mvc;
    using System.Collections.Generic;

    public class DropDownListController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "DropDownList";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult DropDownRendering()
        {
            return View();
        }

        public ActionResult ClientEvents()
        {
            return View();
        }

        public ActionResult ClientAPI()
        {
            return View();
        }

        public JsonResult _AjaxDropDownList()
        {
            IEnumerable<SelectListItem> list = new List<SelectListItem> { 
                new SelectListItem{
                    Text = "Item1",
                    Value = "1",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item2",
                    Value = "2",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item3",
                    Value = "3",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item4",
                    Value = "4",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item5",
                    Value = "5",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item6",
                    Value = "6",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item7",
                    Value = "7",
                    Selected = true
                },
                new SelectListItem{
                    Text = "Item8",
                    Value = "8",
                    Selected = true
                }
            };

            return new JsonResult { JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet, Data = list };
        }
    }
}
