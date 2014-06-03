<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MessageBox Error Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().MessageBox()
                    .Name("error")
                    .MessageType(MessageBoxType.Error)
                    .Content(() =>
                                 {%>
                                    <strong>Alert:</strong> There is something wrong.
                                 <%}
                             )
                    .Render(); %>
</asp:Content>