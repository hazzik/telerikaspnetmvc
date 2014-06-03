<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Custom Route</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Grid(Model)
		.Name("Grid")
        .Columns(columns =>
        {
            columns.Add(o => o.OrderID).Width(100);
            columns.Add(o => o.Customer.ContactName).Width(200);
            columns.Add(o => o.ShipAddress);
            columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .Sortable()
		.PrefixUrlParameters(false)
		.Pageable()
		.Filterable()
%>
</asp:Content>