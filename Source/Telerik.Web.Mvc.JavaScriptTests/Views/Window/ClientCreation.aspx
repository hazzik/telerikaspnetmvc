<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="Window1"></div>
    <div id="Window2"></div>
    <div id="Window3"></div>
    <div id="Window4"></div>

    <% Html.Telerik().ScriptRegistrar().DefaultGroup(group => group
           .Add("telerik.common.js")
           .Add("telerik.draganddrop.js")
           .Add("telerik.window.js")); %>

</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

    <script type="text/javascript">
    
        $.extend($.fn.tWindow.defaults, {
            effects: { list: [{ name: 'toggle' }], openDuration: 'fast', closeDuration: 'fast' }
        });
    
        test('creates default html structure', function() {
            var dialog = $('#Window1').tWindow();

            ok(dialog.is('.t-widget, .t-window'));
            ok(dialog.children().eq(0).is('.t-window-titlebar, .t-header'));

            dialog.data('tWindow').destroy();
        });

        test('if contentUrl is local call ajaxRequest on refresh method', function() {
            var dialog = $('#Window2').tWindow().data('tWindow');
            var hasRequestedData = false;

            dialog.contentUrl = "~/something";
        
            dialog.ajaxRequest = function () { hasRequestedData = true };

            dialog.refresh();

            ok(hasRequestedData);

            dialog.destroy();
        });

        test('if contentUrl is remote should not call ajaxRequest on refresh method', function() {
            var dialog = $('#Window3').tWindow().data('tWindow');
            var hasRequestedData = false;

            dialog.contentUrl = "http://";

            dialog.ajaxRequest = function () { hasRequestedData = true };

            dialog.refresh();

            ok(!hasRequestedData);

            dialog.destroy();
        });

        test('construction triggers open and activate events', function() {
            var isActivated = false,
                isOpened = false;

            var dialog = $('#Window4').tWindow({
                    onOpen: function () { isOpened = true; },
                    onActivate: function () {isActivated = true; }
                }).data('tWindow');

            ok(isActivated);
            ok(isOpened);

            dialog.destroy();
        });

        test('construction does not trigger open and activate events on hidden windows', function() {
            var isActivated = false,
                isOpened = false;

            var dialog = $('<div />').hide().tWindow({
                    onOpen: function () { isOpened = true; },
                    onActivate: function () {isActivated = true; }
                }).data('tWindow');

            ok(!isActivated);
            ok(!isOpened);

            dialog.destroy();
        });

        test('construction of modal window shows overlay', function() {
            var dialog = $t.window.create({
                    html: '<div id="content">content</div>',
                    modal: true
                }).data('tWindow');

            ok($('.t-overlay').is(':visible'));

            dialog.destroy();
        });

        test('hiding second modal window does not hide first overlay', function() {
            var dialog1 = $t.window.create({
                    html: 'content',
                    modal: true
                }).data('tWindow');

            var dialog2 = $t.window.create({
                    html: 'content, too',
                    modal: true
                }).data('tWindow');
                
            dialog2.close();

            ok($('.t-overlay').is(':visible'));

            dialog1.close();

            ok(!$('.t-overlay').is(':visible'));

            dialog1.open();

            ok($('.t-overlay').is(':visible'));

            dialog1.destroy();
            dialog2.destroy();
        });

        test('destroy() does not delete overlay if there are other opened modal windows', function() {
            var dialog1 = $t.window.create({
                    html: 'content',
                    modal: true
                }).data('tWindow');

            var dialog2 = $t.window.create({
                    html: 'content, too',
                    modal: true
                }).data('tWindow');
                
            dialog2.destroy();
            
            equals($('.t-overlay').length, 1);
            ok($('.t-overlay').is(':visible'));

            dialog1.destroy();

            equals($('.t-overlay').length, 0);
        });

        test('creating a modal window after closing another shows overlay', function() {
            var dialog1 = $t.window.create({
                    html: 'content',
                    modal: true
                }).data('tWindow');

            dialog1.close();

            var dialog2 = $t.window.create({
                    html: 'content, too',
                    modal: true
                }).data('tWindow');
            
            ok($('.t-overlay').is(':visible'));
                
            dialog2.destroy();
            dialog1.destroy();
        });

    </script>

</asp:Content>