<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Dom</h2>
     <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;

        var Dom;
        var enumerator;

        function setUp() {
            editor = getEditor();
            Dom = $.telerik.editor.Dom;
        }

        function test_commonAncestor_returns_common_ancestor_for_root_nodes_returns_body() {
            editor.value('<span>foo</span><span>bar</span>');
            assertEquals(editor.body, Dom.commonAncestor(editor.body.firstChild.firstChild, editor.body.lastChild.lastChild));
        }
        
        function test_commonAncestor_single_node_returns_same_element() {
            editor.value('<span>foo</span><span>bar</span>');
            assertEquals(editor.body.firstChild.firstChild, Dom.commonAncestor(editor.body.firstChild.firstChild));
        }

        function test_commonAncestor_returns_common_ancestor_for_parent_and_child() {
            editor.value('<span>foo</span>');
            assertEquals(editor.body.firstChild, Dom.commonAncestor(editor.body.firstChild.firstChild, editor.body.firstChild));
        }

        function test_commonAncestor_returns_null_when_called_with_empty_arguments() {
            assertNull(Dom.commonAncestor());
        }

        function test_style_applies_specified_style() {
            var element = document.createElement('span');
            Dom.style(element, {color:'red'});
            assertEquals('red', element.style.color);
        }

        function test_style_does_nothing_if_no_style_specified() {
            var element = document.createElement('span');
            Dom.style(element);
            assertEquals('', element.style.cssText);
        }

        function test_unstyle_does_nothing_if_no_style_specified() {
            var element = document.createElement('span');
            Dom.style(element, {color:'red'});
            Dom.unstyle(element);
            assertEquals('red', element.style.color);
        }

        function test_unstyle_removes_the_specified_attributes() {
            var element = document.createElement('span');
            Dom.style(element, { color: 'red' });
            Dom.unstyle(element, { color: 'red' });
            assertEquals('', element.style.cssText);
        }

        function test_createElement_creates_element() {
            var element = Dom.create(document, 'span');
            assertNotUndefined(element);
            assertEquals('span', element.tagName.toLowerCase());
        }

        function test_createElement_creates_element_and_sets_attributes() {
            var element = Dom.create(document, 'span', {className:'test'});
            assertEquals('test', element.className);
        }
        
        function test_createElement_can_set_style() {
            var element = Dom.create(document, 'span', { style: {color:'red'}});
            assertEquals('red', element.style.color);
        }

        function test_change_tag_updates_the_element_tag_name() {
            var source = Dom.create(document, 'div');
            document.body.appendChild(source);
            var result = Dom.changeTag(source, 'span');
            assertEquals('span', result.tagName.toLowerCase());
            assertEquals(document.body, result.parentNode);
        }

        function test_change_tag_clones_attributes() {
            var source = Dom.create(document, 'div', {className:'test'});
            document.body.appendChild(source);
            
            var result = Dom.changeTag(source, 'span');
            assertEquals('test', result.className);
        }
        
        function test_change_tag_clones_style() {
            var source = Dom.create(document, 'div', { style: {textAlign:'center'}});
            
            document.body.appendChild(source);
            var result = Dom.changeTag(source, 'span');
            assertEquals('center', result.style.textAlign);
        }

        function test_find_last_text_node_single_node() {
            var node = Dom.create(document, 'div');
            node.innerHTML = 'foo';

            assertEquals(node.firstChild, Dom.lastTextNode(node));
        }
        
        function test_find_last_text_node_when_it_is_first() {
            var node = Dom.create(document, 'div');
            node.innerHTML = 'foo<span></span>';
            
            assertEquals(node.firstChild, Dom.lastTextNode(node));
        }
        
        function test_find_last_text_node_when_it_is_child_of_child() {
            var node = Dom.create(document, 'div');
            node.innerHTML = '<span>foo</span>';
            
            assertEquals(node.firstChild.firstChild, Dom.lastTextNode(node));
        }
        
        function test_find_last_text_returns_null() {
            var node = Dom.create(document, 'div');
            node.innerHTML = '<span></span>';

            assertEquals(null, Dom.lastTextNode(node));
        }

        function test_find_last_text_node_first_child_of_child() {
            var node = Dom.create(document, 'div');
            node.innerHTML = '<span>foo<span></span></span>';
            
            assertEquals(node.firstChild.firstChild, Dom.lastTextNode(node));
        }

    </script>
</asp:Content>