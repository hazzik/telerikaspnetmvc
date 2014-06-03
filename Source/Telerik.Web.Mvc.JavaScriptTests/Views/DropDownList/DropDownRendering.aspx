<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>DropDown Rendering</h2>

    <script type="text/javascript">
        function dataBinding() { }

        function getDropDownList() {
            return $('#DropDownList').data('tDropDownList');
        }

        function test_DDL_should_tabindex_0_applied_on_client() {
            assertTrue(getDropDownList().element.tabIndex == 0);
        }

        function test_on_initialize_DDL_should_render_hidden_input_with_id() {
            var ddl = getDropDownList();
            var id = ddl.$element.attr('id');
            var input = ddl.$element.find('input');
            

            assertTrue(input.length == 1);
            assertTrue(input[0].type == 'text');
            assertTrue(input.attr('id') == id + '-value');
        }

        function test_open_method_should_open_dropDown_list() {
            var ddl = getDropDownList();
            ddl.effects = ddl.dropDown.effects = $.telerik.fx.toggle.defaults();
            ddl.close();
            ddl.open();

            assertTrue(ddl.dropDown.isOpened());
        }

        function test_open_method_reposition_dropDown_list() {
            var ddl = getDropDownList();
            ddl.effects = $.telerik.fx.toggle.defaults();

            ddl.close();
            ddl.open();

            var animatedContainer = ddl.dropDown.$element.parent();

            var elementPosition = ddl.$element.offset();

            elementPosition.top += ddl.$element.outerHeight();

            assertTrue(animatedContainer.css('position') == 'absolute');
            assertTrue(animatedContainer.css('top') == Math.round(elementPosition.top * 1000) / 1000 + 'px');
            assertTrue(animatedContainer.css('left') == Math.round(elementPosition.left * 1000) / 1000 + 'px');
        }

        function test_open_method_should_append_dropdown_list_to_body() {

            var ddl = getDropDownList();
            ddl.effects = $.telerik.fx.toggle.defaults();

            ddl.close();
            ddl.open();

            assertTrue($.contains(document.body, ddl.dropDown.$element[0]));
        }

        function test_close_method_should_remove_dropdown_list_to_body() {

            var ddl = getDropDownList();
            ddl.effects = $.telerik.fx.toggle.defaults();

            ddl.open();
            ddl.close();

            assertTrue(!$.contains(document.body, ddl.dropDown.$element[0]));
        }

        function test_click_item_in_items_list_when_it_is_shown_should_call_select_method() {

            var isSelectCalled = false;
            
            var ddl = getDropDownList();
            ddl.effects = $.telerik.fx.toggle.defaults();

            var old = ddl.select;
            ddl.select = function () { isSelectCalled = true; }

            ddl.open();

            $(ddl.dropDown.$element.find('li')[2]).click();

            assertTrue(isSelectCalled);

            ddl.select = old;
        }

        function test_select_should_select_one_item_only() {
            
            var ddl = getDropDownList();

            var li = ddl.dropDown.$element.find('li')[3];

            ddl.select(li);

            assertTrue(ddl.dropDown.$element.find('.t-state-selected').length == 1);
            assertTrue($(li).hasClass('t-state-selected'));
        }

        function test_select_should_cache_selected_item() {
            var ddl = getDropDownList();
            
            ddl.selectedIndex = -1;

            var li = ddl.dropDown.$element.find('li')[3];

            ddl.select(li);

            assertEquals(3, ddl.selectedIndex);
        }

        function test_highlight_should_highlight_only_one_item_by_index() {
            var ddl = getDropDownList();

            ddl.highlight(2);

            assertEquals(1, ddl.dropDown.$element.find('.t-state-selected').length);
        }

        function test_highlight_should_highlight_fourth_item_by_index() {
            var ddl = getDropDownList();

            ddl.highlight(3);

            assertEquals(3, ddl.dropDown.$element.find('.t-state-selected').first().index());
        }

        function test_highlight_should_preserve_previous_value_after_rebind() {
            var ddl = getDropDownList();
           
            ddl.highlight(4);
            
            var value = ddl.previousValue;
            
            ddl.highlight(2);

            assertEquals(value, ddl.previousValue);
        }

        function test_hightlight_method_should_return_negative_if_no_such_index() {
            var ddl = getDropDownList();
            
            var result = ddl.highlight(3000);

            assertEquals(-1, result);
        }

        function test_hightlight_method_should_call_close_and_dataBind_if_correct_index_is_passed() {
            var ddl = getDropDownList();

            var isCloseCalled = false;
            var isDataBindCalled = false;

            var close = ddl.close;
            var dataBind = ddl.dropDown.dataBind;

            ddl.close = function () { isCloseCalled = true; };
            ddl.dropDown.dataBind = function () { isDataBindCalled = true; };

            ddl.highlight(2);

            assertTrue(isCloseCalled);
            assertTrue(isDataBindCalled);

            ddl.close = close;
            ddl.dropDown.dataBind = dataBind;
        }

        function test_highlight_should_higlight_item_found_by_predicate() {
            var ddl = getDropDownList();
            ddl.fill();
            
            ddl.highlight(function (dataItem) {
                return dataItem.Value == 2;
            });

            assertEquals(1, ddl.dropDown.$element.find('.t-state-selected').first().index());
        }

        function test_hightlight_method_should_return_negative_if_no_such_item() {
            var ddl = getDropDownList();
            ddl.fill();
            
            var result = ddl.highlight(function (dataItem) {
                return dataItem.Value == 1000;
            });

            assertEquals(-1, result);
        }

        function test_hightlight_method_should_call_close_and_dataBind_if_item_is_found_by_predicate() {
            var ddl = getDropDownList();
            ddl.fill();

            var isCloseCalled = false;
            var isDataBindCalled = false;

            var close = ddl.close;
            var dataBind = ddl.dropDown.dataBind;

            ddl.close = function () { isCloseCalled = true; };
            ddl.dropDown.dataBind = function () { isDataBindCalled = true; };

            ddl.highlight(function (dataItem) {
                return dataItem.Value == 2;
            });

            assertTrue(isCloseCalled);
            assertTrue(isDataBindCalled);

            ddl.close = close;
            ddl.dropDown.dataBind = dataBind;
        }

        function test_highlight_method_should_return_negative_index_if_data_is_undefined() {

            var ddl = $('#DropDownList2').data('tDropDownList');
            
            var index = ddl.highlight(0);

            assertEquals(-1, index);
        }
        
        function test_text_method_should_set_html_of_text_span() {

            var item = { "Selected": false, "Text": "Item2", "Value": "2" };

            var ddl = getDropDownList();

            ddl.text("Item2");

            assertEquals(item.Text, ddl.$text.html());
        }

        function test_text_method_should_return_innerHtml_if_no_parameters() {
            
            var item = { "Selected": false, "Text": "Item2", "Value": "2" };
            
            var ddl = getDropDownList();

            ddl.text("Item2");

            var result = ddl.text();

            assertEquals(item.Text, result);
        }

        function test_value_method_should_call_select_method_and_set_previousSelected_value_if_such_item() {
            var item = { "Selected": false, "Text": "Item2", "Value": "2" };
            var isCalled = false;

            var ddl = getDropDownList();
            var select = ddl.select;
            ddl.previousValue = null;
            ddl.select = function () { isCalled = true; return 2; }
            
            ddl.value(2);

            assertEquals(true, isCalled);
            assertEquals('previous value was not updated', '2', ddl.previousValue.toString());

            ddl.select = select;
        }

        function test_value_method_should_return_value_hidden_input() {
            var item = { "Selected": false, "Text": "Item2", "Value": "2" };

            var ddl = getDropDownList();

            ddl.value(2);

            assertEquals(ddl.value(), item.Value);
        }

        function test_keyPress_should_find_element_т() {
            var ddl = getDropDownList();
            
            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keypress", keyCode: 1058 });

            assertTrue($(ddl.dropDown.$element.find('.t-state-selected')[0]).text() == "тtem20");
        }

        function test_pressing_right_arrow_should_select_next_item() {
            var ddl = getDropDownList();

            var $initialSelectedItem = $(ddl.dropDown.$element.find('.t-item')[0]);
            ddl.select($initialSelectedItem);

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 39 });

            var $nextSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertFalse($initialSelectedItem.text() == $nextSelectedItem.text());
        }


        function test_pressing_down_arrow_should_select_next_item() {
            var ddl = getDropDownList();

            var $initialSelectedItem = $(ddl.dropDown.$element.find('.t-item')[0]);
            ddl.select($initialSelectedItem);

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 40 });

            var $nextSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertFalse($initialSelectedItem.text() == $nextSelectedItem.text());
        }

        function test_pressing_up_arrow_should_select_prev_item() {
            var ddl = getDropDownList();

            ddl.select(ddl.dropDown.$element.find('li')[1]);

            var $initialSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 38 });

            var $prevSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertFalse($initialSelectedItem.text() == $prevSelectedItem.text());
        }

        function test_pressing_left_arrow_should_select_prev_item() {
            var ddl = getDropDownList();

            ddl.select(ddl.dropDown.$element.find('li')[1]);

            var $initialSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 37 });

            var $prevSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertFalse($initialSelectedItem.text() == $prevSelectedItem.text());
        }

        function test_Home_should_select_first_item() {
            var ddl = getDropDownList();

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 36 });

            var $nextSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertTrue(ddl.dropDown.$element.find('.t-item').first().text() == $nextSelectedItem.text());
        }

        function test_End_should_select_last_item() {
            var ddl = getDropDownList();

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 35 });

            var $nextSelectedItem = $(ddl.dropDown.$element.find('.t-state-selected')[0]);

            assertTrue(ddl.dropDown.$element.find('.t-item').last().text() == $nextSelectedItem.text());
        }

        function test_pressing_alt_down_arrow_should_open_dropdown_list() {
            var ddl = getDropDownList();
            ddl.effects = $.telerik.fx.toggle.defaults();

            ddl.close();

            var $ddl = $('#DropDownList');
            $ddl.focus();

            $ddl.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(ddl.dropDown.$element.is(':visible'));
        }

        function test_pressing_escape_should_close_dropdown_list() {
            var ddl = getDropDownList();
            ddl.effects = ddl.dropDown.effects = $.telerik.fx.toggle.defaults();
            
            ddl.open();
            
            var $ddl = $('#DropDownList');
            $ddl.focus();

            $ddl.trigger({ type: "keydown", keyCode: 27});

            assertFalse(ddl.dropDown.isOpened());
        }

        function test_scrollTo_method_should_return_if_item_is_undefined() {
            var ddl2 = getDropDownList();

            var throwException = false;

            try {
                ddl2.dropDown.scrollTo(undefined);
            }
            catch (e) {
                throwException = true;
            }
                        
            assertFalse("Thrown exception when item is undefined.", throwException);
        }

        function test_Fill_method_on_ajax_should_call_change_event_handler() {
            var isCalled = false;
            var ddl = $('#AjaxDropDownList').data('tDropDownList');
            var old = ddl.trigger.change;

            ddl.ajaxRequest = function (callback) { callback(); }
            ddl.trigger.change = function () { isCalled = true; }

            ddl.fill();

            assertTrue(isCalled);
        }

        function test_open_sets_dropdown_zindex() {
            var ddl = getDropDownList();
            ddl.effects = ddl.dropDown.effects = $.telerik.fx.toggle.defaults();
            
            var $ddl = $(ddl.element)

            var lastZIndex = $ddl.css('z-index');

            $ddl.css('z-index', 42);

            ddl.close();
            ddl.open();

            assertEquals('43', '' + ddl.dropDown.$element.parent().css('z-index'));

            $ddl.css('z-index', lastZIndex);
        }
    </script>

    <%= Html.Telerik().DropDownList()
        .Name("DropDownList")
        .Items(items =>
        {
            items.Add().Text("Item1").Value("1");
            items.Add().Text("Item2").Value("2");
            items.Add().Text("Item3").Value("3");
            items.Add().Text("Item4").Value("4");
            items.Add().Text("Item5").Value("5");
            items.Add().Text("Item6").Value("6");
            items.Add().Text("Item7").Value("7");
            items.Add().Text("Item8").Value("8");
            items.Add().Text("Item9").Value("9");
            items.Add().Text("Item10").Value("10");
            items.Add().Text("Item11").Value("11");
            items.Add().Text("Item12").Value("12");
            items.Add().Text("Item13").Value("13");
            items.Add().Text("Item14").Value("14");
            items.Add().Text("Item15").Value("15");
            items.Add().Text("Item16").Value("16");
            items.Add().Text("Item17").Value("17");
            items.Add().Text("Item18").Value("18");
            items.Add().Text("Item19").Value("19");
            items.Add().Text("тtem20").Value("20");
        })
    %>

    <div style="display:none">
    <%= Html.Telerik().DropDownList()
        .Name("DDLWithServerAttr")
        .DropDownHtmlAttributes(new { style = "width:400px"})
        .Items(items =>
        {
            items.Add().Text("Item1").Value("1");
            items.Add().Text("Item2").Value("2");
            items.Add().Text("Item3").Value("3");
        })
    %>
    </div>

    <%= Html.Telerik().DropDownList()
        .Name("DropDownList2") %>

    <%= Html.Telerik().DropDownList()
        .Name("AjaxDropDownList")
        .ClientEvents(e => e.OnDataBinding("dataBinding")) 
    %>

</asp:Content>
