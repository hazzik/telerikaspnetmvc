<%@ Page Title="Auto generate columns" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CustomerDto>>" %>

<asp:content contentPlaceHolderID="ExampleTitle" runat="server">Auto Generated Columns</asp:content>

<asp:content contentPlaceHolderID="MainContent" runat="server">
	
<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Sortable()
        .Scrollable()
        .Pageable()
%>
</asp:content>
