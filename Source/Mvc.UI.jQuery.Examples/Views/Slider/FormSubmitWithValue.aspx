<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Task>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Slider Value Form Submit Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using(Html.BeginForm()){%>
        <fieldset>
            <legend>Submit Task</legend>
            <span style="color:Blue;font-weight:bold">
                <%= ViewData.Get<string>("successMessage") %>
            </span>
            <%= Html.ValidationSummary("Please correct the following errors:") %>
            <div>
                <label for="name">Name:</label>
            </div>
            <div style="margin-top:5px">
                <%= Html.TextBox("task.Name", null, new { style = "width:200px" })%>
            </div>
            <div style="margin-top:5px">
                <%= Html.ValidationMessage("task.Name")%>
            </div>
            <div style="margin-top:10px">
                Completed: <span id="completedPercent" style="color:Black"></span>%
            </div>
            <div style="margin-top:5px;width:200px">
                <% Html.jQuery().Slider()
                                .Name("task.Completed")
                                .UpdateElements("#completedPercent")
                                .Render(); %>
            </div>
            <div style="margin-top:5px">
                <%= Html.ValidationMessage("task.Completed")%>
            </div>
            <div style="margin-top:10px">
                <input type="submit" value="Submit"/>
            </div>
        </fieldset>
    <% }%>
</asp:Content>