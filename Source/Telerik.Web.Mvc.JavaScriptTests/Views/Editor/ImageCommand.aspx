<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ImageCommand</h2>
    <%= Html.Telerik().Editor().Name("Editor") %>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/editorTestHelper.js") %>"></script>
    <script type="text/javascript">
        var editor;
        var ImageCommand;

        function setUp() {
            editor = getEditor();
            ImageCommand = $.telerik.editor.ImageCommand;
        }

        function tearDown() {
            var wnd = $('.t-window').data('tWindow');
            if (wnd) wnd.destroy();
        }

        function execImageCommandOnRange(range) {
            var command = new ImageCommand({ range: range });
            command.editor = editor;
            command.exec();

            return command;
        }

        function test_exec_creates_window() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            assertEquals(1, $('.t-window').length)
        }

        function test_clicking_close_closes_the_window() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('.t-dialog-close').click();
            assertEquals(0, $('.t-window').length)
        }

        function test_clicking_insert_closes_the_window() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('.t-dialog-insert').click();
            assertEquals(0, $('.t-window').length)
        }

        function test_clicking_insert_inserts_image_if_url_is_set() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('#t-editor-image-url').val('foo');
            $('.t-dialog-insert').click();
            assertEquals('<img alt="" src="foo" />', editor.value())
        }

        function test_clicking_insert_does_not_inserts_image_if_url_is_not_set() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('#t-editor-image-url').val('');
            $('.t-dialog-insert').click();
            assertEquals('foo', editor.value())
        }

        function test_clicking_insert_updates_existing_src() {
            editor.value('<img src="bar" style="float:left" />');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);
            execImageCommandOnRange(range);

            $('#t-editor-image-url').val('foo');
            $('.t-dialog-insert').click();
            assertEquals('<img alt="" src="foo" style="float:left;" />', editor.value())
        }

        function test_url_text_is_set() {
            editor.value('<img src="bar" />');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);
            execImageCommandOnRange(range);

            assertEquals('bar', $('#t-editor-image-url').val());
        }

        function test_hitting_enter_in_url_inserts_image() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 13;

            $('#t-editor-image-url')
                .val('http://foo')
                .trigger(e);

            assertEquals('<img alt="" src="http://foo" />', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_hitting_esc_in_url_cancels() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 27;

            $('#t-editor-image-url')
                .val('foo')
                .trigger(e);

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_hitting_enter_in_title_field_inserts_link() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 13;

            $('#t-editor-image-url')
                .val('http://foo')
            $('#t-editor-image-title').trigger(e);

            assertEquals('<img alt="" src="http://foo" />', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_hitting_esc_in_title_cancels() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            var e = new $.Event();
            e.type = 'keydown';
            e.keyCode = 27;

            $('#t-editor-image-url')
                .val('foo')

            $('#t-editor-image-title').trigger(e);

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_closing_the_window_restores_content() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('.t-window .t-close').click();

            assertEquals('foo', editor.value())
            assertEquals(0, $('.t-window').length);
        }

        function test_setting_title_sets_alt() {
            var range = createRangeFromText(editor, '|foo|');
            execImageCommandOnRange(range);

            $('#t-editor-image-url')
                .val('http://foo');

            $('#t-editor-image-title')
                .val('bar')

            $('.t-dialog-insert').click();
            assertEquals('<img alt="bar" src="http://foo" />', editor.value())
        }

        function test_title_text_box_is_filled_from_alt() {
            editor.value('<img src="foo" alt="bar" />');
            var range = editor.createRange();
            range.selectNode(editor.body.firstChild);
            execImageCommandOnRange(range);

            assertEquals('bar', $('#t-editor-image-title').val());
        }

        function test_undo_restores_content() {
            var range = createRangeFromText(editor, '|foo|');

            var command = execImageCommandOnRange(range);

            $('#t-editor-image-url')
                .val('foo');

            $('.t-dialog-insert').click();
            command.undo();
            assertEquals('foo', editor.value());
        }


        function test_exec_inserts_image_with_empty_range() {
            editor.value('foo ');
            var range = editor.createRange();
            range.setStart(editor.body.firstChild, 4);
            range.setEnd(editor.body.firstChild, 4);
            
            execImageCommandOnRange(range);

            $('#t-editor-image-url')
                .val('http://foo');

            $('.t-dialog-insert').click();
            assertEquals('foo <img alt="" src="http://foo" />', editor.value())
        }

        function test_link_is_not_created_if_url_is_http_slash_slash() {
            var range = createRangeFromText(editor, '|foo|');
            
            execImageCommandOnRange(range);

            $('.t-dialog-insert').click();
            assertEquals('foo', editor.value())
        }

        function test_cursor_is_put_after_image() {
            var range = createRangeFromText(editor, '|foo|bar');
            execImageCommandOnRange(range);

            $('#t-editor-image-url')
                .val('http://foo');
            
            $('.t-dialog-insert').click();
            
            range = editor.getRange();
            range.insertNode(editor.document.createElement('span'));
            assertEquals('<img alt="" src="http://foo" /><span></span>bar', editor.value());
        }
    </script>
</asp:Content>
