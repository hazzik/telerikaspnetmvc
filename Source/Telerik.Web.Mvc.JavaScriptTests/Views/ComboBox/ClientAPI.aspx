<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>ComboBox Rendering</h2>

    <script type="text/javascript">
        function getComboBox(selector) {
            return $(selector || '#ComboBox').data('tComboBox');
        }

        var $t;

        function setUp() {
            $t = $.telerik;
        }

        function test_show_method_should_open_dropDown_list() {
            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();
            combo.close();
            combo.open();

            assertTrue(combo.dropDown.isOpened());
        }

        function test_show_method_reposition_dropDown_list() {
            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();

            combo.close();
            combo.open();

            var animatedContainer = combo.dropDown.$element.parent();

            var elementPosition = combo.$element.offset();

            elementPosition.top += combo.$element.outerHeight();

            assertTrue(animatedContainer.css('position') == 'absolute');
            assertTrue(animatedContainer.css('top') == Math.round(elementPosition.top * 1000) / 1000 + 'px');
            assertTrue(animatedContainer.css('left') == Math.round(elementPosition.left * 1000) / 1000 + 'px');
        }

        function test_select_should_select_one_item_only() {

            var combo = getComboBox();
            combo.fill();

            var li = combo.dropDown.$element.find('li')[3];

            combo.select(li);

            assertTrue(combo.dropDown.$element.find('.t-state-selected').length == 1);
            assertTrue($(li).hasClass('t-state-selected'));
        }

        function test_select_should_cache_selected_index() {
            var combo = getComboBox();

            combo.selectedIndex = -1;

            var li = combo.dropDown.$element.find('li')[3];

            combo.select(li);

            assertTrue(combo.selectedIndex == 3);
        }

        function test_select_method_should_select_correct_item_index_after_filtration() {
            var combo = $('#ComboBox2').data('tComboBox');

            combo.fill();
            combo.selectedIndex = -1;

            combo.$text.val('тt');
            combo.filter = 1;

            combo.filters[combo.filter](combo, combo.data, 'тt');
     
            combo.select(combo.dropDown.$items[0]);

            assertEquals(19, combo.selectedIndex);

            combo.filter = 0;
        }

        function test_clicking_items_with_templates_selects_items_correctly() {
            var combo = $('#ComboBox4').data('tComboBox');

            combo.fill();

            var oldSelect = combo.select;
            var item = null;

            combo.select = function() { item = arguments[0]; };

            combo.open();

            var testee = combo.dropDown.$element.find('li');

            testee.html('<span>Item1</span>');

            testee.find('span').trigger('click');

            assertEquals(testee[0], item);
            
            testee.html('Item1');

            combo.select = oldSelect;
        }

        function test_text_method_should_set_value_of_text_span() {

            var item = { Selected: false, Text: "Item2", Value: "2" };

            var combo = getComboBox();

            combo.text("Item2");

            assertEquals(item.Text, combo.$text.val());
        }

        function test_text_method_should_return_value_if_no_parameters() {

            var item = { "Selected": false, "Text": "Item2", "Value": "2" };

            var combo = getComboBox();

            combo.text(item.Text);

            var result = combo.text();

            assertEquals(item.Text, result);
        }

        function test_value_method_should_set_value_attribute_of_hidden_input() {
            var item = { "Selected": false, "Text": "Item2", "Value": "2" };

            var combo = getComboBox();

            combo.value(2);

            assertEquals(item.Value, combo.$input.val());
        }

        function test_value_method_should_return_value_hidden_input() {
            var item = { "Selected": false, "Text": "Item2", "Value": "2" };

            var combo = getComboBox();

            combo.value(2);

            var result = combo.value();

            assertEquals(item.Value, result);
        }

        function test_open_method_should_show_hidden_items_if_there_is_selected_item() {

            var combo = getComboBox();
            
            combo.select(combo.dropDown.$items[0]);
            combo.isFiltered = true;

            combo.close();
            combo.open();
            
            assertEquals(20, combo.dropDown.$items.length);
        }

        function test_open_method_should_set_height_of_the_dropDown_if_items_are_filtered_and_item_is_selected() {

            var combo = getComboBox();
            
            combo.dropDown.$element.css('height', 'auto'); // set auto in order to simulate filtering of items and reducing their count to less then 10

            combo.select(combo.dropDown.$items[0]);
            combo.isFiltered = true;
                        
            combo.close();
            combo.open();

            assertEquals("200px", combo.dropDown.$element.css('height'));
        }

        function test_open_method_should_unformat_filtered_items_if_there_is_selected_item() {

            var combo = getComboBox();

            var text = combo.dropDown.$items.first().html();
            combo.dropDown.$items.first().html("<strong>" + text + "<strong>");
            combo.isFiltered = true;

            combo.select(combo.dropDown.$items[0]);
            combo.close();
            combo.open();
            
            assertEquals(-1, combo.dropDown.$items.first().html().indexOf('<strong>'));
        }

        function test_open_method_should_select_item_if_input_text_is_same_as_selected_item() {

            var combo = getComboBox();

            var $item = combo.dropDown.$items.first();

            combo.select($item[0]);

            combo.dropDown.$items.removeClass('t-state-selected');

            combo.$text.val($item.text());
            combo.isFiltered = true;

            combo.close();
            combo.open();

            assertTrue($(combo.dropDown.$items[combo.selectedIndex]).hasClass('t-state-selected'));
        }

        function test_open_method_should_remove_selection_and_filter_items_if_text_is_different_then_selected_item() {
            
            var combo = getComboBox();

            combo.filter = 1;
            combo.select(combo.dropDown.$items[0]);
            combo.$text.val('тt');

            combo.isFiltered = true;
            
            combo.close();
            combo.open();

            assertEquals(-1, combo.selectedIndex);
            assertEquals(1, combo.dropDown.$items.length);
        }

        function test_open_method_should_call_filter_method_if_text_is_different_than_selected_item() {

            var filterIsCalled = false;
            
            var combo = getComboBox();
            combo.fill();
            
            combo.filter = 0;

            var old = combo.filters[0];
            combo.filters[0] = function () { filterIsCalled = true; };
            combo.isFiltered = true;

            combo.select(combo.dropDown.$items[0]);
            combo.$text.val('тt');

            combo.close();
           
            try {
                combo.open(); //will throw error because the replacing filter method
            }
            catch (e) { }

            combo.filters[0] = old;

            assertTrue(filterIsCalled);
        }

        function test_show_method_should_append_dropdown_list_to_body() {

            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();

            combo.close();
            combo.open();

            assertTrue($.contains(document.body, combo.dropDown.$element[0]));
        }
        
        function test_hide_method_should_remove_dropdown_list_to_body() {

            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();

            combo.open();
            combo.close();

            assertTrue(!$.contains(document.body, combo.dropDown.$element[0]));
        }

        function test_updateTextAndValue_method_should_set_value_to_Text_property_if_Value_is_not_specified() {

            var combo = getComboBox();

            var dataitem = {Text:"Custom"};

            $t.list.updateTextAndValue(combo, dataitem.Text, dataitem.Value);

            assertEquals("Custom", combo.value());
            assertEquals("Custom", combo.text());
        }

        function test_updateTextAndValue_method_should_set_value_and_text() {

            var combo = getComboBox();

            var dataitem = { Text: "Custom", Value: "1" };

            $t.list.updateTextAndValue(combo, dataitem.Text, dataitem.Value);

            assertEquals("1", combo.value());
            assertEquals("Custom", combo.text());
        }

        function test_updateTextAndValue_method_set_value_to_text_when_it_does_not_exists() {

            var combo = getComboBox();
            $t.list.updateTextAndValue(combo, "Item11");

            assertEquals("Item11", combo.value());
            assertEquals("Item11", combo.text());
        }

        function test_combobox_initializes_correctly_when_no_name_and_ids_are_rendered() {
            var combo = $('.t-fontFamily').tComboBox().data('tComboBox');

            assertEquals(combo.element.lastChild, combo.$input[0]);
        }

        function test_reload_should_call_fill_method_with_empty_$items_collection() {
            var isCalled = false;
            var isEmpty = false;
            var combo = getComboBox();
            var oldFill = combo.fill;          

            combo.fill = function () { isCalled = true; if (!combo.dropDown.$items) isEmpty = true; };

            combo.reload();

            assertTrue("fill method was not called", isCalled);
            assertTrue("$items is not empty", isEmpty);
            
            combo.fill = oldFill;
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
    %>

        <%= Html.Telerik().ComboBox()
            .Name("ComboBox2")
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
            .Filterable(f => f.FilterMode(AutoCompleteFilterMode.StartsWith))
                    
    %>

    <%= Html.Telerik().ComboBox()
            .Name("ComboBox3") %>

    <%= Html.Telerik().ComboBox()
        .Name("ComboBox4")
        .Items(items =>
        {
            items.Add().Text("Item1").Value("1");
        }) %>

    <div class="t-combobox t-header t-fontFamily"><div class="t-dropdown-wrap t-state-default"><input type="text" title="Select font family" class="t-input" autocomplete="off"><span class="t-select t-header"><span class="t-icon t-arrow-down">select</span></span></div><input type="text" style="display: none;"></div>
</asp:Content>
