<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Apply Inline Commands</h2>

    <%= Html.Telerik().Editor().Name("Editor1").Tools(tools => tools.Bold().Italic()) %>

    <script type="text/javascript">
        var impl;

        function getEditor() {
            return $('#Editor1').data("tEditor");
        }

        function setUp() {
            impl = {
                formatRange: function(range, format) {
                    var command = new $.telerik.editor.FormatCommand({
                        range:range,
                        format: format,
                        formatter: function() {
                         return new $.telerik.editor.BlockFormatter(format);
                        }
                    })
                    command.exec();
                }
            }
        }

        function test_applyFormat_applies_block_format_on_full_selection() {
            var editor = getEditor();

            editor.value('<p>golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(pararagraph, 0);
            range.setEnd(pararagraph, 13);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p style="text-align:center;">golgafrincham</p>', editor.value());
        }

        function test_applyFormat_applies_block_format_on_partial_selection() {
            var editor = getEditor();

            editor.value('<p>golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(pararagraph, 5);
            range.setEnd(pararagraph, 10);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p style="text-align:center;">golgafrincham</p>', editor.value());
        }

        function test_applyFormat_applies_block_format_on_partial_selection_with_line_break() {
            var editor = getEditor();

            editor.value('<p>golga<br />frincham</p>');

            var pararagraph = $('p', editor.document)[0];
            var range = editor.createRange();
            range.setStart(pararagraph.firstChild, 2);
            range.setEnd(pararagraph.lastChild, 4);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p style="text-align:center;">golga<br />frincham</p>', editor.value());
        }

        function test_applyFormat_applies_block_format_on_block_level_around_selection() {
            var editor = getEditor();

            editor.value('golgafrincham');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 5);
            range.setEnd(editor.body.firstChild, 9);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<div style="text-align:center;">golgafrincham</div>', editor.value());
        }

    </script>
</asp:Content>
