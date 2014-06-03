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

    var $t;
    var dv;
    var isRtl;
    var $input;
    var position;

    function setUp() {

        $t = $.telerik;
        $input = $('#testInput');
        isRtl = $('#testInput').closest('.t-rtl').length;

        dv = new $t.dateView({
            minValue: new $t.datetime(1800, 10, 10),
            maxValue: new $t.datetime(2100, 10, 10),
            selectedValue: null,
            effects: $t.fx.toggle.defaults(),
            isRtl: isRtl
        });

        dv.$calendar.data('tCalendar').stopAnimation = true;

        position = {
            offset: $input.offset(),
            outerHeight: $input.outerHeight(),
            outerWidth: $input.outerWidth(),
            zIndex: $t.getElementZIndex($input[0])
        }
    }

    function test_creating_dateView_will_create_sharedCalendar_if_it_is_not_created_yet() {
        var dateView = new $t.dateView({
            minValue: new $t.datetime(2000,10,10),
            maxValue: new $t.datetime(2010, 10, 10),
            selectedValue: null,
            effects: $t.fx.toggle.defaults(),
            isRtl: $('#testInput').closest('.t-rtl').length
        });

        assertNotEquals('$calendar is undefined', undefined, dateView.$calendar);
    }
    
    function test_second_dateView_should_use_shared_calendar() {
        var options = {
            minValue: new $t.datetime(2000, 10, 10),
            maxValue: new $t.datetime(2010, 10, 10),
            selectedValue: new $t.datetime(2005, 10, 10),
            effects: $t.fx.toggle.defaults(),
            isRtl: $('#testInput').closest('.t-rtl').length
        };

        var dateView = new $t.dateView(options);
        var dateView2 = new $t.dateView(options);

        assertEquals(dateView.$calendar, dateView2.$calendar);
    }

    function test_open_method_should_show_calendar_on_correct_position() {
        dv.open(position);

        var $animationContainer = dv.$calendar.parent();

        assertEquals('position', 'absolute', $animationContainer.css('position'));
        assertEquals('direction','ltr', $animationContainer.css('direction'));
        assertEquals('display', 'block', $animationContainer.css('display'));
        assertEquals('top', position.offset.top, $animationContainer.offset().top);
        assertEquals('left', position.offset.left, $animationContainer.offset().left);
        assertEquals('zindex', position.zIndex, $animationContainer.css('zIndex'));
    }

    function test_assignToNewDateView_should_update_sharedCalendar() {
        var isCalled = false;
        var options = {
            minValue: new $t.datetime(1900, 10, 10),
            maxValue: new $t.datetime(2100, 10, 10),
            selectedValue: new $t.datetime(2000, 0, 1),
            effects: $t.fx.toggle.defaults(),
            isRtl: $('#testInput').closest('.t-rtl').length
        };

        var calendar = dv.$calendar.data('tCalendar');

        var oldFunc = calendar.updateSelection;
        calendar.updateSelection = function () { isCalled = true; };
        
        var dateView = new $t.dateView(options);

        dateView._reassignSharedCalendar();

        assertTrue('Calendar update selection is not called', isCalled);
        assertEquals('selectedValue is not equal', 0, calendar.selectedValue.value - options.selectedValue.value);
        assertEquals('minValue is not equal', 0, calendar.minDate.value - options.minValue.value);
        assertEquals('maxValue is not equal', 0, calendar.maxDate.value - options.maxValue.value);

        calendar.updateSelection = oldFunc;
    }

    function test_isOpened_should_return_true_if_calendar_visible() {
        dv.close();
        dv.open(position);

        assertTrue(dv.isOpened());
    }

    function test_isOpened_should_return_false_if_calendar_is_not_visible() {
        dv.open(position);
        
        dv.close();

        assertFalse(dv.isOpened());
    }

    function test_value_method_should_set_selectedValue_of_dateView() {
        var date = new Date(2007, 7, 7);

        dv.value(date);

        assertEquals('selectedValue was not updated', 0, dv.selectedValue.value - date);
        assertEquals('focusedValue was not updated', 0, dv.focusedValue.value - date);
    }

    function test_value_method_should_set_selectedValue_to_null_if_value_is_null() {
        dv.value(null);

        var today = new $t.datetime();

        assertEquals('selectedValue is not set to null', null, dv.selectedValue);
        assertEquals('focusedValue is not set to today', today.year(), dv.focusedValue.year());
        assertEquals('focusedValue is not set to today', today.month(), dv.focusedValue.month());
        assertEquals('focusedValue is not set to today', today.date(), dv.focusedValue.date());
        assertEquals('calendar was not updated', null, dv._getCalendar().value());
    }

    function test_value_method_should_call_calendar_value_method_and_focusDate() {
        var isCalled = false;
        var isMethodCalled = false;
        var date = new Date(2007, 7, 7);

        var calendar = dv._getCalendar();
        var oldValue = calendar.value;
        calendar.value = function () { isCalled = true; }

        var oldFunc = $t.calendar.focusDate;
        $t.calendar.focusDate = function () { isMethodCalled = true; }

        dv.value(date);

        assertTrue('value method is not called', isCalled);
        assertTrue('focus method is not called', isMethodCalled);

        calendar.value = oldValue;
        $t.calendar.focusDate = oldFunc;
    }

    function test_minValue_method_should_set_minValue_close_and_null_sharedCalendar() {
        var isCalled = false;
        
        var date = new Date(1907, 7, 7);

        var method = dv._reassignSharedCalendar;

        dv._reassignSharedCalendar = function () { isCalled = true; }
        dv.open(position);
        dv.min(date);
        
        assertTrue('create calendar was not called', isCalled);
        assertEquals('minValue is not set', 0, dv.minValue.value - date);
        
        dv._reassignSharedCalendar = method;
    }

    function test_page_down_should_navigate_to_future() {
        
        var date = new Date(2004, 6, 28);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        dv.navigate({ keyCode: 34, preventDefault: function () { } });

        assertEquals('focused date is not one month in the futute', 0, dv.focusedValue.toDate() - new Date(2004, 7, 28));
    }

    function test_page_up_should_navigate_to_past() {

        var date = new Date(2004, 6, 28);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);
        
        dv.navigate({ keyCode: 33, preventDefault: function () { } });

        assertEquals('focused date is not one month earlier', 0, dv.focusedValue.toDate() - new Date(2004, 5, 28));
    }

    function test_home_button_should_focus_first_day_of_month() {

        var date = new Date(2004, 6, 28);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        dv.navigate({ keyCode: 36, preventDefault: function () { } });

        assertEquals('first day of the month is not focused', 0, dv.focusedValue.toDate() - new Date(2004, 6, 1));
    }

    function test_end_button_should_focus_last_day_of_month() {

        var date = new Date(2004, 6, 28);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        dv.navigate({ keyCode: 35, preventDefault: function () { } });

        assertEquals('last day of the month is not focused', 0, dv.focusedValue.toDate() - new Date(2004, 6, 31));
    }

    function test_alt_and_down_arrow_should_open_calendar() {

        var date = new Date(2004, 6, 28);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        dv.navigate({ keyCode: 35, altKey: true, preventDefault: function () { } });

        assertTrue(dv.$calendar.is(':visible'));
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_month() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2000, 5, 1);
        
        var calendar = configureCalendar(date, views.Month);
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        dv.navigate({ keyCode: 37, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past month', 0, calendar.viewedMonth.toDate() - resultDate);
    }


    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_year() {
        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1999, 6, 1);
        
        dv.focusedValue = new $.telerik.datetime(date);
        
        dv.open(position);

        var calendar = configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 37, ctrlKey: true, preventDefault: function () { } });
        
        assertEquals('does not navigate to the past year', 0, calendar.viewedMonth.toDate() - resultDate);
    }

    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_decade() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1990, 6, 1);
       
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 37, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past decade', 0, calendar.viewedMonth.toDate() - resultDate);
    }


    function test_if_datepicker_focused_ctrl_and_left_arrow_should_navigate_to_passed_century() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(1900, 6, 1);

        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 37, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past century', 0, calendar.viewedMonth.toDate() - resultDate);
    }


    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_future_month() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2000, 7, 1);

        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 39, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past century', 0, calendar.viewedMonth.toDate() - resultDate);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_year() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2001, 6, 1);
        
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 39, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past century', 0, calendar.viewedMonth.toDate() - resultDate);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_decade() {

        var date = new Date(2000, 6, 1);
        var resultDate = new Date(2010, 6, 1);
        
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 39, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past century', 0, calendar.viewedMonth.toDate() - resultDate);
    }

    function test_if_datepicker_focused_ctrl_and_right_arrow_should_navigate_to_passed_century() {

        var date = new Date(1999, 6, 1);
        var resultDate = new Date(2099, 6, 1);
        
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 39, ctrlKey: true, preventDefault: function () { } });

        assertEquals('does not navigate to the past century', 0, calendar.viewedMonth.toDate() - resultDate);
    }

    function test_if_datepicker_focused_ctrl_and_down_arrow_change_centuryView_to_decadeView() {

        var date = new Date(2000, 6, 1);

        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 40, ctrlKey: true, preventDefault: function () { } });

        assertEquals('currentView is not Decade', views.Decade, calendar.currentView.index);
    }

    function test_if_datepicker_focused_ctrl_and_down_arrow_change_decadeView_to_yearView() {

        var date = new Date(2000, 6, 1);

        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 40, ctrlKey: true, preventDefault: function () { } });

        assertEquals('currentView is not Year', views.Year, calendar.currentView.index);
    }

    function test_if_datepicker_focused_ctrl_and_up_arrow_change_view_to_wider_range() {

        var date = new Date(2000, 6, 1);
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        var calendar = configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 38, ctrlKey: true, preventDefault: function () { } });

        assertEquals('currentView is not Year', views.Year, calendar.currentView.index);
    }

    function test_left_key_should_focus_previous_date() {
        var date = new Date(2000, 10, 10);
        dv.focusedValue = new $.telerik.datetime(date);

        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 37, preventDefault: function () { } });

        var $element = $('.t-state-focus', dv.$calendar);

        assertEquals('focused date is not correct', '9', $element.find('.t-link').html());
        assertEquals(9, dv.focusedValue.date());
    }

    function test_right_key_should_focus_next_date() {

        var date = new Date(2000, 10, 10);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 39, preventDefault: function () { } });

        var $element = $('.t-state-focus', dv.$calendar);

        assertEquals('focused date is not correct', '11', $element.find('.t-link').html());
        assertEquals(11, dv.focusedValue.date());
    }

    function test_down_key_should_focus_next_week_day() {

        var date = new Date(2000, 10, 10);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        var $element = $('.t-state-focus', dv.$calendar);

        assertEquals('focused date is not correct', '17', $element.find('.t-link').html());
        assertEquals(17, dv.focusedValue.date());
    }

    function test_up_key_should_focus_previous_week_day() {

        var date = new Date(2000, 10, 10);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        var $element = $('.t-state-focus', dv.$calendar);

        assertEquals('focused date is not correct', '3', $element.find('.t-link').html());
        assertEquals(3, dv.focusedValue.date());
    }

    function test_left_key_should_navigate_to_prev_month_day() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 37, preventDefault: function () { } });

        assertEquals('month is not correct', 9, dv.focusedValue.month());
        assertEquals('day is not correct', 31, dv.focusedValue.date());
    }

    function test_right_key_should_navigate_to_next_month_day() {
        var date = new Date(2000, 10, 30);

        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 39, preventDefault: function () { } });

        assertEquals('month is not correct', 11, dv.focusedValue.month());
        assertEquals('day is not correct', 1, dv.focusedValue.date());
    }

    function test_up_key_should_navigate_to_prev_week_and_navigate() {
        var date = new Date(2000, 10, 4);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('month is not correct', 9, dv.focusedValue.month());
        assertEquals('day is not correct', 28, dv.focusedValue.date());
    }

    function test_down_key_should_navigate_to_next_week_and_navigate() {

        var date = new Date(2000, 10, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('month is not correct', 11, dv.focusedValue.month());
        assertEquals('day is not correct', 5, dv.focusedValue.date());
    }

    function test_left_key_should_navigate_to_prev_month() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 37, preventDefault: function () { } });

        assertEquals('month is not correct', 9, dv.focusedValue.month());
    }

    function test_right_key_should_navigate_to_next_month() {
        var date = new Date(2000, 10, 31);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Month);

        dv.navigate({ keyCode: 39, preventDefault: function () { } });

        assertEquals('month is not correct', 11, dv.focusedValue.month());
    }

    function test_up_key_should_focus_july() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('month is not correct', 6, dv.focusedValue.month());
    }

    function test_down_key_should_focus_november() {

        var date = new Date(2000, 6, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('month is not correct', 10, dv.focusedValue.month());
    }

    function test_up_key_should_focus_prev_november() {
        var date = new Date(2000, 2, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('month is not correct', 10, dv.focusedValue.month());
        assertEquals('year is not correct', 1999, dv.focusedValue.year());
    }

    function test_down_key_should_focus_next_march() {

        var date = new Date(2000, 10, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Year);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('month is not correct', 2, dv.focusedValue.month());
        assertEquals('year is not correct', 2001, dv.focusedValue.year());
    }

    function test_left_key_should_navigate_to_prev_year() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 37, preventDefault: function () { } });

        assertEquals('year is not correct', 1999, dv.focusedValue.year());
    }

    function test_right_key_should_navigate_to_next_year() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 39, preventDefault: function () { } });

        assertEquals('year is not correct', 2001, dv.focusedValue.year());
    }

    function test_up_key_should_focus_2000() {
        var date = new Date(2004, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('year is not correct', 2000, dv.focusedValue.year());
    }

    function test_down_key_should_focus_2004() {

        var date = new Date(2000, 6, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('year is not correct', 2004, dv.focusedValue.year());
    }

    function test_up_key_should_focus_prev_1996() {
        var date = new Date(2000, 2, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('year is not correct', 1996, dv.focusedValue.year());
    }

    function test_down_key_should_focus_next_2014() {

        var date = new Date(2010, 10, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Decade);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('year is not correct', 2014, dv.focusedValue.year());
    }

    function test_left_key_should_navigate_to_prev_decade() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 37, preventDefault: function () { } });

        assertEquals('year is not correct', 1990, dv.focusedValue.year());
    }

    function test_right_key_should_navigate_to_next_decade() {
        var date = new Date(2000, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 39, preventDefault: function () { } });

        assertEquals('year is not correct', 2010, dv.focusedValue.year());
    }

    function test_up_key_should_focus_2004_in_century_view() {
        var date = new Date(2044, 10, 1);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 38, preventDefault: function () { } });

        assertEquals('year is not correct', 2004, dv.focusedValue.year());
    }

    function test_down_key_should_focus_2044() {

        var date = new Date(2004, 6, 28);
        dv.focusedValue = new $.telerik.datetime(date);
        dv.open(position);

        configureCalendar(date, views.Century);

        dv.navigate({ keyCode: 40, preventDefault: function () { } });

        assertEquals('year is not correct', 2044, dv.focusedValue.year());
    }

    function test_enter_key_should_call_onChage_callback_if_view_is_month() {
        var passedValue;
        var isCalled = false;

        dv.onChange = function (value) { isCalled = true; }
        dv.open(position);

        dv.navigate({ keyCode: 13, preventDefault: function () { }, stopPropagation: function () { } });

        assertTrue(isCalled);
    }


    function configureCalendar(viewedMonth, currentView) {
        var calendar = dv._getCalendar();

        if (viewedMonth) calendar.viewedMonth = new $.telerik.datetime(viewedMonth);
        calendar.currentView = $.telerik.calendar.views[currentView];
        calendar.stopAnimation = true;

        return calendar;
    }

</script>

<input id="testInput" />

 <% Html.Telerik().ScriptRegistrar()
        .DefaultGroup(group => group.Add("telerik.common.js")
                                    .Add("telerik.calendar.js")
                                    .Add("telerik.datepicker.js")); 
 %>
</asp:Content>