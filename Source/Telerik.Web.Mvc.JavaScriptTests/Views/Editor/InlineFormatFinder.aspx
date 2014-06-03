<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>InlineFormatFinder</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>


    <script type="text/javascript">
    
    var editor;

    var InlineFormatFinder;
    var enumerator;

    function setUp() {
        editor = getEditor();
        InlineFormatFinder = $.telerik.editor.InlineFormatFinder;
    }

    function test_find_suitable_does_not_return_for_single_text_node() {
        editor.value('foo');

        var finder = new InlineFormatFinder(editor.formats.bold);
        assertNull(finder.findSuitable(editor.body.firstChild));
    }

    function test_find_suitable_returns_matching_tag() {
        editor.value('<span>foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertEquals(editor.body.firstChild, finder.findSuitable(editor.body.firstChild.firstChild));
    }

    function test_find_suitable_returns_closest() {
        editor.value('<span><span>foo</span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertEquals(editor.body.firstChild.firstChild, finder.findSuitable(editor.body.firstChild.firstChild.firstChild));
    }

    function test_find_suitable_does_not_return_in_case_of_partial_selection() {
        editor.value('<span>foo<em>bar</em></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertNull(finder.findSuitable(editor.body.firstChild.firstChild));
    }

    function test_findSuitable_and_caret() {
        editor.value('<span>foo<span class="t-marker"></span>bar</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertEquals(editor.body.firstChild, finder.findSuitable(editor.body.firstChild.firstChild));
    }

    function test_find_suitable_skips_markers() {
        editor.value('<span>foo<span class="t-marker"></span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertEquals(editor.body.firstChild, finder.findSuitable(editor.body.firstChild.firstChild));
    }

    function test_find_format_finds_formatted_node_by_tag() {
        editor.value('<strong>foo</strong>');

        var finder = new InlineFormatFinder(editor.formats.bold);

        assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild));
    }

    function test_find_format_finds_formatterd_node_by_tag_and_style() {
        editor.value('<span style="text-decoration:underline">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);

        assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild));
    }
    
    function test_find_format_returns_null_if_node_does_not_match_tag_and_style() {
        editor.value('<span>foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);

        assertNull(finder.findFormat(editor.body.firstChild.firstChild));
    }

    function test_find_format_returns_parent_element() {
        editor.value('<span style="text-decoration:underline"><span>foo</span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild.firstChild));
    }

    function test_find_format_checks_all_formats() {
        editor.value('<span style="font-weight:bold">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.bold);

        assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild));
    }

    function test_is_formatted_returns_true_if_at_least_one_node_has_format() {
        editor.value('<span style="font-weight:bold">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.bold);
        assertTrue(finder.isFormatted([editor.body.firstChild.firstChild]));
    }

    function test_is_formatted_returns_false_if_all_nodes_dont_have_format() {    
        editor.value('foo');

        var finder = new InlineFormatFinder(editor.formats.bold);
        assertFalse(finder.isFormatted([editor.body.firstChild]));
    }

    function test_is_formatted_returns_true_for_formatted_and_unformatted_nodes() {
        editor.value('<strong>foo</strong>bar');

        var finder = new InlineFormatFinder(editor.formats.bold);
        assertTrue(finder.isFormatted([editor.body.firstChild.firstChild, editor.body.lastChild]));
    }

    function test_is_formatted_returns_true_when_the_format_node_is_the_argument() {
        editor.value('<strong>foo</strong>');

        var finder = new InlineFormatFinder(editor.formats.bold);
        assertTrue(finder.isFormatted([editor.body.firstChild]));
    }
    </script>
</asp:Content>
