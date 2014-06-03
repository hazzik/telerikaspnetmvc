<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Slider Minimum, Maximum, Range Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Slider()
                    .Name("mySlider")
                    .Animate(true)
                    .Value(2000)
                    .Minimum(1000)
                    .Maximum(10000)
                    .Steps(100)
                    .Render(); %>
</asp:Content>