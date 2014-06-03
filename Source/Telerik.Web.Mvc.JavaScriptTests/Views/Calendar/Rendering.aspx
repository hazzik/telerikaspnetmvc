<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().Calendar()
            .Name("Calendar1")
    %>

    <%= Html.Telerik().Calendar()
            .Name("Calendar2")
            .MinDate(new DateTime(2010, 10, 1))
            .Value(new DateTime(2010, 10, 1))
    %>

    <%= Html.Telerik().Calendar()
            .Name("Calendar3")
            .MaxDate(new DateTime(2010, 10, 30))
            .Value(new DateTime(2010, 10, 1))
    %>

    <script type="text/javascript">

        function getCalendar(selector) {
            return $(selector || "#Calendar1").data("tCalendar");
        }

        function test_calendar_should_render_previous_button() {
            var calendar = getCalendar();
            
            assertTrue($('.t-header .t-link', calendar.element).hasClass('t-nav-prev'));
        }

        function test_calendar_should_render_disable_previous_button() {
            var calendar = getCalendar("#Calendar2");

            assertTrue($('.t-header .t-nav-prev', calendar.element).hasClass('t-state-disabled'));
        }

        function test_calendar_should_render_navigation_button() {
            var calendar = getCalendar();

            assertTrue($('.t-header .t-link', calendar.element).hasClass('t-nav-fast'));
        }

        function test_calendar_should_render_next_button() {
            var calendar = getCalendar();

            assertTrue($('.t-header .t-link', calendar.element).hasClass('t-nav-next'));
        }

        function test_calendar_should_render_disable_next_button() {
            var calendar = getCalendar("#Calendar3");

            assertTrue($('.t-header .t-nav-next', calendar.element).hasClass('t-state-disabled'));
        }
    
    </script>

</asp:Content>