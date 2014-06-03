<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ClientSideApi</h2>

    
    <%= Html.Telerik().Editor().Name("EditorEncodeFalse").Encode(false)%>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <%= Html.Telerik().Editor().Name("EditorWithStyleSheets").StyleSheets(style => style.Add("telerik.common.css").Add("telerik.vista.css"))%>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;
        var rootUrl = '<%= Url.Content("~/") %>';

        function setUp() {
            editor = getEditor();
        }

        function test_editor_value_should_not_be_encoded() {
            var editorEncodeFalse = getEditor('#EditorEncodeFalse');
            editorEncodeFalse.value('<p>foo</p>');

            editorEncodeFalse.update();

            assertEquals('<p>foo</p>', editorEncodeFalse.textarea.value);
        }

        function test_editor_default_Encode_value_must_be_true() {
            assertEquals(true, editor.encoded);
        }

        function test_editor_should_add_stylesheets() {
            var editorWithStyleSheets = getEditor('#EditorWithStyleSheets');

            assertEquals(rootUrl + 'Content/telerik.common.css', editorWithStyleSheets.stylesheets[0]);
            assertEquals(rootUrl + 'Content/telerik.vista.css', editorWithStyleSheets.stylesheets[1]);
        }

        function test_editor_outputs_css_links_in_iframe() {
            var editorWithStyleSheets = getEditor('#EditorWithStyleSheets');
            
            var links = $('link', editorWithStyleSheets.document);
            
            assertEquals(rootUrl + 'Content/telerik.common.css', links.eq(0).attr('href'));
            assertEquals(rootUrl + 'Content/telerik.vista.css', links.eq(1).attr('href'));
        }

    </script>

</asp:Content>
