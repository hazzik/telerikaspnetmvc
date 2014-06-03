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

        function test_internal_toggleDateView_should_call_change_and_open_if_dateView_is_closed() {
            var isOpenCalled = false;
            var isCloseCalled = false;
            var isChangeCalled = false;

            var openValue;
            var closeValue;
            var changeValue;

            var dateTimePicker = getDateTimePicker();

            var oldOpen = dateTimePicker._open;
            var oldClose = dateTimePicker._close;
            var oldChange = dateTimePicker._change;

            dateTimePicker._open = function (value) { isOpenCalled = true; openValue = value; };
            dateTimePicker._close = function (value) { isCloseCalled = true; closeValue = value; };
            dateTimePicker._change = function (value) { isChangeCalled = true; changeValue = value; };
            
            dateTimePicker.close('date');
            dateTimePicker.$input.val('');
            dateTimePicker._toggleDateView();

            assertTrue('_open method was not called', isOpenCalled);
            assertEquals('_open method was not called with "date" argument', 'date', openValue);
            assertTrue('_close method was not called', isCloseCalled);
            assertEquals('_close method was not called with "time" argument', 'time', closeValue);
            assertTrue('_change method was not called', isChangeCalled);
            assertEquals('_change method was not called with parsed value', null, changeValue);

            dateTimePicker._open = oldOpen;
            dateTimePicker._close = oldClose;
            dateTimePicker._change = oldChange;
        }

        function test_internal_toggleTimeView_should_call_change_and_open_if_timeView_is_closed() {
            var isOpenCalled = false;
            var isCloseCalled = false;
            var isChangeCalled = false;

            var openValue;
            var closeValue;
            var changeValue;

            var dateTimePicker = getDateTimePicker();

            var oldOpen = dateTimePicker._open;
            var oldClose = dateTimePicker._close;
            var oldChange = dateTimePicker._change;

            dateTimePicker._open = function (value) { isOpenCalled = true; openValue = value; };
            dateTimePicker._close = function (value) { isCloseCalled = true; closeValue = value; };
            dateTimePicker._change = function (value) { isChangeCalled = true; changeValue = value; };

            dateTimePicker.close('time');
            dateTimePicker.$input.val('');
            dateTimePicker._toggleTimeView();

            assertTrue('_open method was not called', isOpenCalled);
            assertEquals('_open method was not called with "time" argument', 'time', openValue);
            assertTrue('_close method was not called', isCloseCalled);
            assertEquals('_close method was not called with "date" argument', 'date', closeValue);
            assertTrue('_change method was not called', isChangeCalled);
            assertEquals('_change method was not called with parsed value', null, changeValue);

            dateTimePicker._open = oldOpen;
            dateTimePicker._close = oldClose;
            dateTimePicker._change = oldChange;
        }

        function test_internal_toggleDateView_should_call_close_with_date_argument() {
            var isCloseCalled = false;
            var closeValue;

            var dateTimePicker = getDateTimePicker();
            var oldClose = dateTimePicker._close;

            dateTimePicker._close = function (value) { isCloseCalled = true; closeValue = value; };

            dateTimePicker.open('date');
            dateTimePicker._toggleDateView();

            assertTrue('_close method was not called', isCloseCalled);
            assertEquals('_close method was not called with "date" argument', 'date', closeValue);

            dateTimePicker._close = oldClose;
        }

        function test_internal_toggleTimeView_should_call_close_with_date_argument() {
            var isCloseCalled = false;
            var closeValue;

            var dateTimePicker = getDateTimePicker();
            var oldClose = dateTimePicker._close;

            dateTimePicker._close = function (value) { isCloseCalled = true; closeValue = value; };

            dateTimePicker.open('time');

            console.log(dateTimePicker.timeView.isOpened());

            dateTimePicker._toggleTimeView();

            assertTrue('_close method was not called', isCloseCalled);
            assertEquals('_close method was not called with "time" argument', 'time', closeValue);

            dateTimePicker._close = oldClose;
        }

        function test_internal_open_method_should_call_internal_trigger_with_date_argument_if_dateView_is_closed() {

            var arg1, arg2;
            var dateTimePicker = getDateTimePicker();

            var oldTrigger = dateTimePicker._trigger;
            dateTimePicker._trigger = function (popup, value) { arg1 = popup, arg2 = value};
            dateTimePicker.dateView.close();

            dateTimePicker._open('date');

            assertEquals('trigger method was not called with correct popup', 'date', arg1);
            assertEquals('trigger method was not called with correct method', 'open', arg2);

            dateTimePicker._trigger = oldTrigger;
        }

        function test_internal_open_method_should_call_internal_trigger_with_time_argument_if_dateView_is_closed() {

            var arg1, arg2;
            var dateTimePicker = getDateTimePicker();

            var oldTrigger = dateTimePicker._trigger;
            dateTimePicker._trigger = function (popup, value) { arg1 = popup, arg2 = value };
            dateTimePicker.timeView.close();

            dateTimePicker._open('time');

            assertEquals('trigger method was not called with correct popup', 'time', arg1);
            assertEquals('trigger method was not called with correct method', 'open', arg2);

            dateTimePicker._trigger = oldTrigger;
        }

        function test_internal_close_method_should_call_internal_trigger_with_date_argument_if_dateView_is_opened() {

            var arg1, arg2;
            var dateTimePicker = getDateTimePicker();

            var oldTrigger = dateTimePicker._trigger;
            dateTimePicker._trigger = function (popup, value) { arg1 = popup, arg2 = value };
            var $input = dateTimePicker.$input;
            dateTimePicker.dateView.open({
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: $input.outerWidth(),
                zIndex: $.telerik.getElementZIndex($input[0])
            });
            
            dateTimePicker._close('date');

            assertEquals('trigger method was not called with correct popup', 'date', arg1);
            assertEquals('trigger method was not called with correct method', 'close', arg2);

            dateTimePicker._trigger = oldTrigger;
        }

        function test_internal_close_method_should_call_internal_trigger_with_time_argument_if_dateView_is_opened() {

            var arg1, arg2;
            var dateTimePicker = getDateTimePicker();

            var oldTrigger = dateTimePicker._trigger;
            dateTimePicker._trigger = function (popup, value) { arg1 = popup, arg2 = value };
            var $input = dateTimePicker.$input;
            dateTimePicker.timeView.open({
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: $input.outerWidth(),
                zIndex: $.telerik.getElementZIndex($input[0])
            });
            dateTimePicker._close('time');

            assertEquals('trigger method was not called with correct popup', 'time', arg1);
            assertEquals('trigger method was not called with correct method', 'close', arg2);

            dateTimePicker._trigger = oldTrigger;
        }

        function test_internal_trigger_should_call_open_with_date_parameter() {
            var pop;
            var dateTimePicker = getDateTimePicker();
            var old = dateTimePicker.open;

            dateTimePicker.open = function (popup) { pop = popup; };

            dateTimePicker._trigger('date', 'open');

            assertEquals('open was not called with "date" parapm', 'date', pop);

            dateTimePicker.open = old;
        }

        function test_internal_trigger_should_call_open_with_time_parameter() {
            var pop;
            var dateTimePicker = getDateTimePicker();
            var old = dateTimePicker.open;

            dateTimePicker.open = function (popup) { pop = popup; };

            dateTimePicker._trigger('time', 'open');

            assertEquals('open was not called with "time" parapm', 'time', pop);

            dateTimePicker.open = old;
        }

        function test_internal_trigger_should_call_close_with_date_parameter() {
            var pop;
            var dateTimePicker = getDateTimePicker();
            var old = dateTimePicker.close;

            dateTimePicker.close = function (popup) { pop = popup; };

            dateTimePicker._trigger('date', 'close');

            assertEquals('open was not called with "date" parapm', 'date', pop);

            dateTimePicker.close = old;
        }

        function test_internal_trigger_should_call_close_with_time_parameter() {
            var pop;
            var dateTimePicker = getDateTimePicker();
            var old = dateTimePicker.close;

            dateTimePicker.close = function (popup) { pop = popup; };

            dateTimePicker._trigger('time', 'close');

            assertEquals('open was not called with "time" parapm', 'time', pop);

            dateTimePicker.close = old;
        }

        function test_disable_method_should_add_disable_attr() {
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.disable();

            assertEquals('input is not disabled', true, dateTimePicker.$input.attr('disabled'))

            dateTimePicker.enable();
        }

        function test_disable_method_should_add_enable_attr() {
            var dateTimePicker = getDateTimePicker();

            dateTimePicker.enable();

            assertEquals('input is not disabled', false, dateTimePicker.$input.attr('disabled'))

            dateTimePicker.disable();
        }

        function test_min_method_should_set_minDate_property_and_call_dateView_min_method() {
            var isCalled = false;
            var dateTimePicker = getDateTimePicker();

            var oldM = dateTimePicker.dateView.min;
            dateTimePicker.dateView.min = function () { isCalled = true; }

            dateTimePicker.min('10/10/1904');

            assertTrue(isCalled);
            assertEquals('year', 1904, dateTimePicker.minValue.year());
            assertEquals('month', 9, dateTimePicker.minValue.month());
            assertEquals('day', 10, dateTimePicker.minValue.date());

            dateTimePicker.dateView.min = oldM;
        }

    </script>
    
    <%= Html.Telerik().DateTimePicker()
            .Name("DateTimePicker")
            .HtmlAttributes(new { style = "width:300px" })
            .Effects(e => e.Toggle()) 
    %>

</asp:Content>