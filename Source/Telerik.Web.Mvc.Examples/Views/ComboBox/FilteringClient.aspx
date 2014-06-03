﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Telerik.Web.Mvc.Examples.ComboBoxFilteringModel>" %>

<asp:content ID="Content2" contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().ComboBox()
                      .Name("ComboBox")
                      .AutoFill(Model.ComboBoxAttributes.AutoFill.Value)
                      .BindTo(new SelectList(Model.Products, "ProductID", "ProductName"))
                      .Filterable(filtering =>
                      {
                          if (Model.ComboBoxAttributes.FilterMode != 0)
                          {
                              filtering.FilterMode((AutoCompleteFilterMode)Enum.Parse(typeof(AutoCompleteFilterMode), Model.ComboBoxAttributes.FilterMode.ToString()));
                              filtering.MinimumChars(Model.ComboBoxAttributes.MinimumChars.Value);
                          }
                      })
                      .HighlightFirstMatch(Model.ComboBoxAttributes.HighlightFirst.Value)
    %>
    
    <% using (Html.Configurator("The ComboBox should...")
                .PostTo("FilteringClient", "ComboBox")
                .Begin())
       { %>
        <ul>
            <li>
                have <strong>filter mode</strong>&nbsp;
                <% Html.Telerik().DropDownList()
                                 .Name("ComboBoxAttributes.FilterMode")
                                 .HtmlAttributes(new { style = "width:100px" })
                                 .Items(items =>
                                 {
                                     items.Add().Text("None").Value("0");
                                     items.Add().Text("StartsWith").Value("1");
                                     items.Add().Text("Contains").Value("2");
                                 })
                                 .SelectedIndex(Model.ComboBoxAttributes.FilterMode.Value)
                                 .ClientEvents(events =>
                                     events.OnChange(() =>
                                     {
                                     %>
                                         function(e) {
                                             var textbox = $('#ComboBoxAttributes_MinimumChars').data('tTextBox');
                                             if(e.value == 0) textbox.disable();
                                             else textbox.enable();
                                         }
                                     <%
                                     })
                                 ).Render();
                %>
            </li>
            <li>
                <strong>start filtering</strong> after
                <% Html.Telerik().IntegerTextBoxFor(m => m.ComboBoxAttributes.MinimumChars)
                                 .InputHtmlAttributes(new { style = "width: 60px" })
                                 .MinValue(0)
                                 .MaxValue(1000)
                                 .ClientEvents(events =>
                                     events.OnLoad(() =>
                                     {                                
                                     %>
                                         function(e) {
                                             var dropDownList = $('#ComboBoxAttributes_FilterMode').data('tDropDownList');
                                             if(dropDownList.index == 0) $(this).data('tTextBox').disable();
                                         }
                                     <%
                                 })
                                 ).Render();
                %>
                chars            
            </li>
            <li>
                <strong>highlight</strong> first item
                <%= Html.CheckBox("ComboBoxAttributes.HighlightFirst", Model.ComboBoxAttributes.HighlightFirst.GetValueOrDefault(false), new { title = "HighlightFirst" })%>
            </li>
            <li>
                <strong>auto-filling</strong> text
                <%= Html.CheckBox("ComboBoxAttributes.AutoFill", Model.ComboBoxAttributes.AutoFill.GetValueOrDefault(false), new { title = "AutoFill" })%>
            </li>
        </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

    <%= Html.Telerik().AutoComplete()
                      .Name("AutoComplete")
                      .AutoFill(Model.AutoCompleteAttributes.AutoFill.Value)
                      .BindTo(Model.Products.Select(p => p.ProductName))
                      .Filterable(filtering =>
                      {
                            filtering.FilterMode((AutoCompleteFilterMode)Enum.Parse(typeof(AutoCompleteFilterMode), Model.AutoCompleteAttributes.FilterMode.ToString()));
                            filtering.MinimumChars(Model.AutoCompleteAttributes.MinimumChars.Value);
                      })
                      .HighlightFirstMatch(Model.AutoCompleteAttributes.HighlightFirst.Value)
                      .Multiple( multi => multi.Enabled(Model.AutoCompleteAttributes.AllowMultipleValues.Value))
    %>
    
    <% using (Html.Configurator("The AutoComplete should...")
                .PostTo("FilteringClient", "ComboBox")
                .Begin())
       { %>
        <ul>
            <li>
                have <strong>filter mode</strong>&nbsp;
                <%= Html.Telerik().DropDownList()
                                  .Name("AutoCompleteAttributes.FilterMode")
                                  .HtmlAttributes(new { style = "width:100px" })
                                  .Items(items =>
                                  {
                                      items.Add().Text("StartsWith").Value("1");
                                      items.Add().Text("Contains").Value("2");
                                  })
                                  .SelectedIndex(Model.AutoCompleteAttributes.FilterMode.Value - 1)
                %>
            </li>
            <li>
                <strong>start filtering</strong> after
                <%= Html.Telerik().IntegerTextBoxFor(m => m.AutoCompleteAttributes.MinimumChars)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                                  .MinValue(1)
                                  .MaxValue(1000)
                %>
                chars            
            </li>
            <li>
                <strong>highlight</strong> first item
                <%= Html.CheckBox("AutoCompleteAttributes.HighlightFirst", Model.AutoCompleteAttributes.HighlightFirst.GetValueOrDefault(false), new { title = "HighlightFirst" })%>
            </li>
            <li>
                <strong>auto-filling</strong> text
                <%= Html.CheckBox("AutoCompleteAttributes.AutoFill", Model.AutoCompleteAttributes.AutoFill.GetValueOrDefault(false), new { title = "AutoFill" })%>
            </li>
            <li>
                allow <strong>multiple</strong> values
                <%= Html.CheckBox("AutoCompleteAttributes.AllowMultipleValues", Model.AutoCompleteAttributes.AllowMultipleValues.GetValueOrDefault(false), new { title = "AllowMultipleValues" })%>
            </li>
        </ul>
        <button type="submit" class="t-button t-state-default">Apply</button>
    <% } %>

</asp:content>

<asp:content ID="Content1" contentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        #AutoComplete
        {
            clear: both;
            margin-bottom: 280px;
            float: left;
        }
        
        #ComboBox
        {
            margin-bottom: 280px;
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