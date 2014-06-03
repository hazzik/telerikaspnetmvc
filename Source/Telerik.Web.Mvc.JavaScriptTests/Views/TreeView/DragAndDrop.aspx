<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>DragAndDrop</h2>
    
    <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .DragAndDrop(true)
            .Effects(fx => fx.Toggle()) %>

    <script type="text/javascript">
        var treeview;

        function setUp() {
            treeview = $('#TreeView').data('tTreeView');

            treeview.bindTo([
                { Text: "Product 1", Expanded: true,
                    Items: [
                      { Text: "Subproduct 1.1", Expanded: true,
                          Items: [
                            { Text: "Subsubproduct 1.1.1" },
                            { Text: "Subsubproduct 1.1.2" }
                          ]
                      }
                    ]
                },
                { Text: "Product 2", Enabled: false },
                { Text: "Product 3" },
                { Text: "Product 4", Expanded: true,
                    Items: [
                      { Text: "Subproduct 4.1", Expanded: true },
                      { Text: "Subproduct 4.2", Expanded: true }
                    ]
                }
            ]);
        }

        function findItemByText(text) {
            return $('.t-in:contains(' + text + ')').closest('.t-item');
        }

        function moveTreeViewNode(sourceText, destinationText) {
            var source = findItemByText(sourceText),
                destination = findItemByText(destinationText);

            var startOffset = source.find('.t-in:first').offset();
            var endOffset = destination.find('.t-in:first').offset();

            source.find('.t-in:first')
                .simulate('mousedown', { clientX: startOffset.left + 5, clientY: startOffset.top + 5 });

            $(document)
                .simulate('mousemove', { clientX: endOffset.left + 5, clientY: endOffset.top + 5, target: destination.find('.t-in:first') });
            
            destination.find('.t-in:first')
                .simulate('mouseup', { clientX: endOffset.left + 5, clientY: endOffset.top + 5 });
        }

        function test_move_parent_to_child_item_does_not_move_it() {

            moveTreeViewNode('Product 1', 'Subproduct 1.1');

            assertTrue(findItemByText('Product 1').parent().hasClass('t-treeview-lines'));
        }

        function test_moving_items_onto_node_appends_them_at_the_end() {

//            moveTreeViewNode('Subsubproduct 1.1.2', 'Product 1');

//            assertEquals(2, findItemByText('Product 1').find('.t-group:first').children().length);
        }
    </script>
</asp:Content>
