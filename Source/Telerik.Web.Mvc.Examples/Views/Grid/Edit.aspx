<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CustomerDto>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Editing (<%= Model.CustomerID %>)</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">
    
    <h3>Editing cutomer <strong><%= Model.CustomerID%></strong></h3>
<% using (Html.BeginForm("Save", "Grid", FormMethod.Post, new { id = "example-form" })) { %>
    <%= Html.Hidden("CustomerID")%>
    <ul>
        <li>
            <label for="ContactName">Contact Name:</label>
            <%= Html.TextBox("ContactName")%>
        </li>
        <li>
            <label for="Address">Address:</label>
            <%= Html.TextBox("Address")%>
        </li>
        <li>
            <label for="Country">Country:</label>
            <%= Html.TextBox("Country")%>
        </li>
    </ul>
    
    <button type="submit" class="t-button t-state-default">Save</button>
     or 
    <%= Html.ActionLink("Cancel", "Editing", "Grid", new { id = string.Empty }, new { @class = "t-link" })%>
    
<% } %>
</asp:Content>

<asp:Content contentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
	#example-form form
	{
		padding: 10px 0 30px;
	}
	
	#example-form label,
	#example-form .t-button
	{
		display: inline-block;
		*display: inline;
		zoom: 1;
	}
	
	#example-form ul
	{
		list-style-type: none;
		padding: 0 0 10px;
	}
	
	#example-form li
	{
		padding-bottom: 10px;
	}
	
	#example-form label
	{
		width: 100px;
		padding-right: 7px;
		text-align: right;
	}
	
	#example-form .t-button
	{
		margin-left: 107px;
	}
</style>
</asp:Content>
