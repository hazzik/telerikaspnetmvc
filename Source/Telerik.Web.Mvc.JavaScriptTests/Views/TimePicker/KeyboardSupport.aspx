<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        function getTimePicker() {
            return $('#TimePicker').data('tTimePicker');
        }

        function test_pressing_alt_down_arrow_should_open_dropdown_list() {
            var timepicker = getTimePicker();
            timepicker.effects = $.telerik.fx.toggle.defaults();

            timepicker.close();
            timepicker.$input.focus();
            timepicker.$input.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(timepicker.timeView.dropDown.isOpened());
        }

        function test_pressing_Enter_should_close_dropdown_list() {
            var isCalled = false;
            var timepicker = getTimePicker();

            timepicker.$input.focus();

            var old = timepicker.close;

            timepicker.close = function () { isCalled = true; }

            timepicker.$input.trigger({ type: "keydown", keyCode: 40 });
            timepicker.$input.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCalled);

            timepicker.close = old;
        }

        function test_pressing_Enter_should_call_value_method() {
            var isCalled = false;
            var timepicker = getTimePicker();

            timepicker.$input.focus();

            var old = timepicker._change;

            timepicker._change = function () { isCalled = true; }

            timepicker.$input.trigger({ type: "keydown", keyCode: 40 });
            timepicker.$input.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isCalled);

            timepicker._change = old;
        }

        function test_pressing_escape_should_call_close() {
            var isCalled = false;
            var timepicker = getTimePicker();
            timepicker.effects = timepicker.timeView.dropDown.effects = $.telerik.fx.toggle.defaults();

            timepicker.open();
            timepicker.$input.focus();
            var old = timepicker.close;

            timepicker.close = function () { isCalled = true; }

            timepicker.$input.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isCalled);

            timepicker.close = old;
        }

    </script>
    
    <%= Html.Telerik().TimePicker()
            .Name("TimePicker")
            .Value(new Nullable<DateTime>())
            .Effects(e => e.Toggle()) 
    %>

</asp:Content>