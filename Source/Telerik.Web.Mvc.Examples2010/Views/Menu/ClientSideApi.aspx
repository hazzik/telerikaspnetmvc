<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="ExampleTitle" runat="server">Client-side API</asp:content>

<asp:content contentPlaceHolderID="MainContent" runat="server">
<%= Html.Telerik().Menu()
        .Name("Menu")
        .HtmlAttributes(new { style = " margin-bottom: 80px;" })
		.Items(menu =>
		{
			menu.Add().Text("UI Components")
				.Items(item =>
				{
					item.Add().Text("ASP.NET WebForms");
					item.Add().Text("Silverlight");
					item.Add().Text("ASP.NET MVC");
					item.Add().Text("WinForms");
					item.Add().Text("WPF");
				});

			menu.Add().Text("Data")
				.Items(item =>
				{
					item.Add().Text("OpenAccess ORM");
					item.Add().Text("Reporting");
				});

			menu.Add().Text("TFS Tools")
				.Items(item =>
				{
					item.Add().Text("Work Item Manager");
					item.Add().Text("Project Dashboard");
				});

			menu.Add().Text("Automated Testing")
				.Items(item =>
				{
					item.Add().Text("Web Testing Tools");
				});

			menu.Add().Text("ASP.NET CMS")
				.Items(item =>
				{
					item.Add().Text("Sitefinity CMS");
				});
		})
%>
	
<% using (Html.Configurator("Client API").Begin()) { %>
    <p>
        <label for="itemIndex">Item index:</label>
        <%= Html.TextBox("itemIndex", "0", new { style = "width: 40px" })%>
    </p>
    
    <p>
        <button onclick="Open()">Open</button> / <button onclick="Close()">Close</button>
    </p>
    
    <p>
        <button onclick="Enable()">Enable</button> / <button onclick="Disable()">Disable</button>
    </p>
<% } %>
        
	<script type="text/javascript">
		
	    function Open() {
	        var menu = $("#Menu").data("tMenu");
	        var item = $("> li", menu.element)[$("#itemIndex").val()];

	        menu.open(item);
	    }

	    function Close() {
	        var menu = $("#Menu").data("tMenu");
	        var item = $("> li", menu.element)[$("#itemIndex").val()];

	        menu.close(item);
	    }

	    function Enable() {
	        var menu = $("#Menu").data("tMenu");
	        var item = $("> li", menu.element)[$("#itemIndex").val()];

	        menu.enable(item);
	    }

	    function Disable() {
	        var menu = $("#Menu").data("tMenu");
	        var item = $("> li", menu.element)[$("#itemIndex").val()];

	        menu.disable(item);
	    }
	</script>
			
</asp:content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
	    .configurator p
	    {
	        margin: 0;
	        padding: .4em 0;
	    }
    </style>
</asp:Content>