<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentplaceholderid="MainContent" runat="server">
    <% Html.BeginForm(); %>

        <label for="piecesOfCake-input">Set number:</label>

        <%= Html.Telerik().NumericTextBox()
                          .Name("numericTextBox")
        %>

        <p>
            <button class="t-button t-state-default" type="submit">Save</button>
        </p>
    
    <% Html.EndForm(); %>
    
    <% if (ViewData["value"] != null)
       { %>
        <p><strong>Posted value is : <%= ViewData["value"] %></strong></p>
    <% } %>

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>