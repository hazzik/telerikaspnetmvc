<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentplaceholderid="MainContent" runat="server">
    <% Html.BeginForm(); %>

        <label for="numericTextBox">Set number:</label>

        <%= Html.Telerik().NumericTextBox()
                          .Name("numericTextBox")
                          .InputHtmlAttributes(new { title = "set a number" })
        %>

        <p>
            <button class="t-button" type="submit">Save</button>
        </p>
    
    <% Html.EndForm(); %>
    
    <% if (ViewData["value"] != null)
       { %>
        <p><strong>Posted value is : <%= ViewData["value"] %></strong></p>
    <% } %>

    <noscript>
        <p>Your browsing experience on this page will be better if you visit it with a JavaScript-enabled browser / if you enable JavaScript.</p>
    </noscript>

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>