<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Product>>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">
   <div class="panel">
        <h3>ComboBox</h3>

        <%= Html.Telerik().ComboBox()
                .Name("ComboBox")
                .AutoFill(true)
                .BindTo(new SelectList(Model, "ProductID", "ProductName"))
                .HtmlAttributes(new { style = string.Format("width: 250px") })
                .Filterable(filtering =>
                {
                    filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                })
                .HighlightFirstMatch(true)
        %>
    
    </div>
    <div class="panel">
        <h3>AutoComplete</h3>

        <%= Html.Telerik().AutoComplete()
                .Name("AutoComplete")
                .BindTo(Model.Select(p=>p.ProductName))
                .AutoFill(true)
                .HtmlAttributes(new { style = string.Format("width: 250px") })
                .HighlightFirstMatch(true)
                .Filterable(filtering =>
                {
                    filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                })
        %>
    </div>

    <% Html.RenderPartial("AccessibilityValidation"); %>
</asp:content>
<asp:content ID="Content1" contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .panel 
        {
             float:left;
             width:30%;
             padding-bottom: 3em;
        }
        
        .panel h3
        {
            font-weight: normal;
        }
        
        .panel .t-autocomplete
        {
            margin-top: 2px;
        }
       
        .t-dropdown select
        {
            position: absolute;
            top: 0;
            width: 100%;
        }
    </style>
</asp:content>
