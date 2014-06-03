<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Exec</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    
    <script type="text/javascript">
        var FormatCommand, impl, editor;


        function setUp() {
            editor = getEditor();
            FormatCommand = $.telerik.editor.FormatCommand;
        }
        
        function test_undo_restores_original_content() {
            editor.value('foo');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            var command = editor.tools['bold'].command({range:range});
            command.exec();
            assertEquals('<strong>foo</strong>', editor.value());
            command.undo();
            assertEquals('foo', editor.value());
        }

        function test_undo_restores_selection() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 2);

            var command = editor.tools['bold'].command({range:range});
            command.exec();
            command.undo();

            var selectionRange = editor.getRange();
            assertEquals(1, selectionRange.startOffset);
            assertEquals(2, selectionRange.endOffset);
            assertEquals(editor.body.firstChild, selectionRange.startContainer);
            assertEquals(editor.body.firstChild, selectionRange.endContainer);
        }

        function test_redo_executes_the_command() {
            editor.value('foo');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            var command = editor.tools['bold'].command({range:range});
            command.exec();
            assertEquals('<strong>foo</strong>', editor.value());
            command.undo();
            assertEquals('foo', editor.value());
            command.exec();
            assertEquals('<strong>foo</strong>', editor.value());
        }

        function test_fontName_exec() {
            var range = createRangeFromText(editor, '|foo|');
            editor.selectRange(range);
            editor.exec('fontName', {value: 'Arial,Helvetica,sans-serif'});
            assertEquals('<span style="font-family:Arial,Helvetica,sans-serif;">foo</span>', editor.value());
        }        
        
        function test_fontSize_exec() {
            var range = createRangeFromText(editor, '|foo|');
            editor.selectRange(range);
            editor.exec('fontSize', {value: '8pt'});
            assertEquals('<span style="font-size:8pt;">foo</span>', editor.value());
        }        
        
        function test_foreColor_exec() {
            var range = createRangeFromText(editor, '|foo|');
            editor.selectRange(range);
            editor.exec('foreColor', {value: '#a0b0c0'});
            assertEquals('<span style="color:#a0b0c0;">foo</span>', editor.value());
        }        
        
        function test_backColor_exec() {
            var range = createRangeFromText(editor, '|foo|');
            editor.selectRange(range);
            editor.exec('backColor', {value: '#a0b0c0'});
            assertEquals('<span style="background-color:#a0b0c0;">foo</span>', editor.value());
        }
    </script>
</asp:Content>
