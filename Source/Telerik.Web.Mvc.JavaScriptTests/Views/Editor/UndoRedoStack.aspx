<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Exec</h2>

    <%= Html.Telerik().Editor().Name("Editor1") %>

    <script type="text/javascript">
        function getUndoRedoStack() {
            return new $.telerik.editor.UndoRedoStack();
        }
        
        function test_stack_is_initially_empty() {
            var undoRedoStack = getUndoRedoStack();

            assertFalse(undoRedoStack.canUndo());
            assertFalse(undoRedoStack.canRedo());
        }

        function test_canUndo_returns_true_after_command_is_pushed_in_stack() {
            var undoRedoStack = getUndoRedoStack();

            undoRedoStack.push({});

            assertTrue(undoRedoStack.canUndo());
            assertFalse(undoRedoStack.canRedo());
        }

        function test_canRedo_returns_true_after_undo() {
            var undoRedoStack = getUndoRedoStack();

            undoRedoStack.push({ undo: function() {} });
            undoRedoStack.undo();

            assertTrue(undoRedoStack.canRedo());
        }

        function test_canUndo_returns_false_when_at_the_bottom_of_the_stack() {
            var undoRedoStack = getUndoRedoStack();

            undoRedoStack.push({ undo: function() {} });
            undoRedoStack.undo();
            
            assertFalse(undoRedoStack.canUndo());
        }

        function test_canRedo_returns_false_when_a_new_command_is_pushed() {
            var undoRedoStack = getUndoRedoStack();

            undoRedoStack.push({ undo: function() {} });
            undoRedoStack.undo();
            undoRedoStack.push({ undo: function() {} });
            
            assertFalse(undoRedoStack.canRedo());
            assertTrue(undoRedoStack.canUndo());
        }

        function test_undo_delegates_undo_to_current_command() {
            var undoRedoStack = getUndoRedoStack();

            var called = false;

            undoRedoStack.push({ undo: function() { called = true; } });
            undoRedoStack.undo();
            
            assertTrue(called);
        }
        
        function test_redo_delegates_to_exec_to_current_command() {
            var undoRedoStack = getUndoRedoStack();

            var called = false;

            undoRedoStack.push({ undo: function() { }, redo: function() { called = true; } });
            undoRedoStack.undo();
            undoRedoStack.redo();
            
            assertTrue(called);
            assertFalse(undoRedoStack.canRedo());
            assertTrue(undoRedoStack.canUndo());
        }
        
        function test_redo_does_not_delegate_to_exec_when_at_top_of_stack() {
            var undoRedoStack = getUndoRedoStack();

            var called = false;

            undoRedoStack.push({ undo: function() { }, redo: function() { called = true; } });
            undoRedoStack.redo();
            
            assertFalse(called);
        }

        function test_canUndo_is_true_after_undoing_the_second_command() {
            var undoRedoStack = getUndoRedoStack();

            undoRedoStack.push({ undo: function() { } });
            undoRedoStack.push({ undo: function() { } });
            undoRedoStack.undo();
            
            assertTrue(undoRedoStack.canUndo());
            
        }
    </script>
</asp:Content>
