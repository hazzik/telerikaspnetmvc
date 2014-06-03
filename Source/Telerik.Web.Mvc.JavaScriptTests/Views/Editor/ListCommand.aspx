<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ListCommandFormatter</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    
    <script type="text/javascript">

        var editor;

        var ListCommand;
        var enumerator;

        function setUp() {
            editor = getEditor();
            ListCommand = $.telerik.editor.ListCommand;
        }

        function test_exec_adds_list() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new ListCommand({tag:'ul', range:range});
            command.exec();
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_undo_removes_list() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new ListCommand({tag:'ul', range:range});
            command.exec();
            command.undo();

            assertEquals('foo', editor.value());
        }

        function test_redo_adds_list() {
            var range = createRangeFromText(editor, '|foo|');
            var command = new ListCommand({ tag: 'ul', range: range });
            command.exec();
            command.undo();
            command.exec();

            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_exec_with_collapsed_range() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            var command = new ListCommand({ tag: 'ul', range: range });
            command.exec();

            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_exec_keeps_selection() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            var command = new ListCommand({ tag: 'ul', range: range });
            command.exec();
            range = editor.getRange();
            assertEquals(editor.body.firstChild.firstChild.firstChild, range.startContainer);
            assertEquals(1, range.startOffset);
            assertTrue(range.collapsed);
        }

        function test_apply_and_cursor() {
            editor.value('foo<ul><li>bar</li></ul>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ListCommand({ tag: 'ul', range: range });
            command.exec();
            assertEquals('<ul><li>foo</li></ul><ul><li>bar</li></ul>', editor.value())
        }

        function test_exec_puts_cursor_in_empty_li() {
            editor.value('');
            editor.focus();
            var command = new ListCommand({ tag: 'ul', range: editor.getRange() });
            command.exec();
            editor.getRange().insertNode(editor.document.createElement('a'));
            assertEquals('<ul><li><a></a></li></ul>', editor.value())
        }
    </script>
</asp:Content>
