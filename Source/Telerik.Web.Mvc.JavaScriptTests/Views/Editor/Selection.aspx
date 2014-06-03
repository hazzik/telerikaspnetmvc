<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Selection</h2>

    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
        var editor;

        function setUp() {
            editor = getEditor();
        }

        function test_getSelection_returns_selection_like_object() {
            var selection = editor.getSelection();

            assertNotUndefined(selection);
            assertNotUndefined(selection.getRangeAt);
            assertNotUndefined(selection.removeAllRanges);
            assertNotUndefined(selection.addRange);
        }

        /* helpers */
        
        function selectTextNodeContents(node, start, end)
        {
            if (window.getSelection)
            {
                var range = editor.createRange();
                range.setStart(node, start || 0);
                range.setEnd(node, end || node.nodeValue.length);
                editor.getSelection().removeAllRanges();
                editor.getSelection().addRange(range);
            } else {
                var textRange = editor.document.body.createTextRange();
                
                var startPoint = textRange.duplicate();
                startPoint.moveToElementText(node.parentNode);
                if (start)
                    startPoint.moveStart('character', start);
                textRange.setEndPoint('StartToStart', startPoint);

                var endPoint = textRange.duplicate();
                endPoint.moveToElementText(node.parentNode);
                if (end) {
                    endPoint.moveStart('character', end);
                    textRange.setEndPoint('EndToStart', endPoint);
                } else {
                    textRange.setEndPoint('EndToEnd', endPoint);
                }

                textRange.select();
            }
        }

        function selectRange(start, startOffset, end, endOffset, endChars) {
            if (window.getSelection) {
                var range = editor.createRange();
                range.setStart(start, startOffset);
                range.setEnd(end, endOffset);
                var selection = editor.getSelection();
                selection.removeAllRanges();
                selection.addRange(range);
            } else {
                var textRange = editor.document.body.createTextRange();
                
                var startPoint = textRange.duplicate();
                startPoint.moveToElementText(start.parentNode);
                startPoint.moveStart('character', startOffset);
                textRange.setEndPoint('StartToStart', startPoint);

                var endPoint = textRange.duplicate();
                endPoint.moveToElementText(end.parentNode);
                endPoint.moveStart('character', endChars || endOffset);
                textRange.setEndPoint('EndToStart', endPoint);

                textRange.select();
            }
        }

        /* addRange */
        
        function test_addRange_with_text_range() {
            editor.value('foo');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 2);

            var selection = editor.getSelection();
            
            selection.removeAllRanges();
            selection.addRange(range);
            
            range = editor.getSelection().getRangeAt(0);
            
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
        }
        
        function test_addRange_with_collapsed_range() {
            editor.value('<strong>golga</strong>frin');

            var range = editor.createRange();
            range.setStart(editor.body.lastChild, 2);
            range.setEnd(editor.body.lastChild, 2);

            var selection = editor.getSelection();
            
            selection.removeAllRanges();
            selection.addRange(range);
            
            range = editor.getSelection().getRangeAt(0);
            
            assertEquals(editor.body.lastChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_addRange_with_text_to_element_range() {
            editor.value('<strong>Haifisch</strong>, der hat Tränen');

            var selection = editor.getSelection();
            var range = editor.createRange();
            range.setStart(editor.body.lastChild, 0);
            range.setEnd(editor.body.lastChild, 5);

            selection.removeAllRanges();
            selection.addRange(range);

            range = selection.getRangeAt(0);
            
            assertEquals(0, range.startOffset);
            assertEquals(5, range.endOffset);
            assertEquals(editor.body.lastChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
        }

        function test_addRange_with_collapsed_range_at_start_of_element() {
            editor.value('<p>foo</p><p>bar</p>');

            var selection = editor.getSelection();
            var range = editor.createRange();
            range.setStart(editor.body.lastChild.firstChild, 0);
            range.setEnd(editor.body.lastChild.firstChild, 0);

            selection.removeAllRanges();
            selection.addRange(range);

            range = selection.getRangeAt(0);
            
            assertEquals(0, range.startOffset);
            assertEquals(0, range.endOffset);
            assertEquals(editor.body.lastChild.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild.firstChild, range.endContainer);
        }

        /* getRangeAt */

        function test_getRangeAt_on_selection_of_single_text_node() {
            editor.value('foo');
            var textNode = editor.body.firstChild;
            
            selectTextNodeContents(textNode);

            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(0, range.startOffset);
            assertEquals(3, range.endOffset);
            assertEquals(textNode, range.startContainer);
            assertEquals(textNode, range.endContainer);
        }

        function test_getRangeAt_on_selection_of_part_of_text_node() {
            editor.value('foo');
            var textNode = editor.body.firstChild;
            
            selectTextNodeContents(textNode, 1, 2);
            
            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(textNode, range.startContainer);
            assertEquals(textNode, range.endContainer);
        }

        function test_getRangeAt_on_selection_with_end_points_in_text_node_and_tag() {
            editor.value('fo<em>obar</em>');

            selectRange(editor.body.firstChild, 1, editor.body.lastChild.firstChild, 2);

            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild.firstChild, range.endContainer);
        }

        function test_getRangeAt_on_selection_with_end_points_in_tag_and_text_node() {
            editor.value('<em>foob</em>ar');
            
            selectRange(editor.body.firstChild.firstChild, 2, editor.body.lastChild, 1, 5);
            
            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(2, range.startOffset);
            assertEquals(1, range.endOffset);
            assertEquals(editor.body.firstChild.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
        }

        function test_getRangeAt_on_selection_with_end_points_in_the_middle_of_different_tags() {
            editor.value('<em>foo</em><strong>bar</strong>');

            selectRange(editor.body.firstChild.firstChild, 1, editor.body.lastChild.firstChild, 2);
            
            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(editor.body.firstChild.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild.firstChild, range.endContainer);
        }

        function test_getRangeAt_on_selection_with_end_points_in_the_middle_of_different_text_nodes() {
            editor.value('fo<em>ob</em>ar');
            
            selectRange(editor.body.firstChild, 1, editor.body.lastChild, 1, 5);
            
            var range = editor.getSelection().getRangeAt(0);
            
            assertEquals(1, range.startOffset);
            assertEquals(1, range.endOffset);
            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
        }

        function test_getRangeAt_on_collapsed_selection_at_end_of_paragraph() {
            editor.value('<p>foo</p>');
            
            selectRange(editor.body.firstChild.firstChild, 3, editor.body.firstChild.firstChild, 3);
            var range = editor.getSelection().getRangeAt(0);
            
            assertTrue(range.collapsed);
            assertEquals(3, range.startOffset);
            assertEquals(editor.body.firstChild.firstChild, range.startContainer);
        }
    </script>
</asp:Content>
