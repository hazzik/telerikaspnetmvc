<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Sitemap binding</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().TabStrip()
           .Name("TabStrip")
		   /* ViewData["sample"] contains the sitemap */
		   .BindTo("sample")
           .Render();
    %>

</asp:Content>
