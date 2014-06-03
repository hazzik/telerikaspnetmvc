﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"%>
<asp:content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Calendar()
        .Name("Calendar")
        .HtmlAttributes(new { style = "width: 294px;" })
%>

<% Html.RenderPartial("CulturePicker"); %>

<% Html.Telerik().ScriptRegistrar().Globalization(true); %>

</asp:Content>
<asp:Content contentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
.configurator
{
    float: right; 
    margin: 0 200px 0 0; 
    width: 200px;
}
</style>
</asp:Content>