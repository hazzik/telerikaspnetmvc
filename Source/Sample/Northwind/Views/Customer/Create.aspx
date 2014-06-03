<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Customers>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customer Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customer</h2>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>Details</legend>
            <p>
                <label for="CompanyName">Company Name:</label>
                <%= Html.TextBox("CompanyName") %>
                <%= Html.ValidationMessage("CompanyName", "*") %>
            </p>
            <p>
                <label for="ContactName">Contact Name:</label>
                <%= Html.TextBox("ContactName") %>
                <%= Html.ValidationMessage("ContactName", "*") %>
            </p>
            <p>
                <label for="Address">Address:</label>
                <%= Html.TextArea("Address") %>
                <%= Html.ValidationMessage("Address", "*") %>
            </p>
            <p>
                <label for="City">City:</label>
                <%= Html.TextBox("City") %>
                <%= Html.ValidationMessage("City", "*") %>
            </p>
            <p>
                <label for="PostalCode">Postal Code:</label>
                <%= Html.TextBox("PostalCode") %>
                <%= Html.ValidationMessage("PostalCode", "*") %>
            </p>
            <p>
                <label for="Country">Country:</label>
                <%= Html.TextBox("Country") %>
                <%= Html.ValidationMessage("Country", "*") %>
            </p>
            <p>
                <label for="Phone">Phone:</label>
                <%= Html.TextBox("Phone") %>
                <%= Html.ValidationMessage("Phone", "*") %>
            </p>
            <p>
                <label for="Fax">Fax:</label>
                <%= Html.TextBox("Fax") %>
                <%= Html.ValidationMessage("Fax", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>
    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>