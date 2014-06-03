<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Selection</h2>
    
    <% Html.Telerik().PanelBar()
            .Name("myPanelBar")
            .Items(panelbar =>
            {
                panelbar.Add().Text("Item 1");
                panelbar.Add().Text("Item 2")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 2.1");
                        item.Add().Text("Child Item 2.2");
                        item.Add().Text("Child Item 2.3");
                        item.Add().Text("Child Item 2.4");
                    })
                    .Expanded(true);
                panelbar.Add().Text("Item 3")
                    .Items(item =>
                    {
                        item.Add().Text("Child Item 3.1");
                        item.Add().Text("Child Item 3.2");
                    });
                panelbar.Add().Text("Item 4");
                panelbar.Add().Text("Item 5");
                panelbar.Add().Text("Item 6");
            }
            )
            .Effects(fx => fx.Toggle())
            .Render(); %>
            

    <script type="text/javascript">

        var panelbar;

        function setUp() {
            panelbar = $('#myPanelBar').data('tPanelBar');
        }
            
        function getRootItem(index) {
            return $('#myPanelBar').find('.t-header').parent().eq(index)
        }

        function test_clicking_root_items_selects_them() {
            var firstLink = getRootItem(0).find('> .t-link');
        
            firstLink.trigger({ type: 'click' });

            assertTrue(firstLink.hasClass('t-state-selected'));
        }

        function test_selecting_root_items_deselects_their_siblings() {
            var firstLink = getRootItem(0).find('> .t-link');
            var secondLink = getRootItem(1).find('> .t-link');
        
            firstLink.trigger({ type: 'click' });
            secondLink.trigger({ type: 'click' });

            assertEquals(1, $(panelbar.element).find('.t-state-selected').length);
        }

    </script>
</asp:Content>
