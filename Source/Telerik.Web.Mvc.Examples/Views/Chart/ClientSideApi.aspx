<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().Chart<SalesData>()
            .Name("chart")
            .Theme(Html.GetCurrentTheme())
            .Title("Representative Sales vs. Total Sales")
            .Legend(legend => legend
                .Position(ChartLegendPosition.Bottom)
            )
            .Series(series => {
                series.Column(s => s.RepSales).Name("Representative Sales").Stack(true);
                series.Column(s => s.TotalSales).Name("Total Sales").Stack(true);
            })
            .CategoryAxis(axis => axis
                .Categories(s => s.DateString)
            )
            .ValueAxis(axis => axis
                .Numeric().Format("${0:#,##0}")
            )
            .DataBinding(dataBinding => dataBinding
                .Ajax().Select("_SalesDataRandom", "Chart")
            )            
            .HtmlAttributes(new { style = "width: 670px; height: 400px;" })
    %>

    <% using (Html.Configurator("Client API").Begin()) { %>
        <p>
            <button class="t-button" onclick="Refresh()">refresh</button>
        </p>
    <% } %>

    <script type="text/javascript">

        function getChart(){
            return $("#chart").data("tChart");
        }

        function Refresh() {
            getChart().refresh();
        }

    </script>


</asp:Content>

<asp:Content ID="Content1" contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .configurator p
        {
            margin: 0;
        }
        
        .example .configurator {
            float: right;
            width: 170px;
            margin: 0 0 0 0;
            display: inline;
        }
        
        .t-chart {
            float: left;
        }
    </style>
</asp:Content>