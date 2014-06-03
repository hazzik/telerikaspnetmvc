<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatePicker Basic Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().DatePicker()
                    .Name("dateField")
                    .AllowMonthChange(true)
                    .AllowYearChange(true)
                    .Render(); %>
</asp:Content>