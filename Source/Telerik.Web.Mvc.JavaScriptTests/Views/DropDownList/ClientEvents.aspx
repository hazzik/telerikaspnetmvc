<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        DropDown Rendering</h2>
    <script type="text/javascript">
        var isChanged;
        var isRaised;
        var isItemDataBound;
        var isDataBinding;
        var isDataBound;

        var itemText;

        function getDropDownList() {
            return $('#DropDownList').data('tDropDownList');
        }

        function getAjaxDropDownList() {
            return $('#AjaxDropDownList').data('tDropDownList');
        }

        function test_clicking_toggle_button_should_raise_onOpen_event() {
            getDropDownList().close();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger('click');

            assertTrue(isRaised);
        }

        function test_clicking_toggle_button_should_raise_onClose_event() {
            getDropDownList().open();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger('click');

            assertTrue(isRaised);
        }

        function test_clicking_alt_and_down_arrow_should_raise_onOpen() {
            getDropDownList().close();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger({ type: "keydown", keyCode: 40, altKey: true });

            assertTrue(isRaised);
        }

        function test_clicking_escape_should_raise_onClose_if_opened() {
            getDropDownList().open();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isRaised);
        }

        function test_clicking_escape_should_not_raise_onClose_if_closed() {
            getDropDownList().close();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger({ type: "keydown", keyCode: 27 });

            assertFalse(isRaised);
        }

        function test_clicking_enter_should_raise_onClose_if_list_is_opened() {
            getDropDownList().open();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isRaised);
        }

        function test_clicking_tab_should_raise_onClose_if_list_is_opened() {
            getDropDownList().open();

            var dropdownlist = $('#DropDownList .t-input');

            isRaised = false;

            dropdownlist.trigger({ type: "keydown", keyCode: 9 });

            assertTrue(isRaised);
        }

        function test_clicking_item_from_dropDownList_should_raise_onClose_when_it_is_opened() {
            getDropDownList().open();

            isRaised = false;
            
            var $selectedItems = $('.t-state-selected', getDropDownList().dropDown.$items);
            $selectedItems = $selectedItems.length > 0 ? $selectedItems : getDropDownList().dropDown.$items.first();
            $selectedItems.next().click();

            assertTrue(isRaised);
        }

        function test_enter_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            getDropDownList().open();

            isChangeRaised = false;

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 40 });
            $ddl.trigger({ type: "keydown", keyCode: 13 });

            assertTrue(isChangeRaised);
        }

        function test_escape_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            getDropDownList().open();

            isChangeRaised = false;

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 40 });
            $ddl.trigger({ type: "keydown", keyCode: 27 });

            assertTrue(isChangeRaised);
        }

        function test_down_arrow_should_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_closed() {
            getDropDownList().close();

            isChangeRaised = false;

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 40 });

            assertTrue(isChangeRaised);
        }

        function test_down_arrow_should_not_raise_onChange_event_if_other_item_is_selected_and_dropDown_is_shown() {
            getDropDownList().open();

            isChangeRaised = false;

            var $ddl = $('#DropDownList');
            $ddl.focus();
            $ddl.trigger({ type: "keydown", keyCode: 40 });

            assertFalse(isChangeRaised);
        }

        function test_clicking_on_new_item_should_raise_onChange_event() {
            var ddl = getDropDownList();
            ddl.open();

            isChangeRaised = false;

            $(ddl.dropDown.$element.find('li')[2]).click();

            assertTrue(isChangeRaised);
        }

        function test_focus_open_close_blur_will_not_raise_change_event() {
            var ddl = getDropDownList();

            var $ddl = $('#DropDownList');
            $ddl.focus();

            ddl.open();
            ddl.close();

            $ddl.blur();

            isChangeRaised = false;

            assertFalse(isChangeRaised);
        }
        
        //handlers
        function onLoad(sender, args) {
            isRaised = true;
        }

        function onChange(sender) {
            isChangeRaised = true;
        }

        function onClose(sender, args) {
            isRaised = true;
        }

        function onOpen(sender, args) {
            isRaised = true;
        }

        function onItemDataBound(e) {

            isItemDataBound = true;

            itemText = e.text;

            e.text = "modified";
        }

        function onDataBinding(sender, args) {
            isDataBinding = true;
        }

        function onDataBound(sender, args) {
            isDataBound = true;
        }

        function DataBindDDL() {
            getAjaxDropDownList().dataBind([{ Text: "1", Value: 1, Selected: false }, { Text: "2", Value: 2, Selected: false }, { Text: "3", Value: 3, Selected: false}]);
        }
    </script>

    <%= Html.Telerik().DropDownList()
            .Name("DropDownList")
            .Items(items =>
            {
                items.Add().Text("Item1").Value("1");
                items.Add().Text("Item2").Value("2");
                items.Add().Text("Item3").Value("3");
                items.Add().Text("Item4").Value("4");
                items.Add().Text("Item5").Value("5");
                items.Add().Text("Item6").Value("6");
                items.Add().Text("Item7").Value("7");
                items.Add().Text("Item8").Value("8");
                items.Add().Text("Item9").Value("9");
                items.Add().Text("Item10").Value("10");
                items.Add().Text("Item11").Value("11");
                items.Add().Text("Item12").Value("12");
                items.Add().Text("Item13").Value("13");
                items.Add().Text("Item14").Value("14");
            })
            .Effects(effects => effects.Toggle())
                    .ClientEvents(events => events.OnLoad("onLoad")
                                                  .OnOpen("onOpen")
                                                  .OnClose("onClose")
                                                  .OnChange("onChange"))
    %>

     <%= Html.Telerik().DropDownList()
            .Name("AjaxDropDownList")
            .Effects(effects => effects.Toggle())
            .DataBinding( binding => binding.Ajax().Select("_AjaxDropDownList","DropDownList"))
            .ClientEvents(events => events.OnDataBinding("onDataBinding")
                                          .OnDataBound("onDataBound"))
    %>

    <%= Html.Telerik().DropDownList()
            .Name("ItemDataBoundDropDownList")
            .Effects(effects => effects.Toggle())
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
           .Add("telerik.common.js")
           .Add("telerik.list.js")); 
    %>

    <br />
</asp:Content>
