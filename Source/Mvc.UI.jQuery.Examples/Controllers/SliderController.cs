namespace Mvc.UI.jQuery.Examples
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    [AutoPopulateSourceCodes, HandleError]
    public class SliderController : Controller
    {
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult VerticalOrientation()
        {
            return View();
        }

        public ActionResult Range()
        {
            return View();
        }

        public ActionResult MinimumMaximumSteps()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FormSubmitWithValue()
        {
            return View(new Task());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FormSubmitWithValue([Bind] Task task)
        {
            if (string.IsNullOrEmpty(task.Name))
            {
                ModelState.AddModelError("task.Name", "Name cannot be blank.");
            }

            if (task.Completed <= 0)
            {
                ModelState.AddModelError("task.Completed", "Invalid task complete percent.");
            }

            if (ModelState.IsValid)
            {
                //Save here;
                ViewData.Set("successMessage", "Task saved successfully.");
            }

            return View(task);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FormSubmitWithValues()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FormSubmitWithValues(string name, IList<int> salary)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("name", "Name cannot be blank.");
            }

            if (salary.Count != 2)
            {
                ModelState.AddModelError("salary", "Invalid salary range.");
            }

            if (salary.Count == 2)
            {
                if (salary[0] <= 1000)
                {
                    ModelState.AddModelError("salary", "Salary minimum range should be greater than 1000.");
                }

                if (salary[1] <= 2500)
                {
                    ModelState.AddModelError("salary", "Salary maximum range should be greater than 2500.");
                }
            }

            if (ModelState.IsValid)
            {
                //Save here;
                ViewData.Set("successMessage", "Department saved successfully.");
            }

            return View();
        }

        public ActionResult Events()
        {
            return View();
        }
    }
}