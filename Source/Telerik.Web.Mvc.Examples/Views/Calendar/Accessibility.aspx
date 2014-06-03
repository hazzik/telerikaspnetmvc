<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">
    <%= Html.Telerik().Calendar()
            .Name("Calendar")
            .Selection(settings => settings
                .Action("Accessibility", new { date = "{0}", theme = Request.QueryString["theme"] }))
            .Value((DateTime)ViewData["date"])
    %>
    
    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>