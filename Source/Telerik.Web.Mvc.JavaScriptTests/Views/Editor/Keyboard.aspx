<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Keyboard</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript">
        var Keyboard;

        var editor;
        function setUp() {
            Keyboard = $.telerik.editor.Keyboard;
            editor = $('#Editor').data('tEditor');
        }

        function test_editor_has_keyboard() {
            assertNotUndefined(editor.keyboard);
        }

        function test_keydown_calls_clearTimeout() {
            
            var originalKeyDown = editor.keyboard.keydown;
            var originalClearTimeout = editor.keyboard.clearTimeout;
            try {
                editor.keyboard.keydown = function () {
                }
                var called = false;
                editor.keyboard.clearTimeout = function() {
                    called = true;
                }
                var e = new $.Event();
                e.keyCode = 18;
                e.type = 'keydown';
                
                $(editor.document).trigger(e);

                assertTrue(called);
            } finally {
                editor.keyboard.keydown = originalKeyDown;
                editor.keyboard.clearTimeout = originalClearTimeout;
            }
        }
        function test_keydown_calls_keyboard_keydown() {
            var originalKeyDown = editor.keyboard.keydown;
            var originalClearTimeout = editor.keyboard.clearTimeout;
            try {
                
                var called = false;
                
                editor.keyboard.clearTimeout = function () {
                }

                editor.keyboard.keydown = function () {
                    called = true;
                }
                var e = new $.Event();
                e.keyCode = 18;
                e.type = 'keydown';

                $(editor.document).trigger(e);

                assertTrue(called);
            } finally {
                editor.keyboard.keydown = originalKeyDown;
                editor.keyboard.clearTimeout = originalClearTimeout;
            }
        }
        function test_keyup_calls_keyboard_keyup() {
            var originalKeyUp = editor.keyboard.keyup;
            try {
                var called = false;

                editor.keyboard.keyup = function () {
                    called = true;
                }
                var e = new $.Event();
                e.keyCode = 18;
                e.type = 'keyup';

                $(editor.document).trigger(e);

                assertTrue(called);
            } finally {
                editor.keyboard.keyup = originalKeyUp;
            }
        }

        function test_isTypingKey_returns_true_if_char_is_typed() {
            var e = {
                keyCode: 'B'.charCodeAt(0)
            };

            var keyboard = new Keyboard();

            assertTrue(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_true_if_backspace_is_typed() {
            var e = {
                keyCode: 8
            };

            var keyboard = new Keyboard();

            assertTrue(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_true_if_delete_is_typed() {
            var e = {
                keyCode: 46
            };

            var keyboard = new Keyboard();

            assertTrue(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_ctrl_and_char_is_typed() {
            var e = {
                keyCode: 'B'.charCodeAt(0),
                ctrlKey: true
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_alt_and_char_is_typed() {
            var e = {
                keyCode: 'B'.charCodeAt(0),
                altKey: true
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_true_if_shift_and_char_is_typed() {
            var e = {
                keyCode: 'B'.charCodeAt(0),
                shiftKey: true
            };

            var keyboard = new Keyboard();

            assertTrue(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_shift_and_delete_is_typed() {
            var e = {
                keyCode: 46,
                shiftKey: true
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_ctrl_and_delete_is_typed() {
            var e = {
                keyCode: 46,
                ctrlKey: true
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_alt_and_delete_is_typed() {
            var e = {
                keyCode: 46,
                altKey: true
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_ctrl_is_typed() {
            var e = {
                keyCode: 17
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_shift_is_typed() {
            var e = {
                keyCode: 16
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_isTypingKey_returns_false_if_alt_is_typed() {
            var e = {
                keyCode: 18
            };

            var keyboard = new Keyboard();

            assertFalse(keyboard.isTypingKey(e));
        }

        function test_typingInProgress_returns_false_initially() {
            var keyboard = new Keyboard();

            assertFalse(keyboard.typingInProgress());
        }

        function test_typingInProgress_returns_true_after_startTyping() {
            var keyboard = new Keyboard();

            keyboard.startTyping();
            assertTrue(keyboard.typingInProgress());
        }

        function test_typingInProgress_returns_false_after_endTyping() {
            var keyboard = new Keyboard();
            var original = window.setTimeout;

            keyboard.startTyping(function () { });
            try {

                window.setTimeout = function () {
                    arguments[0]();
                }

                keyboard.endTyping();
                assertFalse(keyboard.typingInProgress());
            } finally {
                window.setTimeout = original;
            }
        }
        function test_typingInProgress_does_not_immediately_return_false_after_endTyping() {
            var keyboard = new Keyboard();
            var original = window.setTimeout;

            keyboard.startTyping(function () { });
            try {

                window.setTimeout = function () {
                }

                keyboard.endTyping();
                assertTrue(keyboard.typingInProgress());
            } finally {
                window.setTimeout = original;
            }
        }
        
        function test_typingInProgress_return_false_after_forced_endTyping() {
            var keyboard = new Keyboard();
            var original = window.setTimeout;

            keyboard.startTyping(function () { });
            try {

                window.setTimeout = function () {
                }

                keyboard.endTyping(true);
                assertFalse(keyboard.typingInProgress());
            } finally {
                window.setTimeout = original;
            }
        }

        function test_end_typing_calls_callback_specified_in_start_typing() {
            var keyboard = new Keyboard();
            var callbackInvoked = false;

            var original = window.setTimeout;
            try {

                window.setTimeout = function () {
                    arguments[0]();
                }

                keyboard.startTyping(function () {
                    callbackInvoked = true;
                });

                assertFalse(callbackInvoked);
                keyboard.endTyping();
                assertTrue(callbackInvoked);
            } finally {
                window.setTimeout = original;
            }
        }
        
        function test_endTyping_creates_timeout() {
            var original = window.setTimeout;

            try {
                var setTimeoutArgument;
                window.setTimeout = function () {
                    setTimeoutArgument = arguments[0];
                }

                var keyboard = new Keyboard();
                var callback = function () { }
                keyboard.startTyping(callback);
                keyboard.endTyping();
                assertNotUndefined(setTimeoutArgument);
            } finally {
                window.setTimeout = original;
            }
        }
        function test_endTyping_calls_clear_timeout() {
            var original = window.setTimeout;
            try {
                window.setTimeout = function () {
                }

                var keyboard = new Keyboard();
                var called = false;
                keyboard.clearTimeout = function() {
                    called = true;
                }
                keyboard.endTyping();
                assertTrue(called);
            } finally {
                window.setTimeout = original;
            }
        }


        function test_isModifierKey_returns_true_for_ctrl() {
            var keyboard = new Keyboard();
            assertTrue(keyboard.isModifierKey({ keyCode: 17 }));
        }

        function test_isModifierKey_returns_true_for_shift() {
            var keyboard = new Keyboard();
            assertTrue(keyboard.isModifierKey({ keyCode: 16 }));
        }

        function test_isModifierKey_returns_true_for_alt() {
            var keyboard = new Keyboard();
            assertTrue(keyboard.isModifierKey({ keyCode: 18 }));
        }

        function test_isModifierKey_returns_false_for_character() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 'B'.charCodeAt(0) }));
        }

        function test_isModifierKey_returns_false_for_character_and_ctrl() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 'B'.charCodeAt(0), ctrlKey: true }));
        }

        function test_isModifierKey_returns_false_for_ctrl_and_shift() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 17, shiftKey: true }));
        }

        function test_isModifierKey_returns_false_for_ctrl_and_alt() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 17, altKey: true }));
        }

        function test_isModifierKey_returns_false_for_shift_and_ctrl() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 16, ctrlKey: true }));
        }

        function test_isModifierKey_returns_false_for_shift_and_alt() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 16, altKey: true }));
        }

        function test_isModifierKey_returns_false_for_alt_and_ctrl() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 18, ctrlKey: true }));
        }

        function test_isModifierKey_returns_false_for_alt_and_shift() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isModifierKey({ keyCode: 18, shiftKey: true }));
        }

        function test_isSystem_returns_true_for_ctrl_and_del() {
            var keyboard = new Keyboard();
            assertTrue(keyboard.isSystem({ keyCode: 46, ctrlKey: true }));
        }
        
        function test_isSystem_returns_false_for_ctrl_and_del_and_alt() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isSystem({ keyCode: 46, ctrlKey: true, altKey: true }));
        }

        function test_isSystem_returns_false_for_ctrl_and_del_and_shift() {
            var keyboard = new Keyboard();
            assertFalse(keyboard.isSystem({ keyCode: 46, ctrlKey: true, shiftKey: true }));
        }

        function test_keydown_calls_the_keydown_method_of_the_handlers() {
            var calls = 0;
            var handlers = [
                { keydown: function () { calls++ } },
                { keydown: function () { calls++ } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keydown();
            assertEquals(2, calls)
        }

        function test_keydown_calls_the_keydown_breaks_if_some_handler_returns_true() {
            var calls = 0;
            var handlers = [
                { keydown: function () { calls++; return true; } },
                { keydown: function () { calls++ } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keydown();
            assertEquals(1, calls)
        }

        function test_keydown_passes_the_argument_to_handler() {
            var e = {};
            var arg;
            var handlers = [
                { keydown: function () { arg = arguments[0] } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keydown(e);
            assertEquals(e, arg)
        }

        function test_keyup_calls_the_keydup_method_of_the_handlers() {
            var calls = 0;
            var handlers = [
                { keyup: function () { calls++ } },
                { keyup: function () { calls++ } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keyup();
            assertEquals(2, calls)
        }

        function test_keyup_calls_the_keyup_breaks_if_some_handler_returns_true() {
            var calls = 0;
            var handlers = [
                { keyup: function () { calls++; return true; } },
                { keyup: function () { calls++ } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keyup();
            assertEquals(1, calls)
        }

        function test_keyup_passes_the_argument_to_handler() {
            var e = {};
            var arg;
            var handlers = [
                { keyup: function () { arg = arguments[0] } }
            ];

            var keyboard = new Keyboard(handlers);
            keyboard.keyup(e);
            assertEquals(e, arg)
        }
    </script>
</asp:Content>
