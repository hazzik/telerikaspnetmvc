namespace Telerik.Web.Mvc.JavaScriptTests.Extensions
{
    using System.Linq;
    using System.Web.Mvc;

    public static class ControllerExtensions
    {
        static public string[] GetActions(this Controller controller)
        {
            return controller.GetType().GetMethods()
                .Where(x => x.ReturnType == typeof(ActionResult) && x.Name != "Index" && x.GetCustomAttributes(true).Length == 0)
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToArray();
        }
    }
}