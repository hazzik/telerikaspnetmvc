<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>OutdentCommand</h2>
     <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var OutdentCommand;

        function setUp() {
            editor = getEditor();
            OutdentCommand = $.telerik.editor.OutdentCommand;
        }

        function test_exec_indents() {
            var range = createRangeFromText(editor, '<div style="margin-left:30px;">|foo|</div>');
            var command = new OutdentCommand({range:range});
            command.exec();
            assertEquals('<div>foo</div>', editor.value());
        }

        function test_undo_removes_margin() {
            var range = createRangeFromText(editor, '<div style="margin-left:30px;">|foo|</div>');
            var command = new OutdentCommand({range:range});
            command.exec();
            command.undo();

            assertEquals('<div style="margin-left:30px;">foo</div>', editor.value());
        }
        
        function test_redo_indents() {
            var range = createRangeFromText(editor, '<div style="margin-left:30px;">|foo|</div>');
            var command = new OutdentCommand({ range: range });
            command.exec();
            command.undo();
            command.exec();

            assertEquals('<div>foo</div>', editor.value());
        }

        function test_tool_is_initially_disabled() {
            editor.value('foo');
            editor.focus();
            $(editor.element).trigger('selectionChange');
            assertTrue($('.t-outdent').hasClass('t-state-disabled'));
        }
        
        function test_tool_is_enabled_when_cursor_is_inside_list_item() {
            var range = createRangeFromText(editor, '<ul><li>|foo|</li></ul>');
            editor.selectRange(range);
            editor.focus();
            $(editor.element).trigger('selectionChange');
            assertFalse($('.t-outdent').hasClass('t-state-disabled'));
        }        
        
        function test_tool_is_enabled_when_cursor_is_inside_block_node_with_marginLeft() {
            var range = createRangeFromText(editor, '<p style="margin-left:10px">|foo|</p>');
            editor.selectRange(range);
            editor.focus();
            $(editor.element).trigger('selectionChange');
            assertFalse($('.t-outdent').hasClass('t-state-disabled'));
        }
</script>
</asp:Content>
