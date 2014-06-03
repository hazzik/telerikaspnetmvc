<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <%= Html.Telerik().Calendar()
          .Name("Calendar")
          .ClientEvents(e => e.OnLoad("onLoad"))
  %>
    <script type="text/javascript">

        var onLoadCalendar;

        function onLoad() {
            onLoadCalendar = $(this).data('tCalendar');
        }

        function test_client_object_is_available_in_on_load() {
            assertNotNull(onLoadCalendar);
            assertNotUndefined(onLoadCalendar);
        }
    </script>
</asp:Content>
