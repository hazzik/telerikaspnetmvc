<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Clipboard</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    
    <script type="text/javascript">
    var editor;
    var clipboard;

    function setUp() {
        editor = getEditor();
        clipboard = editor.clipboard;
    }

    function test_paste_empty_content() {
        var range = createRangeFromText(editor, 'fo||o');
        editor.selectRange(range);
        clipboard.paste('');
        range = editor.getRange();
        assertEquals('foo', editor.value());
        assertEquals(2, range.startOffset);
        assertEquals(editor.body.firstChild, range.startContainer);
        assertTrue(range.collapsed);
    }

    function test_paste_text_inserts_it_at_caret_position() {
        var range = createRangeFromText(editor, 'fo||o');
        editor.selectRange(range);
        clipboard.paste('bar');
        range = editor.getRange();
        assertEquals('fobaro', editor.value());
        assertEquals(5, range.startOffset);
        assertEquals(editor.body.firstChild, range.startContainer);
        assertTrue(range.collapsed);
    }
    
    function test_paste_inline_inserts_it_at_caret_position() {
        var range = createRangeFromText(editor, 'fo||o');
        editor.selectRange(range);
        
        clipboard.paste('bar<strong>baz</strong>');
        assertEquals('fobar<strong>baz</strong>o', editor.value());
    }

    function test_paste_inline_in_inline() {
        var range = createRangeFromText(editor, '<strong>fo||o</strong>');
        editor.selectRange(range);
        clipboard.paste('<em>baz</em>');
        assertEquals('<strong>fo</strong><em>baz</em><strong>o</strong>', editor.value());
    }

    function test_paste_deletes_contents() {
        var range = createRangeFromText(editor, '<strong>|foo|</strong>');
        editor.selectRange(range);
        clipboard.paste('bar');
        assertEquals('bar', editor.value());
    }

    function test_paste_single_block_in_inline() {
        var range = createRangeFromText(editor, '<strong>fo||o</strong>');
        editor.selectRange(range);
        clipboard.paste('<div>bar</div>');
        assertEquals('<strong>fo</strong><div>bar</div><strong>o</strong>', editor.value());
    }

    function test_paste_block_in_paragraph_splits_the_paragraph() {
        var range = createRangeFromText(editor, '<p>fo||o</p>');
        editor.selectRange(range);
        clipboard.paste('<div>bar</div>');
        assertEquals('<p>fo</p><div>bar</div><p>o</p>', editor.value());
    }
    
    function test_paste_block_in_paragraph_which_contains_inline_splits_the_paragraph() {
        var range = createRangeFromText(editor, '<p><span>fo||o</span></p>');
        editor.selectRange(range);
        clipboard.paste('<div>bar</div>');
        assertEquals('<p><span>fo</span></p><div>bar</div><p><span>o</span></p>', editor.value());
    }

    function test_paste_inline_content_in_block_element_does_not_split() {
        var range = createRangeFromText(editor, '<p>fo||o</p>');
        editor.selectRange(range);
        clipboard.paste('bar');
        assertEquals('<p>fobaro</p>', editor.value());
    }
    
    function test_paste_inline_content_in_block_with_inline_child_element_splits() {
        var range = createRangeFromText(editor, '<p><span>fo||o</span></p>');
        editor.selectRange(range);
        clipboard.paste('bar');
        assertEquals('<p><span>fo</span>bar<span>o</span></p>', editor.value());
    }

    function test_paste_inline_content_in_block_with_nested_inline_child_element_splits() {
        var range = createRangeFromText(editor, '<p><span><strong>fo||o</strong></span></p>');
        editor.selectRange(range);
        clipboard.paste('bar');
        assertEquals('<p><span><strong>fo</strong></span>bar<span><strong>o</strong></span></p>', editor.value());
    }

    function test_empty_elements_are_not_stripped() {
        var range = createRangeFromText(editor, '<p><img />fo||o</p>');
        editor.selectRange(range);
        clipboard.paste('bar');
        assertEquals('<p><img />fobaro</p>', editor.value());
    }

    function test_paste_block_in_li_splits_ul(){
        var range = createRangeFromText(editor, '<ul><li>fo||o</li></ul>');
        editor.selectRange(range);
        clipboard.paste('<div>bar</div>');
        assertEquals('<ul><li>fo</li></ul><div>bar</div><ul><li>o</li></ul>', editor.value());
    }
    
    function test_paste_block_in_td_does_not_split() {
        var range = createRangeFromText(editor, '<table><tr><td>fo||o</td></tr></table>');
        editor.selectRange(range);
        clipboard.paste('<div>bar</div>');
        assertEquals('<table><tbody><tr><td>fo<div>bar</div>o</td></tr></tbody></table>', editor.value());
    }

    function test_paste_invalid_list_markup() {
        var range = createRangeFromText(editor, 'f||oo');
        editor.selectRange(range);
        clipboard.paste('<li>foo</li>');
        assertEquals('f<ul><li>foo</li></ul>oo', editor.value());
    }
    </script>
</asp:Content>
