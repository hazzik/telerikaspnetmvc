<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ComboBox Rendering</h2>

    <script type="text/javascript">
        function getAutoComplete(selector) {
            return $(selector || '#AutoComplete').data('tAutoComplete');
        }

        function test_click_item_in_dropDown_list_when_it_is_shown_should_call_select_method() {

            var isSelectCalled = false;

            var autoComplete = getAutoComplete();
            autoComplete.effects = $.telerik.fx.toggle.defaults();

            var old = autoComplete.select;
            autoComplete.select = function () { isSelectCalled = true; }

            autoComplete.open();         

            $(autoComplete.dropDown.$items[2]).click();

            assertTrue(isSelectCalled);

            autoComplete.select = old;
        }

        function test_open_sets_dropdown_zindex() {
            var autoComplete = getAutoComplete();
            autoComplete.effects = autoComplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var $combo = $(autoComplete.element)

            var lastZIndex = $combo.css('z-index');

            $combo.css('z-index', 42);

            autoComplete.close();
            autoComplete.open();

            assertEquals('43', '' + autoComplete.dropDown.$element.parent().css('z-index'));

            $combo.css('z-index', lastZIndex);
        }
    </script>

    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(new string[] { "Item1", "Item2", "Item3" })
            .HtmlAttributes(new { style="position: relative"})
            .Effects(effect => effect.Toggle())
    %>
    
    <div style="display:none">
    <%= Html.Telerik().AutoComplete()
        .Name("AutoCompleteWithServerAttr")
        .DropDownHtmlAttributes(new { style = "width:400px"})
        .BindTo(new string[] {"Item1", "Item2", "Item3"})
        .Effects(effect => effect.Toggle())
    %>
    </div>

</asp:Content>