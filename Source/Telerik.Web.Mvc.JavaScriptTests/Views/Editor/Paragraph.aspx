<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Paragraph</h2>
     <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;

        var ParagraphCommand;
        var enumerator;

        function setUp() {
            editor = getEditor();
            ParagraphCommand = $.telerik.editor.ParagraphCommand;
        }

        function test_exec_wraps_the_node_in_paragraph_and_creates_a_new_paragraph() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>f</p><p>oo</p>', editor.value());
        }

        function test_exec_splits_paragraph() {
            editor.value('<p>foo</p>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>f</p><p>oo</p>', editor.value());
        }

        function test_exec_splits_inline_elements() {
            editor.value('fo<em>ob</em>ar');
            var range = editor.createRange();
            range.setStart(editor.body.childNodes[1].firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>fo<em>o</em></p><p><em>b</em>ar</p>', editor.value());
        }

        function test_exec_deletes_selected_content() {
            editor.value('foobar');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.firstChild, 5)
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>f</p><p>r</p>', editor.value());
        }

        function test_exec_adds_paragraph_around_inline_content() {
            editor.value('foo<p>bar</p>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>f</p><p>oo</p><p>bar</p>', editor.value());
        }

        function test_exec_creates_new_li_when_inside_ul() {
            editor.value('<ul><li>foo</li></ul>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<ul><li>f</li><li>oo</li></ul>', editor.value());
        }

        function test_exec_when_inside_empty_li() {
            editor.value('<ul><li></li></ul>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild.firstChild);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p></p>', editor.value());
        }
        
        function test_exec_when_inside_empty_li_and_p() {
            editor.value('<ul><li><p></p></li></ul>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild.firstChild.firstChild);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<p></p>', editor.value());
        }
        
        function test_exec_creates_new_li_when_inside_ul_and_p() {
            editor.value('<ul><li><p>foo</p></li></ul>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild.firstChild.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<ul><li><p>f</p></li><li><p>oo</p></li></ul>', editor.value());
        }
        
        function test_exec_when_in_last_li_and_it_is_empty() {
            editor.value('<ul><li>foo</li><li></li></ul>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild.lastChild);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<ul><li>foo</li></ul><p></p>', editor.value());
        }
        
        function test_exec_in_empty_list_item_preserves_line_breaks_in_others() {
            editor.value('<ul><li>fo<br />o</li><li></li><li>ba<br />r</li></ul>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild.childNodes[1]);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<ul><li>fo<br />o</li></ul><p></p><ul><li>ba<br />r</li></ul>', editor.value());
        }
        
        function test_exec_when_there_is_empty_li() {
            editor.value('<ul><li>foo</li><li></li></ul>');
            var range = editor.createRange();
            range.setStartAfter(editor.body.firstChild.firstChild.firstChild);
            range.setEndAfter(editor.body.firstChild.firstChild.firstChild);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<ul><li>foo</li><li></li><li></li></ul>', editor.value());
        }

        function test_exec_handles_li_containing_br() {
            editor.value('<ul><li><br/></li></ul>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<p></p>', editor.value());
        }

        function test_exec_removes_br() {
            editor.value('foo<br/>bar');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);
            range.collapse(false);
            var command = new ParagraphCommand({ range: range });
            command.exec();
            assertEquals('<p>foo</p><p>bar</p>', editor.value());
        }

        function test_exec_deletes_selected_inline_content() {
            editor.value('foo<p>bar</p>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.setEnd(editor.body.lastChild.firstChild, 1);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>f</p><p>ar</p>', editor.value());
        }

        function test_exec_deletes_all_contents() {
            editor.value('foo');
            var range = editor.createRange();
            range.selectNodeContents(editor.body);
            
            var command = new ParagraphCommand({range:range});
            
            command.exec();
            assertEquals('<p></p><p></p>', editor.value());
        }

        function test_exec_caret_at_end_of_content() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 3);
            range.setEnd(editor.body.firstChild, 3);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>foo</p><p></p>', editor.value());
        }

        function test_undo_reverts_content() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            command.undo();
            assertEquals('foo', editor.value());
        }

        function test_redo() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            
            command.exec();
            command.undo();
            command.exec();

            assertEquals('<p>f</p><p>oo</p>', editor.value());
        }

        function test_exec_moves_caret() {
            editor.value('foo');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 1);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            
            range = editor.getRange();
            range.insertNode(editor.document.createElement('span'));
            
            assertEquals('<p>f</p><p><span></span>oo</p>', editor.value());            
        }

        function test_exec_at_end_of_text_node_wraps_with_paragraph_and_inserts_new_paragraph() {
            editor.value('<p>foo</p>');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 3);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>foo</p><p></p>', editor.value());
        }

        function test_exec_in_empty_paragraph_at_middle_of_text_adds_more_paragraphs() {
            editor.value('<p>foo</p><p></p><p>bar</p>');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.childNodes[1]);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();
            assertEquals('<p>foo</p><p></p><p></p><p>bar</p>', editor.value());
        }

        function test_exec_at_start_of_paragraph_leaves_selection_in_paragraph() {
            editor.value('<p>foo</p><p>bar</p>');
            var range = editor.createRange();
            range.setStart(editor.body.lastChild, 0);
            range.collapse(true);
            var command = new ParagraphCommand({range:range});
            command.exec();

            range = editor.getRange();
            
            range.insertNode(editor.document.createElement('a'));
            
            assertEquals('<p>foo</p><p></p><p><a></a>bar</p>', editor.value());
        }

        function test_exec_in_td() {
            var range =  createRangeFromText(editor, '<table><tr><td>|f|oo</td></tr></table>');
            var command = new ParagraphCommand({range:range});
            command.exec();
            
            assertEquals('<table><tbody><tr><td><p></p><p>oo</p></td></tr></tbody></table>', editor.value());
        }
    </script>
</asp:Content>