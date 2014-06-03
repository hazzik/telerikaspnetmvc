<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Product>>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <%= Html.Telerik().ComboBox()
                      .Name("ComboBox")
                      .BindTo(new SelectList(Model, "ProductID", "ProductName"))
                      .HtmlAttributes(new { style = "width: 200px; float: left; margin-bottom: 280px;" })
    %>
    
    <script type="text/javascript">

        function selectItemInComboBox() {
            var comboBox = $("#ComboBox").data("tComboBox");
            
            var index = $("#comboBoxItemIndex").data("tTextBox").value();

            comboBox.select(index);
        }

        function getSelectedFromComboBoxt() {
            var comboBox = $("#ComboBox").data("tComboBox");

            alert("Text: " + comboBox.text() + " Value: " + comboBox.value());
        }

        // ComboBox openDropDown event.
        function openComboBoxDropDown(e) {
            $("#ComboBox").data("tComboBox").open();

            // prevent the click from bubbling, thus, closing the popup
            if (e.stopPropagation) e.stopPropagation();
            e.cancelBubble = true;
        }

        // ComboBox openDropDown event.
        function closeComboBoxDropDown(e) {
            $("#ComboBox").data("tComboBox").close();

            // prevent the click from bubbling, thus, closing the popup
            if (e.stopPropagation) e.stopPropagation();
            e.cancelBubble = true;
        }

    </script>

    <% using ( Html.Configurator("ComboBox client API")
                   .Begin() ) 
       { %>
        <ul>
            <li>
                <label>Item index:</label>
                
                <%= Html.Telerik().IntegerTextBox()
                                  .Name("comboBoxItemIndex")
                                  .Value(0) 
                                  .MinValue(0)
                                  .MaxValue(Model.Count() - 1)
                                  .InputHtmlAttributes(new { style = "width: 60px" })
                %>
                
                <button class="t-button t-state-default" onclick="selectItemInComboBox()">Select</button>
            </li>
            <li>
                <button class="t-button t-state-default" onclick="getSelectedFromComboBoxt()" style="width: 100px;">Get Value</button>
            </li>
            <li>
                <button class="t-button t-state-default" onclick="openComboBoxDropDown(event)">Open</button>
                /
                <button class="t-button t-state-default" onclick="closeComboBoxDropDown(event)">Close</button>
            </li>
        </ul>
    <% } %>

    <%= Html.Telerik().DropDownList()
            .Name("DropDownList")
            .BindTo(new SelectList(Model, "ProductID", "ProductName"))
            .HtmlAttributes(new { style = "width: 200px; float: left; clear:both; margin-bottom: 230px;" })
    %>

    <script type="text/javascript">

        function selectItemInDropDownList() {
            var dropDownList = $("#DropDownList").data("tDropDownList");

            var index = $("#dropDownListItemIndex").data("tTextBox").value();

            var $items = dropDownList.dropDown.$element.find('> .t-reset > .t-item');

            dropDownList.select($items[index]);
        }

        function getSelectedFromDropDownList() {
            var dropDownList = $("#DropDownList").data("tDropDownList");

            alert("Text: " + dropDownList.text() + " Value: " + dropDownList.value());
        }

        // DropDownList openDropDown event.
        function openDropDownListDropDown(e) {
            $("#DropDownList").data("tDropDownList").open();

            // prevent the click from bubbling, thus, closing the popup
            if (e.stopPropagation) e.stopPropagation();
            e.cancelBubble = true;
        }

        // DropDownList closeDropDown event.
        function closeDropDownListDropDown(e) {
            $("#DropDownList").data("tDropDownList").close();

            // prevent the click from bubbling, thus, closing the popup
            if (e.stopPropagation) e.stopPropagation();
            e.cancelBubble = true;
        }

    </script>

    <% using ( Html.Configurator("DropDownList client API")
                   .Begin() ) 
           { %>
            <ul>
                <li>
                    <label>Item index:</label>

                    <%= Html.Telerik().IntegerTextBox()
                                      .Name("dropDownListItemIndex")
                                      .Value(0) 
                                      .MinValue(0)
                                      .MaxValue(Model.Count() - 1)
                                      .InputHtmlAttributes(new { style = "width: 60px" })
                    %>
                
                    <button class="t-button t-state-default" onclick="selectItemInDropDownList()">Select</button>
                </li>
                <li>
                    <button class="t-button t-state-default" onclick="getSelectedFromDropDownList()" style="width: 100px;">Get Value</button>
                </li>
                <li>
                    <button class="t-button t-state-default" onclick="openDropDownListDropDown(event)">
                        Open</button>
                    /
                    <button class="t-button t-state-default" onclick="closeDropDownListDropDown(event)">
                        Close</button>
                </li>
            </ul>
    <% } %>

</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        .example .configurator
        {
            width: 300px;
            float: left;
            margin: 0 0 0 10em;
            display: inline;
        }
        
        .configurator p
        {
            margin: 0;
            padding: .4em 0;
        }
        
        .configurator .t-button
        {
            margin: 0 0 1em;
            display: inline-block;
            *display: inline;
            zoom: 1;
        }
        
    </style>
</asp:Content>
