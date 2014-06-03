<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox")
     %>

     <%= Html.Telerik().NumericTextBox()
             .Name("numerictextbox1")
             .MinValue(-1)
             .MaxValue(10)
     %>

    <script type="text/javascript">

        function getNumericTextBox(selector) {
            return $(selector || '#numerictextbox').data('tTextBox');
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

            assertFalse($links.first().data('events').mousedown[0].handler !== $.telerik.preventDefault);
            assertFalse($links.last().data('events').mousedown[0].handler !== $.telerik.preventDefault);
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

        function test_disable_method_should_add_state_disabled() {
            var numericTextBox = getNumericTextBox();
            numericTextBox.enable();
            numericTextBox.disable();
            
            assertTrue($('#numerictextbox').hasClass('t-state-disabled'));
        }

        function test_enable_method_should_remove_state_disabled() {
            var numericTextBox = getNumericTextBox();
            numericTextBox.disable();
            numericTextBox.enable();

            assertFalse($('#numerictextbox').hasClass('t-state-disabled'));
        }

        function test_enable_method_with_zero_value_should_not_remove_value() {
            var numericTextBox = getNumericTextBox();
            numericTextBox.value(0);
            numericTextBox.enable();

            assertEquals(0, numericTextBox.value());
        }   

        function test_disable_method_should_not_remove_value()
        {
            var numericTextBox = getNumericTextBox();
            numericTextBox.value(10);
            numericTextBox.disable();

            assertEquals(10, numericTextBox.value());
        }  

        function test_disable_method_should_set_empty_value()
        {
            var numericTextBox = getNumericTextBox();
            numericTextBox.value('');
            numericTextBox.disable();

            assertEquals(null, numericTextBox.value());
        }

        function test_value_method_should_return_null_if_value_is_not_between_minValue_and_maxValue() {
            var numericTextBox = getNumericTextBox('#numerictextbox1');
            numericTextBox.value(-2);
            assertEquals(null, numericTextBox.value());

            numericTextBox.value(11);
            assertEquals(null, numericTextBox.value());
        }

    </script>

</asp:Content>
