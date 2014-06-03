namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;

    public partial class MenuController : Controller
	{
        public ActionResult AnimationEffects(bool? enableHeightAnimation, int? heightOpenDuration, int? heightCloseDuration, bool? enableOpacityAnimation, int? opacityOpenDuration, int? opacityCloseDuration)
        {
            ViewData["enableHeightAnimation"] = enableHeightAnimation ?? true;
            ViewData["heightOpenDuration"] = heightOpenDuration ?? 200;
            ViewData["heightCloseDuration"] = heightCloseDuration ?? 200;
			ViewData["enableOpacityAnimation"] = enableOpacityAnimation ?? true;
			ViewData["opacityOpenDuration"] = opacityOpenDuration ?? 200;
			ViewData["opacityCloseDuration"] = opacityCloseDuration ?? 200;

            return View();
        }
    }
}