<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>NumberParsing</h2>

     <%= Html.Telerik().NumericTextBox()
             .Name("NumericTextBox")
             .MinValue(1)
             .MaxValue(8)
     %>

     <%= Html.Telerik().CurrencyTextBox()
              .Name("CurrencyTextBox")
              .MinValue(-100)
              .MaxValue(10000)
     %>
    
    <script type="text/javascript">

        function getInput(selector) {
            return $(selector || "#NumericTextBox").data("tTextBox");
        }

    </script>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">



        test('parse with simple format', function() {
            equal(getInput('#CurrencyTextBox').parse("1.00"), 1.00);
        });

        test('parse with negative currency format', function() {
            equal(getInput('#CurrencyTextBox').parse("($1.00)"), -1.00);
        });

</script>

</asp:Content>