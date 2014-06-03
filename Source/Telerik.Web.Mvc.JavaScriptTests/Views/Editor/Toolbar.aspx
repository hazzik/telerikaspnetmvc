<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Toolbar</h2>

    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
        

        function getEditor() {
            return $('#Editor').data("tEditor");
        }
        
        function value($ui) {
            return $.browser.msie ? $.trim($ui.text()) : $ui.val();
        }
        
        function setUp() {
            window.editor = getEditor();
        }
        
        function test_exec_with_node_parameter_calls_exec() {
            var editor = getEditor();

            var execArgs = [];

            editor.exec = function() { execArgs = arguments; };

            $('.t-bold', editor.element).click();

            assertEquals(1, execArgs.length);
            assertEquals('bold', execArgs[0]);
        }

        function test_handle_carret_selection() {
            editor.value('<strong>foo</strong>')
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.setEnd(editor.body.firstChild.firstChild, 1);

            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            $(editor.element).trigger('selectionChange');

            assertTrue($('.t-bold', editor.element).hasClass('t-state-active'));
        }

        function test_handle_word_selection() {
            editor.value('<strong>foo</strong>')
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);

            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            $(editor.element).trigger('selectionChange');

            assertTrue($('.t-bold', editor.element).hasClass('t-state-active'));
        }

        function test_handle_mixed_selection() {
            editor.value('<ul><li>foo</li></ul><ul><li>bar</li></ul>')
            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild.firstChild, 1);
            range.setEnd(editor.body.firstChild.firstChild.firstChild, 1);

            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            $(editor.element).trigger('selectionChange');

            assertFalse($('t-insertUnorderedList', editor.element).hasClass('t-state-active'));
        }

        function test_handle_image_selection() {
            editor.value('<img style="float:right" src="foo" />');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);

            $(editor.element).trigger('selectionChange');

            assertTrue($('.t-justifyRight', editor.element).hasClass('t-state-active'));
        }

        function test_font_size_combobox_on_mixed_content() {
            editor.selectRange(createRangeFromText(editor, '|foo<span style="font-size:8px;">bar|</span>'));

            $(editor.element).trigger('selectionChange');

            assertEquals('', value($('.t-fontSize .t-input', editor.element)));
        }

        function test_font_size_combobox_on_custom_font_size() {
            editor.selectRange(createRangeFromText(editor, '<span style="font-size:8px;">f|o|o</span>'));

            $(editor.element).trigger('selectionChange');

            assertEquals('8px', value($('.t-fontSize .t-input', editor.element)));
        }        
        
        function test_inherited_font_size() {
            editor.selectRange(createRangeFromText(editor, '<span>f|o|o</span>'));

            $(editor.element).trigger('selectionChange');

            assertEquals(editor.localization.fontSizeInherit, value($('.t-fontSize .t-input', editor.element)));
        }

        function test_font_size_combobox_on_relative_font_size() {
            editor.selectRange(createRangeFromText(editor, '<span style="font-size:x-small;">f|o|o</span>'));

            $(editor.element).trigger('selectionChange');

            assertEquals('2 (10pt)', value($('.t-fontSize .t-input', editor.element)));
        }

    </script>
</asp:Content>
