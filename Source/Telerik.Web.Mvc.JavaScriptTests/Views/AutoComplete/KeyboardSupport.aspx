<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<%@ Import Namespace="Telerik.Web.Mvc.JavaScriptTests" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <%= Html.Telerik().AutoComplete()
            .Name("AutoComplete")
            .BindTo(new string[]{
                "Item1", "Item2", "Item3", "Item4", "Item5", "Item6",
                "Item7", "Item8", "Item9", "Item10", "Item11", "Item12",
                "Item13", "Item14", "Item15", "Item16", "Item17", "Item18",
                "Item19", "тtem20"
            })
    %>

    <% Html.Telerik().ScriptRegistrar()
           .Scripts(scripts => scripts
               .Add("telerik.common.js")
               .Add("telerik.list.js")
               .Add("telerik.autocomplete.js")); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="TestContent" runat="server">

    <script type="text/javascript">


        function getAutoComplete() {
            return $('#AutoComplete').data('tAutoComplete');
        }


        test('pressing down arrow should highlight next item', function() {
            var autocomplete = getAutoComplete();

            autocomplete.fill();

            var $initialSelectedItem = autocomplete.dropDown.$items.first();

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
            
            equal(autocomplete.dropDown.$element.find('.t-state-selected').first().text(), $initialSelectedItem.next().text());
        });

        test('pressing up arrow should highlight prev item', function() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$element.find('.t-item')[1]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            equal(autocomplete.dropDown.$element.find('.t-state-selected').first().text(), $initialSelectedItem.prev().text());
        });

        test('keydown should create dropDown $items if they do not exist', function() {
            var autocomplete = getAutoComplete();
            autocomplete.$dropDown = null;
            autocomplete.dropDown.$items = null;

            autocomplete.open();
            autocomplete.$text.focus();

            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            ok(autocomplete.dropDown.$items.length > 0);
        });

        test('down arrow should select first item if no selected', function() {
            var autocomplete = getAutoComplete();

            autocomplete.dropDown.$element;
            autocomplete.dropDown.$items.removeClass('t-state-selected');

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });

            ok(autocomplete.dropDown.$items.first().hasClass('t-state-selected'));
        });

        test('pressing up arrow should select last item if no selected item', function() {
            var autocomplete = getAutoComplete();

            autocomplete.dropDown.$element;
            autocomplete.dropDown.$items.removeClass('t-state-selected');

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38});

            ok($(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]).hasClass('t-state-selected'));
        });

        test('pressing up arrow when first item is selected should highlight last one', function() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$element.find('.t-item')[0]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 38 });

            ok($(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]).hasClass('t-state-selected'));
        });

        test('pressing down arrow when last item is highlight should select first one', function() {
            var autocomplete = getAutoComplete();

            var $initialSelectedItem = $(autocomplete.dropDown.$items[autocomplete.dropDown.$items.length - 1]);

            autocomplete.highlight($initialSelectedItem);

            autocomplete.open();
            autocomplete.$text.focus();
            autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });

            ok($(autocomplete.dropDown.$items[0]).hasClass('t-state-selected'));
        });

        test('Tab button should close dropDown element', function() {
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;

            try {
                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.open();
                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 9 });

                ok(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        });

        test('pressing Enter should close dropdown list', function() {
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;

            try {

                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
                autocomplete.$text.trigger({ type: "keydown", keyCode: 13 });

                ok(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        });

        test('pressing Enter in dropdown list prevents it', function() {
            var autocomplete = getAutoComplete();

            var isPreventCalled = false;

            var isOpened = autocomplete.isOpened;
            try {
                autocomplete.isOpened = function () { return true; }

                autocomplete.$text.focus();
                autocomplete.$text.trigger({ type: "keydown", keyCode: 40 });
                autocomplete.$text.trigger({ type: "keydown", keyCode: 13, preventDefault: function () { isPreventCalled = true; } });

                ok(isPreventCalled);
            } finally {
                autocomplete.isOpened = isOpened;
            }
        });

        test('pressing escape should close dropdown list', function() {
            var autocomplete = getAutoComplete();
            autocomplete.effects = autocomplete.dropDown.effects = $.telerik.fx.toggle.defaults();

            var close = autocomplete.trigger.close;
            var isCalled = false;
            
            try {
                autocomplete.trigger.close = function () { isCalled = true; };

                autocomplete.open();
                autocomplete.$text.focus();

                autocomplete.$text.trigger({ type: "keydown", keyCode: 27 });

                ok(isCalled);
            } finally {
                autocomplete.trigger.close = close;
            }
        });

    </script>

</asp:Content>