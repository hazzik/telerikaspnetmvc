<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Components</h2>
    
    <%= Html.Telerik().Editor().Name("Editor") %>

    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;
        var colorpicker;

        function setUp() {
            editor = getEditor();
            colorpicker = $(editor.element).find('.t-colorpicker').eq(0).data('tColorPicker');
        }

        function test_colorpicker_value_returns_black_when_no_value_has_been_selected() {
            assertEquals('#000000', colorpicker.value());
        }
    </script>
</asp:Content>
