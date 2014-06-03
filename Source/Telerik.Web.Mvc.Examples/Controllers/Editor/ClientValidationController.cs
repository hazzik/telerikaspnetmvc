namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class EditorController : Controller
    {
        [SourceCodeFile("EmployeeDto (model)", "~/Models/EmployeeDto.cs")]
        public ActionResult ClientValidation(string Editor)
        {
            ViewData["Editor"] = HttpUtility.HtmlDecode(Editor);

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
        public ActionResult ClientValidation(EmployeeDto dto)
        {
            dto.Notes = HttpUtility.HtmlDecode(dto.Notes);
            return View(dto);
        }
    }
}