<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        function getAutoComplete() {
            return $('#AutoComplete').data('tAutoComplete');
        }

        function test_pressing_down_arrow_should_highlight_next_item() {
            var autocomplete = getAutoComplete();

            autocomplete.fill();

            var $initialSelectedItem = autocomplete.dropDown.$items.first();

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
            
            assertEquals($initialSelectedItem.next().text(), autocomplete.dropDown.$element.find('.t-state-selected').first().text());
        }

        function test_pressing_up_arrow_should_highlight_prev_item() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$element.find('.t-item')[1]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            assertEquals($initialSelectedItem.prev().text(), autocomplete.dropDown.$element.find('.t-state-selected').first().text());
        }

        function test_keydown_should_create_dropDown_$items_if_they_do_not_exist() {
            var autocomplete = getAutoComplete();
            autocomplete.$dropDown = null;
            autocomplete.dropDown.$items = null;

            autocomplete.open();
            autocomplete.$text.focus();

            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            assertTrue(autocomplete.dropDown.$items.length > 0);
        }

        function test_down_arrow_should_select_first_item_if_no_selected() {
            var autocomplete = getAutoComplete();

            autocomplete.dropDown.$element;
            autocomplete.dropDown.$items.removeClass('t-state-selected');

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });

            assertTrue(autocomplete.dropDown.$items.first().hasClass('t-state-selected'));
        }

        function test_pressing_up_arrow_should_select_last_item_if_no_selected_item() {
            var autocomplete = getAutoComplete();

            autocomplete.dropDown.$element;
            autocomplete.dropDown.$items.removeClass('t-state-selected');

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38});

            assertTrue($(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]).hasClass('t-state-selected'));
        }

        function test_pressing_up_arrow_when_first_item_is_selected_should_highlight_last_one() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$element.find('.t-item')[0]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            assertTrue($(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]).hasClass('t-state-selected'));
        }

        function test_pressing_down_arrow_when_last_item_is_highlight_should_select_first_one() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });

            assertTrue($(autocomplete.dropDown.$items[0]).hasClass('t-state-selected'));
        }

        function test_Tab_button_should_close_dropDown_element(){
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;

            try {
                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.open();
                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 9 });

                assertTrue(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        }

        function test_pressing_Enter_should_close_dropdown_list() {
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;

            try {

                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
                autocomplete.$text.trigger({ type: "keydown", keyCode: 13 });

                assertTrue(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        }

        function test_pressing_Enter_in_dropdown_list_prevents_it() {
            var autocomplete = getAutoComplete();

            var isPreventCalled = false;

            var isOpened = autocomplete.isOpened;
            try {
                autocomplete.isOpened = function () { return true; }

                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
                autocomplete.$text.trigger({ type: "keydown", keyCode: 13, preventDefault: function () { isPreventCalled = true; } });

                assertTrue(isPreventCalled);
            } finally {
                autocomplete.isOpened = isOpened;
            }
        }

        function test_pressing_escape_should_close_dropdown_list() {
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;
            
            try {
                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.open();
                autocomplete.$text.focus();

                autocomplete.$text.trigger({ type: "keydown", keyCode: 27 });

                assertTrue(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        }

    </script>
    
    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(new string[]{
                "Item1", "Item2", "Item3", "Item4", "Item5", "Item6",
                "Item7", "Item8", "Item9", "Item10", "Item11", "Item12",
                "Item13", "Item14", "Item15", "Item16", "Item17", "Item18",
                "Item19", "тtem20"
            })
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")
               .Add("telerik.autocomplete.js")); %>
</asp:Content>