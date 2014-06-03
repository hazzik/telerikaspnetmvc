<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        function getComboBox() {
            return $('#ComboBox').data('tComboBox');
        }

        function test_pressing_down_arrow_should_select_next_item() {
            var combo = getComboBox();

            combo.fill();

            var $initialSelectedItem = combo.dropDown.$items.first();
            var initialSelectedIndex = $initialSelectedItem.index();
            combo.select($initialSelectedItem);

            
            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 40 });

            assertNotEquals(-1, combo.selectedIndex);
            assertNotEquals(initialSelectedIndex, combo.selectedIndex);
        }

        function test_pressing_up_arrow_should_select_prev_item() {
            var combo = getComboBox();

            combo.select(combo.dropDown.$items[1]);

            var initialSelectedIndex = $(combo.dropDown.$element.find('.t-state-selected')[0]).index();

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 38 });

            assertNotEquals(-1, combo.selectedIndex);
            assertNotEquals(initialSelectedIndex, combo.selectedIndex);
        }

        function test_pressing_alt_down_arrow_should_open_dropdown_list() {
            var combo = getComboBox();
            combo.effects = $.telerik.fx.toggle.defaults();

            combo.close();
            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(combo.dropDown.isOpened());
        }

        function test_keydown_should_create_$items_if_they_do_not_exist() {
            var combo = getComboBox();
            combo.dropDown.$items = null;

            combo.$text.focus();

            combo.$text.trigger({ type: "keydown", keyCode: 38 });

            assertTrue(combo.dropDown.$items.length > 0);
        }

        function test_down_arrow_should_select_next_item() {
            var combo = getComboBox();

            combo.open();

            combo.select(combo.dropDown.$items[0]);

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 40 });

            assertEquals(1, combo.selectedIndex)
        }

        function test_down_arrow_should_select_first_item_if_no_selected() {
            var combo = getComboBox();
            
            combo.open();

            combo.dropDown.$items.removeClass('t-state-selected');
            combo.selectedIndex = -1;

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 40 });

            assertEquals(0, combo.selectedIndex)
        }

        function test_up_arrow_should_select_previous_item() {
            var combo = getComboBox();
            
            combo.open();

            combo.select(combo.dropDown.$items[4]);

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 38 });

            assertEquals(3, combo.selectedIndex)
        }

        function test_clicking_up_arrow_should_select_first_item_if_no_selected_item() {
            var combo = getComboBox();
            
            combo.open();
            combo.selectedIndex = -1;
            combo.dropDown.$items.removeClass('t-state-selected');

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 38});

            assertEquals(0, combo.selectedIndex)
        }

        function test_pressing_Enter_should_close_dropdown_list() {
            var isCalled = false;
            var combo = getComboBox();
            combo.effects = $.telerik.fx.toggle.defaults();

            combo.$text.focus();

            var old = combo.trigger.close;

            combo.trigger.close = function () { isCalled = true; }

            combo.$text.trigger({ type: "keydown", keyCode: 40 });
            combo.$text.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCalled);

            combo.trigger.close = old;
        }

        function test_pressing_Enter_should_change_hidden_input_value() {
            var combo = getComboBox();
            combo.effects = $.telerik.fx.toggle.defaults();

            combo.$text.focus();

            combo.dropDown.$items.removeClass('t-state-selected');

            combo.$text.val('test');
            combo.$text.trigger({ type: "keydown", keyCode: 13 });

            assertEquals('test', combo.value());
        }

        function test_pressing_Enter_should_not_post_if_items_list_is_opened() {
            var combo = getComboBox();
            combo.effects = $.telerik.fx.toggle.defaults();

            var isPreventCalled = false;

            combo.$text.focus();
            combo.$text.trigger({ type: "keydown", keyCode: 40 });
            combo.$text.trigger({ type: "keydown", keyCode: 13, preventDefault: function () { isPreventCalled = true } });

            assertTrue(isPreventCalled);
        }

        function test_pressing_escape_should_call_trigger_close() {
            var isCalled = false;
            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();

            combo.open();
            combo.$text.focus();
            var old = combo.trigger.close;

            combo.trigger.close = function () { isCalled = true; }

            combo.$text.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isCalled);

            combo.trigger.close = old;
        }

        function test_pressing_escape_should_blur_input() {
            var combo = getComboBox();

            var isFocused = true;

            combo.$text.focus();
            combo.$text.blur(function () { isFocused = false });

            combo.$text.trigger({ type: "keydown", keyCode: 27 });

            assertFalse(isFocused);
        }

        function test_if_pressed_key_is_part_of_keyCodes_collection_do_not_continue() {
            var combo = getComboBox();
            
            var oldFilter = combo.filters[combo.filter];

            var isCalled = true;

            combo.filters[combo.filter] = function () { isCalled = false; };

            combo.$text.focus();
            combo.$text.trigger({ type: "keypress", keyCode: 38 });

            assertTrue(isCalled);

            combo.filters[combo.filter] = oldFilter;
        }

    </script>
    
        <%= Html.Telerik().ComboBox()
            .Name("ComboBox")
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
            .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.StartsWith))
    %>

        
    <%= Html.Telerik().ComboBox()
        .Name("ComboBox2")
        .Items(items =>
        {
            items.Add().Text("Chai").Value("1");
            items.Add().Text("Chang").Value("2");
            items.Add().Text("Tofu").Value("3");
            items.Add().Text("Boysenberry").Value("4");
            items.Add().Text("Uncle").Value("5");
            items.Add().Text("Northwoods").Value("6");
            items.Add().Text("Ikura").Value("7");
            items.Add().Text("Queso").Value("8");
            items.Add().Text("Manchego").Value("9");
            items.Add().Text("Dried").Value("10");
            items.Add().Text("тtem20").Value("11");
        })
        .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.StartsWith))
    %>
</asp:Content>