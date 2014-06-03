<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Slider Values Form Submit Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using(Html.BeginForm()){%>
        <fieldset>
            <legend>Submit Department</legend>
            <span style="color:Blue;font-weight:bold">
                <%= ViewData.Get<string>("successMessage") %>
            </span>
            <%= Html.ValidationSummary("Please correct the following errors:") %>
            <div>
                <label for="name">Name:</label>
            </div>
            <div style="margin-top:5px">
                <%= Html.TextBox("name", null, new { style = "width:200px" })%>
            </div>
            <div style="margin-top:5px">
                <%= Html.ValidationMessage("name") %>
            </div>
            <div style="margin-top:10px">
                Salary: $<span id="rangeFrom" style="color:Black"></span> - $<span id="rangeTo" style="color:Black"></span>
            </div>
            <div style="margin-top:5px;width:600px">
                <% Html.jQuery().Slider()
                                .Name("salary")
                                .Range(SliderRange.True)
                                .Values(1000, 2500)
                                .UpdateElements("#rangeFrom", "#rangeTo")
                                .Minimum(1000)
                                .Maximum(10000)
                                .Steps(500)
                                .Render(); %>
            </div>
            <div style="margin-top:5px">
                <%= Html.ValidationMessage("salary")%>
            </div>
            <div style="margin-top:10px">
                <input type="submit" value="Submit"/>
            </div>
        </fieldset>
    <% }%>
</asp:Content>