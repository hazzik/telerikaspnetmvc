<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .DragAndDrop(true)
            .Effects(fx => fx.Toggle()) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="TestContent" runat="server">
    <script type="text/javascript">

        var treeview;

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

            sourceNode.trigger({
                type: "mousedown",
                pageX: startOffset.left + 5,
                pageY: startOffset.top + 5,
                relatedTarget: sourceNode[0]
            });

            destinationNode.trigger({
                type: "mousemove",
                pageX: endOffset.left + 5,
                pageY: endOffset.top + 7,
                target: destinationNode[0]
            }).trigger({
                type: "mousemove",
                pageX: endOffset.left + 5,
                pageY: endOffset.top + 7,
                target: destinationNode[0]
            }).trigger({
                type: "mouseup",
                pageX: endOffset.left + 5,
                pageY: endOffset.top + 7,
                target: destinationNode[0]
            })

        }

        module("TreeView / DragAndDrop", {
            setup: function () {
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
        });

        test('move parent to child item does not move it', function () {

            moveTreeViewNode('Product 1', 'Subproduct 1.1');

            ok(findItemByText('Product 1').parent().hasClass('t-treeview-lines'));
        });

        test('cancelling OnNodeDragStart prevents drag', function () {
            try {

                $(treeview.element).bind('nodeDragStart', function (e) { e.preventDefault(); });

                moveTreeViewNode('Subproduct 1.1', 'Product 3');

                equal(findItemByText('Subproduct 1.1').parent().closest('.t-item')[0], findItemByText('Product 1')[0]);

            } finally {
                $(treeview.element).unbind('nodeDragStart');
            }
        });

        test('move item to collapsed lod sibling triggers onDataBinding handler', function () {
            try {
                var called;

                treeview.onDataBinding = function (e) {
                    called = true;
                    if (e.item != treeview.element) {
                        treeview.dataBind(e.item, [{ Text: "Abyss Node", LoadOnDemand: true, Value: "abyss"}]);
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

                ok(!!treeview.isAjax());

                $(treeview.element).bind('dataBinding', treeview.onDataBinding);

                moveTreeViewNode('Product 2', 'Product 1');

                ok(!!called, 'onDataBinding handler not called');
                equal(findItemByText('Product 1').find('> .t-group').children().length, 2, 'items not appended');
            } finally {
                treeview.onDataBinding = undefined;
                $(treeview.element).unbind('dataBinding');
            }
        });

        test('move item to collapsed lod node rendered on client triggers onDataBinding handler', function () {
            try {
                var called;

                treeview.onDataBinding = function (e) {
                    called = true;
                    if (e.item != treeview.element) {
                        treeview.dataBind(e.item, [{ Text: "Abyss Node", LoadOnDemand: true, Value: "abyss"}]);
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

                ok(!!treeview.isAjax());

                $(treeview.element).bind('dataBinding', treeview.onDataBinding);

                findItemByText('Product 1').find('> div > .t-plus').trigger('click');

                moveTreeViewNode('Product 2', 'Abyss Node');

                ok(!!called, 'onDataBinding handler not called');
                equal(findItemByText('Abyss Node').find('> .t-group').children().length, 2, 'items not appended');
            } finally {
                treeview.onDataBinding = undefined;
                $(treeview.element).unbind('dataBinding');
            }
        });

    </script>
</asp:Content>
