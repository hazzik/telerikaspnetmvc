<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CustomerDto>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Editing</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% 
    Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
           columns.Add(c => c.CustomerID).Width(80);
           columns.Add(c => c.ContactName);
           columns.Add(c => c.Country).Width(90);
           columns.Add(c => c.Address).Width(200);
           columns.Add(c => 
           { 
                %>
                <%= Html.ActionLink("Edit", "Editing", new { id = c.CustomerID },
                        new { @class="t-link action-edit" }) %>
                <%= Html.ActionLink("Delete", "Delete", new { id = c.CustomerID },
                        new { @class = "t-link action-delete" }) %>
                <%
           }).Title("Actions").Width(150);
        })
        .Pageable()
        .Sortable()
        .Render();
%>

</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .action-edit { background-image: url('<%= ResolveUrl("~/Content/Images/Icons/edit.png") %>'); }
    .action-delete { background-image: url('<%= ResolveUrl("~/Content/Images/Icons/delete.png") %>'); }
    .action-edit, .action-delete
    {
        padding-left: 18px;
        background-repeat: no-repeat;
        background-position: 0 50%;
        margin-right: 10px;
    }

    .action-edit:hover,
    .action-delete:hover
    {
        text-decoration: none;
    }
</style>
</asp:Content>
