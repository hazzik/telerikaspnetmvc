<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Product>>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">

    <h3>ComboBox</h3>

    <div class="t-rtl">
        <%= Html.Telerik().ComboBox()
                          .Name("ComboBox")
                          .HtmlAttributes(new { style = "width: 200px" })
                          .BindTo(new SelectList(Model, "ProductID", "ProductName"))
                          .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.StartsWith))
                          .HighlightFirstMatch(true)
                          .AutoFill(true)
        %>
    </div>
    
    <h3>DropDownList</h3>
    
    <div class="t-rtl">
        <%= Html.Telerik().DropDownList()
                          .Name("DropDownList")
                          .HtmlAttributes(new { style = "width: 200px" })
                          .BindTo(new SelectList(Model, "ProductID", "ProductName"))
        %>
    </div>

    <h3>AutoComplete</h3>
    
    <div class="t-rtl">
        <%= Html.Telerik().AutoComplete()
                          .Name("AutoComplete")
                          .HtmlAttributes(new { style = "width: 200px" })
                          .BindTo(Model.Select(p=>p.ProductName))
                          .AutoFill(true)
                          .HighlightFirstMatch(true)
                          .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.StartsWith))
        %>
    </div>


</asp:content>

<asp:content ID="Content1" contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        #ComboBox
        {
            margin-bottom: 70px;
            float: left;
        }
        
        #DropDownList
        {
            clear:both;
            margin-bottom: 70px;
            float: left;
        }
        
        #AutoComplete
        {
            clear:both;
            margin-bottom: 70px;
            float: left;
        }
       
    </style>
</asp:content>