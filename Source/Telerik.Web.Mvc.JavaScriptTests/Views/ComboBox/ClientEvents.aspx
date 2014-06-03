<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client Events</h2>
    <script type="text/javascript">
        var isChanged;
        var returnedValue;
        var isOpenRaised;
        var isCloseRaised;
        var isDataBinding;
        var isDataBound;
        var comboBox;

        function getComboBox(id) {
            return $(id || '#ComboBox').data('tComboBox');
        }

        function setUp() {
            comboBox = getComboBox();
            isOpenRaised = false;
            isCloseRaised = false;
            returnedValue = undefined; 
            isChangeRaised = false;
            isDataBinding = false;
            isDataBound = false;
        }

        function test_clicking_toggle_button_should_raise_onOpen_event() {
            comboBox.close();

            comboBox.$text.focus();

            assertTrue(isOpenRaised);
        }

        function test_clicking_toggle_button_should_raise_onClose_event() {
            comboBox.open();

            $('.t-dropdown-wrap > .t-select', comboBox.element).trigger('click');

            assertTrue(isCloseRaised);
        }

        function test_clicking_document_should_raise_onClose_event() {
            comboBox.open();

            $(document.body).mousedown();

            assertTrue(isCloseRaised);
        }

        function test_clicking_alt_and_down_arrow_should_raise_onOpen() {
            comboBox.close();

            comboBox.$text.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(isOpenRaised);
        }

        function test_clicking_escape_should_raise_onClose_if_opened() {
            comboBox.open();

            comboBox.$text.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_escape_should_not_raise_onClose_if_closed() {
            comboBox.close();

            comboBox.$text.trigger({ type: "keydown", keyCode: 27 });

            assertFalse(isCloseRaised);
        }

        function test_clicking_enter_should_raise_onClose_if_list_is_opened() {

            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 })
                .trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_tab_should_raise_onClose_if_list_is_opened() {
            comboBox.open();

            comboBox.$text.trigger({ type: "keydown", keyCode: 9 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_item_from_dropDownList_should_raise_onClose_when_it_is_opened() {

            comboBox.$text.focus();

            comboBox.dropDown.$items.first().click();

            assertTrue(isCloseRaised);
        }

        function test_enter_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            
            comboBox.open();

            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 })
                .trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isChangeRaised);
        }

        function test_escape_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            comboBox.open();

            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 })
                .trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isChangeRaised);
        }

        function test_down_arrow_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_closed() {

            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 13 })
                .trigger({ type: "keydown", keyCode: 40 });

            assertTrue(isChangeRaised);
        }

        function test_down_arrow_should_not_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            comboBox.open();
            
            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 });

            assertFalse(isChangeRaised);
        }

        function test_clicking_on_new_item_should_raise_onChange_event() {
            comboBox.$text.focus();

            var item = comboBox.dropDown.$items.find('.t-state-selected').first().next();

            if (!item.length)
                item = comboBox.dropDown.$items.first();
            else
                item = item[0];

            $(item).click();

            assertTrue(isChangeRaised);
        }

        function test_cleared_input_should_not_raise_change_event_if_opened_and_close_after_clearing() {
            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 13 })
                .trigger({ type: "keydown", keyCode: 40 })
                .val('');

            comboBox.selectedIndex = -1;
            comboBox.previousSelectedIndex = -1;

            comboBox.$text.blur();

            isChangeRaised = false;
            
            comboBox.$text.focus();
            $(document.body).mousedown();

            assertFalse(isChangeRaised);
        }

        function test_document_clicking_should_raise_value_changed_if_different_item_is_selected() {

            comboBox.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 });

            isChangeRaised = false;

            $(document.body).mousedown();

            assertTrue(isChangeRaised);
        }

        function test_trigger_change_should_set_value_if_text_has_exact_match_with_date_items() {
            
            comboBox.text('Item2');
            comboBox.trigger.change();

            assertEquals('Item2', comboBox.text());
            assertEquals('2', comboBox.value());
        }

        function test_Fill_method_on_ajax_should_call_change_event_handler() {
            var isCalled = false;
            var combo = $('#FakeAjaxComboBox').data('tComboBox');
            var old = combo.trigger.change;

            combo.ajaxRequest = function (callback) { callback(); }
            combo.trigger.change = function () { isCalled = true; }

            combo.fill();

            assertTrue(isCalled);
        }

        //handlers
        function onChange(e) {
            returnedValue = e.value; 
            isChangeRaised = true;
        }

        function onClose(sender, args) {
            isCloseRaised = true;
        }

        function onOpen(sender, args) {
            isOpenRaised = true;
        }

        function onDataBinding(sender, args) {
            isDataBinding = true;
        }

        function onDataBound(sender, args) {
            isDataBound = true;
        }
    </script>

    <%= Html.Telerik().ComboBox()
            .Name("ComboBox")
            .Items(items =>
            {
                for (var i = 1; i <= 14; i++)
                    items.Add().Text("Item" + i).Value(i.ToString());
            })
            .Effects(effects => effects.Toggle())
                    .ClientEvents(events => events.OnOpen("onOpen")
                                                  .OnClose("onClose")
                                                  .OnChange("onChange"))
    %>

     <%= Html.Telerik().ComboBox()
            .Name("AjaxComboBox")
            .Effects(effects => effects.Toggle())
            .DataBinding(binding => binding.Ajax().Select("_AjaxLoading", "ComboBox"))
            .ClientEvents(events => events.OnDataBinding("onDataBinding")
                                          .OnDataBound("onDataBound"))
    %>

    <%= Html.Telerik().ComboBox()
            .Name("FakeAjaxComboBox")
            .ClientEvents(events => events.OnDataBinding("onDataBinding"))
    %>
</asp:Content>
