<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AggregatedProductModel>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">  
    <% Html.BeginForm(); %>  
    <%  
        Html.Telerik().Grid(Model.Products)
        .Name("Grid")
        .Columns(columns =>
        {     
            columns.Template(p =>
                            {
                                %>
                                    <input name="checkedRecords" type="checkbox" 
                                    value="<%=p.ProductID %>" title="checkedRecords"                         
                                    <% if (Model.SelectedProducts.Any(sp => sp.ProductID == p.ProductID)) {
                                        %> checked="checked" <%
                                      } %>
                                   />
                                <%
                            })
                    .HeaderTemplate(() =>
                                {%>
                                <input type="checkbox" title="check all records" onclick="checkAll(this);"
                                <% if (Model.SelectedProducts.Any()) {
                                %> checked="checked" <%
                                } %>
                                />
                                <%})
                    .Width(50)
                    .HeaderHtmlAttributes(new { style = "text-align:center" })
                    .HtmlAttributes(new { style = "text-align:center" });
            columns.Bound(p => p.ProductID).Width(100);
            columns.Bound(p => p.ProductName);
            columns.Bound(p => p.QuantityPerUnit).Width(200);
            columns.Template(t =>
                                 {%>
                                    <table cellspacing="0" class="data-row">
                                        <tr>
                                            <td>
                                                <%=t.UnitsOnOrder%>
                                            </td>
                                            <td>
                                                <%=t.UnitsInStock%>
                                            </td>
                                        </tr>
                                    </table>
                                    <%
                                 })
                   .HeaderTemplate(() =>
                                    {%>
                                    <table cellspacing="0" class="data-header">
                                        <tr>
                                            <td colspan="2">
                                                <strong>Units</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Ordered
                                            </td>
                                            <td>
                                                In Stock
                                            </td>
                                        </tr>
                                    </table>
                                    <%
                                    }).Width(200);
            columns.Bound(p => p.UnitPrice)
                   .FooterTemplate(() =>
                                        {%>
                                        Total:<%= string.Format("{0:c}", Model.TotalPrice)%>
                                        <%})
                   .Width(100)
                   .Format("{0:c}");
            })
            .Pageable()
            .Render();
        %>           
    <p>
        <button type="submit" class="t-button t-state-default">Display selected
            products</button>
    </p>
    <% Html.EndForm(); %>

    <% if (Model.SelectedProducts.Any()) { %>       
      <h3>Selected Products</h3>
      <%= Html.Telerik().Grid(Model.SelectedProducts)
              .Name("CheckedProducts")
              .Columns(columns =>
              {
                    columns.Bound(p => p.ProductID).Width(100);
                    columns.Bound(p => p.ProductName).Width(200);
                    columns.Bound(p => p.UnitPrice).Width(100).Format("{0:c}");
              })
              .Footer(false)
      %>           
    <%}%>

    <script type="text/javascript">
        function checkAll(sender) {            
            $("#Grid tbody input:checkbox").attr("checked", sender.checked);            
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .data-row, .data-header
        {
            width: 100%;
            margin: 0;
            padding: 0;
        }
        
        .data-header td,.data-row td
        {
            width: 50%;
            border: 0;
            vertical-align: middle;
            text-align: center;
        }
    </style>
</asp:Content>
