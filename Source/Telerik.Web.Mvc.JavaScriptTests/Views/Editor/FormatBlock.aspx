<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FormatBlock</h2>

    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;

        function setUp() {
            editor = getEditor();
        }

        function test_tool_should_display_format_initially() {
            editor.value('');
            editor.focus();
            $(editor.element).trigger('selectionChange');
            assertEquals('Format', $('.t-formatBlock .t-input').text())
        }

        function test_tool_displays_known_values() {
            var range = createRangeFromText(editor, '<p>|foo|</p>');
            editor.selectRange(range);
            $(editor.element).trigger('selectionChange');
            assertEquals('Paragraph', $('.t-formatBlock .t-input').text())
        }
    </script>
</asp:Content>
