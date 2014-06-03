<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    
        <%= Html.Telerik().TreeView()
                .Name("TreeView")
                .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
                .ShowCheckBox(true)      
                .ClientEvents(events => events
                        .OnLoad("onLoad")
                        .OnSelect("onSelect")
                        .OnCollapse("onCollapse")
                        .OnExpand("onExpand")
                        .OnChecked("onChecked")
                        
                        // drag & drop related
                        .OnNodeDragStart("onNodeDragStart")
                        .OnNodeDragging("onNodeDragging")
                        .OnNodeDrop("onNodeDrop")
                        .OnNodeDropped("onNodeDropped")
                        .OnNodeDragCancelled("onNodeDragCancelled")
                )
                .DragAndDrop(true)      
                .Items(treeView =>
                {
                    treeView.Add().Text("UI Components")
                        .Items(item =>
                        {
                            item.Add().Text("ASP.NET WebForms");
                            item.Add().Text("Silverlight");
                            item.Add().Text("ASP.NET MVC");
                            item.Add().Text("WinForms");
                            item.Add().Text("WPF");
                        })
                        .Expanded(true);

                    treeView.Add().Text("Data")
                        .Items(item =>
                        {
                            item.Add().Text("OpenAccess ORM");
                            item.Add().Text("Reporting");
                        });

                    treeView.Add().Text("TFS Tools")
                        .Items(item =>
                        {
                            item.Add().Text("Work Item Manager");
                            item.Add().Text("Project Dashboard");
                        });

                    treeView.Add().Text("Automated Testing")
                        .Items(item =>
                        {
                            item.Add().Text("Web Testing Tools");
                        });

                    treeView.Add().Text("ASP.NET CMS")
                        .Items(item =>
                        {
                            item.Add().Text("Sitefinity CMS");
                        });
                })
      %>
        
    <script type="text/javascript">
        function treeView() {
            return $('#TreeView').data('tTreeView');
        }    
    
        function onSelect(e) {
            $console.log('OnSelect :: ' + treeView().getItemText(e.item));
        }
        
        function onCollapse(e) {
            $console.log('OnCollapse :: ' + treeView().getItemText(e.item));
        }

        function onExpand(e) {
            $console.log('OnExpand :: ' + treeView().getItemText(e.item));
        }

        function onNodeDragStart(e) {
            $console.log('OnNodeDragStart :: ' + treeView().getItemText(e.item));
        }
        
        function onNodeDragging(e) {
            // no logging - too verbose
        }

        function onNodeDragCancelled(e) {
            $console.log('OnNodeDragCancelled :: ' + treeView().getItemText(e.item));
        }

        function onNodeDrop(e) {
            $console.log('OnNodeDrop :: ' + treeView().getItemText(e.item) + " " + (e.isValid ? "(valid)" : "(not valid)"));
        }

        function onNodeDropped(e) {
            $console.log('OnNodeDropped :: `' + treeView().getItemText(e.item) + '` '
                                        + e.dropPosition +
                                        ' `' + treeView().getItemText(e.destinationItem) + '`');
        }

        function onChecked(e) {
            $console.log('OnChecked :: ' + treeView().getItemText(e.item) +
                         ' (' + (e.checked ? 'checked' : 'unchecked') + ')');
        }

        function onLoad(e) {
            $console.log('TreeView loaded');
        }
        
    </script>
 
    <% Html.RenderPartial("EventLog"); %>
            
</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .event-log-wrap
        {
            float: left;
            display: inline;
            width: 468px;
            margin-left: 10em;
        }
    </style>
</asp:content>
