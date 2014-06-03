<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Marker</h2>

    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
        var FormatCommand, editor, Marker;

        function setUp() {
            editor = getEditor();
            Marker = $.telerik.editor.Marker;
        }

        /* helpers */

        function createCollapsedRange(container, offset) {
            var range = editor.createRange();
            range.setStart(container, offset);
            range.setEnd(container, offset);

            return range;
        }

        function createRange(startContainer, startOffset, endContainer, endOffset) {
            var range = editor.createRange();
            range.setStart(startContainer, startOffset);
            range.setEnd(endContainer, endOffset);

            return range;
        }

        /* add/removeMarker tests */

        function test_addMarker_inserts_markers() {
            editor.value('foo');
            
            var range = createRange(editor.body.firstChild, 1, editor.body.firstChild, 2);

            var marker = new Marker();
            marker.add(range);

            assertEquals('f<span class="t-marker"></span>o<span class="t-marker"></span>o', editor.value());
        
        }
        function test_addMarker_normalizes() {
            editor.value('foo');

            var range = createRange(editor.body.firstChild, 0, editor.body.firstChild, 3);

            var marker = new Marker();
            marker.add(range);
            
            assertTrue(editor.body.childNodes.length < 5);
        }

        function test_addMarker_does_not_remove_line_breaks() {
            editor.value('foo<br />bar');
            
            var range = createRange(editor.body.firstChild, 0, editor.body.firstChild, 3);

            var marker = new Marker();
            marker.add(range);

            assertEquals('<span class="t-marker"></span>foo<span class="t-marker"></span><br />bar', editor.value());
        }

        function test_addMarker_on_collapsed_range() {
            editor.value('foo');
            
            var range = createCollapsedRange(editor.body.firstChild, 1);

            var marker = new Marker();
            marker.add(range);

            assertEquals('f<span class="t-marker"></span><span class="t-marker"></span>oo', editor.value());
        }
        
        function test_removeMarker_removes_markers() {
            var range = createRangeFromText(editor, '|foo|');
            
            var marker = new Marker();
            marker.add(range);
            marker.remove(range);

            assertEquals('foo', editor.value());
        }
        
        function test_removeMarker_restores_range() {
            var range = createRangeFromText(editor, '|foo|');

            var marker = new Marker();
            marker.add(range);
            marker.remove(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(3, range.endOffset);
        }
        
        function test_removeMarker_normalizes_neighbouring_text_nodes() {
            var range = createRangeFromText(editor, 'foo|bar|baz');

            var marker = new Marker();
            marker.add(range);
            marker.remove(range);
            
            assertEquals(1, editor.body.childNodes.length);
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(3, range.startOffset);
            assertEquals(6, range.endOffset);
        }

        function test_removeMarker_on_collapsed_range() {
            editor.value('foo');
            var range = createCollapsedRange(editor.body.firstChild, 1);

            var marker = new Marker();
            marker.add(range);
            marker.remove(range);
            
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(1, range.startOffset);
            assertEquals(true, range.collapsed);
        }

        function test_addMarker_on_collapsed_range_selects_markers() {
            editor.value('foo bar');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 3);
            range.collapse(true);
        
            var marker = new $.telerik.editor.Marker();
            range = marker.add(range, true);

            assertEquals(editor.body, range.startContainer);
            assertEquals(editor.body, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(4, range.endOffset);
        }
        
        /* add/removeCaretMarker tests */

        function test_addCaretMarker_inserts_caret_marker() {
            editor.value('foo');
            
            var range = createCollapsedRange(editor.body.firstChild, 2);

            var marker = new Marker();
            marker.addCaret(range);

            assertEquals('fo<span class="t-marker"></span>o', editor.value());
        }

        function test_addCaretMarker_updates_range_to_include_caret_marker() {
            editor.value('foo');
            
            var range = createCollapsedRange(editor.body.firstChild, 2);

            var marker = new Marker();
            marker.addCaret(range);

            assertEquals(editor.body, range.startContainer);
            assertEquals(editor.body, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_removeCaretMarker_removes_caret_marker() {
            editor.value('foo');
            
            var range = createRange(editor.body.firstChild, 1, editor.body.firstChild, 2);
            var marker = new Marker();
            marker.addCaret(range);
            marker.removeCaret(range);

            assertEquals('foo', editor.value());
        }

        function test_removeCaretMarker_normalizes_dom() {
            editor.value('foo');
            
            var range = createRange(editor.body.firstChild, 1, editor.body.firstChild, 2);
            var marker = new Marker();
            marker.addCaret(range);
            marker.removeCaret(range);

            assertEquals(1, editor.body.childNodes.length);
        }

        function test_removeCaretMarker_updates_range_to_collapsed_state() {
            editor.value('foo');
            
            var range = createRange(editor.body.firstChild, 1, editor.body.firstChild, 2);
            var marker = new Marker();
            marker.addCaret(range);
            marker.removeCaret(range);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(1, range.endOffset);
        }

        function test_removeCaretMarker_when_caret_at_the_beginning() {
            editor.value('foo');
            var range = createRange(editor.body.firstChild, 0, editor.body.firstChild, 0);
            
            var marker = new Marker();
            marker.addCaret(range);
            
            editor.body.normalize();

            marker.removeCaret(range);
            
            assertEquals(0, range.startOffset);
            assertEquals(editor.body.firstChild, range.startContainer);
            assertTrue(range.collapsed);
        }

        function test_removeCaretMarker_within_element() {
            editor.value('foo<strong></strong> bar');
            
            var range = createRange(editor.body.childNodes[1], 0, editor.body.childNodes[1], 0);
        
            var marker = new Marker();
            marker.addCaret(range);

            marker.removeCaret(range);
            
            assertEquals(0, range.startOffset);
            assertEquals(editor.body.childNodes[1], range.startContainer);
            assertTrue(range.collapsed);
        }

        function test_remove_marker_before_br() {
            editor.value('<br />');
            var range = editor.getRange();
            range.setStartBefore(editor.body.firstChild);
            range.collapse(true);
            var marker = new Marker();
            marker.addCaret(range);
            marker.removeCaret(range);
            assertEquals(0, range.startOffset);
            assertEquals(editor.body, range.startContainer);
        }

        function test_remove_marker_after_element() {
            editor.value('<a></a>');
            var range = editor.getRange();
            range.setEndAfter(editor.body.firstChild);
            range.collapse(false);
            var marker = new Marker();
            marker.add(range);
            
            marker.remove(range);
            assertEquals(1, range.startOffset);
            assertEquals(editor.body, range.startContainer);
        }

        function test_remove_caret_after_element() {
            editor.value('<a></a>');
            var range = editor.getRange();
            range.setEndAfter(editor.body.firstChild);
            range.collapse(false);
            var marker = new Marker();
            marker.addCaret(range);
            
            marker.removeCaret(range);
            assertEquals(1, range.startOffset);
            assertEquals(editor.body, range.startContainer);
        }

        function test_remove_marker_before_element() {
            editor.value('<a></a>');
            var range = editor.getRange();
            range.setStartBefore(editor.body.firstChild);
            range.collapse(true);
            var marker = new Marker();
            marker.add(range);
            
            marker.remove(range);
            assertEquals(0, range.startOffset);
            assertEquals(editor.body, range.startContainer);
        }
        
        function test_remove_caret_before_element() {
            editor.value('<a></a>');
            var range = editor.getRange();
            range.setStartBefore(editor.body.firstChild);
            range.collapse(true);
            var marker = new Marker();
            marker.addCaret(range);

            marker.removeCaret(range);
            assertEquals(0, range.startOffset);
            assertEquals(editor.body, range.startContainer);
        }

        function test_remove_caret_puts_range_at_end_of_last_text_node(){
            editor.value('<a>foo</a>');
            var range = editor.getRange();
            range.setStartAfter(editor.body.firstChild);
            range.collapse(true);

            var marker = new Marker();
            marker.addCaret(range);
            marker.removeCaret(range);

            assertEquals(editor.body.firstChild.firstChild, range.startContainer);
            assertEquals(3, range.startOffset);
            assertTrue(range.collapsed);
        }

        function test_remove_marker_from_empty_paragraph() {
            editor.value('<p>&nbsp;</p>');
            var range = editor.getRange();
            range.selectNodeContents(editor.body.firstChild);
            range.collapse(true);

            var marker = new Marker();
            marker.add(range);
            marker.remove(range);
        }
    </script>
</asp:Content>
