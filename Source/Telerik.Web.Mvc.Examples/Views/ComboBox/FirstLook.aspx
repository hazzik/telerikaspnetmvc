<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Telerik.Web.Mvc.Examples.ComboBoxFirstLookModel>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">

    <h3>ComboBox</h3>

    <%= Html.Telerik().ComboBox()
                      .Name("ComboBox")
                      .AutoFill(Model.ComboBoxAttributes.AutoFill.Value)
                      .SelectedIndex(Model.ComboBoxAttributes.SelectedIndex.Value)
                      .BindTo(new SelectList(Model.Products, "ProductID", "ProductName"))
                      .HtmlAttributes(new { style = string.Format("width:{0}px", Model.ComboBoxAttributes.Width) })
                      .Filterable(filtering =>
                      {
                          if (Model.ComboBoxAttributes.FilterMode != 0) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                          }
                      })
                      .HighlightFirstMatch(Model.ComboBoxAttributes.HighlightFirst.Value)
    %>
    
    <% using (Html.Configurator("The ComboBox should...")
                  .PostTo("FirstLook", "ComboBox")
                  .Begin())
       { %>
        <ul>
            <li>
                be
                <%= Html.Telerik().IntegerTextBoxFor(m => m.ComboBoxAttributes.Width)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(0)
                                  .MaxValue(1000)
                %>
                pixels <strong>wide</strong>
            </li>
            <li>
                <label><strong>select item</strong> with index</label>
                <%= Html.Telerik().NumericTextBoxFor(m => m.ComboBoxAttributes.SelectedIndex)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(-1)
                                  .MaxValue(Model.Products.Count() - 1)
                                  .DecimalDigits(0)
                %>
            </li>
            <li>
                <strong>highlight</strong> first item
                <%= Html.CheckBox("ComboBoxAttributes.HighlightFirst", Model.ComboBoxAttributes.HighlightFirst.GetValueOrDefault(false)) %>
            </li>
            <li>
                <strong>auto-filling</strong> text
                <%= Html.CheckBox("ComboBoxAttributes.AutoFill", Model.ComboBoxAttributes.AutoFill.GetValueOrDefault(false)) %>
            </li>
        </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>
    
    <h3>DropDownList</h3>

    <%= Html.Telerik().DropDownList()
                      .Name("DropDownList")
                      .SelectedIndex(Model.DropDownListAttributes.SelectedIndex.Value)
                      .BindTo(new SelectList(Model.Products, "ProductID", "ProductName"))
                      .HtmlAttributes(new { style = string.Format("width:{0}px", Model.DropDownListAttributes.Width) })
    %>
    
    <% using (Html.Configurator("The DropDownList should...")
                  .PostTo("FirstLook", "ComboBox")
                  .Begin())
       { %>
        <ul>
            <li>
                be
                <%= Html.Telerik().IntegerTextBoxFor(m => m.DropDownListAttributes.Width)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(0)
                                  .MaxValue(1000)
                %>
                pixels <strong>wide</strong>
            </li>
            <li>
                <label><strong>select item</strong> with index</label>
                <%= Html.Telerik().IntegerTextBoxFor(m => m.DropDownListAttributes.SelectedIndex)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(0)
                                  .MaxValue(Model.Products.Count() - 1)
                %>
            </li>
        </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

    <h3>AutoComplete</h3>

    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(Model.Products.Select(p=>p.ProductName))
            .AutoFill(Model.AutoCompleteAttributes.AutoFill.Value)
            .HtmlAttributes(new { style = string.Format("width:{0}px", Model.AutoCompleteAttributes.Width) })
            .HighlightFirstMatch(Model.AutoCompleteAttributes.HighlightFirst.Value)
            .Filterable(filtering =>
            {
                if (Model.AutoCompleteAttributes.FilterMode != 0) 
                {
                    filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                }
            })
            .Multiple(multi =>
            {
                multi.Separator(Model.AutoCompleteAttributes.MultipleSeparator)
                     .Enabled(Model.AutoCompleteAttributes.AllowMultipleValues.Value);
                     
            })
    %>

    <% using (Html.Configurator("The AutoComplete should...")
                .PostTo("FirstLook", "ComboBox")
                .Begin())
       { %>
        <ul>
            <li>
                be
                <%= Html.Telerik().IntegerTextBoxFor(m => m.AutoCompleteAttributes.Width)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(0)
                                  .MaxValue(1000)
                %>
                pixels <strong>wide</strong>
            </li>
            <li>
                allow <strong>multiple</strong> values
                <%= Html.CheckBox("AutoCompleteAttributes.AllowMultipleValues", Model.AutoCompleteAttributes.AllowMultipleValues.GetValueOrDefault(false)) %>
                separated by
                <%= Html.TextBoxFor(m => m.AutoCompleteAttributes.MultipleSeparator, new { style = "width: 40px"}) %>
            </li>
            <li>
                <strong>highlight</strong> first item
                <%= Html.CheckBox("AutoCompleteAttributes.HighlightFirst", Model.AutoCompleteAttributes.HighlightFirst.GetValueOrDefault(false)) %>
            </li>
            <li>
                <strong>auto-filling</strong> text
                <%= Html.CheckBox("AutoCompleteAttributes.AutoFill", Model.AutoCompleteAttributes.AutoFill.GetValueOrDefault(false)) %>
            </li>
        </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

</asp:content>

<asp:content ID="Content1" contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #ComboBox
        {
            margin-bottom: 280px;
            float: left;
        }
        
        #DropDownList
        {
            clear:both;
            margin-bottom: 230px;
            float: left;
        }
        
        #AutoComplete
        {
            clear:both;
            margin-bottom: 230px;
            float: left;
        }
       
        .example .configurator
        {
            width: 400px;
            float: left;
            margin: 0 0 0 15em;
            display: inline;
        }
        
        .configurator p
        {
            margin: 0;
            padding: .4em 0;
        }
        
        .configurator li
        {
            padding-bottom: 3px;
        }
        
        .configurator .t-dropdown
        {
            vertical-align: middle;
        }
    </style>
</asp:content>