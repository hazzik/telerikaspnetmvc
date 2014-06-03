<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>IndentCommand</h2>
    
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var IndentCommand;

        function setUp() {
            editor = getEditor();
            IndentCommand = $.telerik.editor.IndentCommand;
        }

        function test_exec_indents() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new IndentCommand({range:range});
            command.exec();
            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }

        function test_undo_removes_margin() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new IndentCommand({range:range});
            command.exec();
            command.undo();

            assertEquals('foo', editor.value());
        }
        
        function test_redo_indents() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new IndentCommand({ range: range });
            command.exec();
            command.undo();
            command.exec();

            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }

        function test_indent_list() {
            editor.focus();
            var range = createRangeFromText(editor, '<ul><li>foo</li><li>|b|ar</li></ul>');
            var command = new IndentCommand({ range: range });
            command.exec();
            assertEquals('<ul><li>foo<ul><li>bar</li></ul></li></ul>', editor.value());
        }
    </script>
</asp:Content>
