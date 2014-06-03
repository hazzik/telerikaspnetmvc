<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>DropDown Rendering</h2>

    <script type="text/javascript">
        var loader;
        var component;
        var $component;
        var $t;
        var isCalled = false;

        function setUp() {
            $t = $.telerik;

            $component = $('#DDL');
            component = $component.data('tDropDownList')
            component.onDataBinding = "dataBinding";
            loader = new $t.list.loader(component);
        }

        function test_isAjax_method_should_return_component_ajax_method() {
            component.ajax = { selectUrl: "selectUrl" };

            var result = loader.isAjax();

            assertEquals(component.ajax, result);
        }

        function test_isAjax_method_should_return_component_ws_method() {
            component.ws = { selectUrl: "selectUrl" };

            var result = loader.isAjax();

            assertEquals(component.ws.selectUrl, result.selectUrl);
        }

        function test_isAjax_method_should_return_component_onDataBinding_method() {
            component.onDataBinding = "dataBinding";

            component.ajax = undefined;
            component.ws = undefined;

            var result = loader.isAjax();

            assertEquals("dataBinding", result);
        }

        function test_ajaxRequest_should_raise_dataBinding_event() {
            isCalled = false;
            loader.ajaxRequest(function () { });
            assertTrue(isCalled);
        }

        function test_ajaxRequest_should_create_ajaxOptions_and_pass_them_to_jQuery_ajax_method() {
            var ajaxOptions;
            component.ws = { selectUrl: "testURL" };
            $.ajax = function (options) { ajaxOptions = options; }
            loader.ajaxRequest(function () { });

            assertEquals('testURL', ajaxOptions.url);
            assertEquals('POST', ajaxOptions.type);
            assertEquals('text', ajaxOptions.dataType);
            assertEquals('application/json; charset=utf-8', ajaxOptions.contentType);

            component.ajax = undefined;
        }

        //event handler
        function dataBinding(e) {
            e.data = $.extend({}, e.data, { Test: "test" });
            isCalled = true;
        }

    </script>

    <%= Html.Telerik().DropDownList()
            .Name("DDL")
            .ClientEvents(events => events.OnDataBinding("dataBinding"))
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")); %>
</asp:Content>