<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FormatFinder</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
    
    var editor;

    var InlineFormatter;
    var RangeEnumerator;
    var enumerator;

    function setUp() {
        editor = getEditor();
        InlineFormatter = $.telerik.editor.InlineFormatter;
        RangeEnumerator = $.telerik.editor.RangeEnumerator;
    }
    
    function test_apply_format_applies_style() {
        var range = createRangeFromText(editor, '<span>|foo|</span>');

        var formatter = new InlineFormatter(editor.formats.underline);

        formatter.apply(new RangeEnumerator(range).enumerate());

        assertEquals('<span style="text-decoration:underline;">foo</span>', editor.value());
    }

    function test_apply_wraps_text_node() {
        var range = createRangeFromText(editor, '|foo|');

        var formatter = new InlineFormatter(editor.formats.bold);

        formatter.apply(new RangeEnumerator(range).enumerate());

        assertEquals('<strong>foo</strong>', editor.value());
    }

    function test_apply_wraps_text_node_and_applies_styles() {
        var range = createRangeFromText(editor, '|foo|');

        var formatter = new InlineFormatter(editor.formats.underline);

        formatter.apply(new RangeEnumerator(range).enumerate());

        assertEquals('<span style="text-decoration:underline;">foo</span>', editor.value());
    }

    function test_apply_resolves_style_argument() {
        editor.value('foo');
        var formatter = new InlineFormatter([{tags:['span']}], {style:{color:'#f1f1f1'}});
        formatter.apply([editor.body.firstChild]);
        assertEquals('<span style="color:#f1f1f1;">foo</span>', editor.value());
    }

    function test_apply_applies_attributes() {
        editor.value('foo');
        var formatter = new InlineFormatter([{tags:['span']}], {id:'test'});
        
        formatter.apply([editor.body.firstChild]);
        assertEquals('<span id="test">foo</span>', editor.value());
    }
    
    function test_apply_updates_attributes() {
        editor.value('<span id="foo">foo</span>');
        var formatter = new InlineFormatter([{ tags: ['span']}], {id:'bar'});

        formatter.apply([editor.body.firstChild.firstChild]);
        
        assertEquals('<span id="bar">foo</span>', editor.value());
    }

    function test_consolidate_merges_nodes_of_same_format() {
        editor.value('<span style="text-decoration:underline;">f</span><span style="text-decoration:underline;">oo</span>');
        var formatter = new InlineFormatter(editor.formats.underline);
        formatter.consolidate([editor.body.firstChild, editor.body.lastChild]);
        assertEquals('<span style="text-decoration:underline;">foo</span>', editor.value());
    }

    function test_consolidate_skips_marker() {
        editor.value('<span style="text-decoration:underline;">f</span><span class="t-marker"></span><span style="text-decoration:underline;">oo</span>');
        var formatter = new InlineFormatter(editor.formats.underline);
        formatter.consolidate([editor.body.firstChild, editor.body.lastChild]);
        assertEquals('<span style="text-decoration:underline;">f<span class="t-marker"></span>oo</span>', editor.value());
    }

    function test_consolidate_does_not_merge_nodes_which_are_not_siblings() {
        editor.value('<em>f</em><strong><em>oo</em></strong>');
        var formatter = new InlineFormatter(editor.formats.italic);
        formatter.consolidate([editor.body.firstChild, editor.body.lastChild.firstChild]);
        assertEquals('<em>f</em><strong><em>oo</em></strong>', editor.value());
    }
    
    function test_consolidate_does_not_merge_nodes_with_different_styles() {
        editor.value('<span style="color:#ff0000;">foo</span><span style="font-family:Courier;">bar</span>');
        var formatter = new InlineFormatter([{ tags: ['span'] }], { style: { color: '#ff0000' } }, 'color');
        formatter.consolidate([editor.body.firstChild, editor.body.lastChild]);
        assertEquals('<span style="color:#ff0000;">foo</span><span style="font-family:Courier;">bar</span>', editor.value());
    }

    function test_remove_removes_format_whole_node_contents_selected() {
        var range = createRangeFromText(editor, "<strong>|foo|</strong>");
        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.remove(new RangeEnumerator(range).enumerate());
        assertEquals('foo', editor.value());
    }

    function test_remove_removes_format_whole_node_selected() {
        var range = createRangeFromText(editor, "|<strong>foo</strong>|");
        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.remove(new RangeEnumerator(range).enumerate());
        assertEquals('foo', editor.value());
    }

    function test_splits_format_before_selection() {
        var range = createRangeFromText(editor, "<strong>f|oo|</strong>");
        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.split(range);
        assertEquals('<strong>f</strong><strong>oo</strong>', editor.value());
    }

    function test_splits_format_after_selection() {
        var range = createRangeFromText(editor, "<strong>|fo|o</strong>");
        var formatter = new InlineFormatter(editor.formats.bold);
        
        formatter.split(range);
        assertEquals('<strong>fo</strong><strong>o</strong>', editor.value());
    }

    function test_split_format_keeps_markers() {
        var range = createRangeFromText(editor, '<strong>|fo|o</strong>');
        var formatter = new InlineFormatter(editor.formats.bold);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range);

        formatter.split(range);
        assertEquals('<strong><span class="t-marker"></span>fo<span class="t-marker"></span></strong><strong>o</strong>', editor.value());
    }

    function test_split_trims_nodes_containing_only_invisible_characters() {
        var range = createRangeFromText(editor, '<strong>\ufeff||\ufeff</strong>');
        var formatter = new InlineFormatter(editor.formats.bold);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range);

        formatter.split(range);
        assertEquals('<strong><span class="t-marker"></span><span class="t-marker"></span></strong>', editor.value());
    }

    function test_toggle_applies_format_if_format_is_not_found() {
        var range = createRangeFromText(editor, '|fo|');

        var formatter = new InlineFormatter(editor.formats.bold);
        var argument;
        formatter.apply = function() {
            argument = arguments[0];
        }
        formatter.toggle(range);
        assertTrue($.isArray(argument));
    }

    function test_toggle_removes_format_if_format_is_found() {
        var range = createRangeFromText(editor, '<strong>|fo|</strong>');

        var formatter = new InlineFormatter(editor.formats.bold);
        var argument;
        formatter.remove = function() {
            argument = arguments[0];
        }
        formatter.toggle(range);
        assertTrue($.isArray(argument));
    }

    function test_toggle_split_format_if_format_is_found() {
        var range = createRangeFromText(editor, '<strong>|fo|</strong>');

        var formatter = new InlineFormatter(editor.formats.bold);
        var argument;
        formatter.split = function () {
            argument = arguments[0];
        }
        formatter.toggle(range);
        assertEquals(range, argument);
    }

    function test_toggle_inserts_pending_format_around_caret_marker() {
        editor.value('foo bar');

        var range = editor.createRange();
        range.setStart(editor.body.firstChild, 3);
        range.collapse(true);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range, true);

        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.editor = editor;
        formatter.toggle(range);

        marker.remove(range);

        assertEquals('foo<strong>\ufeff\ufeff</strong> bar', editor.value());
    }

    function test_toggle_inserts_child_pending_format() {
        editor.value('foo<strong>\ufeff</strong> bar');

        var range = editor.createRange();
        range.selectNodeContents(editor.body.childNodes[1]);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range, true);

        var formatter = new InlineFormatter(editor.formats.italic);
        formatter.editor = editor;
        formatter.toggle(range);

        marker.remove(range);

        assertEquals('foo<strong><em>\ufeff</em></strong> bar', editor.value());
    }

    function test_toggle_removes_inserted_pending_format() {
        editor.value('foo<strong>\ufeff</strong> bar');

        var range = editor.createRange();
        range.selectNodeContents(editor.body.childNodes[1]);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range, true);

        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.editor = editor;
        formatter.toggle(range);

        marker.remove(range);

        assertEquals('foo\ufeff bar', editor.value());
    }

    function test_toggle_leaves_marker_within_pending_format() {
        editor.value('foo bar');

        var range = editor.createRange();
        range.setStart(editor.body.firstChild, 3);
        range.collapse(true);
        
        var marker = new $.telerik.editor.Marker();
        range = marker.add(range, true);

        var formatter = new InlineFormatter(editor.formats.bold);
        formatter.editor = editor;
        formatter.toggle(range);

        marker.remove(range);

        var caret = $.telerik.editor.Dom.create(editor.document, 'span', { className: "caret" });
        range.insertNode(caret);

        assertEquals('foo<strong>\ufeff<span class="caret"></span>\ufeff</strong> bar', editor.value());
    }

    function test_toggle_leaves_correct_range_after_inserting_pending_format() {
//        editor.value('foo bar');

//        var range = editor.createRange();
//        range.setStart(editor.body.firstChild, 3);
//        range.collapse(true);
//        
//        var marker = new $.telerik.editor.Marker();
//        range = marker.add(range, true);

//        var formatter = new InlineFormatter(editor.formats.bold);
//        formatter.editor = editor;
//        formatter.toggle(range);

//        range.deleteContents();

//        assertEquals('foo bar', editor.value());
    }

    function test_space_before_content_preserved_after_removing_format() {
        var range = createRangeFromText(editor, 'foo<strong> |bar|</strong>');
        var formatter = new InlineFormatter(editor.formats.bold);
        var marker = new $.telerik.editor.Marker();
        marker.add(range);
        formatter.toggle(range);
        marker.remove(range);
        assertEquals('foo bar', editor.value());
    }

    function test_space_after_content_preserved_after_removing_format() {
        var range = createRangeFromText(editor, '<strong>|foo| </strong>');
        var formatter = new InlineFormatter(editor.formats.bold);
        var marker = new $.telerik.editor.Marker();
        marker.add(range);
        
        formatter.toggle(range);
        marker.remove(range);
        assertEquals('foo ', editor.value());
    }    
    
    function test_space_before_and_after_content_preserved_after_removing_format() {
        var range = createRangeFromText(editor, 'foo<strong> |bar| baz</strong>');
        var formatter = new InlineFormatter(editor.formats.bold);
        var marker = new $.telerik.editor.Marker();
        marker.add(range);
        formatter.toggle(range);
        marker.remove(range);
        assertEquals('foo bar<strong> baz</strong>', editor.value());
    }
    </script>
</asp:Content>
