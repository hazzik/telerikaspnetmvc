<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Remove Inline Commands</h2>

    <%= Html.Telerik().Editor().Name("Editor1").Tools(tools => tools.Bold()) %>

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
                         return new $.telerik.editor.BlockFormatter(format);
                        }
                    })
                    command.exec();
                }
            }
        }

        function test_removeFormat_applies_block_format_on_full_selection() {
            editor.value('<p style="text-align:center;">golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(pararagraph, 0);
            range.setEnd(pararagraph, 13);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p>golgafrincham</p>', editor.value());
        }

        function test_removeFormat_applies_block_format_on_partial_selection() {
            editor.value('<p style="text-align:center;">golgafrincham</p>');

            var pararagraph = $('p', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(pararagraph, 5);
            range.setEnd(pararagraph, 10);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p>golgafrincham</p>', editor.value());
        }

        function test_removeFormat_applies_block_format_on_partial_selection_with_line_break() {
            editor.value('<p style="text-align:center;">golga<br />frincham</p>');

            var pararagraph = $('p', editor.document)[0];
            var range = editor.createRange();
            range.setStart(pararagraph.firstChild, 2);
            range.setEnd(pararagraph.lastChild, 4);

            impl.formatRange(range, editor.formats.justifyCenter);
            
            assertEquals('<p>golga<br />frincham</p>', editor.value());
        }
    </script>
</asp:Content>
