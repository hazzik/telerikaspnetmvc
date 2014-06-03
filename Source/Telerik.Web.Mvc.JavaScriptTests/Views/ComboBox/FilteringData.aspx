<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Fast navigation</h2>
    
    <script type="text/javascript">
        var comboBoxObject;
        var $t;

        function setUp() {
            $t = $.telerik;

            comboBoxObject =
                $($('<div id="ComboBox" class="t-combobox t-header"><div class="t-dropdown-wrap"><input type="text" value="" name="ComboBox" id="ComboBox-input" class="t-input"><span class="t-select t-header"><span class="t-icon t-arrow-down">select</span></span><input type="hidden" id="ComboBox-value" name="ComboBox" value="70"></div></div>'))
                    .appendTo(document.body)
                    .tComboBox()
                    .data('tComboBox');

            comboBoxObject.data = [{ "Text": "Chai", "Value": "1", "Selected": true },
                                   { "Text": "Chang", "Value": "2", "Selected": false },
                                   { "Text": "Aniseed Syrup", "Value": "3", "Selected": false },
                                   { "Text": "Chef Anton\u0027s Cajun Seasoning", "Value": "4", "Selected": false },
                                   { "Text": "Chef Anton\u0027s Gumbo Mix", "Value": "5", "Selected": false },
                                   { "Text": "Grandma\u0027s Boysenberry Spread", "Value": "6", "Selected": false },
                                   { "Text": "Uncle Bob\u0027s Organic Dried Pears", "Value": "7", "Selected": false },
                                   { "Text": "Northwoods Cranberry Sauce", "Value": "8", "Selected": false },
                                   { "Text": "Mishi Kobe Niku", "Value": "9", "Selected": false },
                                   { "Text": "Ikura", "Value": "10", "Selected": false },
                                   { "Text": "Queso Cabrales", "Value": "11", "Selected": false },
                                   { "Text": "Queso Manchego La Pastora", "Value": "12", "Selected": false },
                                   { "Text": "Konbu", "Value": "13", "Selected": false },
                                   { "Text": "Tofu", "Value": "14", "Selected": false },
                                   { "Text": "Genen Shouyu", "Value": "15", "Selected": false },
                                   { "Text": "Pavlova", "Value": "16", "Selected": false },
                                   { "Text": "Alice Mutton", "Value": "17", "Selected": false },
                                   { "Text": "Carnarvon Tigers", "Value": "18", "Selected": false },
                                   { "Text": "Teatime Chocolate Biscuits", "Value": "19", "Selected": false },
                                   { "Text": "Sir Rodney\u0027s Marmalade", "Value": "20", "Selected": false },
                                   { "Text": "Sir Rodney\u0027s Scones", "Value": "21", "Selected": false },
                                   { "Text": "Genen Tofu", "Value": "22", "Selected": false },
                                   { "Text": "Gustaf\u0027s Knäckebröd", "Value": "23", "Selected": false }, 
                                   { "Text": "Tunnbröd", "Value": "24", "Selected": false}];
        }

        function test_filter_type_none_should_select_Tofu_item() {
            
            var firstMatch = comboBoxObject.filters[0]; //none type filter.

            comboBoxObject.fill();
            
            firstMatch(comboBoxObject, comboBoxObject.data, 'To');

            var $selectedItem = comboBoxObject.dropDown.$items.filter('.t-state-selected');

            assertEquals('Tofu', $selectedItem.text());
        }

        function test_filter_type_none_should_deselect_last_selected_if_no_item_is_found() {
            var firstMatch = comboBoxObject.filters[0]; //none type filter.
            
            comboBoxObject.dropDown.$items = [];

            firstMatch(comboBoxObject, comboBoxObject.data, 'Not existing item');

            assertEquals(-1, comboBoxObject.selectedIndex);
        }

        function test_filter_type_none_should_do_nothing_if_no_data() {
            var firstMatch = comboBoxObject.filters[0]; //none type filter.

            comboBoxObject.selectedIndex = -1;

            firstMatch(comboBoxObject, null, 'To'); //if data, select Tofu item

            assertEquals(-1, comboBoxObject.selectedIndex);
        }

        function test_filter_type_none_should_select_first_if_highlightFirst_is_true() {
            var firstMatch = comboBoxObject.filters[0]; //none type filter.

            comboBoxObject.highlightFirst = true;
            comboBoxObject.dropDown.$items = [];

            firstMatch(comboBoxObject, comboBoxObject.data, 'Toffff'); //should not find such item

            assertTrue(comboBoxObject.dropDown.$items.first().hasClass('t-state-selected'));
        }

        function test_filter_type_none_should_create_$items_if_they_were_not_before_that() {

            var firstMatch = comboBoxObject.filters[0]; //none type filter.

            comboBoxObject.dataBind(); //create empty ul

            var data = [{ "Text": "Chai", "Value": "1", "Selected": true },
                        { "Text": "Chang", "Value": "2", "Selected": false },
                        { "Text": "Aniseed Syrup", "Value": "3", "Selected": false }];
                        
            firstMatch(comboBoxObject, data, 'To');

            assertEquals(3, comboBoxObject.dropDown.$items.length);
        }

        function test_filter_type_none_should_create_$items_again_if_component_uses_ajax_binding() {
            
            var firstMatch = comboBoxObject.filters[0]; //none type filter.

            comboBoxObject.dropDown.$items = [];
            comboBoxObject.isAjax = function () { return true; };

            var data = [{ "Text": "Chai", "Value": "1", "Selected": true },
                        { "Text": "Chang", "Value": "2", "Selected": false },
                        { "Text": "Aniseed Syrup", "Value": "3", "Selected": false}];
                        
            firstMatch(comboBoxObject, data, 'To');

            assertEquals(3, comboBoxObject.dropDown.$items.length);
        }

        function test_startsWith_filter_should_render_item_Tofu_with_formated_text() {

            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.open();
            
            startsWith(comboBoxObject, comboBoxObject.data, 'To');

            assertEquals('<strong>To</strong>fu'.toLowerCase(), comboBoxObject.dropDown.$items.first().html().toLowerCase());
        }

        function test_filter_startsWith_type_should_not_render_items_if_no_match() {
            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.open();

            startsWith(comboBoxObject, comboBoxObject.data, 'Not correct item');

            assertTrue(comboBoxObject.dropDown.$items.length == 0);
        }

        function test_filter_startsWith_type_should_render_all_items_if_text_is_empty() {
            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.open();

            startsWith(comboBoxObject, comboBoxObject.data, '');

            assertEquals(comboBoxObject.data.length, comboBoxObject.dropDown.$items.length);
        }

        function test_filter_startsWith_should_format_only_first_match_of_the_filtered_items() {
            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.open();

            startsWith(comboBoxObject, comboBoxObject.data, 'T');

            //Teatime Chocolate Biscuits item
            var $item = $(comboBoxObject.dropDown.$items[1]);

            assertEquals('Teatime Chocolate Biscuits', $item.text());
            assertEquals('<strong>T</strong>eatime Chocolate Biscuits'.toLowerCase(), $item.html().toLowerCase());
        }

        function test_filter_startsWith_should_render_only_one_item() {
            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.open();

            startsWith(comboBoxObject, comboBoxObject.data, 'Chai');

            assertTrue(comboBoxObject.dropDown.$items.length == 1);
        }

        function test_filter_startsWith_should_remove_selected_state_class() {
            var startsWith = comboBoxObject.filters[1]; //startsWith filter.

            comboBoxObject.highlightFirst = false;

            comboBoxObject.open();
            comboBoxObject.select(comboBoxObject.dropDown.$items[0]);

            startsWith(comboBoxObject, comboBoxObject.data, 'Chai');

            assertFalse(comboBoxObject.dropDown.$items.hasClass('t-state-selected'));
        }

        function test_filter_startsWith_should_do_nothing_if_no_data() {
            var firstMatch = comboBoxObject.filters[1];

            comboBoxObject.fill();

            comboBoxObject.select(comboBoxObject.dropDown.$items[0]);

            firstMatch(comboBoxObject, null, 'To'); //should Tofu item

            assertTrue($(comboBoxObject.dropDown.$items[0]).hasClass('t-state-selected'));
        }

        function test_filter_startsWith_should_select_first_item_if_there_are_filtered_items() {
            var firstMatch = comboBoxObject.filters[1];

            comboBoxObject.fill();

            comboBoxObject.highlightFirst = true;

            firstMatch(comboBoxObject, comboBoxObject.data, 'To'); //should Tofu item

            assertTrue($(comboBoxObject.dropDown.$items[0]).hasClass('t-state-selected'));
        }

        function test_filter_Contains_should_not_render_items_if_no_match() {
            var contains = comboBoxObject.filters[2]; 

            comboBoxObject.open();

            contains(comboBoxObject, comboBoxObject.data, 'Not correct item');

            assertTrue(comboBoxObject.dropDown.$items.length == 0);
        }

        function test_filter_Contains_should_render_all_items_if_text_is_empty() {
            var contains = comboBoxObject.filters[2]; 

            comboBoxObject.open();

            contains(comboBoxObject, comboBoxObject.data, '');

            assertEquals(comboBoxObject.data.length, comboBoxObject.dropDown.$items.length);
        }

        function test_filter_Contains_should_apply_format_to_mathes_in_the_item_text() {
            var contains = comboBoxObject.filters[2]; 

            comboBoxObject.open();

            contains(comboBoxObject, comboBoxObject.data, 'y');

            //Teatime Chocolate Biscuits item
            var $item = $(comboBoxObject.dropDown.$items[1]);

            assertEquals("Grandma's Boysenberry Spread", $item.text());
            assertEquals("Grandma's Bo<strong>y</strong>senberr<strong>y</strong> Spread".toLowerCase(), $item.html().toLowerCase());
        }

        function test_filter_Contains_should_render_only_one_item() {
            var contains = comboBoxObject.filters[2]; 

            comboBoxObject.open();

            contains(comboBoxObject, comboBoxObject.data, 'Chai');

            assertTrue(comboBoxObject.dropDown.$items.length == 1);
        }

        function test_filter_contains_should_remove_selected_state_class() {
            var contains = comboBoxObject.filters[2];

            comboBoxObject.highlightFirst = false;

            comboBoxObject.open();
            comboBoxObject.select(comboBoxObject.dropDown.$items[0]);

            contains(comboBoxObject, comboBoxObject.data, 'Chai');

            assertFalse(comboBoxObject.dropDown.$items.hasClass('t-state-selected'));
        }

        function test_filter_contains_should_do_nothing_if_no_data() {
            var contains = comboBoxObject.filters[2];

            comboBoxObject.fill();

            comboBoxObject.select(comboBoxObject.dropDown.$items[0]);

            contains(comboBoxObject, null, '');

            assertTrue($(comboBoxObject.dropDown.$items[0]).hasClass('t-state-selected'));
        }

        function test_filter_contains_should_select_first_item_if_there_are_filtered_items() {
            
            var firstMatch = comboBoxObject.filters[2];

            comboBoxObject.fill();

            comboBoxObject.highlightFirst = true;

            firstMatch(comboBoxObject, comboBoxObject.data, 'To'); //should Tofu item

            assertTrue($(comboBoxObject.dropDown.$items[0]).hasClass('t-state-selected'));
        }

        function tearDown() {
            $(comboBoxObject.element).remove();
            comboBoxObject = null;
        }
    </script>
    
    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")
               .Add("telerik.combobox.js")); %>
</asp:Content>
