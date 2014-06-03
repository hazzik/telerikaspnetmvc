namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    
    [AutoPopulateSourceCode]
    [PopulateProductSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public partial class ComboBoxController : Controller
    {
    }

    public class AjaxComboBoxAttributes
    {
        public int? FilterMode { get; set; }
        public int? MinimumChars { get; set; }
        public int? Delay { get; set; }
        public bool? Cache { get; set; }
        public bool? HighlightFirst { get; set; }
        public bool? AutoFill { get; set; }
    }

    public class AjaxAutoCompleteAttributes
    {
        public int? FilterMode { get; set; }
        public int? MinimumChars { get; set; }
        public int? Delay { get; set; }
        public bool? Cache { get; set; }
        public bool? HighlightFirst { get; set; }
        public bool? AutoFill { get; set; }

        public bool? AllowMultipleValues { get; set; }
        public string MultipleSeparator { get; set; }
    }

    public class AutoCompleteAttributes
    {
        public int? Width { get; set; }
        
        public int? FilterMode { get; set; }

        public int? MinimumChars { get; set; }
        public bool? HighlightFirst { get; set; }
        public bool? AutoFill { get; set; }

        public bool? AllowMultipleValues { get; set; }
        public string MultipleSeparator { get; set; }
    }

    public class ComboBoxAttributes
    {
        public int? Width { get; set; }
        public int? SelectedIndex { get; set; }

        public int? FilterMode { get; set; }

        public int? MinimumChars { get; set; }
        public bool? HighlightFirst { get; set; }
        public bool? AutoFill { get; set; }
    }

    public class DropDownListAttributes
    {
        public int? Width { get; set; }

        public int? SelectedIndex { get; set; }
    }
}