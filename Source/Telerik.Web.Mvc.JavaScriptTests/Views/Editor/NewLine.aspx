<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        NewLine</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    
    <script type="text/javascript">

        var editor;

        var NewLineCommand;
        var enumerator;

        function setUp() {
            editor = getEditor();
            NewLineCommand = $.telerik.editor.NewLineCommand;
        }

        function test_exec_inserts_br_at_carret_position() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            var command = new NewLineCommand({range:range});
            command.exec();
            assertEquals('f<br />oo', editor.value());
        }

        function test_exec_moves_cursor_after_br() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            var command = new NewLineCommand({range:range});
            command.exec();
            range = editor.getRange();
            range.insertNode(editor.document.createElement('hr'));
            assertEquals('f<br /><hr />oo', editor.value())
        }

        function test_exec_replaces_selection_with_br() {
            var range = createRangeFromText(editor, 'f|o|o');
            var command = new NewLineCommand({range:range});
            command.exec();
            assertEquals('f<br />o', editor.value());
        }

        function test_undo_removes_br() {
            var range = createRangeFromText(editor, 'f|o|o');
            var command = new NewLineCommand({range:range});
            command.exec();
            command.undo();
            assertEquals('foo', editor.value());
        }

        function test_undo_leaves_normalized_content() {
            var range = createRangeFromText(editor, 'f|o|o');
            var command = new NewLineCommand({range:range});
            command.exec();
            command.undo();
            assertEquals(1, editor.body.childNodes.length);
        }

        function test_redo() {
            var range = createRangeFromText(editor, 'f|o|o');
            var command = new NewLineCommand({range:range});
            command.exec();
            command.undo();
            command.exec();
            assertEquals('f<br />o', editor.value());
        }
    </script>
</asp:Content>
