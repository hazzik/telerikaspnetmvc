<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="ExampleTitle" runat="server">Client-side Events</asp:content>

<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
		function onSelect(e) {
			var item = $(e.target);
			$console.log('onSelect :: ' + item.text());
		}
		
		function onClose(e) {
			var item = $(e.target);
			$console.log('onClose :: ' + item.text());
		}
		function onOpen(e) {
			var item = $(e.target);
			$console.log('onOpen :: ' + item.text());
		}
	</script>
	
		<% Html.Telerik().Menu()
                .Name("Menu")
                .HtmlAttributes(new { style = "margin-bottom: 80px;" })
				.ClientEvents(events =>
					events
						.OnLoad(() => {%>
							function(e) {
								$console.log('Menu loaded');
							}
						<%})
						.OnSelect("onSelect")
                        .OnOpen("onOpen")
                        .OnClose("onClose")
				)
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
				.Render(); %>
 
    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>