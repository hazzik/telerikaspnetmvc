<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ComboBox Rendering</h2>

    <script type="text/javascript">
        function getComboBox(selector) {
            return $(selector || '#ComboBox').data('tComboBox');
        }

        function test_combobox_method_should_not_select_first_item_if_there_is_no_selectedItem_and_selectIndex_is_negative() {
            
            var ddl = getComboBox();
            ddl.index = -1;
            ddl.$input.val('');
            ddl.dropDown.$items = null;
            ddl.fill();

            assertEquals(0, ddl.dropDown.$element.find('> li.t-state-selected').length);
        }

        function test_click_item_in_dropDown_list_when_it_is_shown_should_call_select_method() {

            var isSelectCalled = false;

            var ddl = getComboBox();
            ddl.effects = $.telerik.fx.toggle.defaults();

            var old = ddl.select;
            ddl.select = function () { isSelectCalled = true; }

            ddl.open();         

            $(ddl.dropDown.$items[2]).click();

            assertTrue(isSelectCalled);

            ddl.select = old;
        }

        function test_dataBind_method_should_not_select_item_if_no_items_selected_and_selectedIndex_is_negative() {
            var combo2 = getComboBox('#ComboBox2');

            combo2.index = -1;
            combo2.highlightFirst = false;
            combo2.$input.val('');
            combo2.data = [{ "Text": "Chai", "Value": "1" },
                           { "Text": "Chang", "Value": "2" },
                           { "Text": "Aniseed Syrup", "Value": "3"}];
            combo2.dropDown.$items = null;
            combo2.fill();

            assertEquals(0, combo2.dropDown.$items.filter('.t-state-selected').length);
            assertEquals(-1, combo2.selectedIndex);
        }

        function test_dataBind_method_should_select_second_item_when_selected_index_is_1() {
            var combo2 = getComboBox('#ComboBox2');
            combo2.index = 1;

            combo2.data = [{ "Text": "Chai", "Value": "1" },
                           { "Text": "Chang", "Value": "2" },
                           { "Text": "Aniseed Syrup", "Value": "3"}];
            combo2.$input.val('');
            combo2.dropDown.$items = null;
            combo2.fill();

            assertEquals(1, combo2.selectedIndex);
        }

        function test_fill_method_should_highlight_first_item_if_no_selected_item_and_highlightFirst_option_is_true() {
            var combo2 = getComboBox('#ComboBox2');
            combo2.index = -1;
            combo2.highlightFirst = true;

            combo2.$input.val('');
            combo2.data[{ "Text": "Chai", "Value": "1" },
                           { "Text": "Chang", "Value": "2" },
                           { "Text": "Aniseed Syrup", "Value": "3"}];
            
            combo2.dropDown.$items = null;
            combo2.fill();

            assertTrue(combo2.dropDown.$items.first().hasClass('t-state-selected'));
        }

        function test_select_should_set_text_defined_in_li_element() { //if text is encoded in the dataItem.Text

            var combo = $('#ComboBox2').data('tComboBox');

            combo.dataBind([{ "Text": "Calendar &raquo; Select Action", "Value": "1" },
                           { "Text": "Chang", "Value": "2" },
                           { "Text": "Aniseed Syrup", "Value": "3"}]);

            var li = combo.dropDown.$items[0];
            
            combo.select(li);

            assertEquals("Calendar » Select Action", combo.text());
        }

        function test_open_sets_dropdown_zindex() {
            var combo = getComboBox();
            combo.effects = combo.dropDown.effects = $.telerik.fx.toggle.defaults();
            
            var $combo = $(combo.element)

            var lastZIndex = $combo.css('z-index');

            $combo.css('z-index', 42);

            combo.close();
            combo.open();

            assertEquals('43', '' + combo.dropDown.$element.parent().css('z-index'));

            $combo.css('z-index', lastZIndex);
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
            .Effects(effect => effect.Toggle())
    %>

    <div style="display:none">
    <%= Html.Telerik().ComboBox()
        .Name("ComboWithServerAttr")
        .DropDownHtmlAttributes(new { style = "width:400px"})
        .Effects(e => e.Toggle())
        .Items(items =>
        {
            items.Add().Text("Item1").Value("1");
            items.Add().Text("Item2").Value("2");
            items.Add().Text("Item3").Value("3");
        })
    %>
    </div>

    <%= Html.Telerik().ComboBox()
            .Name("ComboBox2")
            .Effects(e => e.Toggle())
    %>
</asp:Content>