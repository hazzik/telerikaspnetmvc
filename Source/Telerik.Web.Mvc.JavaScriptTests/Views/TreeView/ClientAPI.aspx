<%@ Page Title="CollapseDelay Tests" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>ClientEvents tests</h2>
    
    <%= Html.Telerik().TreeView()
            .Name("ClientSideTreeView")
            .ShowCheckBox(true)
            .Effects(fx => fx.Toggle()) %>

    <script type="text/javascript">
        var treeview;
        
        function setUp() {
            treeview = $('#ClientSideTreeView').data('tTreeView');
        }

        function test_disable_disables_checkboxes() {
            treeview.bindTo([
                { Text: 'Griffin.Door' }
            ]);

            treeview.disable('.t-item');

            assertTrue($(':checkbox', treeview.element).is('[disabled]'));
        }

        function test_enable_enables_checkboxes() {
            treeview.bindTo([
                { Text: 'Griffin.Door', Enabled: false }
            ]);

            treeview.enable('.t-item');

            assertTrue($(':checkbox', treeview.element).is(':not([disabled])'));
        }

        function test_disable_disables_expand_collapse_icon() {
            treeview.bindTo([
                {
                    Text: 'Griffin.Door', Expanded: false,
                    Items: [{ Text: 'Hairy' }]
                }
            ]);

            treeview.disable('.t-item');

            assertTrue($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled'));
            assertFalse($('.t-item:first > div > .t-icon').hasClass('t-plus'));
        }

        function test_enable_enables_expand_collapse_icon() {
            treeview.bindTo([
                {
                    Text: 'Griffin.Door', Expanded: false, Enabled: false,
                    Items: [{ Text: 'Hairy' }]
                }
            ]);

            treeview.enable('.t-item');

            assertTrue($('.t-item:first > div > .t-icon').hasClass('t-plus'));
            assertFalse($('.t-item:first > div > .t-icon').hasClass('t-plus-disabled'));
        }
        
    </script>

</asp:Content>
