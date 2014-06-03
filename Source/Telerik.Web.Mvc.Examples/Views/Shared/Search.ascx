<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <%= Html.Telerik().ComboBox()
                        .Name("examples-search")
                        .DataBinding(binding => binding.Ajax().Select("_Search", "Search").Cache(false))
                        .HtmlAttributes(new { style = "width: 183px; z-index:100;" })
                        .DropDownHtmlAttributes(new { id="examples-search-popup", style = "width: 183px;" })
                        .ClientEvents(events => events.OnChange("navigateOnSearch"))
                        .HighlightFirstMatch(true)
    %>

   <% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => { %>window.examplesBaseUrl = '<%= Url.Content("~/") %>';<%  }); %>