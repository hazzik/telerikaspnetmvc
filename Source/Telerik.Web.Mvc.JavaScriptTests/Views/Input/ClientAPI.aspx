<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox")
     %>

    <script type="text/javascript">

        function getNumericTextBox() {
            return $('#numerictextbox').data('tTextBox');
        }

        function test_disable_method_should_disable_fake_input() {
            var numericTextBox = getNumericTextBox();

            numericTextBox.enable();
            numericTextBox.disable();

            assertTrue($('#numerictextbox').find('.t-input').first().attr('disabled'));
        }

                function test_disable_method_should_unbind_click_event_of_toggle_button() {
                    var numericTextBox = getNumericTextBox();

                    numericTextBox.enable();
                    numericTextBox.disable();

                    var $links = $('#numerictextbox').find('.t-icon');
                    
                    assertUndefined($links.first().data('events').mousedown);
                    assertUndefined($links.last().data('events').mousedown);
                }

                function test_enable_method_should_enable_input() {
                    var numericTextBox = getNumericTextBox();

                    numericTextBox.disable();
                    numericTextBox.enable();

                    assertFalse($('#numerictextbox').find('.t-input').first().attr('disabled'));
                }

                function test_enable_method_should_bind_click_event_of_toggle_button() {
                    var numericTextBox = getNumericTextBox();
                    
                    numericTextBox.disable();
                    numericTextBox.enable();

                    var $links = $('#numerictextbox').find('.t-icon');

                    assertNotUndefined($links.first().data('events').mousedown);
                    assertNotUndefined($links.last().data('events').mousedown);
                }

    </script>

</asp:Content>
