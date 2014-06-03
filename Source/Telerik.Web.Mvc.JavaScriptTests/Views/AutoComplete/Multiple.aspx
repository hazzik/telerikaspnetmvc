<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Keyboard support</h2>
    
    <script type="text/javascript">

        function getAutoComplete() {
            return $('#AutoComplete').data('tAutoComplete');
        }

        function test_value_method_should_to_set_input_value() {
            var text = "test";
            
            getAutoComplete().value("test");

            assertEquals(text, getAutoComplete().$text.val());

            getAutoComplete().$text.val('')
        }

        function test_value_method_should_return_input_value() {
            var text = "test";

            getAutoComplete().$text.val(text);

            assertEquals(text, getAutoComplete().value());

            getAutoComplete().$text.val('')
        }

        function test_autoFill_method_should_auto_fill_second_word() {
            var $t = $.telerik;
            var autocomplete = getAutoComplete();
            var caretPos = $t.caretPos;

            autocomplete.fill();
            autocomplete.$text.val('Chai, Cha, Tofu, ');

            $t.caretPos = function () { return 9; } //return caret position after 'Cha'
            
            autocomplete.filtering.autoFill(autocomplete, "Chang");

            assertEquals('Chai, Chang, Tofu, ', autocomplete.$text.val());

            //revert changes
            autocomplete.$text.val('');
            $t.caretPos = caretPos;
        }

    </script>
    
    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(new string[]{
                "Chai", "Chang", "Tofu", "Boysenberry", "Uncle", "Northwoods",
                "Ikura", "Queso", "Manchego", "Dried", "тtem20"
            })
            .Multiple()
            .AutoFill(true) 
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")
               .Add("telerik.autocomplete.js")); %>

 <%--              //        function test_select_method_should_update_second_word_in_the_input() {
//            var autocomplete = getAutoComplete();
//            var valueArrayIndex = $.telerik.combobox.valueArrayIndex;

//            autocomplete.dropDown();
//            autocomplete.$text.val('Chai, Chang, Tofu, ');
//            $.telerik.combobox.valueArrayIndex = function () { return 1; };

//            autocomplete.select(autocomplete.$items[4]);

//            assertEquals('Chai, Uncle, Tofu, ', autocomplete.$text.val());

//            //revert changes
//            autocomplete.$text.val('');
//            $.telerik.combobox.valueArrayIndex = valueArrayIndex;
//        }

//        function test_select_method_should_add_separator_if_new_item_is_selected_and_cursor_is_in_the_end() {
//            var autocomplete = getAutoComplete();
//            var valueArrayIndex = $.telerik.combobox.valueArrayIndex;

//            autocomplete.dropDown();
//            autocomplete.$text.val('Chai, Chang, Tofu, d');
//            $.telerik.combobox.valueArrayIndex = function () { return 3; };

//            autocomplete.select(autocomplete.$items[9]);

//            assertEquals('Chai, Chang, Tofu, Dried, ', autocomplete.$text.val());

//            //revert changes
//            autocomplete.$text.val('');
//            $.telerik.combobox.valueArrayIndex = valueArrayIndex;
//        }

//        function test_filter_method_should_pass_word_text_depending_on_valueArrayIndex_method() {
//            var autocomplete = getAutoComplete();
//            var valueArrayIndex = $.telerik.combobox.valueArrayIndex;
//            var filterMethod = autocomplete.filters[1];
//            var filterText;

//            autocomplete.dropDown();
//            autocomplete.$text.val('Chai, Cha, Tofu, ');
//            autocomplete.filters[1] = function (component, data, text) { filterText = text; }
//            $.telerik.combobox.valueArrayIndex = function () { return 1; };

//            $.telerik.autocomplete.filter(autocomplete);

//            assertEquals('Cha', filterText);

//            //revert changes
//            autocomplete.$text.val('');
//            autocomplete.filters[1] = filterMethod;
//            $.telerik.combobox.valueArrayIndex = valueArrayIndex;
//        }

//        function test_autoFill_method_should_auto_fill_last_word_and_not_add_separator() {
//            var $t = $.telerik;
//            var valueArrayIndex = $t.combobox.valueArrayIndex;
//            var autocomplete = getAutoComplete();
//            var caretPos = $t.caretPos;

//            autocomplete.dropDown();
//            autocomplete.$text.val('Chai, Chang, Tofu, Dr');

//            $t.caretPos = function () { return 19; } //return caret position after 'Dr'
//            $t.combobox.valueArrayIndex = function () { return 3; };

//            $t.autocomplete.autoFill(autocomplete, "Dried");

//            assertEquals('Chai, Chang, Tofu, Dried, ', autocomplete.$text.val());

//            //revert changes
//            autocomplete.$text.val('');
//            $t.caretPos = caretPos;
//            $t.combobox.valueArrayIndex = valueArrayIndex;
//        }--%>
</asp:Content>

