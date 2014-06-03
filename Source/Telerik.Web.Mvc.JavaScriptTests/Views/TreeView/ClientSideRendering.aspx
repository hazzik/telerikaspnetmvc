<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ClientSideRendering</h2>

    <% Html.Telerik().ScriptRegistrar()
                        .DefaultGroup(group => group
                            .Add("telerik.common.js")
                            .Add("telerik.treeview.js"));

    %>

    <script type="text/javascript">

        var treeviewStatic;

        function setUp() {
            treeviewStatic = $.telerik.treeview;
        }

//        $t.treeview.getGroupHtml({
//            data: data,
//            html: groupHtml,
//            isAjax: this.isAjax(),
//            isFirstLevel: $item.hasClass('t-treeview'),
//            showCheckBoxes: this.showCheckBox,
//            groupLevel: $index.val(),
//            isExpanded: isExpanded,
//            renderGroup: isGroup,
//            elementId: this.element.id
//        });

        function test_getGroupHtml_with_no_data_renders_empty_group() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getGroupHtml({
                data: [],
                html: html,
                isAjax: null,
                isFirstLevel: false,
                showCheckBoxes: false,
                isExpanded: true
            });

            assertEquals('<ul class="t-group"></ul>', html.string());
        }

        function test_getGroupHtml_for_first_level_renders_lines_class() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getGroupHtml({ 
                data: [],
                html: html,
                isAjax: null,
                isFirstLevel: true,
                showCheckBoxes: false,
                isExpanded: true
            });

            assertEquals('<ul class="t-group t-treeview-lines"></ul>', html.string());
        }

        function test_getGroupHtml_should_render_hidden_group_if_it_is_not_expanded() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getGroupHtml({
                data: [],
                html: html,
                isAjax: null,
                isFirstLevel: false,
                showCheckBoxes: false,
                isExpanded: false
            });

            assertEquals('<ul class="t-group" style="display:none"></ul>', html.string());
        }

        function test_getGroupHtml_calls_getItemHtml_when_data_is_available() {
            var oldGetItemHtml = treeviewStatic.getItemHtml;

            try {
                var html = new $.telerik.stringBuilder();

                var called = false;

                treeviewStatic.getItemHtml = function() { called = true; }

                treeviewStatic.getGroupHtml({
                    data: ["1"],
                    html: html,
                    isAjax: null,
                    isFirstLevel: false
                });

                assertTrue(called);

            } finally {
                treeviewStatic.getItemHtml = oldGetItemHtml;
            }
        }

        function test_getGroupHtml_calls_getItemHtml_for_each_data_item() {
            var oldGetItemHtml = treeviewStatic.getItemHtml;
            
            try {
                var html = new $.telerik.stringBuilder();

                var calls = 0;

                treeviewStatic.getItemHtml = function() { calls++; }
                
                treeviewStatic.getGroupHtml({
                    data: [{}, {}, {}],
                    html: html,
                    isAjax: null,
                    isFirstLevel: true
                });

                assertEquals(3, calls);

            } finally {
                treeviewStatic.getItemHtml = oldGetItemHtml;
            }
        }

