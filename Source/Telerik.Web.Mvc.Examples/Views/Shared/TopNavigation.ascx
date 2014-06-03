﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="examples-search" class="menu-bar">
    <label for="search-input"></label>

    <%= Html.Telerik().ComboBox()
            .Name("search")
            .Encode(false)
            .DataBinding(binding => binding.Ajax().Select("_Search", "Search").Cache(false))
            .HtmlAttributes(new { style = "width: 183px; z-index:100;" })
            .DropDownHtmlAttributes(new { id = "examples-search-popup", style = "width: 188px;" })
            .ClientEvents(events => events.OnLoad("searchBoxLoad"))
            .HighlightFirstMatch(true)
    %>

</div>

<% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => { %>window.examplesBaseUrl = '<%= Url.Content("~/") %>';<%  }); %>

<%  Html.Telerik().Menu()
        .Name("resources")
        .HtmlAttributes(new { @class = "menu-bar" })
        .OpenOnClick(true)
        .Items(menu =>
        {
            menu.Add().Text("Support Resources")
                .Items(resources =>
                {
                    resources.Add().Text("Online Documentation").Url("http://www.telerik.com/help/aspnet-mvc/");
                    resources.Add().Text("Forums").Url("http://www.telerik.com/community/forums/aspnet-mvc/general.aspx");
                    resources.Add().Text("Blogs").Url("http://blogs.telerik.com/");
                    resources.Add().Text("Code Library").Url("http://www.telerik.com/community/code-library.aspx");
                    resources.Add().Text("Videos").Url("http://tv.telerik.com/products/aspnet-mvc");
                    resources.Add().Text("Issue Tracker").Url("http://www.telerik.com/support/pits.aspx");
                });

            menu.Add().Text("All Telerik Products")
                .ContentHtmlAttributes(new { id = "all-products" })
                .Content(() => {%>
                    <div class="column">
                        <h2>Developer Tools</h2>
                        <h3>Web UI Controls &amp; Components</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/aspnet-ajax.aspx">ASP.NET AJAX</a></li>
                            <li><a href="http://www.telerik.com/products/aspnet-mvc.aspx">ASP.NET MVC</a></li>
                            <li><a href="http://www.telerik.com/products/silverlight.aspx">Silverlight</a></li>
                        </ul>

                        <h3>Desktop UI Controls &amp; Components</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/wpf.aspx">WPF</a></li>
                            <li><a href="http://www.telerik.com/products/winforms.aspx">Windows Forms</a></li>
                        </ul>

                        <h3>Mobile UI Controls &amp; Components</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/windows-phone.aspx">Windows Phone</a></li>
                        </ul>

                        <h3>Report Designer and Viewer</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/reporting.aspx">Telerik Reporting</a></li>
                        </ul>

                        <h3>Data Access</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/orm.aspx">OpenAccess ORM</a></li>
                        </ul>

                        <h3>Visual Studio Productivity Plugins</h3>
                        <ul>
                            <li><a href="http://www.telerik.com/products/justcode.aspx">JustCode</a> <span class="product-info-regular">(Code Analysis, Refactoring)</span></li>
                            <li><a href="http://www.telerik.com/products/mocking.aspx">JustMock</a> <span class="product-info-regular">(Unit Testing, Mocking)</span></li>
                        </ul>
                    </div>
                    <div class="column">
                        <h2>Agile Project Management</h2>
                        <ul>
                            <li><a href="http://www.telerik.com/team-productivity-tools.aspx">TeamPulse</a></li>
                        </ul>

                        <h2>Automated Testing</h2>
                        <ul>
                            <li><a href="http://www.telerik.com/automated-testing-tools.aspx">Web UI Testing</a></li>
                            <li><a href="http://www.telerik.com/automated-testing-tools/webaii-framework-features.aspx">Test Automation Framework</a></li>
                        </ul>

                        <h2>Web Content Management</h2>
                        <ul>
                            <li><a href="http://www.sitefinity.com/">Sitefinity ASP.NET CMS</a></li>
                            <li><a href="http://www.sitefinity.com/marketplace.aspx">Add-ons Marketplace</a></li>
                        </ul>

                        <h2>SharePoint</h2>
                        <ul>
                            <li><a href="http://www.telerik.com/products/sharepoint.aspx">SharePoint Acceleration Kit</a></li>
                        </ul>
                    </div>
                <%});
        })
        .Render();
%>

<div id="call-to-action" class="menu-bar">
    <a href="http://www.telerik.com/community/license-agreement.aspx?pId=697" onclick="if (typeof _gaq != 'undefined') _gaq.push(['_trackPageview', '/mvc-demo/topbutton/trial']);"><span>Download</span></a>
    <a href="http://www.telerik.com/purchase/individual-mvc.aspx" onclick="if (typeof _gaq != 'undefined') _gaq.push(['_trackPageview', '/mvc-demo/topbutton/buy']);"><span>Buy now</span></a>
</div>

<div id="product-lines">
    Other Demos:
    <ul>
        <li><a href="http://demos.telerik.com/aspnet-ajax">ASP.NET AJAX</a></li>
        <li><a href="http://demos.telerik.com/silverlight">Silverlight</a></li>
        <li><a href="http://demos.telerik.com/wpf">WPF</a></li>
        <li><a href="http://demos.telerik.com/reporting">Reporting</a></li>
        <li><a href="http://demos.telerik.com/orm">OpenAccess</a></li>
        <li class="last"><a href="http://www.telerik.com/products/windows-phone.aspx">Windows Phone 7</a></li>
    </ul>
</div>