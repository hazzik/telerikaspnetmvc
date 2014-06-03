﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
    
    <div id="examples-search">
        <label for="search-input">Search examples</label>

        <%= Html.Telerik().ComboBox()
                .Name("search")
                .DataBinding(binding => binding.Ajax().Select("_Search", "Search").Cache(false))
                .HtmlAttributes(new { style = "width: 183px; z-index:100;" })
                .DropDownHtmlAttributes(new { id="examples-search-popup", style = "width: 183px;" })
                .ClientEvents(events => events.OnChange("navigateOnSearch"))
                .HighlightFirstMatch(true)
        %>

    </div>

   <% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => { %>window.examplesBaseUrl = '<%= Url.Content("~/") %>';<%  }); %>