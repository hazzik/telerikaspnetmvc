namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    public partial class GridController : Controller
    {
        [SourceCodeFile("EditableProduct (model)", "~/Models/EditableProduct.cs")]
        [SourceCodeFile("Date.ascx (editor)", "~/Views/Shared/EditorTemplates/Date.ascx")]
        [SourceCodeFile("ASMX", "~/Models/Products.asmx.cs")]
        [SourceCodeFile("WCF", "~/Models/Products.svc.cs")]
        public ActionResult EditingWebService(GridEditMode? mode, GridButtonType? type)
        {
            ViewData["mode"] = mode ?? GridEditMode.InLine;
            ViewData["type"] = type ?? GridButtonType.Text;
            return View();
        }
    }
}
