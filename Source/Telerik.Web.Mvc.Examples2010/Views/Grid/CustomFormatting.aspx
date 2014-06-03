<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Custom Formatting</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Add(o => o.OrderID).Width(81);
            columns.Add(o => o.Customer.ContactName).Width(200);
            columns.Add(o => o.ShipAddress);
            columns.Add(o => o.Freight).Width(200);
        })
        .CellAction(cell => 
        {
            if (cell.Column.Name == "Freight")
            {
                Order order = cell.DataItem;
				
				// calculate red/green status: 0 - red, 100+ - green
				double freightCoeficient = (Math.Min((int)order.Freight, 100) / 100.0);
				string redGreenStatus = 
					String.Format(
						"#{0:X2}{1:X2}00",
						(int)(255 * (1 - freightCoeficient)),
						(int)(255 * freightCoeficient));

				cell.HtmlAttributes["style"] = "font-weight: bold; color: " + redGreenStatus;
            }
        })
        .Pageable()
        .Sortable()
%>
</asp:Content>
