<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" contentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" contentPlaceHolderID="MainContent" runat="server">
<h3>Bound to ASMX Web Service</h3>

<%= Html.Telerik().Grid<Order>()
        .Name("AsmxGrid")
        .Columns(columns =>
		{
			columns.Add(o => o.OrderID).Width(100);
			columns.Add(o => o.Customer.ContactName).Width(200);
			columns.Add(o => o.ShipAddress);
			columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .WebService(webService => webService.Url("~/Models/Orders.asmx/GetOrders"))
        .Sortable()
		.Pageable()
        .Filterable()
        .Scrollable()
%>

<h3>Bound to WCF Web Service using DTO objects</h3>

<%= Html.Telerik().Grid<OrderDto>()
        .Name("WcfGrid")
        .Columns(columns =>
		{
			columns.Add(o => o.OrderID).Width(81);
			columns.Add(o => o.ContactName).Width(200);
			columns.Add(o => o.ShipAddress);
			columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
        .WebService(webService => webService.Url("~/Models/Orders.svc/GetOrders"))
        .Sortable()
		.Pageable()
        .Filterable()
        .Scrollable()
%>

</asp:Content>

<asp:Content ID="Content3" contentPlaceHolderID="ExampleTitle" runat="server">Web Service Binding</asp:Content>
