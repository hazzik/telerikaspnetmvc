<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Northwind.Customers>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customer Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customer</h2>
    <fieldset>
        <legend>Details</legend>
        <p>
            Company Name:
            <%= Html.Encode(Model.CompanyName) %>
        </p>
        <p>
            Contact Name:
            <%= Html.Encode(Model.ContactName) %>
        </p>
        <p>
            Address:
            <%= Html.Encode(Model.Address) %>
        </p>
        <p>
            City:
            <%= Html.Encode(Model.City) %>
        </p>
        <p>
            Postal Code:
            <%= Html.Encode(Model.PostalCode) %>
        </p>
        <p>
            Country:
            <%= Html.Encode(Model.Country) %>
        </p>
        <p>
            Phone:
            <%= Html.Encode(Model.Phone) %>
        </p>
        <p>
            Fax:
            <%= Html.Encode(Model.Fax) %>
        </p>
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id = Model.CustomerID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>
</asp:Content>