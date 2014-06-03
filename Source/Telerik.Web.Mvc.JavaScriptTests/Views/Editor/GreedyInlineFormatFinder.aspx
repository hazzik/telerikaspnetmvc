<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>GreedyInlineFormatFinder</h2>
    
    
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
    
    var editor;

    var GreedyInlineFormatFinder;
    var enumerator;

    function setUp() {
        editor = getEditor();
        GreedyInlineFormatFinder = $.telerik.editor.GreedyInlineFormatFinder;
    }
    function test_getFormat_returns_inherit_when_called_on_unformatted_node() {
        editor.value('foo');

        var finder = new GreedyInlineFormatFinder([{ tags: ['span'] }], 'font-family');
        assertEquals('inherit', finder.getFormat([editor.body.firstChild]));
    }

    function test_getFormat_returns_correct_font_when_in_format_node() {
        editor.value('foo<span style="font-family:Courier,monospace;">bar</span>baz');

        var finder = new GreedyInlineFormatFinder([{ tags: ['span'] }], 'font-family');

        var span = editor.body.childNodes[1];
        assertEquals($(span).css('font-family'), finder.getFormat([span.firstChild]));
    }

    function test_getFormat_returns_correct_font_when_deep_in_format_node() {
        editor.value('foo<span style="font-family:Courier,monospace;"><del>bar</del></span>baz');

        var finder = new GreedyInlineFormatFinder([{ tags: ['span'] }], 'font-family');

        var span = editor.body.childNodes[1];
        assertEquals($(span).css('font-family'), finder.getFormat([span.firstChild.firstChild]));
    }

    function test_getFormat_returns_empty_string_when_different_fonts_are_encountered() {
        editor.value('<span style="font-family:Verdana,sans-serif;">foo</span><span style="font-family:Courier,monospace;">bar</span>');

        var finder = new GreedyInlineFormatFinder([{ tags: ['span'] }], 'font-family');

        assertEquals('', finder.getFormat([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]));
    }

    function test_getFormat_returns_relative_font_sizes_when_they_are_set() {
        editor.value('<span style="font-size:x-large;">foo</span>');

        var finder = new GreedyInlineFormatFinder([{ tags: ['span'] }], 'font-size');

        assertEquals('x-large', finder.getFormat([editor.body.firstChild.firstChild]));
    }
    </script>
</asp:Content>
