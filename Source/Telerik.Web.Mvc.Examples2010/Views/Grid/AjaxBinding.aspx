<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Ajax Binding</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">
<%= Html.Telerik().Grid<Order>()
        .Name("Grid")
        .Columns(columns =>
		{
			columns.Add(o => o.OrderID).Width(100);
			columns.Add(o => o.Customer.ContactName).Width(200);
			columns.Add(o => o.ShipAddress);
			columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
		.Ajax(ajax => ajax.Action("_AjaxBinding", "Grid"))
        .Pageable()
        .Sortable()
        .Scrollable()
        .Filterable()
%>
</asp:Content>
