namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class DatePickerController : Controller
    {
        [SourceCodeFile("Time.ascx", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("Date.ascx", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("DateTime.ascx", "~/Views/Shared/EditorTemplates/DateTime.ascx")]
        [SourceCodeFile("OrderInfo.cs", "~/Models/OrderInfo.cs")]
        public ActionResult EditTemplate(OrderInfo orderInfo)
        {
            if (orderInfo.OrderInfoID == 0)
            {
                orderInfo.OrderInfoID = 1;
                orderInfo.Delay = new DateTime(2010, 1, 1, 10, 0, 0);
                orderInfo.DeliveryDate = new DateTime(2010, 1, 1);
                orderInfo.OrderDateTime = new DateTime(2010, 1, 1, 10, 0, 0);
            }
            else
            {
                ViewData["orderInfo"] = orderInfo;
            }

            return View(orderInfo);
        }
    }
}