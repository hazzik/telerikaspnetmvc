<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TextNodeIterator</h2>

    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>


    <script type="text/javascript">
    
    var editor;

    var RangeEnumerator;
    var enumerator;

    function setUp() {
        editor = getEditor();
        RangeEnumerator = $.telerik.editor.RangeEnumerator;
    }

    function assertArrayEquals(expected, actual) {
        assertEquals(expected.length, actual.length);

        for (var i = 0; i < expected.length; i++)
            assertEquals(expected[i], actual[i]);
    }

    function test_enumerate_returns_text_node_when_all_content_selected() {
        var range = createRangeFromText(editor, '|foo|');
        
        var result = new RangeEnumerator(range).enumerate();

        assertArrayEquals([editor.body.firstChild], result);
    }

    function test_enumerate_returns_text_nodes_when_inline_node_is_selected() {
        var range = createRangeFromText(editor, '|<span><span>foo</span></span>|');
        
        var result = new RangeEnumerator(range).enumerate();

        assertArrayEquals([editor.body.firstChild.firstChild.firstChild], result);
    }

    function test_enumerate_returns_does_not_return_comments() {
        var range = createRangeFromText(editor, '|foo<!-- comment -->|');
        
        var result = new RangeEnumerator(range).enumerate();

        assertArrayEquals([editor.body.firstChild], result);
    }
    
    function test_enumerate_returns_the_text_contents_when_more_than_one_node_is_selected() {
        var range = createRangeFromText(editor, '|<span>foo</span><span>bar</span>|');
        
        var result = new RangeEnumerator(range).enumerate();

        assertArrayEquals([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild], result);
    }

    function test_enumerate_returns_the_text_contents_in_case_of_partial_selection() {
        var range = createRangeFromText(editor, '<span>f|oo</span><span>b|ar</span>');
        
        var result = new RangeEnumerator(range).enumerate();

        assertArrayEquals([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild], result);
    }

    function test_enumerate_skips_white_space_nodes() {
        var range = createRangeFromText(editor, '|\r\t<p>test</p>\r\n|');

        var p = $('p', editor.body)[0];
        var result = new RangeEnumerator(range).enumerate();
        
        assertArrayEquals([p.firstChild], result);
    }

    function test_enumerate_returns_images() {
        var range = createRangeFromText(editor, '|<img />|');
        var result = new RangeEnumerator(range).enumerate();
        
        assertArrayEquals([editor.body.firstChild], result);
    }
    </script>
</asp:Content>
