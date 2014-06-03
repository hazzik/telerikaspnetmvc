<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Basic</asp:Content>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<% Html.Telerik().Menu()
            .Name("Menu")
            .HtmlAttributes(new { style = "z-index: 3; position: relative;" })
            .Items(menu =>
            {
                menu.Add()
                    .Text("ASP.NET MVC")
					.ImageUrl("~/Content/Images/Icons/Suites/mvc.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Images/Icons/Mvc/Grid.png");
                        item.Add().Text("Menu").ImageUrl("~/Content/Images/Icons/Mvc/Menu.png");
                        item.Add().Text("PanelBar").ImageUrl("~/Content/Images/Icons/Mvc/PanelBar.png");
                        item.Add().Text("TabStrip").ImageUrl("~/Content/Images/Icons/Mvc/TabStrip.png");
                    });

                menu.Add()
                    .Text("Silverlight")
                    .ImageUrl("~/Content/Images/Icons/Suites/sl.png")
                    .Items(item =>
                    {
                        item.Add().Text("GridView").ImageUrl("~/Content/Images/Icons/Sl/GridView.gif");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Images/Icons/Sl/Scheduler.gif");
                        item.Add().Text("Docking").ImageUrl("~/Content/Images/Icons/Sl/Docking.gif");
                        item.Add().Text("Chart").ImageUrl("~/Content/Images/Icons/Sl/Chart.gif");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/silverlight.aspx");
                    });

                menu.Add()
                    .Text("ASP.NET AJAX")
					.ImageUrl("~/Content/Images/Icons/Suites/ajax.png")
                    .Items(item =>
                    {
                        item.Add().Text("Grid").ImageUrl("~/Content/Images/Icons/Ajax/Grid.png");
                        item.Add().Text("Editor").ImageUrl("~/Content/Images/Icons/Ajax/Editor.png");
                        item.Add().Text("Scheduler").ImageUrl("~/Content/Images/Icons/Ajax/Scheduler.png");
                        item.Add().Text("... and 28 more!").Url("http://www.telerik.com/products/aspnet-ajax.aspx");

                    });

                menu.Add()
                    .Text("OpenAccess ORM")
					.ImageUrl("~/Content/Images/Icons/Suites/orm.png")
                    .Content(() =>
                    {%>
                        <img src="<%= ResolveUrl("~/Content/Common/orm-comparison.png") %>" alt="Telerik OpenAccess" />
                        <a id="buy" href="/purchase/individual/orm.aspx">Telerik OpenAccess ORM</a>
                        <a id="express" href="/community/license-agreement.aspx?pId=639">Telerik OpenAccess ORM Express</a>
                    <%});

                menu.Add()
                    .Text("Reporting")
					.ImageUrl("~/Content/Images/Icons/Suites/rep.png");

                menu.Add()
                    .Text("Sitefinity ASP.NET CMS")
					.ImageUrl("~/Content/Images/Icons/Suites/sitefinity.png");

                menu.Add()
					.HtmlAttributes(new { style = "border-right: 0;" })
                    .Text("Other products")
                    .Items(item =>
                    {
						item.Add().Text("Web Testing Tools").ImageUrl("~/Content/Images/Icons/Suites/test.png");
						item.Add().Text("WinForms UI Controls").ImageUrl("~/Content/Images/Icons/Suites/win.png");
						item.Add().Text("WPF UI Controls").ImageUrl("~/Content/Images/Icons/Suites/wpf.png");
                    });
            })
            .Render();
	%>

</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
	    #buy, #express
	    {
	        position: absolute;
	        width: 50%;
	        height: 100%;
	        text-indent: -9999px;
	    }
	    
	    #buy { left: 0; }
	    #express { right: 0; }
	</style>
</asp:Content>