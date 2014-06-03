<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Marker</h2>

    <div class="t-rtl">
    <%= Html.Telerik().Editor().Name("Editor") %>
    </div>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
        var editor;

        function setUp() {
            editor = getEditor();
        }

        function test_content_iframe_inherits_rtl_direction() {
            assertEquals('rtl', $(editor.body, editor.document).css('direction'));
        }
    </script>
</asp:Content>
