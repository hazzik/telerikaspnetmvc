<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatePicker Basic Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <% Html.jQuery().DatePicker()
                        .Name("dateField")
                        .DateFormat("DD, d MM, yy")
                        .AlternateField("#alternateField")
                        .AlternateFieldFormat("mm/dd/yy")
                        .Render(); %>
    </p>
    <p>
        <%= Html.TextBox("alternateField")%>
    </p>
</asp:Content>