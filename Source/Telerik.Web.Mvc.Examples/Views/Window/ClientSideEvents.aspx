	<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
	<asp:content contentPlaceHolderID="MainContent" runat="server">
		
	<script type="text/javascript">
        function onLoad(e) {
            $console.log('Window loaded');
        }

        function onClose(e) {
            $console.log('OnClose');
        }

        function onOpen(e) {
            $console.log('OnOpen');
        }

        function onRefresh(e) {
	        $console.log('OnRefresh');
        }
	</script>
		
		<span id="undo">You closed the window.<br />If you want, click here to open it again.</span>
		
		<%= Html.Telerik().Window()
            .Name("Window")
            .ClientEvents(
              events =>
                events.OnLoad("onLoad")
                      .OnClose("onClose")
                      .OnOpen("onOpen")
                      .OnRefresh("onRefresh"))
            .LoadContentFrom("AjaxView", "Window")
            .Buttons(buttons => buttons.Refresh().Close())
		    .Height(180)
		    .Width(300)      
		    .Resizable()      
		%>
		
		<% Html.RenderPartial("EventLog"); %>
		
        <% Html.Telerik().ScriptRegistrar()
               .OnDocumentReady(() => { %>
                    $('#undo')
                        .bind('click', function openWindow(e) {
                            $('#Window').data('tWindow').open();
                            $(this).hide();
                        })
                        .toggle(!$('#Window').is(':visible'));
                        
                    $('#Window').bind('close', function() {
                        $('#undo').show();
                    });
               <%
               });
         %>
         
         <script type="text/javascript">
	        function maximizeWindow() { $("#Window").data("tWindow").maximize(); }
	        function restoreWindow() { $("#Window").data("tWindow").restore(); }
         </script>
		
	</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            float: right;
            display: inline;
            width: 468px;
            margin: 0 0 1.3em 10em;
        }
        
        #undo
        {
            text-align: center;
            position: absolute;
            white-space: nowrap;
            border: 1px solid #ccc;
            padding: 2em;
            background: #f1f1f1;
            cursor: pointer;
        }
    </style>
</asp:content>
