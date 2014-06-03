<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Customers>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customer Delete
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customer</h2>
    <% using (Html.BeginForm()) { %>
        Are you sure you want to delete <%= Html.Encode(Model.CompanyName) %> ?
        <br />
        <input type="submit" value="Submit" /> or <%=Html.ActionLink("Cancel", "Index") %>
    <% } %>
</asp:Content>