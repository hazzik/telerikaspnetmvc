<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SystemHandler</h2>
     <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript">
        
        var SystemHandler;

        var editor;
        
        function setUp() {
            SystemHandler = $.telerik.editor.SystemHandler;
            editor = $('#Editor').data('tEditor');
            editor.focus();
        }

        function test_keydown_calls_endTyping_if_typing_in_progress() {
             var force = false;
             editor.keyboard = {
                isModifierKey: function() { return true},
                endTyping: function () { force = arguments[0]; },
                startTyping: function () {},
                typingInProgress: function() { return true}
            };
            var handler = new SystemHandler(editor);
            handler.keydown();

            assertTrue(force);
        }
        
        function test_keydown_does_not_call_endTyping_if_not_modifier_key() {
            var called = false;
            editor.keyboard = {
                isModifierKey: function () { return false },
                isSystem:function() { return false},
                endTyping: function () { called = true },
                startTyping: function () { },
                typingInProgress: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.keydown();

            assertFalse(called);
        }
        
        function test_keydown_does_not_call_endTyping_if_typing_not_in_progress() {
            var called = false;
            editor.keyboard = {
                isModifierKey: function() { return true},
                endTyping: function () { called = true; },
                startTyping: function () { },
                typingInProgress: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.keydown();

            assertTrue(called);
        }


        function test_keydown_if_modifier_key_creates_start_restore_point() {
            editor.keyboard = {
                isModifierKey: function() { return true},
                typingInProgress: function () { return false }
            };

            var handler = new SystemHandler(editor);
            handler.keydown();

            assertNotUndefined(handler.startRestorePoint);
        }
        
        function test_keydown_returns_true_if_modifier_key() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false }
            };

            var handler = new SystemHandler(editor);
            assertTrue(handler.keydown())
        }

        function test_keydown_if_system_command_and_changed_creates_end_restore_point() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem:function(){ return true}
            };

            var handler = new SystemHandler(editor);
            handler.changed = function() {
                return true;
            }
            
            handler.keydown();
            editor.keyboard.isModifierKey = function() { return false};
            handler.keydown();

            assertNotUndefined(handler.endRestorePoint);
        }
        
        function test_keydown_if_system_command_and_changed_sets_start_restore_point_to_end_restore_point() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.changed = function () {
                return true;
            }

            handler.keydown();
            editor.keyboard.isModifierKey = function () { return false };
            handler.keydown();

            assertEquals(handler.endRestorePoint, handler.startRestorePoint);
        }
        function test_keydown_returns_true_if_system_command_and_changed() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.changed = function () {
                return true;
            }
            handler.keydown();
            editor.keyboard.isModifierKey = function() { return false};
            assertTrue(handler.keydown());
        }

        function test_keydown_creates_undo_command_if_system_command_and_changed() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.changed = function () {
                return false;
            }
            var command;
            
            editor.undoRedoStack.push = function() {
                command = arguments[0];
            }
            handler.keydown()
            editor.keyboard.isModifierKey = function() { return false};
            handler.changed = function () {
                return true;
            }
            handler.keydown()
            
            assertNotUndefined(command);
        }

        function test_changed_returns_false_if_editor_contents_remain_the_same() {
            editor.keyboard = {
                isModifierKey: function() { return true},
                typingInProgress: function () { return false }
            };

            var handler = new SystemHandler(editor);
            handler.keydown();

            assertFalse(handler.changed());
        }

        function test_changed_returns_false_if_editor_contents_changed() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false }
            };

            var handler = new SystemHandler(editor);
            handler.keydown();
            editor.body.innerHTML = 'foo';
            assertTrue(handler.changed());
        }


        function test_keyup_creates_undo_command_if_system_command_and_changed() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.changed = function () {
                return false;
            }
            var command;
            
            editor.undoRedoStack.push = function() {
                command = arguments[0];
            }
            
            handler.keydown()
            editor.keyboard.isModifierKey = function() { return false};
            handler.keydown()
            handler.changed = function () {
                return true;
            }
            
            handler.keyup()
            
            assertNotUndefined(command);
        }

        
        function test_keyup_does_not_create_undo_command_if_system_command_and_changed() {
            editor.keyboard = {
                isModifierKey: function () { return true },
                typingInProgress: function () { return false },
                isSystem: function () { return true }
            };

            var handler = new SystemHandler(editor);
            handler.changed = function () {
                return true;
            }
            var command;
            
            editor.undoRedoStack.push = function() {
                command = arguments[0];
            }
            
            handler.keyup()
            
            assertUndefined(command);
        }
    </script>
</asp:Content>