//        getItemHtml({
//            item: data[i],
//            html: html,
//            isAjax: options.isAjax,
//            isFirstLevel: isFirstLevel,
//            showCheckBoxes: options.showCheckBoxes,
//            groupLevel: options.groupLevel,
//            itemIndex: options.i,
//            itemsCount: options.len,
//            elementId: options.elementId
//        });

        function test_getItemHtml_renders_simple_items() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: { Text: "text" },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: false,
                itemIndex: 0,
                itemsCount: 1
            });

            assertEquals('<li class="t-item t-last"><div class="t-bot"><span class="t-in">text</span></div></li>', html.string());
        }

        function test_getItemHtml_renders_links_for_items_with_NavigateUrl() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    NavigateUrl: "http://google.com/"
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: false,
                itemIndex: 0,
                itemsCount: 1
            });

            assertEquals('<li class="t-item t-last"><div class="t-bot"><a href="http://google.com/" class="t-link t-in">text</a></div></li>', html.string());
        }

        function test_getItemHtml_renders_disabled_state_on_disabled_items() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Enabled: false
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: false,
                itemIndex: 0,
                itemsCount: 1
            });

            assertEquals('<li class="t-item t-last"><div class="t-bot"><span class="t-in t-state-disabled">text</span></div></li>', html.string());
        }

        function test_getItemHtml_renders_selected_state_on_selected_items() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Selected: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: false,
                itemIndex: 0,
                itemsCount: 1
            });

            assertEquals('<li class="t-item t-last"><div class="t-bot"><span class="t-in t-state-selected">text</span></div></li>', html.string());
        }

        function test_getItemHtml_renders_collapsed_items_by_default() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "Collapsed",
                    Items: [
                        { Text: "Should not be visible" }
                    ]
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: false,
                itemIndex: 0,
                itemsCount: 1
            });
            

            assertEquals(
'<li class="t-item t-last">\
<div class="t-bot">\
<span class="t-icon t-plus"></span><span class="t-in">Collapsed</span>\
</div>\
<ul class="t-group" style="display:none">\
<li class="t-item t-last"><div class="t-bot"><span class="t-in">Should not be visible</span></div></li>\
</ul>\
</li>', html.string());
        }

        function test_getItemHtml_should_render_span_wrapper_for_the_checkNodes() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1
            });

            var $item = $(html.string());

            assertEquals('Does not render checkbox group wrapper', 1, $item.find('> div > .t-checkbox').length);
        }

        function test_getItemHtml_should_render_checkbox_and_hidden_input() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1
            });

            var $item = $(html.string());

            assertEquals(1, $item.find('.t-input[type="hidden"]').length);
            assertEquals(1, $item.find('.t-input[type="checkbox"]').length);
        }

        function test_getItemHtml_should_render_hidden_input_with_name_attr_in_checkbox_wrapper() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1"
            });

            var $item = $(html.string());

            assertEquals("TreeView1_checkedNodes.Index", $item.find('.t-input[type="hidden"]').attr('name'));
        }

        function test_getItemHtml_should_render_hidden_input_in_checkbox_wrapper_with_value_0_if_it_is_first_level() {
            var html = new $.telerik.stringBuilder();
            
            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: ""
            });

            var $item = $(html.string());

            assertEquals("0", $item.find('.t-input[type="hidden"]').attr('value'));
        }

        function test_getItemHtml_should_render_hidden_input_in_checkbox_wrapper_with_value_created_by_group_level() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 2,
                itemsCount: 3,
                elementId: "TreeView1",
                groupLevel: "1:0"
            });

            var $item = $(html.string());

            assertEquals("1:0:2", $item.find('.t-input[type="hidden"]').attr('value'));
        }

        function test_getItemHtml_should_render_checkbox_input_with_name_attr() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: "0"
            });

            var $item = $(html.string());

            assertEquals("TreeView1_checkedNodes[0:0].Checked", $item.find('.t-input[type="checkbox"]').attr('name'));
        }

        function test_getItemHtml_should_render_checkbox_input_with_value_true_if_item_is_checked() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true,
                    Checked: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: "0"
            });

            var $item = $(html.string());

            assertEquals("True", $item.find('.t-input[type="checkbox"]').attr('value'));
        }

        function test_getItemHtml_should_render_disabled_checkbox_with_disabled_state_class() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true,
                    Checked: true,
                    Enabled: false
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: "0"
            });

            var $item = $(html.string());

            assertEquals(true, $item.find('.t-input[type="checkbox"]').attr('disabled'));
        }

        function test_getItemHtml_should_render_checked_checkbox_Checked_property_is_true()
        {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Checkable: true,
                    Checked: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: "0"
            });

            var $item = $(html.string());

            assertEquals(true, $item.find('[type=checkbox]').attr('checked'));
        }

        function test_getItemHtml_should_render_hidden_inputs_containing_dataItem_values_if_item_is_checked() {
            var html = new $.telerik.stringBuilder();

            treeviewStatic.getItemHtml({
                item: {
                    Text: "text",
                    Value: "1",
                    Checkable: true,
                    Checked: true
                },
                html: html,
                isAjax: false,
                isFirstLevel: false,
                showCheckBoxes: true,
                itemIndex: 0,
                itemsCount: 1,
                elementId: "TreeView1",
                groupLevel: "0"
            });

            var $item = $(html.string());

            assertEquals("1", $item.find('[type=hidden]').eq(1).val());
            assertEquals("text", $item.find('[type=hidden]').eq(2).val());
        }

    </script>
</asp:Content>
