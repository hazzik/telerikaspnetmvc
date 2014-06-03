<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Remove Inline Commands</h2>

    <%= Html.Telerik().Editor().Name("Editor").Tools(tools => tools.Bold()) %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
        var impl;
        var editor;

        function setUp() {
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
            editor = getEditor();
        }

        function test_removeFormat_removes_format_from_text_range() {
            editor.value('<p><strong>golgafrincham</strong> telephone sanitisers</p>');

            var strongContent = $('strong', editor.document)[0].firstChild;
            var range = editor.createRange();
            range.setStart(strongContent, 0);
            range.setEnd(strongContent, 13);
            
            impl.formatRange(range, editor.formats.bold);
            
            assertEquals('<p>golgafrincham telephone sanitisers</p>', editor.value());
            assertEquals(1, editor.body.firstChild.childNodes.length);
        }
        
        function test_removeFormat_removes_format_from_selected_node_contents() {
            editor.value('<strong>golga<em>fr</em>in</strong>');

            var strong = $('strong', editor.document)[0];

            var range = editor.createRange();
            range.selectNodeContents(strong);
            impl.formatRange(range, editor.formats.bold);

            assertEquals('golga<em>fr</em>in', editor.value());
        }

        function test_removeFormat_removes_format_from_complete_tags_across_paragraphs() {
            editor.value('<p>golgafrin<strong>cham</strong></p><p><strong>tele</strong>phone</p>');

            var $strongs = $('strong', editor.document);

            var range = editor.createRange();
            range.setStartBefore($strongs[0]);
            range.setEndAfter($strongs[1]);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('<p>golgafrincham</p><p>telephone</p>', editor.value().replace(/\s+/gi, ''));
        }

        function test_removeFormat_when_formatted_and_unformatted_nodes_are_selected() {
            editor.value('<strong>f</strong>oo');
            var range = editor.createRange();
            range.setStart(editor.body, 0);
            range.setEnd(editor.body, 2);

            impl.formatRange(range, editor.formats.bold);

            assertEquals('foo', editor.value());
        }

        function test_removeFormat_when_there_is_unformatted_content_before_formatted() {
            editor.value('fo<strong>o</strong>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 0);
            range.setEnd(editor.body.childNodes[1].firstChild, 1);
            impl.formatRange(range, editor.formats.bold);

            assertEquals('foo', editor.value());
        }
        
        function test_removeFormat_when_there_is_unformatted_content_after_formatted() {
            editor.value('<strong>fo</strong>ob');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 0);
            range.setEnd(editor.body.lastChild, 1);
            impl.formatRange(range, editor.formats.bold);

            assertEquals('foob', editor.value());
        }

        function test_remove_styled_format_from_selection() {
            editor.value('<span style="text-decoration:underline;">foo</span>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.setEnd(editor.body.firstChild.firstChild, 2);
            
            impl.formatRange(range, editor.formats.underline);
            
            assertEquals('<span style="text-decoration:underline;">f</span>o<span style="text-decoration:underline;">o</span>', editor.value());
        }


    </script>
</asp:Content>
