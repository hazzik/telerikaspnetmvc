<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Shortcuts</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>

    <script type="text/javascript">
        var editor;
        
        function setUp() {
            editor = $('#Editor').data('tEditor')
        }


        function test_find_shorcut_by_single_character() {
            var commands = {bold:{key:'B'}}

            var e = makeEvent();
                
            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertEquals('bold', command);
        }

        function test_find_shorcut_with_ctrl() {
            var commands = { bold: { ctrl: true, key: 'B' } };

            var e = makeEvent();
            e.ctrlKey = true;
            
            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertEquals('bold', command);
        }

        function test_should_not_find_shorcut_with_ctrl_when_ctrl_is_not_pressed() {
            var commands = { bold: { ctrl: true, key: 'B'} };

            var e = makeEvent();

            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertUndefined(command);
        }
        
        function test_should_find_shortcut_with_alt() {
            var commands = { bold: { alt: true, key: 'B' } };

            var e = makeEvent();
            e.altKey = true;

            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertEquals('bold', command);
        }
        
        function test_should_find_shortcut_with_shift() {
            var commands = { bold: { shift: true, key: 'B'} };

            var e = makeEvent();
            e.shiftKey  = true;

            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertEquals('bold', command);
        }

        function test_should_find_shortcut_with_all_modifiers_shift() {
            var commands = { bold: { shift: true, alt: true, ctrl: true, key: 'B'} };

            var e = makeEvent();

            e.shiftKey = true;
            e.altKey = true;
            e.ctrlKey = true;

            var command = editor.keyboard.toolFromShortcut(commands, e);

            assertEquals('bold', command);
        }

        function test_editor_dispatches_shortcuts_to_exec() {
            var command = null;
            
            editor.exec = function() {
                command = arguments[0];
            }

            var e = makeEvent();
            e.ctrlKey = true;
            e.type = 'keydown';
            
            $(editor.document).trigger(e);

            assertEquals('bold', command);
        }

        function test_ctrl_z_calls_undo() {
            var command = null;

            editor.exec = function () {
                command = arguments[0];
            }

            var e = makeEvent('Z'.charCodeAt(0));
            e.ctrlKey = true;
            e.type = 'keydown';

            $(editor.document).trigger(e);

            assertEquals('undo', command);
        }
        
        function test_ctrl_y_calls_redo() {
            var command = null;

            editor.exec = function() {
                command = arguments[0];
            }

            var e = makeEvent('Y'.charCodeAt(0));
            e.ctrlKey = true;
            e.type = 'keydown';

            $(editor.document).trigger(e);

            assertEquals('redo', command);
        }
        
        function test_shift_enter_calls_new_line() {
            var command = null;

            editor.exec = function () {
                command = arguments[0];
            }

            var e = makeEvent(13);
            e.shiftKey = true;
            e.type = 'keydown';

            $(editor.document).trigger(e);

            assertEquals('insertLineBreak', command);
        }

        function test_enter_calls_paragraph() {
            var command = null;

            editor.exec = function () {
                command = arguments[0];
            }

            var e = makeEvent(13);
            e.type = 'keydown';

            $(editor.document).trigger(e);

            assertEquals('insertParagraph', command);
        }

        function test_editor_prevents_default_if_shortcut_is_known() {
            editor.exec = function() {};
            var e = makeEvent();
            e.ctrlKey = true;
            e.type = 'keydown';
            
            $(editor.document).trigger(e);

            assertTrue(e.isDefaultPrevented());
        }

        function makeEvent(keyCode) {
            var e = new $.Event();
            e.keyCode = keyCode ? keyCode : 'B'.charCodeAt(0);
            e.shiftKey = false;
            e.ctrlKey = false;
            e.altKey = false;
            return e;
        }
    </script>
</asp:Content>
