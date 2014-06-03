<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
                
        .t-state-focus
        {
            border-color: Red !important;
            border-width: 2px !important;
        }
    </style>
    
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        function getDateTimePicker() {
            return $('#DateTimePicker').data('tDateTimePicker');
        }

        function test_pressing_alt_down_arrow_should_open_dateView() {
            var dateTimePicker = getDateTimePicker();
            dateTimePicker.effects = $.telerik.fx.toggle.defaults();

            dateTimePicker.$input.focus();
            dateTimePicker.close();
            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(dateTimePicker.dateView.isOpened());
        }

        function test_pressing_Enter_should_close_dropdown_list() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.$input.focus();

            dateTimePicker.open('date');

            var old = dateTimePicker.close;

            dateTimePicker.close = function () { isCalled = true; }
            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 13 });
            assertTrue(isCalled);

            dateTimePicker.close = old;
        }

        function test_pressing_Enter_should_call_value_method() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.$input.focus();

            var old = dateTimePicker._change;

            dateTimePicker._change = function () { isCalled = true; }

            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 40 });
            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCalled);

            dateTimePicker._change = old;
        }

        function test_pressing_escape_should_call_close() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.open();
            dateTimePicker.$input.focus();
            var old = dateTimePicker.close;

            dateTimePicker.close = function () { isCalled = true; }

            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isCalled);

            dateTimePicker.close = old;
        }

        function test_if_dateView_is_opened_and_no_important_key_pressed_should_call_navigate() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.open('date');
            dateTimePicker.$input.focus();
            var old = dateTimePicker.dateView.navigate;

            dateTimePicker.dateView.navigate = function () { isCalled = true; }

            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 40 });

            assertTrue(isCalled);

            dateTimePicker.dateView.navigate = old;
        }

        function test_if_timeView_is_opened_and_no_important_key_pressed_should_call_navigate() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.open('time');
            dateTimePicker.$input.focus();
            var old = dateTimePicker.timeView.navigate;

            dateTimePicker.timeView.navigate = function () { isCalled = true; }

            dateTimePicker.$input.trigger({ type: "keydown", keyCode: 40 });

            assertTrue(isCalled);

            dateTimePicker.timeView.navigate = old;
        }

    </script>
    
    <%= Html.Telerik().DateTimePicker()
            .Name("DateTimePicker")
            .HtmlAttributes(new { style = "width:300px" })
            .Value(new Nullable<DateTime>())
            .Effects(e => e.Toggle())
    %>

</asp:Content>