<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Ajax loading</h2>
    <script type="text/javascript">
        var $t;
        function setUp() {
            $t = $.telerik;
        }

        function getTimePicker(selector) {
            return $('#TimePicker1' || selector).data('tTimePicker');
        }

        function test_open_method_should_fill_list() {
            var timePicker = getTimePicker();

            timePicker.timeView.dropDown.$items = null;

            timePicker.open();

            assertTrue(timePicker.timeView.dropDown.$items.length != 0);
        }

        function test_max_method_should_rebind_items_list() {
            var timepicker = getTimePicker();

            timepicker.max(new $t.datetime(2000, 1, 1, 3, 0, 0))

            var $items = timepicker.timeView.dropDown.$items;

            assertEquals($t.datetime.format(timepicker.maxValue.toDate(), timepicker.format), $($items[$items.length - 1]).text());
        }

        function test_min_method_should_rebind_items_list() {
            var timepicker = getTimePicker();
            
            timepicker.min(new $t.datetime(2000, 1, 1, 10, 0, 0))

            var $items = timepicker.timeView.dropDown.$items;

            assertEquals($t.datetime.format(timepicker.minValue.toDate(), timepicker.format), $($items[0]).text());
        }

        function test_open_method_with_empty_input_should_open_dropDownList() {
            var timePicker = getTimePicker();

            timePicker.open();

            assertTrue(timePicker.timeView.dropDown.isOpened());
        }

    </script>

    <%= Html.Telerik().TimePicker()
            .Name("TimePicker1")
            .Min(new DateTime(2010, 1, 1, 4, 0, 0))
            .Max(new DateTime(2010, 1, 14, 3, 0, 0))
            .Format("h:mm tt")
            .Interval(30)
            .Effects(e => e.Toggle())
            .Value(new DateTime(2010, 1, 1, 10, 0, 0))
    %>

</asp:Content>
