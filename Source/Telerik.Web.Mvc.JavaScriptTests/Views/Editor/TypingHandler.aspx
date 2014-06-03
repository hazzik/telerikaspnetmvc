<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        TypingHandler</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript">
        var TypingHandler;

        var editor;
        
        function setUp() {
            TypingHandler = $.telerik.editor.TypingHandler;
            editor = $('#Editor').data('tEditor');
            editor.focus();
        }

        function test_typing_handler_keydown_creates_start_restore_point_if_typing() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function () {},
                typingInProgress: function() { return false}
            };

            var handler = new TypingHandler(editor);

            handler.keydown();

            assertNotUndefined(handler.startRestorePoint);
        }

        function test_typing_handler_keydown_calls_startTyping() {
            var callback;

            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function () { callback = arguments[0] },
                typingInProgress: function() { return false}
            };

            var handler = new TypingHandler(editor);
            handler.keydown();
            assertNotUndefined(callback);
        }

        function test_typing_handler_keydown_returns_true() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function () {},
                typingInProgress: function() { return false}
            };

            var handler = new TypingHandler(editor);
            assertTrue(handler.keydown())
        }
        
        function test_typing_handler_keydown_returns_false_if_typing_is_in_progress() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function () {},
                typingInProgress: function() { return true}
            };

            var handler = new TypingHandler(editor);
            assertFalse(handler.keydown())
        }

        function test_typing_handler_keydown_returns_false_if_not_typing() {
            editor.keyboard = {
                isTypingKey: function () { return false; },
                startTyping: function () {},
                typingInProgress: function() { return true}
            };

            var handler = new TypingHandler(editor);
            assertFalse(handler.keydown())
        }

        function test_typing_handler_keyup_creates_end_restore_point_if_typing() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            var handler = new TypingHandler(editor);
            
            handler.keydown();
            
            editor.keyboard.typingInProgress = function() { return true};
            
            handler.keyup();

            assertNotUndefined(handler.endRestorePoint);
        }

        function test_typing_handler_keyup_creates_undo_command() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            var command;
            editor.undoRedoStack.push = function(){ command = arguments[0] };
            
            var handler = new TypingHandler(editor);
            
            handler.keydown();
            
            editor.keyboard.typingInProgress = function() { return true};
            
            handler.keyup();
            
            assertNotUndefined(command);
        }

        function test_typing_handler_keyup_returns_true_when_typing() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            var command;
            editor.undoRedoStack.push = function(){ command = arguments[0] };
            
            var handler = new TypingHandler(editor);
            
            handler.keydown();
            
            editor.keyboard.typingInProgress = function() { return true};
            
            assertTrue(handler.keyup())
        }
        
        function test_typing_handler_keyup_returns_false_when_not_typing() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            var command;
            editor.undoRedoStack.push = function(){ command = arguments[0] };
            
            var handler = new TypingHandler(editor);
            
            handler.keydown();
            
            assertFalse(handler.keyup())
        }

        function test_typing_handler_keyup_does_not_create_end_restore_point_if_not_typing_in_progress() {
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            var handler = new TypingHandler(editor);
            
            handler.keydown();

            handler.keyup();

            assertUndefined(handler.endRestorePoint);
        }

        function test_keydown_of_non_typing_key_removes_empty_pending_formats() {

            editor.value('foo<strong></strong>');
            
            editor.keyboard = {
                isTypingKey: function () { return false; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            editor.pendingFormats = [editor.body.childNodes[1]];

            var handler = new TypingHandler(editor);
            handler.keydown();

            assertEquals(1, editor.body.childNodes.length);
        }

        function test_keydown_removes_only_empty_pending_formats() {

            editor.value('foo<strong>bar</strong>');
            
            editor.keyboard = {
                isTypingKey: function () { return false; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            editor.pendingFormats = [editor.body.childNodes[1]];

            var handler = new TypingHandler(editor);
            handler.keydown();

            assertEquals(2, editor.body.childNodes.length);
        }

        function test_keydown_removes_nested_pending_formats() {

            editor.value('foo<strong><em></em></strong>');
            
            editor.keyboard = {
                isTypingKey: function () { return false; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            editor.pendingFormats = [editor.body.lastChild, editor.body.lastChild.firstChild];

            var handler = new TypingHandler(editor);
            handler.keydown();

            editor.body.normalize();
            assertEquals(1, editor.body.childNodes.length);
        }

        function test_keydown_skips_invalid_nodes_from_pending_formats() {

            editor.value('foo<strong></strong>');
            
            editor.keyboard = {
                isTypingKey: function () { return false; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            editor.pendingFormats = [{ parentNode: null, childNodes: [], innerHTML: '' }, editor.body.lastChild];

            var handler = new TypingHandler(editor);
            handler.keydown();

            assertEquals(1, editor.body.childNodes.length);
        }

        function test_keydown_does_not_remove_pending_formats_if_typing() {

            editor.value('foo<strong></strong>');
            
            editor.keyboard = {
                isTypingKey: function () { return true; },
                startTyping: function (callback) {this.callback = callback},
                typingInProgress: function() { return false},
                endTyping: function () {this.callback()}
            };

            editor.pendingFormats = [editor.body.lastChild];

            var handler = new TypingHandler(editor);
            handler.keydown();

            assertEquals(2, editor.body.childNodes.length);
        }

    </script>
</asp:Content>
