<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        CollapseDelay Tests</h2>
     <%= Html.Telerik().NumericTextBox()
             .Name("numerictextbox2")
             .MinValue(1)
             .MaxValue(8)
     %>
     <br />
     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox1")
           .MinValue(-10.2)
           .MaxValue(10000)
           .IncrementStep(1.44)
           .EmptyMessage("Enter text")
           %>
       <br />
     <%= Html.Telerik().NumericTextBox()
           .Name("numerictextbox")
           .MinValue(-10)
           .EmptyMessage("Enter text") %> 
       <br />
     <%= Html.Telerik().NumericTextBox()
           .Name("inputWithAttributes")
           .InputHtmlAttributes(new {
               maxlength = 3,
               style = "width: 40px",
               @readonly = "readonly",
               disabled = "disabled"
           }) %>
           <br />
     

    <div id="test" class="t-widget t-numerictextbox"><a title="Increase value" tabindex="-1" href="#" class="t-link t-icon t-arrow-up">Increment</a><a title="Decrease value" tabindex="-1" href="#" class="t-link t-icon t-arrow-down">Decrement</a><input value="" name="test" id="test-input" class="t-input" style="display: none;"></div>

    <script type="text/javascript">

        function getInput(selector) {
            return $(selector || "#numerictextbox").data("tTextBox");
        }

        function test_on_load_should_create_text_input_appended_to_the_div() {
            assertEquals(2, $('#numerictextbox1').find('.t-input').length);
        }

        function test_on_load_should_create_text_input_with_waterMarkText_value_if_no_value() {
            assertEquals("Enter text", $('#numerictextbox1').find('.t-input:first').attr("value"));
        }

        function test_on_load_should_hide_original_input() {
            assertFalse($('#numerictextbox1').find('.t-input:last').is(':visible'))
        }

        function test_setting_null_to_input_should_be_replaced_with_emptyText_when_creating_object() {
            $(':input[name$="test"]').val('null');
            
            $('#test').tTextBox({ digits: 2, groupSize: 3, negative: 1, type: 'numeric' });

            assertEquals('', $($('#test').data('tTextBox').element).find('.t-input:first').val());
        }

        function test_input_should_not_allow_entering_of_char() {
            var which = "65"; //'a'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.trigger({ type: "keypress",
                keyCode: which,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_allow_entering_digit() {
            var keyCode = "48"; //'0'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');
            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_increase_value_with_one_step_when_up_arrow_keyboard_is_clicked() {
            var keyCode = "38";
            var $input = $('#numerictextbox').find('> .t-input:first');

            getInput().value(null);
            $input.val("");
            
            $input.trigger({ type: "keydown",
                keyCode: keyCode
            });

            assertEquals(1, getInput().value());
        }

        function test_input_should_decrease_value_with_one_step_when_down_arrow_keyboard_is_clicked() {
            var keyCode = "40";
            var $input = $('#numerictextbox').find('> .t-input:first');

            getInput().value(null);
            $input.val("");

            $input.trigger({ type: "keydown",
                keyCode: keyCode
            });

            assertEquals(-1, getInput().value());
        }


        function test_input_should_increase_value_with_one_step_when_up_arrow_is_clicked() {
            var $input = $('#numerictextbox').find('> .t-input:first');
            var $button = $('#numerictextbox').find('> .t-arrow-up');
            
            getInput().value(null);
            $input.val("");

            $button.trigger({ type: "mousedown",
                which: 1
            });

            assertEquals(1, getInput().value());
        }

        function test_input_should_decrease_value_with_one_step_when_down_arrow_is_clicked() {
            var $input = $('#numerictextbox').find('> .t-input:first');
            var $button = $('#numerictextbox').find('> .t-arrow-down');

            getInput().value(null);
            $input.val("");

            $button.trigger({ type: "mousedown",
                which: 1
            });
            
            assertEquals(-1, getInput().value());
        }

        function test_input_should_allow_entering_system_keys() {
            var keyCode = "0"; //'system keys'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');
            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_allow_minus_in_first_position() {
            var keyCode = 45;  // minus
            var isDefaultPrevent = false;

            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('');
            $input.focus();

            $input.trigger({
                type: "keypress",
                keyCode: keyCode,
                preventDefault: function () {
                    isDefaultPrevent = true;
                }                
            });
            assertFalse(isDefaultPrevent);
        }

        function test_input_should_not_allow_minus_if_not_in_first_position() {
            var keyCode = 45;  // minus
            var isDefaultPrevent = false;

            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('1');

            $input.trigger({ type: "keypress",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_allow_110_as_decimal_separator() {
            var keyCode = "110";  // 'DEL' button
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('1');

            getInput().separator = ',';
            getInput().decimals['110'] = ',';

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function () {
                    isDefaultPrevent = true;
                }
            });

            assertFalse(isDefaultPrevent);
        }

        function test_input_should_allow_decimal_separator() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('1');

            getInput().separator = '.';

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertFalse(isDefaultPrevent);
        }

        function test_input_should_not_allow_decimal_separator_if_input_is_empty() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('');

            getInput().separator = '.'

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_input_should_not_allow_decimal_separator_if_it_is_already_entered() {
            var keyCode = "190";  // '.'
            var isDefaultPrevent = false;
            var $input = $('#numerictextbox').find('> .t-input:first');

            $input.val('1.');

            getInput().separator = '.'

            $input.trigger({ type: "keydown",
                keyCode: keyCode,
                preventDefault: function() {
                    isDefaultPrevent = true;
                }
            });

            assertTrue(isDefaultPrevent);
        }

        function test_if_change_input_value_manually_should_parse_entered_value_on_focus() {

            var input = getInput();

            input.value(null);

            var $input = $('#numerictextbox').find('> .t-input:first');
            $input.val('123');
            $input.focus();

            assertEquals(123, input.val);
        }

        function test_value_method_should_set_val_property() {

            var input = getInput();

            input.value(123);
            
            assertEquals('123', input.val.toString());
        }

        function test_if_input_value_is_changed_manually_should_be_able_to_parse_it_on_focus() {
            var input = getInput();

            input.value(123);

            $('#numerictextbox').find('> .t-input:first').val('100').focus();

            assertEquals(100, input.value());
        }

        function test_if_input_value_is_changed_manually_should_be_able_to_parse_it_on_down_button() {
            var input = getInput();

            input.value(123);
            
            $('#numerictextbox')
                .find('> .t-input:first').val('100').end()
                .find('> .t-arrow-down')
                    .trigger({
                        type: 'mousedown',
                        which: 1
                    });

            assertEquals(99, input.value());
        }

        function test_inRange_method_with_min_and_max_should_return_check_value() {
            var input = getInput();

            assertTrue(input.inRange(10, 0, 100));
        }

        function test_inRange_method_should_return_true_if_key_is_null() {
            var input = getInput();

            assertTrue(input.inRange(null, 0, 100));
        }

        function test_input_copies_attributes_when_initializing() {
            var input = $('#inputWithAttributes').find('> .t-input:first');

            assertTrue('disabled not copied', input.is('[disabled]'));
            assertNotNull('readonly not copied', input[0].getAttribute("readonly"));
            assertEquals('3', '' + input[0].getAttribute('maxlength'));
            assertEquals(40, input.width());
        }

        function test_input_does_not_copy_inexistent_maxlength() {
            var input = $('#numerictextbox').find('> .t-input:first');
            
            var maxLength = input.attr('maxlength');
            var nonexistentMaxLengthValue = $('<input type="text" />').attr('maxlength');
            assertEquals(nonexistentMaxLengthValue, maxLength);
        }

        function test_input_should_call_value_method_on_blur() {
            var isCalled = false;
            var input = getInput();
            var oldValue = input.value;

            input.value = function () { isCalled = true; }

            $('#numerictextbox-input-text').focus().blur();

            assertTrue(isCalled);

            input.value = oldValue;
        }

        function test_if_input_value_is_bigger_then_maxValue_blur_event_should_change_input_value_to_maxValue() {
            var $input = $('#numerictextbox2').find('> .t-input:first'),
                textBox = getInput('#numerictextbox2');

            var oldSetTimeOut = window.setTimeout;
            
            try {
                window.setTimeout = function (callback, time) { callback(); }

                $input.val(100)
                      .trigger({ type: 'keydown' })
                      .trigger({ type: 'blur' });

                assertEquals(8, textBox.maxValue);

                $input.trigger({ type: "blur" });

                assertEquals(parseInt($input.val()), textBox.maxValue);
            } finally {
                window.setTimeout = oldSetTimeOut;
            }
        }

        function test_if_input_value_is_bigger_then_maxValue_blur_event_should_change_remove_error_class() {
            var $input = $('#numerictextbox2').find('> .t-input:first');

            var oldSetTimeOut = window.setTimeout;
            
            try {
                window.setTimeout = function (callback, time) { callback(); }

                $input.val(100)
                      .trigger({ type: 'keydown' })
                      .trigger({ type: 'blur' });

                assertFalse($input.hasClass('t-state-error'));
            } finally {
                window.setTimeout = oldSetTimeOut;
            }
        }

        function test_if_input_value_is_smaller_then_minValue_blur_event_should_change_input_value_to_minValue() {
            var $input = $('#numerictextbox2').find('> .t-input:first'),
                textBox = getInput('#numerictextbox2');

            var oldSetTimeOut = window.setTimeout;
            
            try {
                window.setTimeout = function (callback, time) { callback(); }

                $input.val(0)
                      .trigger({ type: 'keydown' })
                      .trigger({ type: 'blur' });

                assertEquals(parseInt($input.val()), textBox.minValue);
            } finally {
                window.setTimeout = oldSetTimeOut;
            }
        }

        function test_if_input_value_is_smaller_then_minValue_blur_event_should_change_remove_error_class() {
            var $input = $('#numerictextbox2').find('> .t-input:first');

            var oldSetTimeOut = window.setTimeout;

            try {
                window.setTimeout = function (callback, time) { callback(); }

                $input.val(0)
                      .trigger({ type: 'keydown' })
                      .trigger({ type: 'blur' });

                assertFalse($input.hasClass('t-state-error'));

            } finally {
                window.setTimeout = oldSetTimeOut;
            }
        }
        
        function test_blur_method_with_null_set_default_text() {
            var $input = $('#numerictextbox2').find('> .t-input:first');
            var textbox = getInput('#numerictextbox2');
            var oldSetTimeOut = window.setTimeout;

            try {
                window.setTimeout = function (callback, time) { callback(); }
                $input.trigger({ type: 'blur' });

                textbox.value(null);
                assertEquals(textbox.text, $input.val());
            } finally {
                window.setTimeout = oldSetTimeOut;
            }
        }

        function test_blur_method_should_not_set_value_in_range() {
            var $input = $('#numerictextbox2').find('> .t-input:first');
            var textbox = getInput('#numerictextbox2');
            
            var oldSetTimeOut = window.setTimeout;

            try {
                textbox.minValue = null;
                textbox.maxValue = null;
                $input.val(1);
                window.setTimeout = function (callback, time) { callback(); }
                $input.trigger({ type: 'blur' });
                
                assertEquals(1, textbox.value());
            } finally {
                textbox.minValue = 1;
                textbox.maxValue = 8;
                window.setTimeout = oldSetTimeOut;
            }
        }

    </script>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:Content>
