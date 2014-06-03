<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TextBoxFirstLookModelView>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

<div class="t-rtl">
    <%= Html.Telerik().NumericTextBox()
            .Name("NumericTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42) 
    %>

    <%= Html.Telerik().CurrencyTextBox()
            .Name("CurrencyTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
    %>

    <%= Html.Telerik().PercentTextBox()
            .Name("PercentTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
    %>

    <%= Html.Telerik().IntegerTextBox()
            .Name("IntegerTextBox")
            .HtmlAttributes(new { @class = "t-numerictextbox-rtl" })
            .Value(42)
%>
</div>

</asp:content>

<asp:content contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        .t-numerictextbox
        {
            margin-bottom: 30px;
            display: block !important;
        }
        
    </style>
</asp:content>