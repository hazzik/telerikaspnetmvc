<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        DropDown Rendering</h2>
    <script type="text/javascript">

        function getAutoComplete() {
            return $('#AutoComplete').data('tAutoComplete');
        }

        function test_fill_method_should_call_component_dataBind_method() {
            var autoComplete = getAutoComplete();

            var isCalled = false;

            autoComplete.dataBind = function () { isCalled = true; };
            autoComplete.dropDown.$items = null;
            autoComplete.fill();

            assertTrue(isCalled);
        }

        function test_enable_method_should_enable_autoComplete() {
            var autoComplete = getAutoComplete();

            autoComplete.enable();
            autoComplete.disable();

            assertTrue($('#AutoComplete').hasClass('t-state-disabled'));
            assertEquals(true, $('#AutoComplete').attr('disabled'));
        }

        function test_enable_method_should_disable_autoComplete() {
            var autoComplete = getAutoComplete();

            autoComplete.disable();
            autoComplete.enable();

            assertFalse($('#AutoComplete').hasClass('t-state-disabled'));
            assertFalse($('#AutoComplete').attr('disabled'));
        }

    </script>

    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
    %>

    <br />
</asp:Content>
