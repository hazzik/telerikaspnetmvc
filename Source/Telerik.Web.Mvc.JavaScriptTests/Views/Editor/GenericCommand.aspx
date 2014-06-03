<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>DeleteCommand</h2>
    <%= Html.Telerik().Editor().Name("Editor1") %>
    <script type="text/javascript">
        
        var GenericCommand;
        var RestorePoint;
        function setUp() {
            GenericCommand = $.telerik.editor.GenericCommand;
            RestorePoint = $.telerik.editor.RestorePoint;
        }

        function getEditor() {
            return $('#Editor1').data("tEditor");
        }
        
        function test_generic_command_undo_returns_old_contents() {
            var editor = getEditor();
            editor.value('foo');
            
            var range = editor.createRange();
            
            range.selectNodeContents(editor.body);

            var startRestorePoint = new RestorePoint(range);
            editor.value('');
            var endRestorePoint = new RestorePoint(range);

            var command = new GenericCommand(startRestorePoint, endRestorePoint);
            command.undo();

            assertEquals('foo', editor.value());
        }

        function test_generic_command_redo_sets_new_contents() {
            var editor = getEditor();
            editor.value('foo');

            var range = editor.createRange();

            range.selectNodeContents(editor.body);

            var startRestorePoint = new RestorePoint(range);
            editor.value('');
            var endRestorePoint = new RestorePoint(range);

            var command = new GenericCommand(startRestorePoint, endRestorePoint);
            command.undo();
            command.redo();
            assertEquals('', editor.value());
        }

        function test_generic_command_undo_restores_selection() {
            var editor = getEditor();
            editor.value('foo');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 2);
            
            var restorePoint = new RestorePoint(range);
            editor.value('');
            
            var command = new GenericCommand(restorePoint, restorePoint);
            command.undo();
            var selectedRange = editor.getRange();
            assertEquals(1, selectedRange.startOffset);
            assertEquals(2, selectedRange.endOffset);
            assertEquals(editor.body.firstChild, selectedRange.startContainer);
            assertEquals(editor.body.firstChild, selectedRange.endContainer);
        }
        
        function test_generic_command_redo_restores_selection() {
            var editor = getEditor();
            editor.value('foo');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 2);
            
            var restorePoint = new RestorePoint(range);
            
            var command = new GenericCommand(restorePoint, restorePoint);
            
            command.redo();
            
            var selectedRange = editor.getRange();
            assertEquals(1, selectedRange.startOffset);
            assertEquals(2, selectedRange.endOffset);
            assertEquals(editor.body.firstChild, selectedRange.startContainer);
            assertEquals(editor.body.firstChild, selectedRange.endContainer);
        }
    </script>
</asp:Content>
