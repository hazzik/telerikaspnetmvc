<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Client API</h2>
    
    <script type="text/javascript">
        var $t;

        function setUp() {
            $t = $.telerik;
        }

        function getTimePicker(selector) {
            return $(selector || '#TimePicker').data('tTimePicker');
        }

        function test_value_method_should_return_selectedValue_property() {
            var date = new $t.datetime(2000, 10, 10, 4, 0, 0, 909);
            var timepicker = getTimePicker();
            
            timepicker.selectedValue = date;

            var result = timepicker.value();

            assertEquals(0, date.value - result);
        }

        function test_value_method_should_set_internal_value_property_to_passed_Date_object() {
            var date = new Date(2000, 10, 10, 14, 0, 0);
            var timepicker = getTimePicker();
            
            timepicker.value(date);

            assertEquals(0, date - timepicker.selectedValue.toDate());
        }

        function test_value_method_should_set_internal_value_property_passed_datetime_object() {
            
            var date = new $t.datetime(2000, 10, 10, 14, 0, 0, 909);
            var timepicker = getTimePicker();

            timepicker.value(date);

            assertEquals(0, date.toDate() - timepicker.selectedValue.toDate());
        }

        function test_value_method_should_set_internal_selectedValue_property_to_parsed_string() {
            
            var date = new $t.datetime();         

            var timepicker = getTimePicker();
            timepicker.selectedValue = date;
            
            timepicker.value("2:20 PM");

            var result = timepicker.selectedValue;

            assertEquals("hours", 14, result.hours());
            assertEquals("minutes", 20, result.minutes());
            assertEquals("seconds", 0, result.seconds());
        }

        function test_value_method_should_set_value_if_min_is_less_then_max() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 10, 0, 0)

            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 6, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 18, 0, 0)

            timepicker.value(date);

            var result = timepicker.selectedValue;
            assertNotEquals("", timepicker.$input.val());
            assertEquals("hours", 10, result.hours());
            assertEquals("minutes", 0, result.minutes());
            assertEquals("seconds", 0, result.seconds());
        }

        function test_value_method_should_set_value_if_max_is_less_then_min() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 4, 0, 0)

            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 18, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 6, 0, 0)
            
            timepicker.value(date);

            var result = timepicker.selectedValue;
            assertEquals($t.datetime.format(date.toDate(), timepicker.format), timepicker.$input.val());
            assertEquals("hours", 4, result.hours());
            assertEquals("minutes", 0, result.minutes());
            assertEquals("seconds", 0, result.seconds());
        }

        function test_value_method_should_no_set_value_if_is_not_in_range_and_if_max_is_less_then_min() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 10, 0, 0)

            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 18, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 6, 0, 0)
            
            timepicker.value(date);

            assertEquals("", timepicker.$input.val());
            assertEquals(null, timepicker.selectedValue);
        }

        function test_value_method_should_no_set_value_if_is_not_in_range_and_if_min_is_less_then_max() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 4, 0, 0)
            
            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 6, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 18, 0, 0)

            timepicker.value(date);

            assertEquals("", timepicker.$input.val());
            assertEquals(null, timepicker.selectedValue);
        }

        function test_value_method_should_update_input_val() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 16, 0, 0)

            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 6, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 18, 0, 0)

            timepicker.value(date);

            assertEquals($t.datetime.format(date.toDate(), timepicker.format), timepicker.$input.val());
        }

        function test_parse_method_should_return_null_if_passed_value_is_null() {
            var timepicker = getTimePicker();

            var result = timepicker.parse(null);

            assertEquals(null, result);
        }

        function test_parse_method_should_return_datetime_object_created_from_passed_Date() {
            var timepicker = getTimePicker();

            var date = new Date(2000, 10, 10, 14, 14, 9);

            var result = timepicker.parse(date);

            assertEquals(date, result.toDate());
        }

        function test_parse_method_should_return_passed_datetime() {
            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 10, 10, 14, 14, 9);

            var result = timepicker.parse(date);

            assertEquals(date, result);
        }

        function test_parse_method_should_parse_passed_string() {
            var timepicker = getTimePicker();

            var date = "10:25 PM";

            var result = timepicker.parse(date);

            assertEquals("hours", 22, result.hours());
            assertEquals("minutes", 25, result.minutes());
            assertEquals("seconds", 0, result.seconds());
        }

        function test_value_method_should_select_item_depending_on_passed_date_object() {
            var timepicker = getTimePicker('#TimePicker2');

            timepicker.timeView.dropDown.$items = null;
            var date = new Date(1900, 10, 10, 8, 15, 0);
            
            timepicker.value(date);

            assertTrue(timepicker.timeView.dropDown.$items.eq(1).hasClass('t-state-selected'));
        }

        function test_value_method_should_set_value_10_if_minValue_4AM_and_max_time_3AM() {

            var timepicker = getTimePicker();

            var date = new $t.datetime(2000, 1, 1, 10, 0, 0)

            timepicker.$input.val('');
            timepicker.selectedValue = null;
            timepicker.minValue = new $t.datetime(2000, 1, 1, 4, 0, 0)
            timepicker.maxValue = new $t.datetime(2000, 1, 1, 3, 0, 0)

            timepicker.value(date);

            assertEquals($t.datetime.format(date.toDate(), timepicker.format), timepicker.$input.val());
        }

        function test_open_method_should_open_dropDown_and_call_value_method_input_val() {

            var timepicker = getTimePicker();
            
            timepicker.close();
            
            timepicker.open();

            assertTrue(timepicker.timeView.dropDown.isOpened());
        }

        function test_close_method_should_close_dropDown() {
            var timepicker = getTimePicker();

            timepicker.open();

            timepicker.close();

            assertFalse(timepicker.timeView.dropDown.isOpened());
        }

        function test_value_method_should_set_empty_string_to_input_and_deselect_items() {
            var timepicker = getTimePicker();
            timepicker.open();

            timepicker.value('10:30');

            timepicker.value(null);

            assertEquals('', timepicker.$input.val());
            assertEquals(0, timepicker.timeView.dropDown.$items.filter('t-state-selected').length);
        }

        function test_disable_method_should_disable_input() {
            var timepicker = getTimePicker();

            timepicker.enable();
            timepicker.disable();

            assertTrue($('#TimePicker').find('.t-input').attr('disabled'));
        }

        function test_disable_method_should_unbind_click_event_of_toggle_button() {
            var timepicker = getTimePicker();

            timepicker.enable();
            timepicker.disable();

            var $icon = $('#TimePicker').find('.t-icon');
            assertEquals(-1, $icon.data('events').click.toString().indexOf('e.preventDefault();'));
        }

        function test_enable_method_should_enable_input() {
            var timepicker = getTimePicker();

            timepicker.disable();
            timepicker.enable();

            assertFalse($('#TimePicker').find('.t-input').attr('disabled'));
        }

        function test_enable_method_should_bind_click_event_of_toggle_button() {
            var timepicker = getTimePicker();

            timepicker.disable();
            timepicker.enable();

            var $icon = $('#TimePicker').find('.t-icon');
            
            assertNotNull($icon.data('events').click);
        }

        function test_disable_method_should_add_state_disabled() {
            var timepicker = getTimePicker();

            timepicker.enable();
            timepicker.disable();

            assertTrue($('#TimePicker').hasClass('t-state-disabled'));
        }

        function test_enable_method_should_remove_state_disabled() {
            var timepicker = getTimePicker();

            timepicker.disable();
            timepicker.enable();

            assertFalse($('#TimePicker').hasClass('t-state-disabled'));
        }

        function test_internal_change_method_should_set_value_to_minValue_if_parsedValue_close_to_min() {
            var timepicker = getTimePicker();
            var oldMinValue = timepicker.minValue;
            var oldMaxValue = timepicker.maxValue;
            
            timepicker.minValue = new $t.datetime(2000, 10, 10, 13, 30, 0);
            timepicker.maxValue = new $t.datetime(2000, 10, 10, 18, 0, 0);
            
            timepicker._change('10:10 AM');

            assertEquals('selectedDate is not equal to MinValue', 0, timepicker.selectedValue.value - timepicker.minValue.value);

            timepicker.minValue = oldMinValue;
            timepicker.maxValue = oldMaxValue;
        }

        function test_internal_change_method_should_set_value_to_maxValue_if_parsedValue_close_to_max() {
            var timepicker = getTimePicker();
            var oldMinValue = timepicker.minValue;
            var oldMaxValue = timepicker.maxValue;

            timepicker.minValue = new $t.datetime(2000, 10, 10, 13, 30, 0);
            timepicker.maxValue = new $t.datetime(2000, 10, 10, 18, 0, 0);

            timepicker._change('7:10 PM');

            assertEquals('selectedDate is not equal to MaxValue', 0, timepicker.selectedValue.value - timepicker.maxValue.value);

            timepicker.minValue = oldMinValue;
            timepicker.maxValue = oldMaxValue;
        }

        function test_internal_change_method_should_set_value_to_maxValue_if_parsedValue_close_to_max_and_max_is_less_then_minValue() {
            var timepicker = getTimePicker();
            var oldMinValue = timepicker.minValue;
            var oldMaxValue = timepicker.maxValue;
            
            timepicker.minValue = new $t.datetime(2000, 10, 10, 22, 30, 0);
            timepicker.maxValue = new $t.datetime(2000, 10, 10, 10, 0, 0);
            
            timepicker._change('11:10 AM');

            assertEquals('selectedDate is not equal to MaxValue', 0, timepicker.selectedValue.value - timepicker.maxValue.value);

            timepicker.minValue = oldMinValue;
            timepicker.maxValue = oldMaxValue;
        }

        function test_internal_change_method_should_set_value_to_minValue_if_parsedValue_close_to_min_and_max_is_less_then_minValue() {
            var timepicker = getTimePicker();
            var oldMinValue = timepicker.minValue;
            var oldMaxValue = timepicker.maxValue;

            timepicker.minValue = new $t.datetime(2000, 10, 10, 22, 30, 0);
            timepicker.maxValue = new $t.datetime(2000, 10, 10, 10, 0, 0);

            timepicker._change('9:10 PM');

            assertEquals('selectedDate is not equal to MinValue', 0, timepicker.selectedValue.value - timepicker.minValue.value);

            timepicker.minValue = oldMinValue;
            timepicker.maxValue = oldMaxValue;
        }

        function test_internal_value_method_should_set_selectedValue_to_passed_val() {
            var timepicker = getTimePicker();

            var value = new $t.datetime(2000, 10, 10, 14, 14, 0);

            timepicker._value(value);

            assertEquals('selectedValue was not set', 0, value.value - timepicker.selectedValue.value);
        }

        function test_internal_value_method_should_set_selectedValue_to_null() {
            var timepicker = getTimePicker();

            timepicker._value(null);

            assertNull(timepicker.selectedValue);
        }

        function test_internal_value_method_should_call_timeView_value_method() {
            var passed;
            var timepicker = getTimePicker();
            var oldM = timepicker.timeView.value;
            var value = new $t.datetime(2000, 10, 10, 14, 14, 0);

            timepicker.timeView.value = function (val) { passed = val };

            timepicker._value(value);

            assertNotUndefined(passed);

            timepicker.timeView.value = oldM;
        }

        function test_internal_value_method_should_set_error_state_if_parameter_is_null_and_input_is_not_empty() {
            var timepicker = getTimePicker();

            timepicker.$input.val('invalid');

            timepicker._value(null);

            assertTrue('t-error-state is not applied', timepicker.$input.hasClass('t-state-error'));
        }

        function test_internal_value_method_should_remove_error_if_input_is_empty() {
            var timepicker = getTimePicker();

            timepicker.$input.val('');

            timepicker._value(null);

            assertFalse('t-error-state is not applied', timepicker.$input.hasClass('t-state-error'));
        }

        function test_internal_value_method_should_remove_error_if_correct_date_is_passed() {
            var timepicker = getTimePicker();

            var value = new $t.datetime(2000, 10, 10, 14, 14, 0);
            
            timepicker._value(value);

            assertFalse('t-error-state is not applied', timepicker.$input.hasClass('t-state-error'));
        }

    </script>

    <%= Html.Telerik().TimePicker()
            .Name("TimePicker")
            .Value(new Nullable<DateTime>())
            .Effects(e => e.Toggle()) 
    %>

    <%= Html.Telerik().TimePicker()
            .Name("TimePicker2")
            .Interval(15)
            .Value(new Nullable<DateTime>())
            .Min(new DateTime(2000,10,10, 8, 0, 0))
            .Max(new DateTime(2000, 10, 10, 18, 0, 0))
    %>

</asp:Content>
