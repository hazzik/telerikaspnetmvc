<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <%= Html.Telerik().NumericTextBox()
          .Name("NumericTextBox")
          .ClientEvents(e => e.OnLoad("onLoad"))
  %>
    <script type="text/javascript">

        var onLoadNumericTextBox;

        function onLoad() {
            onLoadNumericTextBox = $(this).data('tTextBox');
        }

        function test_client_object_is_available_in_on_load() {
            assertNotNull(onLoadNumericTextBox);
            assertNotUndefined(onLoadNumericTextBox);
        }
    </script>

</asp:Content>
