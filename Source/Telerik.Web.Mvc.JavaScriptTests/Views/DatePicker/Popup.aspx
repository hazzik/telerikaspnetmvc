<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="z-index: 10">
      <%= Html.Telerik().DatePicker()
          .Name("DatePicker1") %>
    </div>

    <%= Html.Telerik().DatePicker()
        .Name("DatePicker2") %>

    <script type="text/javascript">

        function test_popup_inherits_zIndex_from_component() {
            var datepicker = $('#DatePicker2').css('zIndex', 42).data('tDatePicker');

            datepicker.showPopup();

            var popupZIndex = $('.t-datepicker-calendar').parent().css('zIndex');

            assertEquals(43, parseInt(popupZIndex, 10));

            $('#DatePicker').css('zIndex', 'auto');
        }

        function test_popup_inherits_zIndex_from_parent_container_when_component_zIndex_is_not_set() {
            var datepicker = $('#DatePicker1').data('tDatePicker');

            datepicker.showPopup();

            var popupZIndex = $('.t-datepicker-calendar').parent().css('zIndex');

            assertEquals(11, parseInt(popupZIndex, 10));
        }
    </script>

</asp:Content>
