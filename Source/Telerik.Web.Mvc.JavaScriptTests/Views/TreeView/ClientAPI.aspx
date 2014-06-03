<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().TreeView()
            .Name("ClientSideTreeView")
            .ShowCheckBox(true)
            .Effects(fx => fx.Toggle()) %>

</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">

        var treeview;

        module("TreeView / ClientAPI", {
            setup: function() {
                treeview = $('#ClientSideTreeView').data('tTreeView');
            }
        });

        test('disable disables checkboxes', function() {
            treeview.bindTo([
                { Text: 'Griffin.Door' }
            ]);

            treeview.disable('.t-item');

            ok($(':checkbox', treeview.element).is('[disabled]'));
        });

        test('enable enables checkboxes', function() {
            treeview.bindTo([
                { Text: 'Griffin.Door', Enabled: false }
            ]);

            treeview.enable('.t-item');

            ok($(':checkbox', treeview.element).is(':not([disabled])'));
        });

        test('disable disables expand collapse icon', function() {
            treeview.bindTo([
                {
                    Text: 'Griffin.Door', Expanded: false,
                    Items: [{ Text: 'Hairy' }]
                }
            ]);

            treeview.disable('.t-item');

            ok($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled'));
            ok(!($('.t-item:first > div > .t-icon').hasClass('t-plus')));
        });

        test('enable enables expand collapse icon', function() {
            treeview.bindTo([
                {
                    Text: 'Griffin.Door', Expanded: false, Enabled: false,
                    Items: [{ Text: 'Hairy' }]
                }
            ]);

            treeview.enable('.t-item');

            ok($('.t-item:first > div > .t-icon').hasClass('t-plus'));
            ok(!($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled')));
        });

        test('checking node should set value of the checkbox input to TRUE', function () {
            treeview.bindTo([
                { Text: 'Griffin' }
            ]);
            
            var checkbox = $(':checkbox', treeview.element);
            var item = checkbox.closest('.t-item');
            treeview.nodeCheck(item[0], true);

            equal(checkbox.val(), 'true', 'value of the checkbox was not updated');
        })

</script>

</asp:Content>