<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Basic</asp:Content>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
 
	<%
        MenuOrientation orientation = (MenuOrientation)Enum.Parse(typeof(MenuOrientation), (string)ViewData["orientation"]);

        var htmlAttributes = new { style = "float: left; z-index: 3; position: relative;" };

        if (orientation == MenuOrientation.Vertical)
            htmlAttributes = new { style = "float: left; width: 250px;" };
	       
	    Html.Telerik().Menu()
            .Name("Menu")
            .HtmlAttributes(htmlAttributes)
            .Orientation(orientation)
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
	
	
<% using (Html.Configurator("The menu should be...")
              .PostTo("Orientation", "Menu")
              .Begin())
   { %>
    <ul>
        <li>
            <%= Html.RadioButton("orientation", MenuOrientation.Vertical.ToString(), true, new { id = "vertical" })%>
            <label for="vertical">vertical</label>
        </li>
        <li>
            <%= Html.RadioButton("orientation", MenuOrientation.Horizontal.ToString(), new { id = "horizontal" })%>
            <label for="horizontal">horizontal</label>
        </li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
	    .example .configurator
	    {
	        width: 300px;
	        float: right;
	        margin: 3em 0 0;
	        display: inline;
	    }
	    
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