<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm(); %>
        <label for="delay-input">Delay:</label>
        <%= Html.Telerik().TimePicker()
                .Name("delay")
                .Value(DateTime.Now)
        %>
        <br />
        <label for="deliveryDate-input">Delivery date:</label>
        <%= Html.Telerik().DatePicker()
                .Name("deliveryDate")
                .Value(DateTime.Now)
        %>
        <br />
        <label for="orderDateTime-input">Order date time:</label>
        <%= Html.Telerik().DateTimePicker()
                .Name("orderDateTime")
                .Value(DateTime.Now)
        %>
        <p>
            <button class="t-button t-state-default" type="submit">Save</button>
        </p>

    <% Html.EndForm(); %>
    
    <% if (ViewData["delay"] != null)
       { %>
            <p><strong>Posted value from TimePicker is : <%= Convert.ToDateTime(ViewData["delay"]).TimeOfDay %></strong></p>
    <% } %>

    <% if (ViewData["deliveryDate"] != null)
       { %>
            <p><strong>Posted value from DatePicker is : <%= ViewData["deliveryDate"] %></strong></p>
    <% } %>
    
    <% if (ViewData["orderDateTime"] != null)
       { %>
            <p><strong>Posted value from DateTimePicker is : <%= ViewData["orderDateTime"] %></strong></p>
    <% } %>	

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>