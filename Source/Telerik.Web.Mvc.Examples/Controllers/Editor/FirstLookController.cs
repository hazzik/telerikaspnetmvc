namespace Telerik.Web.Mvc.Examples
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml;
    using Telerik.Web.Mvc.Extensions;

    public partial class EditorController : Controller
    {
        public ActionResult FirstLook(string editor)
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