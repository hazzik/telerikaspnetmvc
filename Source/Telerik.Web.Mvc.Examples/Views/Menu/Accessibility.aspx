<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<% Html.Telerik().Menu()
            .Name("Menu")
            .Items(menu =>
            {
                menu.Add()
                    .Text("ASP.NET MVC")
					.ImageUrl("~/Content/Common/Icons/Suites/mvc.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Common/Icons/Mvc/Grid.png");
                        item.Add().Text("Menu").ImageUrl("~/Content/Common/Icons/Mvc/Menu.png");
                        item.Add().Text("PanelBar").ImageUrl("~/Content/Common/Icons/Mvc/PanelBar.png");
                        item.Add().Text("TabStrip").ImageUrl("~/Content/Common/Icons/Mvc/TabStrip.png");
                    });

                menu.Add()
                    .Text("Silverlight")
                    .ImageUrl("~/Content/Common/Icons/Suites/sl.png")
                    .Items(item =>
                    {
                        item.Add().Text("GridView").ImageUrl("~/Content/Common/Icons/Sl/GridView.gif");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Common/Icons/Sl/Scheduler.gif");
                        item.Add().Text("Docking").ImageUrl("~/Content/Common/Icons/Sl/Docking.gif");
                        item.Add().Text("Chart").ImageUrl("~/Content/Common/Icons/Sl/Chart.gif");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/silverlight.aspx");
                    });

                menu.Add()
                    .Text("ASP.NET AJAX")
					.ImageUrl("~/Content/Common/Icons/Suites/ajax.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Common/Icons/Ajax/Grid.png");
                        item.Add().Text("Editor").ImageUrl("~/Content/Common/Icons/Ajax/Editor.png");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Common/Icons/Ajax/Scheduler.png");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/aspnet-ajax.aspx");

                    });

                menu.Add()
                    .Text("Reporting")
					.ImageUrl("~/Content/Common/Icons/Suites/rep.png");

                menu.Add()
                    .Text("Sitefinity ASP.NET CMS")
					.ImageUrl("~/Content/Common/Icons/Suites/sitefinity.png");

                menu.Add()
					.HtmlAttributes(new { style = "border-right: 0;" })
                    .Text("Other products")
                    .Items(item =>
                    {
						item.Add().Text("Web Testing Tools").ImageUrl("~/Content/Common/Icons/Suites/test.png");
						item.Add().Text("WinForms UI Controls").ImageUrl("~/Content/Common/Icons/Suites/win.png");
						item.Add().Text("WPF UI Controls").ImageUrl("~/Content/Common/Icons/Suites/wpf.png");
                    });
            })
            .Render();
	%>

    <noscript>
	    <style type="text/css">
	        .t-menu .t-item:hover > .t-group
	        {
	            display: block;
	        }
	    </style>
    </noscript>

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:Content>