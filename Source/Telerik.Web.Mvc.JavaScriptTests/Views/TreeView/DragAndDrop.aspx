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

        function moveTreeViewNode(sourceNode, destinationNode) {
            var source = findItemByText(sourceNode);
            var destination = findItemByText(destinationNode);

            var sourceNode = source.find('.t-in:first');
            var destinationNode = destination.find('.t-in:first');

            var startOffset = sourceNode.offset();
            var endOffset = destinationNode.offset();

            sourceNode.simulate('mousedown', { clientX: startOffset.left + 5, clientY: startOffset.top + 5, relatedTarget: sourceNode[0] });

            destinationNode
                .simulate('mousemove', { clientX: endOffset.left + 5, clientY: endOffset.top + 5, target: destinationNode[0] })
                .simulate('mousemove', { clientX: endOffset.left + 5, clientY: endOffset.top + 5, target: destinationNode[0] })
                .simulate('mouseup', { clientX: endOffset.left + 5, clientY: endOffset.top + 5, target: destinationNode[0] });
        }

        function test_move_parent_to_child_item_does_not_move_it() {

            moveTreeViewNode('Product 1', 'Subproduct 1.1');

            assertTrue(findItemByText('Product 1').parent().hasClass('t-treeview-lines'));
        }

        function test_cancelling_OnNodeDragStart_prevents_drag() {
            try {

                $(treeview.element).bind('nodeDragStart', function(e) { e.preventDefault(); });
                
                moveTreeViewNode('Subproduct 1.1', 'Product 3');
                
                assertEquals(findItemByText('Product 1')[0], findItemByText('Subproduct 1.1').parent().closest('.t-item')[0]);

            } finally {
                $(treeview.element).unbind('nodeDragStart');
            }
        }

        function test_move_item_to_collapsed_lod_sibling_triggers_onDataBinding_handler() {
            try {
                var called;
                
                treeview.onDataBinding = function(e) {
                    called = true;
                    if (e.item != treeview.element) {
                        treeview.dataBind(e.item, [{ Text: "Abyss Node", LoadOnDemand: true, Value: "abyss" }]);
                    }
                };

                treeview.bindTo([
                    { Text: "Product 1", Expanded: false, LoadOnDemand: true, Value: "abyss" },
                    { Text: "Product 2", Expanded: false,
                        Items: [
                          { Text: "Subproduct 2.1" },
                          { Text: "Subproduct 2.2" }
                        ]
                    }
                ]);

                assertTrue(!!treeview.isAjax());

                $(treeview.element).bind('dataBinding', treeview.onDataBinding);

                moveTreeViewNode('Product 2', 'Product 1');

                assertTrue('onDataBinding handler not called', !!called);
                assertEquals('items not appended', 2, findItemByText('Product 1').find('> .t-group').children().length);
            } finally {
                treeview.onDataBinding = undefined;
                $(treeview.element).unbind('dataBinding');
            }
        }

        function test_move_item_to_collapsed_lod_node_rendered_on_client_triggers_onDataBinding_handler() {
            try {
                var called;
                
                treeview.onDataBinding = function(e) {
                    called = true;
                    if (e.item != treeview.element) {
                        treeview.dataBind(e.item, [{ Text: "Abyss Node", LoadOnDemand: true, Value: "abyss" }]);
                    }
                };

                treeview.bindTo([
                    { Text: "Product 1", Expanded: false, LoadOnDemand: true, Value: "abyss" },
                    { Text: "Product 2", Expanded: false,
                        Items: [
                          { Text: "Subproduct 2.1" },
                          { Text: "Subproduct 2.2" }
                        ]
                    }
                ]);

                assertTrue(!!treeview.isAjax());

                $(treeview.element).bind('dataBinding', treeview.onDataBinding);

                findItemByText('Product 1').find('> div > .t-plus').trigger('click');

                moveTreeViewNode('Product 2', 'Abyss Node');

                assertTrue('onDataBinding handler not called', !!called);
                assertEquals('items not appended', 2, findItemByText('Abyss Node').find('> .t-group').children().length);
            } finally {
                treeview.onDataBinding = undefined;
                $(treeview.element).unbind('dataBinding');
            }
        }
    </script>
</asp:Content>
