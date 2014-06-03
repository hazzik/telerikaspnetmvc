<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>InlineFormatFinder</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>


    <script type="text/javascript">
    
    var editor;

    var InlineFormatFinder;
    var enumerator;
    </script>
</asp:Content>


<asp:Content ContentPlaceHolderID="TestContent" runat="server">

<script type="text/javascript">



    QUnit.testStart = function() {
        editor = getEditor();
        InlineFormatFinder = $.telerik.editor.InlineFormatFinder;
    }

    test('find suitable does not return for single text node', function() {
        editor.value('foo');

        var finder = new InlineFormatFinder(editor.formats.bold);
        ok(null === finder.findSuitable(editor.body.firstChild));
    });

    test('find suitable returns matching tag', function() {
        editor.value('<span>foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        equal(finder.findSuitable(editor.body.firstChild.firstChild), editor.body.firstChild);
    });

    test('find suitable returns closest', function() {
        editor.value('<span><span>foo</span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        equal(finder.findSuitable(editor.body.firstChild.firstChild.firstChild), editor.body.firstChild.firstChild);
    });

    test('find suitable does not return in case of partial selection', function() {
        editor.value('<span>foo<em>bar</em></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        ok(null === finder.findSuitable(editor.body.firstChild.firstChild));
    });

    test('findSuitable and caret', function() {
        editor.value('<span>foo<span class="t-marker"></span>bar</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        equal(finder.findSuitable(editor.body.firstChild.firstChild), editor.body.firstChild);
    });

    test('find suitable skips markers', function() {
        editor.value('<span>foo<span class="t-marker"></span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        equal(finder.findSuitable(editor.body.firstChild.firstChild), editor.body.firstChild);
    });

    test('find format finds formatted node by tag', function() {
        editor.value('<strong>foo</strong>');

        var finder = new InlineFormatFinder(editor.formats.bold);

        equal(finder.findFormat(editor.body.firstChild.firstChild), editor.body.firstChild);
    });

    test('find format finds formatterd node by tag and style', function() {
        editor.value('<span style="text-decoration:underline">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);

        equal(finder.findFormat(editor.body.firstChild.firstChild), editor.body.firstChild);
    });
    
    test('find format returns null if node does not match tag and style', function() {
        editor.value('<span>foo</span>');

        var finder = new InlineFormatFinder(editor.formats.underline);

        ok(null === finder.findFormat(editor.body.firstChild.firstChild));
    });

    test('find format returns parent element', function() {
        editor.value('<span style="text-decoration:underline"><span>foo</span></span>');

        var finder = new InlineFormatFinder(editor.formats.underline);
        equal(finder.findFormat(editor.body.firstChild.firstChild.firstChild), editor.body.firstChild);
    });

    test('find format checks all formats', function() {
        editor.value('<span style="font-weight:bold">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.bold);

        equal(finder.findFormat(editor.body.firstChild.firstChild), editor.body.firstChild);
    });

    test('is formatted returns true if at least one node has format', function() {
        editor.value('<span style="font-weight:bold">foo</span>');

        var finder = new InlineFormatFinder(editor.formats.bold);
        ok(finder.isFormatted([editor.body.firstChild.firstChild]));
    });

    test('is formatted returns false if all nodes dont have format', function() {    
        editor.value('foo');

        var finder = new InlineFormatFinder(editor.formats.bold);
        ok(!finder.isFormatted([editor.body.firstChild]));
    });

    test('is formatted returns true for formatted and unformatted nodes', function() {
        editor.value('<strong>foo</strong>bar');

        var finder = new InlineFormatFinder(editor.formats.bold);
        ok(finder.isFormatted([editor.body.firstChild.firstChild, editor.body.lastChild]));
    });

    test('is formatted returns true when the format node is the argument', function() {
        editor.value('<strong>foo</strong>');

        var finder = new InlineFormatFinder(editor.formats.bold);
        ok(finder.isFormatted([editor.body.firstChild]));
    });

</script>

</asp:Content>