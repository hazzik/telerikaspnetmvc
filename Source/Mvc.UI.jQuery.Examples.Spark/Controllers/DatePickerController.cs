namespace Mvc.UI.jQuery.Examples.SparkView
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    [AutoPopulateSourceCodes, HandleError]
    public class DatePickerController : Controller
    {
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult PopulateAlternateField()
        {
            return View();
        }

        public ActionResult ConstrainInput()
        {
            return View();
        }

        public ActionResult MonthYearMenu()
        {
            return View();
        }

        public ActionResult DefaultValue()
        {
            return View();
        }

        public ActionResult RestrictDateRange()
        {
            return View();
        }

        public ActionResult DateFormat()
        {
            IList<SelectListItem> availableFormates = new List<SelectListItem>
                                                      {
                                                          new SelectListItem { Text = "Default - mm/dd/yy", Value = "mm/dd/yy" },
                                                          new SelectListItem { Text = "ISO 8601 - yy-mm-dd", Value = "yy-mm-dd" },
                                                          new SelectListItem { Text = "Short - d M, yy", Value = "d M, y" },
                                                          new SelectListItem { Text = "Short - d M, yy", Value = "d MM, y" },
                                                          new SelectListItem { Text = "Full - DD, d MM, yy", Value = "DD, d MM, yy" },
                                                          new SelectListItem { Text = "With text - 'day' d 'of' MM 'in the year' yy", Value = "'day' d 'of' MM 'in the year' yy" }
                                                      };

            return View(availableFormates);
        }

        public ActionResult NumberOfMonthsToShow()
        {
            return View();
        }

        public ActionResult ShowOn()
        {
            return View();
        }
    }
}