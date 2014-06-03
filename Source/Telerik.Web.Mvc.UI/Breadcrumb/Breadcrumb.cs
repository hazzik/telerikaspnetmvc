namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;

    public class Breadcrumb : ViewComponentBase, IHideObjectMembers
    {
        private static string defaultSeparatorText = " > ";

        public Breadcrumb(ViewContext viewContext, IStyleSheetContainer styleSheetContainer, IScriptContainer scriptContainer, IResponseCapturer responseCapturer) : base(viewContext, styleSheetContainer, scriptContainer, responseCapturer)
        {
            SeparatorText = DefaultSeparatorText;
        }

        public static string DefaultSeparatorText
        {
            [DebuggerStepThrough]
            get
            {
                return defaultSeparatorText;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                defaultSeparatorText = value;
            }
        }

        public string ViewDataKey
        {
            get;
            set;
        }

        public string SeparatorText
        {
            get;
            set;
        }

        public Action SeparatorHtmlMarkups
        {
            get;
            set;
        }

        public string Theme
        {
            get;
            set;
        }
    }
}