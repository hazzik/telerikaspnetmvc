<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Sorting</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% using (Html.Configurator("Sorting should...")
              .PostTo("Sorting", "Grid")
              .Begin())
   { %>
    <ul>
        <li>
            <%= Html.RadioButton("sortMode", GridSortMode.SingleColumn.ToString(), true, new { id = "single" })%>
            <label for="single">sort <strong>one column</strong> only</label>
        </li>
        <li>
            <%= Html.RadioButton("sortMode", GridSortMode.MultipleColumn.ToString(), new { id = "multi" })%>
            <label for="multi">sort <strong>multiple columns</strong> simultaneously</label>
        </li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Add(o => o.OrderID).Width(81);
            columns.Add(o => o.Customer.ContactName).Width(200);
            columns.Add(o => o.ShipAddress);
            columns.Add(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
		.Ajax(ajax => ajax.Action("_Sorting", "Grid"))
        .Pageable()
        .Sortable(sorting => sorting.SortMode((GridSortMode)Enum.Parse(typeof(GridSortMode), (string)ViewData["sortMode"])))
%>

</asp:Content>
