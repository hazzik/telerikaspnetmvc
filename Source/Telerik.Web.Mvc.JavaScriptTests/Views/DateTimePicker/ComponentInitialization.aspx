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

        function test_initialized_DateTimePicker_should_create_timeView_with_proper_settings() {
            var datetimepicker = getDateTimePicker();
            var timeView = datetimepicker.timeView;

            assertNotUndefined('timeView is undefined', timeView);
            assertNotUndefined(timeView.onChange);
            assertNotUndefined(timeView.onNavigateWithOpenPopup);
            assertEquals('not correct effects are set', datetimepicker.effects.list[0].name, timeView.effects.list[0].name);
            assertEquals('not correct dropDownAttr is set', datetimepicker.dropDownAttr, timeView.dropDownAttr);
            assertEquals('not correct timeFormat is set', datetimepicker.timeFormat, timeView.format);
            assertEquals('not correct interval is set', datetimepicker.interval, timeView.interval);
            assertEquals('not correct minValue is set', 0, datetimepicker.minValue.value - timeView.minValue.value);
            assertEquals('not correct maxValue is set', 0, datetimepicker.maxValue.value - timeView.maxValue.value);
        }

        function test_initialized_DateTimePicker_should_create_dateView_with_proper_settings() {
            var datetimepicker = getDateTimePicker();
            var dateView = datetimepicker.dateView;

            assertNotUndefined('timeView is undefined', dateView);
            assertNotUndefined(dateView.onChange);
            assertEquals('not correct effects are set', datetimepicker.effects.list[0].name, dateView.effects.list[0].name);
            assertEquals('not correct minValue is set', 0, datetimepicker.minValue.value - dateView.minValue.value);
            assertEquals('not correct maxValue is set', 0, datetimepicker.maxValue.value - dateView.maxValue.value);
            assertEquals('not correct maxValue is set', 0, datetimepicker.selectedValue.value - dateView.selectedValue.value);
        }
    </script>
    
    <%= Html.Telerik().DateTimePicker()
            .Name("DateTimePicker")
            .Effects(e => e.Toggle())
            .Value(DateTime.Now)
    %>

</asp:Content>