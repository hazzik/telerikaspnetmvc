<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>GreedyBlockFormatter</h2>
    
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">

        var editor;

        var GreedyBlockFormatter;

        function setUp() {
            editor = getEditor();
            GreedyBlockFormatter = $.telerik.editor.GreedyBlockFormatter;
        }

        function test_apply_replaces_outer_block_element() {
            editor.value('<p>foo</p>');

            var formatter = new GreedyBlockFormatter([{ tags: ['h1'] }], {}, true);

            formatter.apply([editor.body.firstChild.firstChild]);
            
            assertEquals('<h1>foo</h1>', editor.value());
        }        
        
        function test_apply_adds_block_element() {
            editor.value('foo');

            var formatter = new GreedyBlockFormatter([{ tags: ['h1'] }], {}, true);

            formatter.apply([editor.body.firstChild]);
            
            assertEquals('<h1>foo</h1>', editor.value());
        }

        function test_apply_splits_lists() {
            editor.value('<ul><li>foo</li><li>bar</li><li>baz</li></ul>');

            var formatter = new GreedyBlockFormatter([{ tags: ['h1'] }], {}, true);

            formatter.editor = editor;
            formatter.apply([editor.body.firstChild.childNodes[1].firstChild]);
            
            assertEquals('<ul><li>foo</li></ul><h1>bar</h1><ul><li>baz</li></ul>', editor.value());
        }

        function test_apply_is_applied_on_multiple_list_items() {
            editor.value('<ul><li>foo</li><li>bar</li><li>baz</li></ul>');

            var formatter = new GreedyBlockFormatter([{ tags: ['h1']}], {}, true);

            formatter.editor = editor;
            formatter.apply([editor.body.firstChild.childNodes[0].firstChild, editor.body.firstChild.childNodes[1].firstChild]);

            assertEquals('<h1>foo</h1><h1>bar</h1><ul><li>baz</li></ul>', editor.value());
        }

        function test_apply_is_applied_on_multiple_paragraphs() {
            editor.value('<p>foo</p><p>bar</p><p>baz</p>');

            var formatter = new GreedyBlockFormatter([{ tags: ['h1'] }], {}, true);
            
            formatter.editor = editor;
            formatter.apply([editor.body.childNodes[0].firstChild, editor.body.childNodes[1].firstChild]);
            
            assertEquals('<h1>foo</h1><h1>bar</h1><p>baz</p>', editor.value());
        }

        function test_toggle_calls_apply() {
            var formatter = new GreedyBlockFormatter([{ tags: ['h1'] }], {}, true);
            
            formatter.editor = editor;

            var called = false;

            formatter.apply = function() { called = true; }; 

            formatter.toggle(createRangeFromText(editor, '<p>|foo|</p>'));
            
            assertTrue(called);
        }
    </script>
</asp:Content>
