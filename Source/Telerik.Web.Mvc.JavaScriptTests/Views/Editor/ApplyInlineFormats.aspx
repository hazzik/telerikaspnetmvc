<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Apply Inline Commands</h2>

    <%= Html.Telerik().Editor().Name("Editor1").Tools(tools => tools.Bold().Italic()) %>

    <script type="text/javascript">
        var impl;
        var editor;
        function getEditor() {
            return $('#Editor1').data("tEditor");
        }

        function setUp() {
            editor = getEditor();
            impl = {
                formatRange: function(range, format) {
                    var command = new $.telerik.editor.FormatCommand({
                        range:range,
                        format: format,
                        formatter: function() {
                         return new $.telerik.editor.InlineFormatter(format);
                        }
                    })
                    command.exec();
                }
            }
        }

        function test_applyFormat_applies_format_on_text_range() {
            editor.value('<p>golgafrincham telephone sanitisers</p>');

            var pararagraph = $('p', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(pararagraph, 0);
            range.setEnd(pararagraph, 13);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('<p><strong>golgafrincham</strong> telephone sanitisers</p>', editor.value());
        }
        
        function test_applyFormat_applies_inline_formats_properly_on_block_elements() {
            editor.value('<p>golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0];

            var range = editor.createRange();
            range.selectNode(pararagraph);

            impl.formatRange(range, editor.formats.bold);
            
            assertEquals('<p><strong>golgafrincham</strong></p>', editor.value());
        }
        
        function test_applyFormat_applies_format_on_split_text_elements() {
            editor.value('<p>golga<em>frin</em>cham</p>');

            var pararagraph = $('p', editor.document)[0];

            var range = editor.createRange();
            range.setStart(pararagraph.firstChild, 2);
            range.setEnd(pararagraph.lastChild, 2);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('<p>go<strong>lga</strong><em><strong>frin</strong></em><strong>ch</strong>am</p>', editor.value());
        }
        
        function test_applyFormat_uses_the_supplied_selector() {
            editor.value('<p>golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0];

            var range = editor.createRange();
            range.selectNode(pararagraph);

            impl.formatRange(range, editor.formats.italic);
            
            assertEquals('<p><em>golgafrincham</em></p>', editor.value());
        }
        
        function test_formatRange_applies_style_commands() {
            editor.value('<p>golgafrincham</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 5);
            range.setEnd(editor.body.firstChild.firstChild, 9);

            impl.formatRange(range, editor.formats.underline);

            var span = $(editor.value()).find('span');

            assertEquals(1, span.length);
            assertEquals('underline', span.css('textDecoration'));
        }
        
        function test_formatRange_does_not_introduce_blank_text_nodes() {
            editor.value('golgafrincham');

            var range = editor.createRange();
            range.setStart(editor.body, 0);
            range.setEnd(editor.body, 1);
            
            impl.formatRange(range, editor.formats.bold);

            assertEquals(1, editor.body.childNodes.length);
        }

        function test_formatRange_does_honor_block_elements() {
            editor.value('<p>golgafrincham</p><p>telephone</p>');

            var range = editor.createRange();
            range.setStart(editor.body, 0);
            range.setEnd(editor.body, 2);
            
            impl.formatRange(range, editor.formats.bold);

            assertEquals('<p><strong>golgafrincham</strong></p><p><strong>telephone</strong></p>', editor.value().replace(/\s*/gi, ''));
        }

        function test_formatRange_honors_nested_block_element() {
            editor.value('<div><div>golgafrincham</div></div>');

            var range = editor.createRange();
            range.setStart(editor.body, 0);
            range.setEnd(editor.body, 1);
            
            impl.formatRange(range, editor.formats.bold);

            assertEquals('<div><div><strong>golgafrincham</strong></div></div>', editor.value().replace(/\s*/gi, ''));
        }

        function test_formatRange_honors_multiple_nested_block_elements() {
            editor.value('<ul><li>golgafrincham</li><li>telephone</li></ul>');
            
            var range = editor.createRange();
            range.setStart(editor.body, 0);
            range.setEnd(editor.body, 1);
            
            impl.formatRange(range, editor.formats.bold);

            assertEquals('<ul><li><strong>golgafrincham</strong></li><li><strong>telephone</strong></li></ul>', editor.value().replace(/\s*/gi, ''));
        }
        
        function test_formatRange_reuses_span() {
            editor.value('<span>golgafrincham</span>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            impl.formatRange(range, editor.formats.underline);

            assertEquals('<span style="text-decoration:underline;">golgafrincham</span>', editor.value());
        }
        
        function test_formatRange_reuses_span_when_node_contents_are_selected() {
            editor.value('<span>golgafrincham</span>');

            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);
            
            impl.formatRange(range, editor.formats.underline);

            assertEquals('<span style="text-decoration:underline;">golgafrincham</span>', editor.value());
        }

        function test_formatRange_does_not_reuse_span_if_tags_are_specified() {
            editor.value('<span>golgafrincham</span>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('<span><strong>golgafrincham</strong></span>', editor.value());
        }
        
        function test_formatRange_with_inline_format_on_collapsed_range_formats_to_word_boundary() {
            editor.value('foo');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('<strong>foo</strong>', editor.value());
        }

        function test_underline_and_collapsed_range() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 1);
            
            impl.formatRange(range, editor.formats.underline);

            assertEquals('<span style="text-decoration:underline;">foo</span>', editor.value());
        }

    </script>
</asp:Content>
