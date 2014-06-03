<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%= Html.Telerik().Window()
            .Name("Window")
            .Resizable()
            .Width(100)
            .Height(200) 
    %>

    <div id="Window1"></div>
    <div id="Window2"></div>
    <div id="Window3"></div>
    <div id="Window4"></div>
    
    <script type="text/javascript">
    
    function test_creates_default_html_structure() {
        var $window = $('#Window1').tWindow();

        assertTrue($window.is('.t-widget, .t-window'));
        assertTrue($window.children().eq(0).is('.t-window-titlebar, .t-header'));
    }

    function test_if_contentUrl_is_local_call_ajaxRequest_on_refresh_method() {
        var window = $('#Window2').tWindow().data('tWindow');
        var isCalled = false;

        window.contentUrl = "~/something";
        
        window.ajaxRequest = function () { isCalled = true };

        window.refresh();

        assertEquals(true, isCalled);
    }

    function test_if_contentUrl_is_remote_should_not_call_ajaxRequest_on_refresh_method() {
        var window = $('#Window3').tWindow().data('tWindow');
        var isCalled = false;

        window.contentUrl = "http://";

        window.ajaxRequest = function () { isCalled = true };

        window.refresh();

        assertEquals(false, isCalled);
    }

    function test_construction_triggers_open_and_activate_events() {
        var isActivated = false, isOpened = false;

        $('#Window4').tWindow({
            onOpen: function () { isOpened = true; },
            onActivate: function () {isActivated = true; }
        }).data('tWindow');

        assertEquals(true, isActivated);
        assertEquals(true, isOpened);
    }

    function test_construction_does_not_trigger_open_and_activate_events_on_hidden_windows() {
        var isActivated = false, isOpened = false;

        $('<div />').hide().tWindow({
            onOpen: function () { isOpened = true; },
            onActivate: function () {isActivated = true; }
        }).data('tWindow');

        assertEquals(false, isActivated);
        assertEquals(false, isOpened);
    }

    function drag(start, end, element) {
        element
            .simulate('mousedown', $.extend({ which: 1 }, start))
            .simulate('mousemove', $.extend({ which: 1 }, end))
            .simulate('mouseup', $.extend({ which: 1 }, end));
    }

    </script>
</asp:Content>

<%-- FAILS IN IE    
    function test_resize_east() {

        var $window = $('#Window');
        var resizeHandle = $('.t-resize-e');

        drag({ clientX: resizeHandle.offset().left, clientY: resizeHandle.offset().top },
             { clientX: $window.offset().left + 200 + resizeHandle.width() * 2, clientY: resizeHandle.offset().top },
             resizeHandle);

        assertEquals(200, $window.find('.t-window-content').width());
    }--%>