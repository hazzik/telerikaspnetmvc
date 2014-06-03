<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Range</h2>
    
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
    
        var editor;

        function setUp() {
            editor = getEditor();
        }

        function getRange() {
            return getEditor().createRange(document);
        }

        function test_range_creation() {
            var range = editor.createRange();

            assertEquals(editor.document, range.startContainer);
            assertEquals(editor.document, range.endContainer);
            assertEquals(editor.document, range.commonAncestorContainer);
            assertEquals(0, range.startOffset);
            assertEquals(0, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setStart_setEnd_within_the_same_text_node() {
            editor.value('this is only a test of the <span>emergency</span> <em>broadcast</em> system');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild, 2);
            range.setEnd(editor.body.firstChild, 10);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(10, range.endOffset);
            assertEquals(false, range.collapsed);
        }

        function test_setStart_setEnd_collapsing() {
            editor.value('this is only a test of the <span>emergency</span> <em>broadcast</em> system');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild, 4);
            range.setEnd(editor.body.firstChild, 4);

            assertEquals(true, range.collapsed);
        }

        function test_setStart_setEnd_in_different_containers() {
            editor.value('this is only a test of the <span>emergency</span> <em>broadcast</em> system');

            var range = editor.createRange();

            range.setStart(editor.body.childNodes[1].firstChild, 2);
            range.setEnd(editor.body.childNodes[3].firstChild, 3);

            assertEquals(editor.body, range.commonAncestorContainer);
        }

        function test_setStart_setEnd_in_nested_containers() {
            editor.value('this is only a test of the <span>emergency</span> <em>broadcast</em> system');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild, 2);
            range.setEnd(editor.body.childNodes[1].firstChild, 3);

            assertEquals(editor.body, range.commonAncestorContainer);
        }

        function test_selectNode_selects_node() {
            editor.value('<strong>golga</strong>');

            var range = editor.createRange();

            range.selectNode(editor.body.firstChild);

            assertEquals(editor.body, range.commonAncestorContainer);

            assertEquals(editor.body, range.startContainer);
            assertEquals(editor.body, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(1, range.endOffset);
        }

        function test_selectNodeContents_selects_node_contents() {
            editor.value('<strong>golga</strong>');

            var range = editor.createRange();

            range.selectNodeContents(editor.body.firstChild);

            assertEquals(editor.body.firstChild, range.commonAncestorContainer);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(1, range.endOffset);
        }

        function test_insertNode_on_expanded_range_in_text_element() {
            editor.value('golgafrincham');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 5);
            range.setEnd(editor.body.firstChild, 10);

            range.insertNode(editor.document.createElement('span'));

            assertEquals('golga<span></span>frincham', editor.value());
        }

        function test_insertNode_on_expanded_range_inserts_node_at_start() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            range.insertNode(editor.document.createElement('span'));

            assertEquals('<span></span><p>foo</p>', editor.value());
        }

        function test_insertNode_on_collapsed_range_at_end_of_text_element() {
        
            editor.value('golgafrincham<br />');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 13);
            range.setEnd(editor.body.firstChild, 13);

            range.insertNode(editor.document.createElement('span'));

            assertEquals('golgafrincham<span></span><br />', editor.value());
        }

        function test_extractContents_on_text_node() {
            editor.value('golgafrincham');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild, 5);
            range.setEnd(editor.body.firstChild, 9);

            var contents = range.extractContents();

            assertEquals('frin', contents.firstChild.nodeValue);
            assertEquals('golgacham', editor.value());
        }

        function test_extractContents_extracts_tags() {
            editor.value('golga<strong>frincham</strong>');

            var range = editor.createRange();
            
            range.setStart(editor.body.lastChild.firstChild, 4);
            range.setEndAfter(editor.body.lastChild);

            var contents = range.extractContents();
            
            assertEquals(1, contents.childNodes.length);
            assertEquals('strong', contents.firstChild.tagName.toLowerCase());
            assertEquals('cham', contents.firstChild.innerHTML.toLowerCase());
            assertEquals('golga<strong>frin</strong>', editor.value());
        }

        function test_extractContents_after_range_manipulation() {
            editor.value('golga<strong>frincham</strong>');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild, 0);
            range.setEnd(editor.body.firstChild, 4);

            range = range.cloneRange();
            range.collapse(false);
            range.setEndAfter(editor.body.lastChild);

            var contents = range.extractContents();
            
            assertEquals('golg', editor.value());
            assertEquals(2, contents.childNodes.length);
            assertEquals('a', contents.firstChild.nodeValue);
            assertEquals('strong', contents.lastChild.tagName.toLowerCase());
            assertEquals('frincham', contents.lastChild.firstChild.nodeValue);
        }
        
        /* traversal tests */

        function getRangeFromHtml(html) {
            var editor = getEditor();

            editor.value(html);

            var range = editor.createRange();
            range.setStartBefore(editor.body.firstChild);
            range.setEndAfter(editor.body.lastChild);

            return range;
        }

        function test_extractContents_updates_original_range_when_container_is_text_node() {
            var range = createRangeFromText(editor, "<strong>f|oo|</strong>");

            var leftRange = range.cloneRange();
            leftRange.collapse(true);
            leftRange.setStartBefore(editor.body.lastChild);
            leftRange.extractContents();

            assertEquals(editor.body.lastChild.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild.firstChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_extractContents_updates_original_range_when_container_is_element_node() {
            var range = createRangeFromText(editor, '<strong>|fo|o</strong>');
        
            var marker = new $.telerik.editor.Marker();
            marker.add(range);

            var leftRange = range.cloneRange();
            leftRange.collapse(true);
            leftRange.setStartBefore(editor.body.firstChild);
            leftRange.extractContents();

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(3, range.endOffset);
        }

        function test_extractContents_updates_original_range_when_whole_text_element_is_removed() {
            editor.value('<p>foo</p><p>bar<a></a>baz</p>')
            var range = editor.createRange();
            var anchor = editor.body.lastChild.childNodes[1];
            range.selectNode(anchor);

            var leftRange = range.cloneRange();
            leftRange.collapse(true);
            leftRange.setStartBefore(anchor.parentNode);

            leftRange.extractContents();

            assertEquals(editor.body.lastChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(1, range.endOffset);
        }

        function test_extractContents_does_not_update_original_range_when_outside_range() {
            editor.value('<p>foo</p><p>bar<strong>baz</strong>foo</p>')
            var range = editor.createRange();
            var anchor = editor.body.lastChild.childNodes[1];
            range.selectNode(anchor);

            var rightRange = range.cloneRange();
            rightRange.collapse(false);
            rightRange.setEndAfter(anchor.parentNode);
            rightRange.extractContents();

            assertEquals(editor.body.lastChild, range.startContainer);
            assertEquals(editor.body.lastChild, range.endContainer);
            assertEquals(1, range.startOffset);
            assertEquals(2, range.endOffset);
        }

        function test_setStart_to_marker_after_end_collapses_range_to_new_start() {
            var range = createRangeFromText(editor, '|f|oo');

            range.setStart(editor.body.firstChild, 2);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setEnd_to_marker_before_start_collapses_range_to_new_end() {
            var range = createRangeFromText(editor, 'fo|o|');

            range.setEnd(editor.body.firstChild, 1);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(1, range.startOffset);
            assertEquals(1, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setStart_validation_across_nested_containers() {
            var range = createRangeFromText(editor, '<div><span>f|oo</span><span>ba|r</span></div>');

            range.setStart(editor.body.firstChild, 2);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setStart_validation_across_sibling_containers() {
            var range = createRangeFromText(editor, '<span>f|oo</span><span>ba|r</span><span>baz</span');

            range.setStart(editor.body.lastChild.firstChild, 2);

            assertEquals(editor.body.lastChild.firstChild, range.startContainer);
            assertEquals(editor.body.lastChild.firstChild, range.endContainer);
            assertEquals(editor.body.lastChild.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setEnd_validation_across_nested_containers() {
            var range = createRangeFromText(editor, '<div><span>f|oo</span><span>ba|r</span></div>');

            range.setEnd(editor.body.firstChild, 0);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(0, range.startOffset);
            assertEquals(0, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setEnd_validation_across_sibling_containers() {
            var range = createRangeFromText(editor, '<span>foo</span><span>ba|r</span><span>ba|z</span');

            range.setEnd(editor.body.firstChild.firstChild, 2);

            assertEquals(editor.body.firstChild.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_setEndAfter_validation_across_sibling_containers() {
            editor.value('<p>foo<strong>bar</strong>baz<br />foo<em>bar</em>baz<br />foo</p>');

            var range = editor.createRange();

            range.setStart(editor.body.firstChild.childNodes[1], 1);
            range.setEnd(editor.body.firstChild.childNodes[5], 1);

            range.collapse(false);
            range.setEndAfter(editor.body.firstChild.childNodes[1]);

            assertEquals(editor.body.firstChild, range.startContainer);
            assertEquals(editor.body.firstChild, range.endContainer);
            assertEquals(editor.body.firstChild, range.commonAncestorContainer);
            assertEquals(2, range.startOffset);
            assertEquals(2, range.endOffset);
            assertEquals(true, range.collapsed);
        }

        function test_getRange_returns_body_when_editor_is_empty() {
            editor.value('');
            editor.focus();
            
            var range = editor.getRange();

            assertEquals(editor.body, range.startContainer);
            assertEquals(editor.body, range.endContainer);
            assertEquals(0, range.startOffset);
            assertEquals(0, range.endOffset);
        }

    </script>
</asp:Content>
