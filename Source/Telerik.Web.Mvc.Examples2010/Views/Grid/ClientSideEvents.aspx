<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentPlaceHolderID="ExampleTitle" runat="server">Client-side Events</asp:content>

<asp:content contentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
function onLoad(e) {
    $console.log("onLoad");
}

function onDataBinding(e) {
    $console.log("onDataBinding");
}

function onRowDataBound(e) {
    var dataItem = e.dataItem;
    $console.log("onRowDataBound :: " + dataItem.OrderID);
}
</script>
<%= Html.Telerik().Grid<Order>()
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Add(o => o.OrderID).Width(100);
            columns.Add(o => o.Customer.ContactName).Width(200);
            columns.Add(o => o.ShipAddress);
            columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .ClientEvents(events => events
            .OnLoad("onLoad")
            .OnDataBinding("onDataBinding")
            .OnRowDataBound("onRowDataBound")
        )
        .Ajax(ajax => ajax.Action("_ClientSideEvents_Ajax", "Grid"))
        .Pageable(paging => paging.PageSize(4))
        .Sortable()
        .Filterable()
%>

<% Html.RenderPartial("EventLog"); %>
</asp:content>