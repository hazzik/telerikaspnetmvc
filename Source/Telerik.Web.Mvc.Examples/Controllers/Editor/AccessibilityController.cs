namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;

    public partial class EditorController : Controller
    {
        [ValidateInput(false)]
        public ActionResult Accessibility(string editor)
        {
            // The HTML comes encoded so we should decode it first
            var html = HttpUtility.HtmlDecode(editor);
            if (html != null)
            {
                ViewData["editor"] = html;

                ViewData["value"] = HttpUtility.HtmlEncode(html.IndentHtml());
            }
            return View();
        }
    }
}