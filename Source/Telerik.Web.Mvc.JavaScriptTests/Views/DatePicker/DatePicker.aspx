<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Date parsing</h2>

<script type="text/javascript">
    var views = {
        Month: 0,
        Year: 1,
        Decade: 2,
        Century: 3
    }

    function getDatePicker() {
        return $('#DatePicker').data('tDatePicker');
    }

    function test_calendar_with_toggle_animation_should_hide_animation_container() {
        getDatePicker().open();

        $(document.documentElement).mousedown();
        
        assertFalse(getDatePicker().dateView.isOpened());
    }
    
    function test_parse_date_less_than_min_date_should_return_null() {
        var datepicker = getDatePicker()
        datepicker.format = "MM/dd/yyyy";

        datepicker.value("01/23/1400");
        assertNull(datepicker.selectedValue);
    }

    function test_parse_date_bigger_than_min_date_should_return_null() {
        var datepicker = getDatePicker()
        datepicker.format = "MM/dd/yyyy";

        datepicker.value("01/23/2500");
        assertNull(datepicker.selectedValue);
    }

    function test_parse_date_equal_to_min_should_parsed_correctly() {
        var datepicker = getDatePicker()
        datepicker.format = "MM/dd/yyyy";

        datepicker.value("1/1/1900"); //in FF new Date(1899, 11, 31) == 1.1.1900
        assertTrue(isValidDate(1900, 1, 1, datepicker.selectedValue.toDate()));
    }

    function test_parse_date_equal_to_max_should_parsed_correctly() {

        var datepicker = getDatePicker()
        datepicker.format = "MM/dd/yyyy";

        datepicker.value("1/1/2100");
        assertTrue(isValidDate(2100, 1, 1, datepicker.selectedValue.toDate()));
    }

    function test_should_parse_input_value_even_calendar_is_opened() {
        var datepicker = getDatePicker();
        var date = new Date(2004, 6, 28);

        datepicker.format = 'M/d/yyyy';
        datepicker.dateView.focusedValue = new $.telerik.datetime(date);
        datepicker.open();

        var input = $('#DatePicker').find('.t-input');
        input.val("10/10/2000");
        
        input.trigger({ type: "keydown", keyCode: 13 }); //trigger enter to parse value.

        assertEquals(2000, datepicker.dateView.focusedValue.year());
    }

    function test_if_input_value_is_bigger_then_maxDate_blur_event_should_change_input_value_to_maxDate() {
        var $input = $('#DatePicker').find('.t-input');
        var datepicker = getDatePicker();
        
        $input.val('1/1/2410');;
        datepicker._change($input.val());
        
        assertEquals(datepicker.parse($input.val()).toDate(), datepicker.maxDate.toDate());
    }

    function test_if_input_value_is_bigger_then_maxDate_blur_event_should_change_remove_error_class() {
        var $input = $('#DatePicker').find('.t-input');
        var datepicker = getDatePicker();

        $input.val('1/1/2410');
        datepicker._change('1/1/2410');

        assertFalse($input.hasClass('t-state-error'));
    }

    function test_if_input_value_is_smaller_then_minDate_blur_event_should_change_input_value_to_minDate() {
        var $input = $('#DatePicker').find('.t-input');
        var datepicker = getDatePicker();
        
        $input.val('1/1/1500');
        datepicker._change($input.val());

        assertEquals(datepicker.parse($input.val()).toDate(), datepicker.minDate.toDate());
    }

    function test_if_input_value_is_smaller_then_minDate_blur_event_should_change_remove_error_class() {
        var $input = $('#DatePicker').find('.t-input');
        var datepicker = getDatePicker();

        $input.val('1/1/1500');
        datepicker._change('1/1/1500');

        assertFalse($input.hasClass('t-state-error'));
    }

    function test_datepicker_should_call_change_and_close_internal_methods_if_document_mousedown() {
        var isCloseCalled = false;
        var isChangeCalled = false;
        var datepicker = getDatePicker();

        var oldClose = datepicker._close;
        var oldChange = datepicker._change;

        datepicker._close = function () { isCloseCalled = true; };
        datepicker._change = function () { isChangeCalled = true; };

        datepicker.open();

        $(document.documentElement).trigger('mousedown');

        assertTrue('_close was not called', isCloseCalled);
        assertTrue('_change was not called', isChangeCalled);

        datepicker._close = oldClose;
        datepicker._change = oldChange;
    }

    function isValidDate(year, month, day, date) {
        var isValid = true;

        if (year != date.getFullYear())
            isValid = false;
        else if (month != date.getMonth() + 1)
            isValid = false;
        else if (day != date.getDate())
            isValid = false;

        return isValid;
    }  
        
</script>

 <%= Html.Telerik().DatePicker().Name("DatePicker")
                   .Effects(e=>e.Toggle())
                   .MinDate(new DateTime(1600, 1, 1))
                   .MaxDate(new DateTime(2400, 1, 1))
 %>
</asp:Content>