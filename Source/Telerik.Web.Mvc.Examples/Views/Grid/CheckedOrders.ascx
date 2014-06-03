<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Order>>" %>

<h3>Checked Orders</h3>

<%= Html.Telerik().Grid(Model)
        .Name("CheckedOrders")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .Footer(false)
%>