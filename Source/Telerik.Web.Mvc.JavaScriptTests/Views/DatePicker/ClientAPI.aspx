<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <%= Html.Telerik().DatePicker()
          .Name("DatePicker")
  %>
    <script type="text/javascript">

        function getDatePicker() {
            return $('#DatePicker').data('tDatePicker');
        }

        function test_disable_method_should_disable_input() {
            var datepicker = getDatePicker();

            datepicker.enable();
            datepicker.disable();

            assertTrue($('#DatePicker').find('.t-input').attr('disabled'));
        }

        function test_disable_method_should_unbind_click_event_of_toggle_button() {
            var datepicker = getDatePicker();

            datepicker.enable();
            datepicker.disable();

            var $icon = $('#DatePicker').find('.t-icon');
            assertEquals(-1, $icon.data('events').click.toString().indexOf('e.preventDefault();'));
        }

        function test_enable_method_should_enable_input() {
            var datepicker = getDatePicker();

            datepicker.disable();
            datepicker.enable();

            assertFalse($('#DatePicker').find('.t-input').attr('disabled'));
        }

        function test_enable_method_should_bind_click_event_of_toggle_button() {
            var datepicker = getDatePicker();

            datepicker.disable();
            datepicker.enable();

            var $icon = $('#DatePicker').find('.t-icon');
            
            assertNotNull($icon.data('events').click);
        }

    </script>

</asp:Content>
