<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Customer>>" %>

<asp:Content ID="Content3" contentPlaceHolderID="ExampleTitle" runat="server">Templates</asp:Content>

<asp:Content ID="Content1" contentPlaceHolderID="MainContent" runat="server">
<% 
    Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Add(c => { 
                %><img 
                    alt="<%= c.CustomerID %>" 
                    src="<%= Url.Content("~/Content/Images/Customers/" + c.CustomerID + ".jpg") %>" 
                  /><% 
            }).Title("Picture");
            columns.Add(c => c.ContactName).Title("Name");
            columns.Add(c => c.Phone);
        })
        .Sortable()
        .Scrollable(scrolling => scrolling.Height(250))
        .Pageable()
        .Render();
 %>
</asp:Content>