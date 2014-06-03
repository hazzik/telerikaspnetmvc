<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="ExampleTitle" runat="server">Client-side Events</asp:content>

<asp:content contentPlaceHolderID="MainContent" runat="server">
        
	<script type="text/javascript">
		function onSelect(e) {
			var item = $(e.target);
			$console.log('onSelect :: ' + item.text());
		}
		
		function onCollapse(e) {
			var item = $(e.target);
			$console.log('onCollapse :: ' + item.text());
		}
		function onExpand(e) {
			var item = $(e.target);
			$console.log('onExpand :: ' + item.text());
		}
	</script>
	
		<% Html.Telerik().TabStrip()
                .Name("TabStrip")
				.ClientEvents(events =>
					events
						.OnLoad(() => {%>
							function(e) {
								$console.log('TabStrip loaded');
							}
						<%})
						.OnSelect("onSelect")
				)
				.Items(tabstrip =>
				{
					tabstrip.Add().Text("UI Components");
					tabstrip.Add().Text("Data");
					tabstrip.Add().Text("TFS Tools");
					tabstrip.Add().Text("Automated Testing");
					tabstrip.Add().Text("ASP.NET CMS");
				})
				.Render(); %>
 
    <% Html.RenderPartial("EventLog"); %>
			
</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            margin-top: 3em;
        }
    </style>
</asp:content>