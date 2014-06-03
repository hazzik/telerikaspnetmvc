<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ClientEvents tests</h2>
    
    <%= Html.Telerik().TreeView()
            .Name("ClientSideTreeView")
            .ClientEvents(ce => 
                            ce.OnDataBinding("onDataBinding_ClientSideTreeView")
                              .OnSelect("onSelect")
                              .OnLoad("onLoad"))
            .Effects(fx => fx.Toggle()) %>

    <%= Html.Telerik().TreeView()
            .Name("DisabledTreeView")
            .Items(treeview =>
            {
                treeview.Add().Text("Item 1")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 1.1");
                        item.Add().Text("Child Item 1.2");
                    })
                    .Enabled(false);
            })
            .Effects(fx => fx.Toggle()) %>

    <%= Html.Telerik().TreeView()
            .Name("ContentTreeView")
            .Items(treeview =>
            {
                treeview.Add().Text("InputContent")
                        .Content("<input type='text' value='asdf'/>");
            })
            .ClientEvents(ce => ce.OnSelect("onSelectInput"))
            .Effects(fx => fx.Toggle()) %>

    <script type="text/javascript">
        var onLoadTreeView;
        
        function onLoad() {
            onLoadTreeView = $(this).data('tTreeView');
        }

        var isRaised;
        var selectedItem;
        
        function test_client_object_is_available_in_on_load() {
            assertNotNull(onLoadTreeView);
            assertNotUndefined(onLoadTreeView);
        }

        function onDataBinding_ClientSideTreeView(e) {
            var treeview = $('#ClientSideTreeView').data('tTreeView');
            var jsonObject;

            if (e.item == treeview.element) {
                jsonObject = [
                    { Text: "LoadOnDemand", LoadOnDemand: true, Value: "abyss" }
                ];

                    treeview.bindTo(jsonObject);

                    selectedItem = e.item;
            }
        }

        function onSelect(e) {
            selectedItem = null;
            selectedItem = e.item;
        }

        function onSelectInput(e) {
            isRaised = true;
        }

        function test_clicking_item_should_raise_onselect_event_return_selected_item() {
            
            var treeview = $("#ClientSideTreeView").data("tTreeView");

            var nodeToClick = $(treeview.element).find('.t-item');

            nodeToClick.find('.t-in').click();

            assertTrue($(selectedItem).hasClass('t-item'));
        }

        function test_trigger_input_select_should_not_bubble() {
            
            isRaised = false;

            var treeview = $("#ContentTreeView").data("tTreeView");

            var $input = $(treeview.element).find('.t-item').find('input').last();

            $input.trigger('select');

            assertFalse(isRaised);
        }
        
        function test_clicking_load_on_demand_nodes_triggers_databinding_event() {
            var treeview = $("#ClientSideTreeView").data("tTreeView"),
                node = $(treeview.element).find('.t-item'),
                hasCalledDataBinding = false,
                eventContainsItem = false;
        
            $(treeview.element).bind('dataBinding', function(e) {
                hasCalledDataBinding = true;
                eventContainsItem = e.item == node[0];
            });
            
            treeview.nodeToggle(null, node, true);
            
            assertTrue("DataBinding event should be fired when elements with LoadOnDemand are clicked.", hasCalledDataBinding);
            assertTrue("DataBinding event should contain item in event data", eventContainsItem);
        }

        function getItemHtml(item) {
            var html = new $.telerik.stringBuilder();
                          
            $.telerik.treeview.getItemHtml({
                item: item,
                html: html,
                isFirstLevel: false,
                groupLevel: 0,
                itemIndex: 0,
                itemsCount: 1
            });

            return html.string();
        }

        function test_reload_method_should_remove_items_group() {
            var treeview = $("#ClientSideTreeView").data("tTreeView"),
                $item = $(getItemHtml({ Text: 'Steven Buchanan', Items: [{ Text: 'Michael Suyama' }] }));

            treeview.reload($item);

            assertEquals(0, $item.find('.t-group').length);
        }

        function test_reload_method_should_call_ajaxRequest_method() {
            var treeview = $("#ClientSideTreeView").data("tTreeView"),
                oldAjaxRequest = treeview.ajaxRequest,
                isCalled = false;

            try {
                treeview.ajaxRequest = function () { isCalled = true; };
            
                treeview.reload($('<li></li>'));

                assertTrue(isCalled);

            } finally {
                treeview.ajaxRequest = oldAjaxRequest;
            }
        }

        function test_clicking_disabled_items_does_not_trigger_select_event() {
            var treeviewElement = $("#DisabledTreeView"),
                isCalled = false;
                
            try {
                treeviewElement.bind('select', function(e) { isCalled = true; });

                treeviewElement.find('.t-in.t-state-disabled').trigger('click');

                assertFalse(isCalled);
            } finally {
                treeviewElement.unbind('select');
            }
        }

        function test_expanding_load_on_demand_nodes_triggers_expand_event() {
            var treeviewElement = $("#ClientSideTreeView"),
                treeview = treeviewElement.data("tTreeView"),
                node = treeviewElement.find('.t-item:first'),
                expandTrigggered = false,
                eventContainsItem = false;
        
            try {
                treeviewElement.bind('expand', function(e) {
                    expandTrigggered = true;
                    eventContainsItem = e.item == node[0];
                });
            
                treeview.nodeToggle(null, node, true);
            
                assertTrue("Expand event should be fired when elements with LoadOnDemand are expanded.", expandTrigggered);
                assertTrue("Expand event should contain item in event data", eventContainsItem);
            } finally {
                treeviewElement.unbind('expand');
            }
        }
        
    </script>

</asp:Content>
