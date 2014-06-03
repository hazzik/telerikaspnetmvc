<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>InsertHtmlCommand</h2>
    
     <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;
        var InsertHtmlCommand;

        function setUp() {
            editor = getEditor();
            InsertHtmlCommand = $.telerik.editor.InsertHtmlCommand;
        }

        function test_exec_calls_clipboard_paste() {
            var range = createRangeFromText(editor, 'f|o|o');

            var oldPaste = editor.clipboard.paste;
            var argument;
            
            try { 
                editor.clipboard.paste = function() { argument = arguments[0]; }

                var command = new InsertHtmlCommand({ range:range, value: '<span class="test-icon"></span>' });
                command.editor = editor;
                command.exec();
                assertEquals('<span class="test-icon"></span>', argument);

           } finally {
                editor.clipboard.paste = oldPaste;
           }
        }

        function test_exec_inserts_html_parameter() {
            var range = createRangeFromText(editor, 'fo||o');
            editor.selectRange(range);

            var command = new InsertHtmlCommand({ range:range, value: '<span class="test-icon"></span>' });
            command.editor = editor;
            command.exec();
            assertEquals('fo<span class="test-icon"></span>o', editor.value());
        }
    </script>
</asp:Content>
