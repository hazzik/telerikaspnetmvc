<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MessageBox Info Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().MessageBox()
                    .Name("info")
                    .MessageType(MessageBoxType.Info)
                    .Content(() =>
                                 {%>
                                    <strong>Hey!</strong> this info message box.
                                 <%}
                             )
                    .Render(); %>
</asp:Content>