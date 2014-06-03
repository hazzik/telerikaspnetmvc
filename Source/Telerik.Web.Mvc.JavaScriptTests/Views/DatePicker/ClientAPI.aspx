<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <%= Html.Telerik().DatePicker()
              .Name("DatePicker")
              .Effects(e => e.Toggle())
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

        function test_disable_method_should_add_state_disabled() {
            var datepicker = getDatePicker();

            datepicker.enable();
            datepicker.disable();

            assertTrue($('#DatePicker').hasClass('t-state-disabled'));
        }

        function test_enable_method_should_remove_state_disabled() {
            var datepicker = getDatePicker();

            datepicker.disable();
            datepicker.enable();

            assertFalse($('#DatePicker').hasClass('t-state-disabled'));
        }

        function test_open_method_should_call_dateView_open_method() {
            
            var datepicker = getDatePicker();
            var $input = datepicker.$input;

            var position = {
                offset: $input.offset(),
                outerHeight: $input.outerHeight(),
                outerWidth: $input.outerWidth(),
                zIndex: $.telerik.getElementZIndex($input[0])
            }

            var passedPos;

            var oldM = datepicker.dateView.open;
            datepicker.dateView.open = function(posisiton) { passedPos = posisiton; }

            datepicker.open(); 

            assertNotUndefined(passedPos);
            assertEquals(position.offset.top, passedPos.offset.top);
            assertEquals(position.offset.left, passedPos.offset.left);
            assertEquals(position.elemHeight, passedPos.elemHeight);
            assertEquals(position.outerWidth, passedPos.outerWidth);
            assertEquals(position.zIndex, passedPos.zIndex);

            datepicker.dateView.open = oldM;
        }

        function test_close_should_close_dateView() {
            var datepicker = getDatePicker();

            datepicker.open();
            datepicker.close();

            assertFalse(datepicker.dateView.isOpened());
        }

        function test_value_method_should_set_selectedValue_of_the_component() {
            var datepicker = getDatePicker();

            datepicker.value("10/10/2000");
            
            assertEquals('year', 2000, datepicker.selectedValue.year());
            assertEquals('month', 9, datepicker.selectedValue.month());
            assertEquals('day', 10, datepicker.selectedValue.date());
        }

        function test_value_method_should_call_dateView_value_method() {
            var isCalled = false;
            var datepicker = getDatePicker();
            var oldM = datepicker.dateView.value;

            datepicker.dateView.value = function () { isCalled = true; }
            
            datepicker.value("");

            assertTrue(isCalled);

            datepicker.dateView.value = oldM;
        }

        function test_min_method_should_set_minDate_property_and_call_dateView_min_method() {
            var isCalled = false;
            var datepicker = getDatePicker();

            var oldM = datepicker.dateView.min;
            datepicker.dateView.min = function () { isCalled = true; }

            datepicker.min('10/10/1904');

            assertTrue(isCalled);
            assertEquals('year', 1904, datepicker.minDate.year());
            assertEquals('month', 9, datepicker.minDate.month());
            assertEquals('day', 10, datepicker.minDate.date());

            datepicker.dateView.min = oldM;
        }
        
        function test_value_method_with_null_should_set_empty_text() {
            var datepicker = getDatePicker();
            var $input = datepicker.$input;

            datepicker.value(null);
            assertEquals('', $input.val());

            datepicker.value('');
            assertEquals('', $input.val());
        }

        function test_value_method_with_null_when_input_has_error_class_should_set_empty_text() {
            var datepicker = getDatePicker();
            var $input = datepicker.$input;

            datepicker.value('11/31/2010');
            datepicker.value(null);

            assertEquals('', $input.val());
        
            datepicker.value('11/31/2010');
            datepicker.value('');

            assertEquals('', $input.val());
        }

        function test_min_method_should_return_Date_object_of_minDate() {
            var datepicker = getDatePicker();

            assertEquals(0, datepicker.min() - datepicker.minDate.value);
        }

    </script>

</asp:Content>
