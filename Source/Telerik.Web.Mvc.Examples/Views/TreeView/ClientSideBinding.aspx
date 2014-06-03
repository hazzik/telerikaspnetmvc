<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentplaceholderid="MainContent" runat="server">
	
    <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .ClientEvents(events => events
                .OnDataBinding("onDataBinding")
            )				
    %>

	<script type="text/javascript">
	    function onDataBinding(e) {
	        var treeview = $('#TreeView').data('tTreeView');

	        var jsonObject = [
	            { Value: "1", Text: "Product 1", Expanded: true,
	                Items: [
	                  { Value: "4", Text: "Product 4", Expanded: true,
	                      Items: [
	                        { Value: "6", Text: "Product 6" },
	                        { Value: "7", Text: "Product 7" }
	                    ]
	                  },
	                  { Value: "5", Text: "Product 5" }
	              ]
	            },
	            { Value: "2", Text: "Product 2 (unavailable)", Enabled: false },
	            { Value: "3", Text: "Product 3" }
	        ];

	        treeview.bindTo(jsonObject);
	    }
	</script>
</asp:content>
