<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ListFormatter</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var ListFormatter;

        function setUp() {
            editor = getEditor();
            ListFormatter = $.telerik.editor.ListFormatter;
        }

        function test_apply_on_text_node() {
            editor.value('foo');
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild]);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_apply_on_inline_node() {
            editor.value('<strong>foo</strong>');
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li><strong>foo</strong></li></ul>', editor.value());
        }

        function test_apply_on_block_node() {
            editor.value('<div>foo</div>');
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_apply_on_block_nodes() {
            editor.value('<div>foo</div><div>bar</div>');
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild.firstChild,editor.body.lastChild.firstChild]);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_apply_on_list_and_other_content_merges_the_list() {
            editor.value('<ul><li>foo</li></ul>bar');
            var formatter = new ListFormatter('ul');
            
            formatter.apply([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild]);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_apply_applies_to_selected_block_contents_only() {
            editor.value('<div>foo</div><div>bar</div>');
            var formatter = new ListFormatter('ul');
            
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li></ul><div>bar</div>', editor.value());
        }

        function test_apply_applies_to_inline_siblings() {
            editor.value('<span>foo</span><span>bar</span>');
            var formatter = new ListFormatter('ul');

            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li><span>foo</span><span>bar</span></li></ul>', editor.value());
        }
        
        function test_apply_when_text_node_and_inline_node_selected() {
            editor.value('<p>foo<strong>bar</strong></p>');
            
            var formatter = new ListFormatter('ul');
            
            formatter.apply([editor.body.firstChild.firstChild, editor.body.firstChild.childNodes[1].firstChild]);
            assertEquals('<ul><li>foo<strong>bar</strong></li></ul>', editor.value());
        }

        function test_apply_over_paragraph_containing_whitespace() {
            editor.value('<p>foo<strong>foo</strong> </p>');
            
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li>foo<strong>foo</strong> </li></ul>', editor.value());
        }

        function test_apply_converts_ul_to_ol() {
            editor.value('<ul><li>foo</li></ul>');
            var formatter = new ListFormatter('ol');

            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ol><li>foo</li></ol>', editor.value());
        }

        function test_apply_converts_ol_to_li() {
            editor.value('<ol><li>foo</li></ol>');
            var formatter = new ListFormatter('ul');
            
            formatter.apply([editor.body.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_apply_merges_adjacent_lists() {
            editor.value('<ol><li>foo</li></ol><ol><li>bar</li></ol>');
            var formatter = new ListFormatter('ol');
            
            formatter.apply([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]);
            assertEquals('<ol><li>foo</li><li>bar</li></ol>', editor.value());
        }
        
        function test_apply_converts_and_merges_adjacent_lists() {
            editor.value('<ol><li>foo</li></ol><ol><li>bar</li></ol>');
            var formatter = new ListFormatter('ul');

            formatter.apply([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_apply_converts_and_merges_adjacent_lists_of_different_type_li() {
            editor.value('<ol><li>foo</li></ol><ul><li>bar</li></ul>');
            var formatter = new ListFormatter('ul');
            
            formatter.apply([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }
        
        function test_apply_converts_and_merges_adjacent_lists_of_different_type_ol() {
            editor.value('<ol><li>foo</li></ol><ul><li>bar</li></ul>');
            var formatter = new ListFormatter('ol');

            formatter.apply([editor.body.firstChild.firstChild.firstChild, editor.body.lastChild.firstChild.firstChild]);
            assertEquals('<ol><li>foo</li><li>bar</li></ol>', editor.value());
        }

        function test_remove_unwraps() {
            editor.value('<ul><li>foo</li></ul>');

            var formatter = new ListFormatter('ul');

            formatter.remove([editor.body.firstChild.firstChild.firstChild]);
            assertEquals('<p>foo</p>', editor.value());
        }

        function test_split() {
            var range = createRangeFromText(editor, '<ul><li>|foo|</li><li>bar</li></ul>');
            var formatter = new ListFormatter('ul');

            formatter.split(range);
            assertEquals('<ul><li>foo</li></ul><ul><li>bar</li></ul>', editor.value());
        }

        function test_split_partial_selection_across_multiple_list_items() {
            var range = createRangeFromText(editor, '<ul><li>|foo</li><li>bar|</li></ul>');
            var formatter = new ListFormatter('ul');

            formatter.split(range);
            assertEquals('<ul><li>foo</li><li>bar</li></ul>', editor.value());
        }

        function test_split_whole_li_selected() {
            var range = createRangeFromText(editor, '<ul><li>|foo|</li></ul>');
            var formatter = new ListFormatter('ul');

            formatter.split(range);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_split_partial_contents_of_li_selected() {
            var range = createRangeFromText(editor, '<ul><li>|fo|o</li></ul>');
            var formatter = new ListFormatter('ul');

            formatter.split(range);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_toggle_on_partial_selection() {
            var range = createRangeFromText(editor, '<ul><li>|foo</li><li>bar|</li><li>baz</li></ul>');
            var formatter = new ListFormatter('ul');
            
            formatter.toggle(range);
            assertEquals('<p>foo</p><p>bar</p><ul><li>baz</li></ul>', editor.value());
        }

        function test_toggle_formatted_element_amidst_text() {
            var range = createRangeFromText(editor, '<ul><li>foo<strong>b|a|r</strong>baz</li></ul>');
            var formatter = new ListFormatter('ul');
            
            formatter.toggle(range);
            assertEquals('<p>foo<strong>bar</strong>baz</p>', editor.value());
        }

        function test_toggle_unformatted_element_amidst_text() {
            var range = createRangeFromText(editor, 'foo<strong>b|a|r</strong>baz');
            var formatter = new ListFormatter('ul');
            
            formatter.toggle(range);
            assertEquals('<ul><li>foo<strong>bar</strong>baz</li></ul>', editor.value());
        }

        function test_toggle_unformatted_element_from_caret() {
            editor.value('foo <strong>bar</strong> baz');
            var range = editor.createRange();
            range.setStart(editor.body.childNodes[1].firstChild, 1);
            range.collapse(true);

            var formatter = new ListFormatter('ul');
            
            formatter.toggle(range);
            assertEquals('<ul><li>foo <strong>bar</strong> baz</li></ul>', editor.value());
        }

        function test_toggle_applies_format_if_format_is_not_found() {
            var range = createRangeFromText(editor, '|foo|');

            var formatter = new ListFormatter('ul');
            var argument;
            formatter.apply = function () {
                argument = arguments[0];
            }
            formatter.toggle(range);
            assertTrue($.isArray(argument));
        }

        function test_toggle_removes_format_if_format_is_found() {
            var range = createRangeFromText(editor, '<ul><li>|foo|</li>');

            var formatter = new ListFormatter('ul');
            var argument;
            formatter.remove = function () {
                argument = arguments[0];
            }
            formatter.toggle(range);
            assertTrue($.isArray(argument));
        }

        function test_toggle_and_unexpandable_range() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStartAfter(editor.body.firstChild);
            range.setEndAfter(editor.body.firstChild);

            var formatter = new ListFormatter('ul');
            formatter.toggle(range);
            assertEquals('<ul><li>foo</li></ul>', editor.value());
        }

        function test_toggle_removes_list() {
            editor.value('<ul><li>foo</li><li>bar</li></ul>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild.firstChild, 0);
            range.setEnd(editor.body.firstChild.lastChild.firstChild, 3);
            
            var formatter = new ListFormatter('ul');
            formatter.toggle(range);
            assertEquals('<p>foo</p><p>bar</p>', editor.value());
        }
        
        function test_toggle_removes_empty_lists() {
            editor.value('<ul>   <li>foo</li>   <li>bar</li>   </ul>');
            var range = editor.createRange();
            var lis = $('li', editor.body.firstChild);
            range.setStart(lis[0].firstChild, 0);
            range.setEnd(lis[1].firstChild, 3);
            
            var formatter = new ListFormatter('ul');
            formatter.toggle(range);
            assertEquals('<p>foo</p><p>bar</p>', editor.value());
        }

        function test_remove_and_nested_block_node() {
            editor.value('<ul><li><p>foo</p></li></ul>');
            
            var formatter = new ListFormatter('ul');
            formatter.remove([editor.body.firstChild.firstChild.firstChild.firstChild]);
            
            assertEquals('<p>foo</p>', editor.value());
        }

        function test_remove_and_nested_text_and_block_node() {
            editor.value('<ul><li>foo<div>bar</div>baz</li></ul>');
            
            var formatter = new ListFormatter('ul');
            formatter.remove([editor.body.firstChild.firstChild.firstChild.firstChild]);
            
            editor.value('<p>foo</p><div>bar</div><p>baz</p>');
        }

        function test_remove_and_nested_text_and_inline_node() {
            editor.value('<ul><li>foo<strong>bar</strong>baz</li></ul>');

            var formatter = new ListFormatter('ul');
            formatter.remove([editor.body.firstChild.firstChild.firstChild.firstChild]);

            editor.value('<p>foo<strong>bar</strong>baz</p>');
        }
        
        function test_apply_text_nodes_in_inline_element() {
            editor.value('<span>foo<strong>bar</strong></span>baz');
            
            var formatter = new ListFormatter('ul');
            formatter.apply([editor.body.firstChild.firstChild, editor.body.firstChild.lastChild.firstChild]);
            
            assertEquals('<ul><li><span>foo<strong>bar</strong></span>baz</li></ul>', editor.value());
        }

        function test_convert_mixed_nested_ul_to_ol() {
            editor.value('<ol><li>foo<ul><li>bar</li></ul></li></ol>');
            var bar = editor.body.getElementsByTagName('li')[1].firstChild;
            var formatter = new ListFormatter('ol');
            formatter.apply([bar]);
            
            assertEquals('<ol><li>foo<ol><li>bar</li></ol></li></ol>', editor.value());
        }

        function test_convert_nested_ul_to_ol() {
            editor.value('<ul><li>foo<ul><li>bar</li></ul></li></ul>');
            var bar = editor.body.getElementsByTagName('li')[1].firstChild;
            var formatter = new ListFormatter('ol');
            formatter.apply([bar]);

            assertEquals('<ul><li>foo<ol><li>bar</li></ol></li></ul>', editor.value());
        }
        
        function test_convert_mixed_nested_ol_to_ul() {
            editor.value('<ul><li>foo<ol><li>bar</li></ol></li></ul>');
            var bar = editor.body.getElementsByTagName('li')[1].firstChild;
            var formatter = new ListFormatter('ul');
            formatter.apply([bar]);
            
            assertEquals('<ul><li>foo<ul><li>bar</li></ul></li></ul>', editor.value());
        }
        
        function test_convert_nested_ol_to_ul() {
            editor.value('<ol><li>foo<ol><li>bar</li></ol></li></ol>');
            var bar = editor.body.getElementsByTagName('li')[1].firstChild;
            var formatter = new ListFormatter('ul');
            formatter.apply([bar]);
            
            assertEquals('<ol><li>foo<ul><li>bar</li></ul></li></ol>', editor.value());
        }

    </script>
</asp:Content>
