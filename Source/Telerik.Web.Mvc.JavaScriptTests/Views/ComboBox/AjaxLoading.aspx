<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ajax loading</h2>
    
    <script type="text/javascript">

        function getComboBox() {
            return $('#AjaxCombo').data('tComboBox');
        }

        function test_fill_should_call_ajaxRequest_if_ajax_enabled() {
            var isCalled = false;
            var combo = getComboBox();
            var oldMethod = combo.loader.ajaxRequest;

            combo.loader.ajaxRequest = function () { isCalled = true; };
            combo.minChars = 0;
            combo.dropDown.$items = null;
            combo.fill();

            combo.ajaxRequest = oldMethod;

            assertTrue(isCalled);
        }

        function test_fill_should_not_call_ajaxRequest_if_ajax_enabled_and_entered_value_is_shorter_than_minLetters() {
            var isCalled = false;
            var combo = getComboBox();
            var oldMethod = combo.loader.ajaxRequest;

            combo.$text.val('')

            var minChars = combo.minChars;
            combo.minChars = 1;

            combo.loader.ajaxRequest = function () { isCalled = true; };

            combo.fill();

            combo.loader.ajaxRequest = oldMethod;
            combo.minChars = minChars;

            assertFalse(isCalled);
        }

        function test_fill_should_pass_custom_parameters_to_the_ajaxRequest() {
            var ajaxOptions;
            var testText = "test";
            var combo = $('#AjaxCombo2').data('tComboBox');
            var old = $.ajax;
            
            combo.$text.val(testText)

            $.ajax = function (result) { ajaxOptions = result; };
            
            combo.fill();

            $.ajax = old;

            assertEquals(testText, ajaxOptions.data.Test);
        }

        //handlers
        function onLoad(e) {
            isRaised = true;
        }

        function onChange(e) {
            isChangeRaised = true;
        }

        function onClose(e) {
            isRaised = true;
        }

        function onOpen(e) {
            isRaised = true;
        }

        function onDataBindingPassData(e) {
            e.data = $.extend({}, e.data, { Test: "test" });
        }

        function onDataBinding(e) {
            isDataBinding = true;
        }

        function onDataBound(e) {
            isDataBound = true;
        }

        function DataBindCombo() {
            getComboBox().dataBind([{ Text: "1", Value: 1, Selected: false }, { Text: "2", Value: 2, Selected: false }, { Text: "3", Value: 3, Selected: false}]);
        }
    </script>
    
    <%= Html.Telerik().ComboBox()
            .Name("AjaxCombo")
            .Effects(effects => effects.Toggle())
            .DataBinding(binding => binding.Ajax().Select("_AjaxLoading", "ComboBox"))
            .ClientEvents(events => events.OnDataBinding("onDataBinding")
                                        .OnDataBound("onDataBound"))
    %>

    <%= Html.Telerik().ComboBox()
        .Name("AjaxCombo2")
        .Effects(effects => effects.Toggle())
        .DataBinding(binding => binding.Ajax().Select("_AjaxLoading", "ComboBox"))
        .ClientEvents(events => events.OnDataBinding("onDataBindingPassData"))
    %>
</asp:Content>
