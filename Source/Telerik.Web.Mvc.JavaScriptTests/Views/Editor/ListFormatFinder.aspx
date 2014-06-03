<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ListFormatFinder</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var ListFormatFinder;
        
        function setUp() {
            editor = getEditor();
            ListFormatFinder = $.telerik.editor.ListFormatFinder;
        }

        function test_isFormatted_returns_false_for_text_node() {
            editor.value('test');
            var finder = new ListFormatFinder('ul');
            assertFalse(finder.isFormatted([editor.body.firstChild]));
        }

        function test_isFormatted_returns_true_for_list() {
            editor.value('<ul><li>foo</li></ul>');
            var finder = new ListFormatFinder('ul');
            assertTrue(finder.isFormatted([editor.body.firstChild.firstChild.firstChild]));
        }

        function test_isFormatted_returns_true_for_partial_selection() {
            editor.value('<ul><li>foo</li></ul>bar');
            var finder = new ListFormatFinder('ul');
            assertTrue(finder.isFormatted([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild]));
        }

        function test_isFormatted_returns_false_for_two_lists() {
            editor.value('<ul><li>foo</li></ul><ul><li>bar</li></ul>');
            var finder = new ListFormatFinder('ul');
            assertFalse(finder.isFormatted([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]));
        }

        function test_findSuitable_returns_ul() {
            editor.value('<ul><li>foo</li></ul>bar');
            var finder = new ListFormatFinder('ul');
            assertEquals(editor.body.firstChild, finder.findSuitable([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild]));
        }
        
        function test_findSuitable_returns_first_ul_for_adjacent_lists() {
            editor.value('<ul><li>foo</li></ul><ul><li>bar</li></ul>');
            var finder = new ListFormatFinder('ul');
            assertEquals(editor.body.firstChild, finder.findSuitable([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]));
        }

        function test_findSuitable_returns_null_when_ul_is_not_fist_sibling() {
            editor.value('<ol><li>foo</li></ol><ul><li>bar</li></ul>');
            var finder = new ListFormatFinder('ul');
            assertNull(finder.findSuitable([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]));
        }

        function test_isFormatted_returns_false_in_mixed_list_scenario() {
            editor.value('<ol><li>foo<ul><li>bar</li></ul></li></ol>');
            var finder = new ListFormatFinder('ol');
            assertFalse(finder.isFormatted([$(editor.body).find('ul li')[0].firstChild]));
        }

        function test_findSuitable_returns_null_when_ul_is_nested_in_ol() {
            editor.value('<ol><li>foo<ul><li>bar</li></ul></li></ol>');
            var finder = new ListFormatFinder('ol');
            assertNull(finder.findSuitable([$(editor.body).find('ul li')[0].firstChild]));
        }

    </script>
</asp:Content>
