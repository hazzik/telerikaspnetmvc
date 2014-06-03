<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>GreedyInlineFormatter</h2>
    
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
    
        var editor;

        var GreedyInlineFormatter;

        function setUp() {
            editor = getEditor();
            GreedyInlineFormatter = $.telerik.editor.GreedyInlineFormatter;
        }

        function test_toggle_applies_format_on_simple_selection() {
            var range = createRangeFromText(editor, "|foo|");
            var formatter = new GreedyInlineFormatter([{ tags: ['span'] }], { style: { fontFamily: 'Arial' } }, 'font-family');
            formatter.toggle(range);
            assertEquals('<span style="font-family:Arial;">foo</span>', editor.value());
        }

        function test_toggle_applies_format_on_suitable_node() {
            var range = createRangeFromText(editor, '|<span style="font-family:Courier;">foo</span>|');
            var formatter = new GreedyInlineFormatter([{ tags: ['span'] }], { style: { fontFamily: 'Arial' } }, 'font-family');
            formatter.toggle(range);
            assertEquals('<span style="font-family:Arial;">foo</span>', editor.value());
        }

        function test_toggle_inserts_pending_format_in_collapsed_selection() {
            editor.value('foo bar');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 3);
            range.collapse(true);
        
            var marker = new $.telerik.editor.Marker();
            range = marker.add(range, true);

            var formatter = new GreedyInlineFormatter([{ tags: ['span'] }], { style: { fontFamily: 'Arial' } }, 'font-family');
            formatter.editor = editor;
            formatter.toggle(range);

            marker.remove(range);

            assertEquals('foo<span style="font-family:Arial;">\ufeff\ufeff</span> bar', editor.value());
        }

        function test_formats_split_existing_format_nodes() {
            var range = createRangeFromText(editor, '<span style="font-family:Courier;">fo|o</span>bar|');
            var formatter = new GreedyInlineFormatter([{ tags: ['span'] }], { style: { fontFamily: 'Arial' } }, 'font-family');
            formatter.toggle(range);
            assertEquals('<span style="font-family:Courier;">fo</span><span style="font-family:Arial;">obar</span>', editor.value());
        }

        function test_format_splits_span_when_inherit_is_supplied() {
            var range = createRangeFromText(editor, '<span style="font-family:Courier;">fo|o</span>bar|');
            var formatter = new GreedyInlineFormatter([{ tags: ['span'] }], { style: { fontFamily: 'inherit' } }, 'font-family');
            
            formatter.toggle(range);
            assertEquals('<span style="font-family:Courier;">fo</span>obar', editor.value());
        }
    </script>
</asp:Content>
