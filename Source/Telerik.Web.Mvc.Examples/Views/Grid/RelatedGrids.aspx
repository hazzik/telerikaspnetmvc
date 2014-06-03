<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<h3>Customers</h3>

<%= Html.Telerik().Grid<Customer>()
        .Name("Customers")
        .Columns(columns=>
        {
            columns.Add(c => c.CustomerID).Width(100);
            columns.Add(c => c.CompanyName).Width(250);
            columns.Add(c => c.ContactName);
            columns.Add(c => c.Country).Width(200);
        })
        .Ajax(ajax => ajax.Action("_RelatedGrids_Customers", "Grid"))
        .Pageable()
        .Sortable()
        .RowAction(row =>
        {
            if (row.DataItem.CustomerID == "ALFKI")
            {
                row.HtmlAttributes["class"] = "t-state-selected";
            }
        })
        .BindTo((IEnumerable<Customer>)ViewData["Customers"])
%>

<h3>Orders (<span id="customerID">ALFKI</span>)</h3>

<%= Html.Telerik().Grid<Order>()
        .Name("Orders")
        .Columns(columns=>
        {
            columns.Add(c => c.OrderID).Width(100);
            columns.Add(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
            columns.Add(c => c.ShipAddress);
            columns.Add(c => c.ShipCity).Width(200);
        })
        .Pageable()
        .Sortable()
        .Ajax(ajax => ajax.Action("_RelatedGrids_Orders", "Grid", new { customerID = "ALFKI" }))
        .BindTo((IEnumerable<Order>)ViewData["Orders"])
%>

<%
    Html.Telerik().ScriptRegistrar()
        .OnDocumentReady(() => 
        {
            %>
            var ordersGrid = $('#Orders').data('tGrid');

            $('#Customers tbody tr')
                .live("click", function() {
                    // `this` == clicked row

                    var customerID = this.cells[0].innerHTML; 

                    // add selected style to clicked row
                    $(this).addClass('t-state-selected')
                           .siblings()
                           .removeClass('t-state-selected');

                    // update ui text
                    $('#customerID').text(customerID);

                    // rebind the related grid
                    ordersGrid.rebind({
                        customerID: customerID
                    });
                })
                /* add hover effects */
                .live("mouseenter", $.telerik.hover)
                .live("mouseleave", $.telerik.leave);
        <% 
        });
%>
</asp:Content>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Related Grids</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .t-grid tr
        {
            cursor: pointer;
        }
    </style>
</asp:Content>
