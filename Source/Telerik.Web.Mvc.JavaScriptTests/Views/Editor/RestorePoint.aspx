<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Exec</h2>

    <%= Html.Telerik().Editor().Name("Editor1") %>

    <script type="text/javascript">
        var RestorePoint;
        var editor;
        var htmlTagIsFirstChild;
        function getEditor() {
            return $('#Editor1').data("tEditor");
        }

        function setUp() {
            RestorePoint = $.telerik.editor.RestorePoint;
            editor = getEditor();

            htmlTagIsFirstChild = editor.document.firstChild == editor.body.parentNode;
        }
        
        function test_restorePoint_initializes_for_body() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);
            
            var restorePoint = new RestorePoint(range);
            
            assertEquals(2, restorePoint.startContainer.length);
            assertEquals(0, restorePoint.startOffset);
            assertEquals(2, restorePoint.endContainer.length);
            assertEquals(1, restorePoint.endOffset);
        }
        
        function test_restorePoint_initializes_for_root_node() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);
            
            var restorePoint = new RestorePoint(range);
            
            assertEquals(crossBrowserOffset('0,1,0'), restorePoint.startContainer.toString());
            assertEquals(0, restorePoint.startOffset);
            assertEquals(crossBrowserOffset('0,1,0'), restorePoint.endContainer.toString());
            assertEquals(1, restorePoint.endOffset);
        }
        
        function test_restorePoint_initializes_for_root_node_contents() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 0);
            range.setEnd(editor.body.firstChild.firstChild, 3);
            
            var restorePoint = new RestorePoint(range);
            
            assertEquals(crossBrowserOffset('0,0,1,0'), restorePoint.startContainer.toString());
            assertEquals(0, restorePoint.startOffset);
            assertEquals(crossBrowserOffset('0,0,1,0'), restorePoint.endContainer.toString());
            assertEquals(3, restorePoint.endOffset);
        }
        
        function test_restorePoint_initializes_for_different_start_and_end_containers() {

            editor.value('<p>foo</p><p>bar</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.setEnd(editor.body.lastChild.firstChild, 2);
            
            var restorePoint = new RestorePoint(range);
            
            assertEquals(crossBrowserOffset('0,0,1,0'), restorePoint.startContainer.toString());
            assertEquals(1, restorePoint.startOffset);
            assertEquals(crossBrowserOffset('0,1,1,0'), restorePoint.endContainer.toString());
            assertEquals(2, restorePoint.endOffset);
        }

        /***************************************************************************/
        
        function test_toRange_returns_body_range() {

            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);

            var restorePoint = new RestorePoint(range);

            range.collapse(true);
            
            var restorePointRange = restorePoint.toRange();
            
            assertEquals(editor.body, restorePointRange.startContainer);
            assertEquals(0, restorePointRange.startOffset);
            assertEquals(editor.body, restorePointRange.endContainer);
            assertEquals(1, restorePointRange.endOffset);
        }
        
        function test_toRange_returns_root_node() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);

            var restorePoint = new RestorePoint(range);

            range.collapse(true);
            
            var restorePointRange = restorePoint.toRange();
            
            assertEquals(editor.body.firstChild, restorePointRange.startContainer);
            assertEquals(0, restorePointRange.startOffset);
            assertEquals(editor.body.firstChild, restorePointRange.endContainer);
            assertEquals(1, restorePointRange.endOffset);
        }
        
        function test_toRange_returns_root_node_contents() {
            editor.value('<p>foo</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 0);
            range.setEnd(editor.body.firstChild.firstChild, 3);

            var restorePoint = new RestorePoint(range);

            range.collapse(true);
            
            var restorePointRange = restorePoint.toRange();
            
            assertEquals(editor.body.firstChild.firstChild, restorePointRange.startContainer);
            assertEquals(0, restorePointRange.startOffset);
            assertEquals(editor.body.firstChild.firstChild, restorePointRange.endContainer);
            assertEquals(3, restorePointRange.endOffset);
        }
        
        function test_toRange_returns_different_start_and_end_containers() {

            editor.value('<p>foo</p><p>bar</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.setEnd(editor.body.lastChild.firstChild, 2);

            var restorePoint = new RestorePoint(range);

            range.collapse(true);
            
            var restorePointRange = restorePoint.toRange();
            
            assertEquals(editor.body.firstChild.firstChild, restorePointRange.startContainer);
            assertEquals(1, restorePointRange.startOffset);
            assertEquals(editor.body.lastChild.firstChild, restorePointRange.endContainer);
            assertEquals(2, restorePointRange.endOffset);
        }
        
        function test_toRange_does_not_modify_restore_point() {
            editor.value('<p>foo</p><p>bar</p>');

            var range = editor.createRange();
            range.setStart(editor.body.firstChild.firstChild, 1);
            range.setEnd(editor.body.lastChild.firstChild, 2);

            var restorePoint = new RestorePoint(range);
            
            restorePoint.toRange();
            
            assertEquals(crossBrowserOffset('0,0,1,0'), restorePoint.startContainer.toString());
            assertEquals(1, restorePoint.startOffset);
            assertEquals(crossBrowserOffset('0,1,1,0'), restorePoint.endContainer.toString());
            assertEquals(2, restorePoint.endOffset);
        }

        function test_denormalized_content() {
            editor.value('');
            var node = editor.document.createTextNode('foo');
            editor.body.appendChild(node);
            node = editor.document.createTextNode('foo');
            editor.body.appendChild(node);
            var range = editor.createRange(true);
            range.setStart(node, 3);
            range.collapse(true);
            var restorePoint = new RestorePoint(range);
            assertEquals(crossBrowserOffset('0,1,0'), restorePoint.startContainer.toString());
            assertEquals(6, restorePoint.startOffset);
            
        }

        function crossBrowserOffset(offset) {
            if (htmlTagIsFirstChild)
                return offset;
            
            var indices = offset.split(',');
            indices.pop(); 
            indices.push(1); //in IE the <html> element is not the first child bu the second (after the DOCTYPE)
            return indices.join(',');
        }


    </script>
</asp:Content>
