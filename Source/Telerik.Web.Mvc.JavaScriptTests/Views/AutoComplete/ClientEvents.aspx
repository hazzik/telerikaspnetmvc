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
        var autoComplete;

        function getAutoComplete(id) {
            return $(id || '#AutoComplete').data('tAutoComplete');
        }

        function setUp() {
            autoComplete = getAutoComplete();
            isOpenRaised = false;
            isCloseRaised = false;
            returnedValue = undefined; 
            isChangeRaised = false;
            isDataBinding = false;
            isDataBound = false;
        }

        function test_clicking_document_should_raise_onClose_event() {
            autoComplete.open();

            $(document.body).mousedown();

            assertTrue(isCloseRaised);
        }
        
        function test_clicking_escape_should_raise_onClose_if_opened() {
            autoComplete.open();

            autoComplete.$text.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_escape_should_not_raise_onClose_if_closed() {
            autoComplete.close();

            autoComplete.$text.trigger({ type: "keydown", keyCode: 27 });

            assertFalse(isCloseRaised);
        }

        function test_clicking_enter_should_raise_onClose_if_list_is_opened() {

            autoComplete.open();

            autoComplete.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 })
                .trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_tab_should_raise_onClose_if_list_is_opened() {
            autoComplete.open();

            autoComplete.$text.trigger({ type: "keydown", keyCode: 9 });

            assertTrue(isCloseRaised);
        }

        function test_clicking_item_from_dropDownList_should_raise_onClose_when_it_is_opened() {

            autoComplete.$text.focus();

            autoComplete.open();

            autoComplete.dropDown.$items.first().click();

            assertTrue(isCloseRaised);
        }

        function test_enter_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            
            autoComplete.open();

            autoComplete.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 40 })
                .trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isChangeRaised);
        }

        function test_clicking_on_new_item_should_raise_onChange_event() {
            autoComplete.$text.focus();

            autoComplete.open();

            var item = autoComplete.dropDown.$items.find('.t-state-selected').first().next();

            if (!item.length)
                item = autoComplete.dropDown.$items.first();
            else
                item = item[0];

            $(item).click();

            assertTrue(isChangeRaised);
        }

        function test_cleared_input_should_not_raise_change_event_if_opened_and_close_after_clearing() {

            autoComplete.open();

            autoComplete.$text
                .focus()
                .trigger({ type: "keydown", keyCode: 13 })
                .trigger({ type: "keydown", keyCode: 40 })
                .val('');

            $(document.body).mousedown();

            isChangeRaised = false;

            autoComplete.$text.focus();
            $(document.body).mousedown();

            assertFalse(isChangeRaised);
        }

        function test_trigger_change_should_set_value_if_text_has_exact_match_with_date_items() {
            
            autoComplete.text('Item2');
            autoComplete.trigger.change();

            assertEquals('Item2', autoComplete.text());
        }

        function test_Fill_method_on_ajax_should_call_change_event_handler() {
            var isCalled = false;
            var autoComplete = $('#FakeAjaxAutoComplete').data('tAutoComplete');
            var old = autoComplete.trigger.change;

            autoComplete.ajaxRequest = function (callback) { callback(); }
            autoComplete.trigger.change = function () { isCalled = true; }
            autoComplete.$text.val('c');

            autoComplete.fill();

            assertTrue(isCalled);
        }

        function test_document_click_should_raise_change_event_if_dropdown_list_is_never_opened() {

            isChangeRaised = false;
            autoComplete.$text.focus().val('22');

            $(document.documentElement).mousedown();

            assertTrue(isChangeRaised);
            assertEquals('22', returnedValue);
        }

        function test_clicking_enter_should_raise_change_event_if_custom_text_is_typed() {
            autoComplete.open();
            autoComplete.close();
            
            isChangeRaised = false;
            autoComplete.$text
                        .focus()
                        .val('44')
                        .trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isChangeRaised);
            //assertEquals('44', returnedValue);
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

    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(new[] { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6", "Item7", "Item8" })
            .Effects(effects => effects.Toggle())
                    .ClientEvents(events => events.OnOpen("onOpen")
                                                  .OnClose("onClose")
                                                  .OnChange("onChange"))
    %>

     <%= Html.Telerik().AutoComplete()
            .Name("AjaxAutoComplete")
            .Effects(effects => effects.Toggle())
            .DataBinding(binding => binding.Ajax().Select("_AjaxLoading", "ComboBox"))
            .ClientEvents(events => events.OnDataBinding("onDataBinding")
                                          .OnDataBound("onDataBound"))
    %>

    <%= Html.Telerik().AutoComplete()
            .Name("FakeAjaxAutoComplete")
            .ClientEvents(events => events.OnDataBinding("onDataBinding"))
    %>
</asp:Content>
