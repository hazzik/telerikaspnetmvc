	<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
	<asp:content contentPlaceHolderID="MainContent" runat="server">

		<%= Html.Telerik().Window()
            .Name("Window")
            .LoadContentFrom("AjaxView", "Window")
            .Buttons(buttons => buttons.Refresh().Maximize().Close())
		    .Width(450)     
            .Draggable(true)
		%>
		
		<% using (Html.Configurator("Client API").Begin()) { %>
            
            <p>
                <button onclick="openWindow()">Open</button> / <button onclick="closeWindow()">Close</button>
            </p>
            
            <p>
                <button onclick="refreshWindow()">Refresh</button>
            </p>
        <% } %>
		
		<script type="text/javascript">

		    function openWindow() {
		        var window = $("#Window").data("tWindow");

	            window.open();
	        }

	        function closeWindow() {
	            var window = $("#Window").data("tWindow");

	            window.close();
	        }

	        function refreshWindow() {
	            var window = $("#Window").data("tWindow");

	            window.refresh();
	        }

	        function maximizeWindow() {
	            var window = $("#Window").data("tWindow");

	            window.maximize();
	        }


	        function restoreWindow() {
	            var window = $("#Window").data("tWindow");

	            window.restore();
	        }

            function centerWindow() {
	            var window = $("#Window").data("tWindow");

	            window.center();
            }
	    </script>
		
	</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .example .configurator
        {
            width: 300px;
            margin-bottom: 5em;
            float: right;
            display: inline;
        }
        
        .configurator p
        {
            margin: 0;
            padding: .4em 0;
        }
    </style>
</asp:content>
