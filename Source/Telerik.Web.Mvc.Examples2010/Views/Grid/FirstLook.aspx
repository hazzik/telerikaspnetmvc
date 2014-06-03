<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:content contentPlaceHolderID="exampletitle" runat="server">First Look</asp:content>

<asp:content contentPlaceHolderID="maincontent" runat="server">

<% using (Html.Configurator("The grid should...")
              .PostTo("FirstLook", "Grid")
              .Begin())
   { %>
    <ul>
        <li><%= Html.CheckBox("ajax", true, "make <strong>AJAX</strong> requests")%></li>
        <li><%= Html.CheckBox("filtering", true, "allow <strong>filtering</strong> of data")%></li>
        <li><%= Html.CheckBox("paging", true, "have <strong>pages</strong> with 10 items")%></li>
        <li><%= Html.CheckBox("scrolling", true, "show a <strong>scrollbar</strong> when there are many items")%></li>
        <li><%= Html.CheckBox("sorting", true, "allow <strong>sorting</strong> of data")%></li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<%= Html.Telerik().Grid<Order>(Model)
        .Name("Grid")
        .Columns(columns =>
		{
			columns.Add(o => o.OrderID).Width(100);
			columns.Add(o => o.Customer.ContactName).Width(200);
			columns.Add(o => o.ShipAddress);
			columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .ServerBinding(serverBinding => serverBinding.Action("FirstLook", "Grid", new { ajax = ViewData["ajax"] }))
		.Ajax(ajax => ajax.Enabled((bool)ViewData["ajax"]).Action("_FirstLook", "Grid"))
        .Sortable(sorting => sorting.Enabled((bool)ViewData["sorting"]))
		.Scrollable(scrolling => scrolling.Enabled((bool)ViewData["scrolling"]))
        .Pageable(paging => paging.Enabled((bool)ViewData["paging"]))
        .Filterable(filtering => filtering.Enabled((bool)ViewData["filtering"]))
%>

</asp:content>
