namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class EditorController : Controller
    {
        [SourceCodeFile("EmployeeDto (model)", "~/Models/EmployeeDto.cs")]
        public ActionResult ServerValidation()
        {
            EmployeeDto dto = new EmployeeDto
               {
                   FirstName = "Nancy",
                   LastName = "Davolio",
                   Notes = ""
               };

            return View(dto);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [SourceCodeFile("EmployeeDto (model)", "~/Models/EmployeeDto.cs")]
        public ActionResult ServerValidation(EmployeeDto EmployeeDto)
        {
            if (ModelState.IsValid)
            {
                ViewData["FirstName"] = EmployeeDto.FirstName;
                ViewData["LastName"] = EmployeeDto.LastName;
                ViewData["Notes"] = HttpUtility.HtmlDecode(EmployeeDto.Notes);
            }

            return View();
        }
    }
}