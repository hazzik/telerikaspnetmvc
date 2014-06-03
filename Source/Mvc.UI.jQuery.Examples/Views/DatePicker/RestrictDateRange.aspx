<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatePicker Restrict Date Range Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().DatePicker()
                    .Name("dateField")
                    .MinimumValue(DateTime.Today.AddDays(-20))
                    .MaximumValue(DateTime.Today.AddMonths(1))
                    .Render(); %>
</asp:Content>