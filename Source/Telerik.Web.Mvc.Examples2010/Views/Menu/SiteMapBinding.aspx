<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">SiteMap</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">
    
<%= Html.Telerik().Menu()
       .Name("Menu")
       .BindTo("sample")
       .HtmlAttributes(new { style = "z-index: 3; position: relative;" })
%>
    
</asp:Content>