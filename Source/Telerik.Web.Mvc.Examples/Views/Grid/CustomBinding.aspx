<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Custom Binding</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

	<%= Html.Telerik().Grid(Model)
			.Name("Grid")
            .Columns(columns =>
            {
                columns.Add(o => o.OrderID).Width(81);
                columns.Add(o => o.Customer.ContactName).Width(200);
                columns.Add(o => o.ShipAddress);
                columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
            })
			.Ajax(settings => settings.Action("_CustomBinding", "Grid"))
			.Pageable(settings => settings.Total((int)ViewData["total"]))
			.EnableCustomBinding(true)
			.Sortable()
	%>
	
</asp:Content>

