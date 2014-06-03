<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        BlockFormatter</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;

        var BlockFormatter;
        var TextNodeEnumerator;
        var enumerator;

        function setUp() {
            editor = getEditor();
            BlockFormatter = $.telerik.editor.BlockFormatter;
            TextNodeEnumerator = $.telerik.editor.TextNodeEnumerator;
        }

        function test_apply_format_on_suitable_block_node() {
            editor.value('<div>foo</div>');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.apply([editor.body.firstChild.firstChild]);

            assertEquals('<div style="text-align:center;">foo</div>', editor.value());
        }

        function test_apply_wraps_single_node() {
            editor.value('foo');
            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            
            formatter.apply([editor.body.firstChild]);

            assertEquals('<div style="text-align:center;">foo</div>', editor.value());
        }

        function test_apply_wraps_all_inline_nodes() {
            editor.value('<span>foo</span><span>bar</span>');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.apply([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]);
            assertEquals('<div style="text-align:center;"><span>foo</span><span>bar</span></div>', editor.value());
        }

        function test_apply_wraps_block_and_inline_nodes() {
            editor.value('<div>foo</div><span>bar</span>');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);

            formatter.apply([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]);
            assertEquals('<div style="text-align:center;">foo</div><div style="text-align:center;"><span>bar</span></div>', editor.value());
        }

        function test_apply_for_block_nodes() {
            editor.value('<div>foo</div><div>bar</div>');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);

            formatter.apply([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]);
            assertEquals('<div style="text-align:center;">foo</div><div style="text-align:center;">bar</div>', editor.value());
        }

        function test_apply_for_text_and_block() {
            editor.value('foo<div>bar</div>baz');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.apply([editor.body.firstChild, editor.body.childNodes[1].firstChild, editor.body.lastChild]);
            assertEquals('<div style="text-align:center;">foo</div><div style="text-align:center;">bar</div><div style="text-align:center;">baz</div>', editor.value());
        }

        function test_apply_text_node_and_inline_elements() {
            editor.value('foo<span></span>bar<span></span>baz');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.apply([editor.body.childNodes[2]]);
            assertEquals('<div style="text-align:center;">foo<span></span>bar<span></span>baz</div>', editor.value());
        }

        function test_remove_unwraps_text_node() {
            editor.value('<div style="text-align:center">foo</div>');
            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.remove([editor.body.firstChild.firstChild]);
            assertEquals('foo', editor.value());
        }

        function test_remove_preserves_paragraphs() {
            editor.value('<p style="text-align:center">foo</p>');
            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.remove([editor.body.firstChild.firstChild]);
            assertEquals('<p>foo</p>', editor.value());
        }

        function test_remove_unwraps_block_nodes() {
            editor.value('<div style="text-align:center">foo</div><div style="text-align:center">bar</div>');
            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.remove([editor.body.firstChild.firstChild, editor.body.lastChild.firstChild]);
            assertEquals('foobar', editor.value());
        }


        function test_toggle_applies_format_if_format_is_not_found() {
            var range = createRangeFromText(editor, '|fo|');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            var argument;
            formatter.apply = function () {
                argument = arguments[0];
            }
            formatter.toggle(range);
            assertTrue($.isArray(argument));
        }

        function test_toggle_removes_format_if_format_is_found() {
            var range = createRangeFromText(editor, '<div style="text-align:center">|fo|</div>');

            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            var argument;
            formatter.remove = function () {
                argument = arguments[0];
            }
            formatter.toggle(range);
            assertTrue($.isArray(argument));
        }

        function test_toggle_and_empty_range() {
            editor.value('foo');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 0);
            range.setEnd(editor.body.firstChild, 0);
            var formatter = new BlockFormatter(editor.formats.justifyCenter);
            formatter.toggle(range);
            assertEquals('<div style="text-align:center;">foo</div>', editor.value());
        }

        function test_toggle_on_image() {
            editor.value('<img src="foo" />');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            var formatter = new BlockFormatter(editor.formats.justifyRight);
            formatter.toggle(range);
            
            assertEquals('<img src="foo" style="float:right;" />', editor.value());
        }

        function test_toggle_on_image_in_paragarph() {
            editor.value('<p><img src="foo" /></p>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild.firstChild);

            var formatter = new BlockFormatter(editor.formats.justifyRight);
            formatter.toggle(range);
            
            assertEquals('<p><img src="foo" style="float:right;" /></p>', editor.value());
        }

        function test_remove_on_image() {
            editor.value('<img style="float:right" src="foo" />');

            var formatter = new BlockFormatter(editor.formats.justifyRight);
            
            formatter.remove([editor.body.firstChild]);
            
            assertEquals('<img src="foo" />', editor.value());
        }
        
        function test_apply_attribute_on_td() {
            editor.value('<table><tr><td>foo</td></tr></table>');
            var td = $('td', editor.body)[0];
            var formatter = new BlockFormatter(editor.formats.justifyRight);
            
            formatter.apply([td.firstChild]);
            
            assertEquals('<table><tbody><tr><td style="text-align:right;">foo</td></tr></tbody></table>', editor.value());
        }

        function test_apply_wrap_in_td() {
            editor.value('<table><tr><td>foo</td></tr></table>');
            var td = $('td', editor.body)[0];
            var formatter = new BlockFormatter([{tags:['p']}]);
            
            formatter.apply([td.firstChild]);
            
            assertEquals('<table><tbody><tr><td><p>foo</p></td></tr></tbody></table>', editor.value());
        }
        
        function test_apply_to_selection_of_block_elements() {
            editor.value('<div>foo</div><div>bar</div><div>baz</div>');
            var formatter = new BlockFormatter(editor.formats.justifyRight);
            formatter.apply([editor.body.firstChild.firstChild, editor.body.firstChild.nextSibling.firstChild]);
            assertEquals('<div style="text-align:right;">foo</div><div style="text-align:right;">bar</div><div>baz</div>', editor.value());
        }

        function test_apply_wraps_in_div() {
            editor.value('<div>foo</div>');
            var formatter = new BlockFormatter([{tags:['p']}]);
            
            formatter.apply([editor.body.firstChild.firstChild]);
            
            assertEquals('<div><p>foo</p></div>', editor.value());
        }

        function test_apply_empty_container() {
            editor.value('');
            editor.focus();
            editor.exec('justifyRight');
            var range = editor.getRange();
            range.insertNode(editor.document.createElement('a'));
            assertEquals('<div style="text-align:right;"><a></a></div>', editor.value());
        }


        function test_apply_text_nodes_in_inline_element() {
            editor.value('<span>foo<strong>bar</strong></span>');
            var formatter = new BlockFormatter(editor.formats.justifyRight);
            
            formatter.apply([editor.body.firstChild.firstChild, editor.body.firstChild.lastChild.firstChild]);
            
            assertEquals('<div style="text-align:right;"><span>foo<strong>bar</strong></span></div>', editor.value());
        }
    </script>
</asp:Content>
