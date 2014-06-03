<%@ Page Title="Load on Demand tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Load on Demand tests</h2>

    <% Html.Telerik().TreeView()
            .Name("myTreeView")
            .Items(treeview =>
            {
                treeview.Add().Text("Item 1")
                    .Items(item1 => {
                        item1.Add().Text("Item 1.1");
                    })
                    .Value("1")
                    .LoadOnDemand(true)
                    .Expanded(false);
                
                treeview.Add().Text("Item 2")
                    .Value("2")
                    .LoadOnDemand(true)
                    .Expanded(false);

                treeview.Add().Text("Item 3")
                    .Value("3")
                    .LoadOnDemand(true)
                    .Expanded(false);
                
                treeview.Add().Text("Item 4")
                    .Value("4")
                    .LoadOnDemand(true)
                    .Expanded(false);


                treeview.Add().Text("Item 5")
                    .Value("5")
                    .LoadOnDemand(true)
                    .Expanded(false);
            }
            )
            .DataBinding(settings => settings.Ajax().Select("LoadOnDemand", "TreeView"))
            .Effects(fx => fx.Toggle())
            .Render(); %>

    <% Html.Telerik().TreeView()
            .Name("myTreeView_ajaxRequest")
            .DataBinding(settings => settings.Ajax().Select("LoadOnDemand", "TreeView"))
            .Items(treeview =>
            {
                treeview.Add().Text("Item X")
                    .Value("5")
                    .LoadOnDemand(true)
                    .Expanded(false);
            }
            )
            .Effects(fx => fx.Toggle())
            .Render(); %>

    <script type="text/javascript">
        var treeView;
        
        function getTreeView(selector) {
            return $(selector || "#myTreeView").data("tTreeView");
        }
        
        function setUp() {
            treeView = getTreeView();
        }
        
        function test_opening_ajaxified_nodes_with_server_side_rendered_children_does_not_trigger_ajax_request() {
            var oldAjaxRequest = treeView.ajaxRequest;
            try {
                var called = false;

                treeView.ajaxRequest = function() { called = true; }

                treeView.expand('.t-item:eq(0)');
                
                assertFalse(called);
            } finally {
                treeView.ajaxRequest = oldAjaxRequest;
            }
        }
        
        function test_opening_ajaxified_nodes_with_dynamic_children_triggers_ajax_request() {
            var oldAjaxRequest = treeView.ajaxRequest;
            try {
                var called = false;

                treeView.ajaxRequest = function() { called = true; }

                var item = $('.t-item:contains(Item 2)');

                item.append('<ul style="display: none;" class="t-group"><li class="t-item t-last"><div class="t-top t-bot"><span class="t-in">Item 2.1</span></div></li></ul>')

                treeView.nodeToggle(null, item, true);
                
                assertTrue(called);
            } finally {
                treeView.ajaxRequest = oldAjaxRequest;
            }
        }
        
        function test_opening_ajaxified_nodes_with_dynamic_children_inserts_requested_children_at_top() {
            var oldAjaxRequest = treeView.ajaxRequest;
            try {
                treeView.ajaxRequest = function($item) { treeView.dataBind($item, [{ Text: 'NewNode' }]); }

                var item = $('.t-item:contains(Item 3)');

                item.append('<ul style="display: none;" class="t-group"><li class="t-item t-last"><div class="t-top t-bot"><span class="t-in">Item 3.1</span></div></li></ul>')

                treeView.nodeToggle(null, item, true);

                assertEquals(1, item.find('> .t-group').length);
                assertEquals(2, item.find('> .t-group').children().length);
                assertEquals('NewNode', item.find('> .t-group .t-item:first > div').text());
            } finally {
                treeView.ajaxRequest = oldAjaxRequest;
            }
        }
                
        function test_expanding_ajaxified_nodes_appends_group() {
            var oldAjaxRequest = treeView.ajaxRequest;
            try {
                treeView.ajaxRequest = function($item) { treeView.dataBind($item, [{ Text: 'NewNode' }]); }

                var item = $('.t-item:contains(Item 4)');

                treeView.nodeToggle(null, item, true);

                assertEquals(1, item.find('> .t-group').length);
                assertEquals(1, item.find('> .t-group').children().length);
                assertEquals('NewNode', item.find('> .t-group .t-item:first > div').text());
            } finally {
                treeView.ajaxRequest = oldAjaxRequest;
            }
        }
                
        function test_collapsing_expanded_ajaxified_nodes_hides_group() {
            var oldAjaxRequest = treeView.ajaxRequest;
            try {
                treeView.ajaxRequest = function($item) { treeView.dataBind($item, [{ Text: 'NewNode' }]); }

                var item = $('.t-item:contains(Item 5)');

                treeView.nodeToggle(null, item, true);

                treeView.nodeToggle(null, item, true);

                assertEquals(false, item.find('> .t-group').is(':visible'));
            } finally {
                treeView.ajaxRequest = oldAjaxRequest;
            }
        }
                
        function test_ajaxRequest_rebinds_treeview() {
            var treeview = getTreeView('#myTreeView_ajaxRequest');

            var $root = $(treeview.element);
            
            treeview.dataBind($root, [{ Text: 'NewNode' }]);
            treeview.dataBind($root, [{ Text: 'NewNode' }]);

            var group = $root.find('> .t-group');

            assertEquals(1, group.children().length);
            assertEquals(true, group.is(':visible'));
            assertEquals(1, $root.children().length);
        }
    </script>

</asp:Content>
