<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>StyleCommand</h2>

    <%= Html.Telerik().Editor()
            .Name("Editor")
            .Tools(tools => tools
                .Clear()
                .Styles(styles => styles.Add("Foo", "foo"))
            )
            .StyleSheets(stylesheets => stylesheets.Add("styles.css")) %>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>

    <script type="text/javascript">
    
    var editor;
    var StyleCommand;

    function setUp() {
        editor = getEditor();
        $('#Editor .t-style .t-input').click();
        StyleCommand = $.telerik.editor.StyleCommand;
    }

    function test_exec_applies_css_class_to_inline_element() {
        var range = createRangeFromText(editor, '<span>|foo|</span>');
        editor.selectRange(range);
        editor.exec('style', {value:'bar'});
        assertEquals('<span class="bar">foo</span>', editor.value());
    }

    function test_styles_applied_to_list_item() {
        var span = $('.t-animation-container .t-item span')[0];
        
        if (!span) return; //does not currently work in IE!

        assertEquals('#a0b0c0', $.telerik.editor.Dom.toHex(span.style.color));
        
        assertEquals("42px", span.style.paddingLeft);
        assertEquals("42px", span.style.paddingRight);
        assertEquals("42px", span.style.paddingTop);
        assertEquals("42px", span.style.paddingBottom);
        
        assertEquals('#f1f1f1', $.telerik.editor.Dom.toHex(span.style.backgroundColor));
        assertEquals("fixed", span.style.backgroundAttachment);
        assertEquals("none", span.style.backgroundImage);
        assertEquals("no-repeat", span.style.backgroundRepeat);

        assertEquals("solid", span.style.borderTopStyle);
        assertEquals("1px", span.style.borderTopWidth);
        assertEquals("#a0b0c0", $.telerik.editor.Dom.toHex(span.style.borderTopColor));
        
        assertEquals("solid", span.style.borderRightStyle);
        assertEquals("1px", span.style.borderRightWidth);
        assertEquals("#a0b0c0", $.telerik.editor.Dom.toHex(span.style.borderRightColor));
        
        assertEquals("solid", span.style.borderLeftStyle);
        assertEquals("1px", span.style.borderLeftWidth);
        assertEquals("#a0b0c0", $.telerik.editor.Dom.toHex(span.style.borderLeftColor));
        
        assertEquals("solid", span.style.borderLeftStyle);
        assertEquals("1px", span.style.borderLeftWidth);
        assertEquals("#a0b0c0", $.telerik.editor.Dom.toHex(span.style.borderLeftColor));

        assertEquals("Arial", span.style.fontFamily);
        assertEquals("42px", $.telerik.editor.Dom.toHex(span.style.fontSize));
        assertEquals("italic", span.style.fontStyle);
        assertEquals("small-caps", span.style.fontVariant);
        assertEquals("800", span.style.fontWeight);
        assertEquals("69px", span.style.lineHeight);
    }

    function test_tool_displays_styles_initially() {
        editor.focus();
        editor.value('');
        $(editor.element).trigger('selectionChange');
        assertEquals('Styles', $('.t-style .t-input').text())
    }
    
    function test_tool_displays_known_values() {
        editor.focus();
        var range = createRangeFromText(editor, '<span class="foo">|foo|</span>');
        editor.selectRange(range);
        $(editor.element).trigger('selectionChange');
        assertEquals('Foo', $('.t-style .t-input').text())
    }
    </script>
</asp:Content>
