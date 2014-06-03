namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;
    using System.Web.Mvc;

    public static class HtmlHelperExtension
    {
        [DebuggerStepThrough]
        public static ViewComponentFactory Rad(this HtmlHelper helper)
        {
            Guard.IsNotNull(helper, "helper");

            //return new ViewComponentFactory(helper);
            return null;
        }
    }
}