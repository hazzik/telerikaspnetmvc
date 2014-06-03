<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        BlockFormatFinder</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;

        var BlockFormatFinder;
        var enumerator;

        function setUp() {
            editor = getEditor();
            BlockFormatFinder = $.telerik.editor.BlockFormatFinder;
        }

        function test_findSuitable_returns_suitable_container() {
            editor.value('<div>foo</div>');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertEquals(editor.body.firstChild, finder.findSuitable([editor.body.firstChild.firstChild])[0]);
        }

        function test_findSuitable_returns_null_if_no_suitable_found() {
            editor.value('foo');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertEquals(0, finder.findSuitable([editor.body.firstChild]).length);
        }
        
        function test_findSuitable_returns_all_suitable_nodes_null_if_no_suitable_found() {
            editor.value('<div>foo</div><div>bar</div>');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertEquals(2, finder.findSuitable([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]).length);
        }

        function test_findSuitable_returns_distinct_nodes() {
            editor.value('<div><span>foo</span><span>bar</span></div><div>baz</div>');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertEquals(2, finder.findSuitable([editor.body.firstChild.firstChild.firstChild, editor.body.firstChild.lastChild.firstChild, editor.body.lastChild.firstChild]).length);
        }

        function test_findSuitable_looks_for_common_ancestor_which_is_suitable() {
            editor.value('<div>foo</div>bar');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertEquals(0, finder.findSuitable([editor.body.firstChild.firstChild, editor.body.lastChild]).length);
        }

        function test_findSuitable_looks_for_the_outer_most_common_ancestor_which_is_suitable() {
            editor.value('<div><div>foo</div>bar</div>');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertEquals(editor.body.firstChild, finder.findSuitable([editor.body.firstChild.firstChild.firstChild, editor.body.firstChild.lastChild])[0]);
        }

        function test_findSuitable_returns_the_innder_suitable_ancestor() {
            editor.value('<div><div>foo</div></div>');
            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertEquals(editor.body.firstChild.firstChild, finder.findSuitable([editor.body.firstChild.firstChild.firstChild])[0]);
        }

        function test_find_format_finds_formatted_node_by_tag() {
            editor.value('<div style="text-align:center">foo</strong>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            
            assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild));
        }

        function test_find_suitable_finds_td() {
            editor.value('<table><tr><td>foo</td></tr></table>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            var td = $('td', editor.body)[0];
            assertEquals(td, finder.findSuitable([td.firstChild])[0]);
        }

        function test_find_format_returns_null_if_node_does_not_match_tag_and_style() {
            editor.value('<div>foo</div>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertNull(finder.findFormat(editor.body.firstChild.firstChild));
        }

        function test_find_format_checks_all_formats() {
            editor.value('<p style="text-align:center">foo</p>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertEquals(editor.body.firstChild, finder.findFormat(editor.body.firstChild.firstChild));
        }
        function test_is_formatted_checks_all_formats() {
            editor.value('<p style="text-align:center">foo</p>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);

            assertTrue(finder.isFormatted([editor.body.firstChild.firstChild]));
        }

        function test_is_formatted_returns_true_if_all_nodes_are_formatted() {
            editor.value('<div style="text-align:center">foo</div>');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertTrue(finder.isFormatted([editor.body.firstChild.firstChild]));
        }

        function test_is_formatted_returns_true_for_formatted_and_unformatted_nodes() {
            editor.value('<div style="text-align:center">foo</div>bar');

            var finder = new BlockFormatFinder(editor.formats.justifyCenter);
            assertFalse(finder.isFormatted([editor.body.firstChild.firstChild, editor.body.lastChild]));
        }

        function test_is_formatted_returns_true_for_image() {
            editor.value('<img style="float:right" />');

            var finder = new BlockFormatFinder(editor.formats.justifyRight);
            assertTrue(finder.isFormatted([editor.body.firstChild]));
        }

        function test_getFormat_on_single_node() {
            editor.value('<h1>foo</h1>');
            var finder = new BlockFormatFinder([{ tags: 'div,p,h1,h2,h3,h4,h5,h6'.split(',') }]);
            assertEquals('h1', finder.getFormat([editor.body.firstChild.firstChild]));
        }

        function test_getFormat_on_multiple_different_nodes() {
            editor.value('<h1>foo</h1><h2>bar</h2>');
            var finder = new BlockFormatFinder([{ tags: 'div,p,h1,h2,h3,h4,h5,h6'.split(',') }]);
            assertEquals('', finder.getFormat([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]));
        }

        function test_getFormat_on_body_does_not_throw_error() {
            editor.value('foo');
            var finder = new BlockFormatFinder([{ tags: 'div,p,h1,h2,h3,h4,h5,h6'.split(',') }]);
            assertEquals('', finder.getFormat([editor.body]));
        }
    </script>
</asp:Content>
