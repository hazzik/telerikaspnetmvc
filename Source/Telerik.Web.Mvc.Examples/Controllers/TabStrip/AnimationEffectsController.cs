namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;

    public partial class TabStripController : Controller
	{
        public ActionResult AnimationEffects(string enabledAnimation, int? heightOpenDuration, int? heightCloseDuration, int? opacityOpenDuration, int? opacityCloseDuration)
        {
            ViewData["enabledAnimation"] = enabledAnimation ?? "height";
            ViewData["heightOpenDuration"] = heightOpenDuration ?? 200;
            ViewData["heightCloseDuration"] = heightCloseDuration ?? 200;
			ViewData["opacityOpenDuration"] = opacityOpenDuration ?? 200;
			ViewData["opacityCloseDuration"] = opacityCloseDuration ?? 200;

            return View();
        }
    }
}