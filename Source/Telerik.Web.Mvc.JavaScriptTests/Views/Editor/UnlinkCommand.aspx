<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UnlinkCommand</h2>
   <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var UnlinkCommand;

        function setUp() {
            editor = getEditor();
            UnlinkCommand = $.telerik.editor.UnlinkCommand;
        }

        function test_exec_removes_link() {
            var range = createRangeFromText(editor, '<a>|foo|</a>');
            var command = new UnlinkCommand({range:range});
            command.exec();
            assertEquals('foo', editor.value());
        }        
        
        function test_exec_removes_link_with_mixed_selection() {
            var range = createRangeFromText(editor, '|foo<a>bar</a>baz|');
            var command = new UnlinkCommand({range:range});
            command.exec();
            assertEquals('foobarbaz', editor.value());
        }

        function test_exec_maintains_selection() {
            var range = createRangeFromText(editor, '<a>|foo|</a>');
            var command = new UnlinkCommand({range:range});
            command.exec();
            range = editor.getRange();
            assertEquals(0, range.startOffset);
            assertEquals(3, range.endOffset);
        }
        
        function test_exec_from_cursor() {
            editor.value('<a>foo</a>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.collapse(true);

            var command = new UnlinkCommand({range:range});
            command.exec();

            assertEquals('foo', editor.value());
        }

        function test_unlink_tool_is_initially_disabled() {
            editor.focus();
            var range = createRangeFromText(editor, '|foo|');
            editor.selectRange(range);
            $(editor.element).trigger('selectionChange');
            assertTrue($('.t-unlink').hasClass('t-state-disabled'));
        }
        
        function test_unlink_tool_is_enabled_when_cursor_is_inside_a_link() {
            editor.focus();
            var range = createRangeFromText(editor, '<a>|foo|</a>');
            editor.selectRange(range);
            
            $(editor.element).trigger('selectionChange');
            assertFalse($('.t-unlink').hasClass('t-state-disabled'));
        }
        
        function test_unlink_tool_is_enabled_when_there_is_a_link_in_the_selection() {
            editor.focus();
            var range = createRangeFromText(editor, '|foo<a>bar</a>baz|');
            editor.selectRange(range);
            
            $(editor.element).trigger('selectionChange');
            assertFalse($('.t-unlink').hasClass('t-state-disabled'));
        }

    </script>
</asp:Content>
