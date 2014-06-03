<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="position:relative; z-index: 10">
      <%= Html.Telerik().DatePicker()
              .Name("DatePicker1")
      %>
    </div>

    <%= Html.Telerik().DatePicker()
        .Name("DatePicker2") 
        .HtmlAttributes(new { style="position: relative"})
            %>

    <script type="text/javascript">

        function test_popup_inherits_zIndex_from_component() {
            var datepicker = $('#DatePicker2').css('z-index', 42).data('tDatePicker');

            datepicker.open();

            var popupZIndex = $('.t-datepicker-calendar').parent().css('z-index');
            
            assertEquals(43, parseInt(popupZIndex, 10));

            $('#DatePicker').css('z-index', 'auto');
        }

        function test_popup_inherits_zIndex_from_parent_container_when_component_zIndex_is_not_set() {
            var datepicker = $('#DatePicker1').data('tDatePicker');

            datepicker.showPopup();

            var popupZIndex = $('.t-datepicker-calendar').parent().css('z-index');

            assertEquals(11, parseInt(popupZIndex, 10));
        }
    </script>

</asp:Content>
