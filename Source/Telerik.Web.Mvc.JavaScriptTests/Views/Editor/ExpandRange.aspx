<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ExpandRange</h2>
       <%= Html.Telerik().Editor()
              .Name("Editor")
              .Value("foo")     
      %>
        
    <script type="text/javascript">
        
        function getEditor() {
            return $('#Editor').data("tEditor");
        }
        
        var editor, impl;

        function setUp() {
            impl = $.telerik.editor.RangeUtils;
            
            editor = getEditor();
            editor.focus();
        }

        function expandRange(range) {
            var marker = new $.telerik.editor.Marker(range);
            marker.addCaret(range);
            return impl.expand(range);
        }

        function test_expandRange_selects_node_contents() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);

            range = expandRange(range);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_selects_word() {
            editor.value('foo bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);

            range = expandRange(range);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_stops_at_tab() {
            editor.value('foo\tbar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);

            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_stops_at_nbsp() {
            editor.value('foo&nbsp;&nbsp;bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            range = expandRange(range);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_does_not_stop_at_unicode_characters() {
            editor.value('fooщbar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            range = expandRange(range);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(6, range.endOffset);
        }

        function test_expandRange_stops_at_exclamation_mark() {
            editor.value('foo!bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_detects_word_boundary_before_caret() {
            editor.value('foo bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 5);
            range.setEnd(editor.body.firstChild, 5);

            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(4, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_detects_word_boundary_before_and_after_caret() {
            editor.value('foo bar baz');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 5);
            range.setEnd(editor.body.firstChild, 5);

            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.childNodes[2], range.endContainer);
            assertEquals(4, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_does_not_expand_at_end_of_node() {
            editor.value('<strong>foo</strong>');
            
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 3);
            range.collapse(true);
            
            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_expandRange_does_not_crash_in_empty_node() {
            editor.value('<strong></strong>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);

            range = expandRange(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(1, range.endOffset);
        }

        function test_expandRange_does_not_crash_between_element_nodes() {
            editor.value('<span></span><span></span>');
            var range = editor.createRange();
            range.setStart(editor.body, 1);
            range.collapse(true);

            range = expandRange(range);
            
            assertEquals(editor.body, range.startContainer);
            assertEquals(editor.body, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        /** isExpandable **/

        function test_isExpandable_returns_false_for_start_word_boundary() {
            editor.value('foo bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 3);
            range.setEnd(editor.body.firstChild, 3);

            assertFalse(impl.isExpandable(range));
        }

        function test_isExpandable_returns_false_for_end_word_boundary() {
            editor.value('foo bar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 4);
            range.setEnd(editor.body.firstChild, 4);
            
            assertFalse(impl.isExpandable(range));
        }

        function test_isExpandable_returns_false_for_end_of_content() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 3);
            range.setEnd(editor.body.firstChild, 3);

            assertFalse(impl.isExpandable(range));
        }
        
        function test_isExpandable_returns_false_for_beginning_of_content() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 0);
            range.setEnd(editor.body.firstChild, 0);

            assertFalse(impl.isExpandable(range));
        }
        
        function test_isExpandable_returns_false_when_range_is_empty() {
            editor.value('foo');
            var range = editor.createRange();

            assertFalse(impl.isExpandable(range));
        }

        function test_isExpandable_returns_false_when_in_empty_node() {
            editor.value('<strong></strong>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);

            assertFalse(impl.isExpandable(range));
        }
        
    </script>
</asp:Content>