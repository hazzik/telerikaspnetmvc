<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Northwind.Customers>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customers</h2>
    <% Html.Telerik()
           .Grid(Model)
           .Name("customers")
           .PrefixUrlParameters(false)
           .Columns(columns =>
                    {
                        columns.Add(c =>
                                    {
                                        %>
                                            <%= Html.ActionLink("Edit", "Edit", new { id = c.CustomerID })%>
                                            <%= Html.ActionLink("Delete", "Delete", new { id = c.CustomerID })%>
                                        <%
                                    }).Title("Action");

                        columns.Add(c => c.CompanyName).Width(200);
                        columns.Add(c => c.ContactName);
                        columns.Add(c => c.Address);
                        columns.Add(c => c.City);
                        columns.Add(c => c.PostalCode);
                        columns.Add(c => c.Country);
                        columns.Add(c => c.Phone);
                        columns.Add(c => c.Fax);
                    })
            .Filterable()
            .Sortable(sort => sort.SortMode(GridSortMode.MultipleColumn))
            .Pageable()
            .Scrollable(scrolling => scrolling.Height(250))
            .Render(); %>
    <p><%= Html.ActionLink("Create New", "Create") %></p>
</asp:Content>