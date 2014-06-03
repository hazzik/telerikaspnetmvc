<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Exec</h2>

    <%= Html.Telerik().Editor().Name("Editor1") %>

    <iframe src="<%= Url.Action("Toolbar", "Editor") %>"></iframe>

    <script type="text/javascript">
        var editor;

        function getEditor() {
            return $('#Editor1').data("tEditor");
        }

        function setUp() {
            editor = getEditor();
            editor.focus();
        }
        
        function test_exec_pushes_command_to_undo_stack() {
            editor.value('foo');
            var range = editor.createRange();
            range.selectNodeContents(editor.body.firstChild);
            editor.getSelection().removeAllRanges();
            editor.getSelection().addRange(range);
            
            var pushArgument;

            editor.undoRedoStack.push = function() { pushArgument = arguments[0] }
            
            editor.exec('bold');

            assertNotUndefined(pushArgument);
        }
        
        function test_exec_undo_performs_undo() {
            var original = editor.undoRedoStack.undo;
            try {
                var undoIsCalled = false;
                editor.undoRedoStack.undo = function() { undoIsCalled = true; }
                editor.exec('undo');

                assertTrue(undoIsCalled);
            } finally {
                editor.undoRedoStack.undo = original;
            }
        }
        
        function test_exec_redo_performs_redo() {
            

            var original = editor.undoRedoStack.redo;
            try {
                var redoIsCalled = false;
                editor.undoRedoStack.redo = function () { redoIsCalled = true; }
                editor.exec('redo');

                assertTrue(redoIsCalled);
            } finally {
                editor.undoRedoStack.redo = original;
            }
        }

        function assertCommand(name, type) {
            var original = editor.undoRedoStack.push
            try {
                var command;
                editor.value('foo');
                var range = editor.createRange();
                range.selectNodeContents(editor.body);
                editor.getSelection().removeAllRanges();
                editor.getSelection().addRange(range);
                editor.undoRedoStack.push = function () {
                    command = arguments[0];
                }
                editor.exec(name);

                assertTrue(command instanceof type);
            } finally {
                editor.undoRedoStack.push = original;
            }
        }

        function test_exec_inline_command() {
            $.each(['bold', 'italic', 'underline', 'strikethrough'], function() {
                assertCommand(this, $.telerik.editor.FormatCommand);
            });
        }
        
        function test_exec_unordered_list() {
            assertCommand('insertUnorderedList', $.telerik.editor.ListCommand);
        }
        
        function test_exec_ordered_list() {
            assertCommand('insertOrderedList', $.telerik.editor.ListCommand);
        }

        function test_exec_block_command() {
            $.each(['justifyCenter', 'justifyLeft', 'justifyRight', 'justifyFull'], function() {
                assertCommand(this, $.telerik.editor.FormatCommand);
            });
        }

        function test_exec_insertLineBreak_creates_newLineCommand() {
            assertCommand('insertLineBreak', $.telerik.editor.NewLineCommand);
        }
        
        function test_exec_insertParagraph_creates_paragraph_command() {
            assertCommand('insertParagraph', $.telerik.editor.ParagraphCommand);
        }

        function test_exec_createLink_creates_LinkCommand() {
            assertCommand('createLink', $.telerik.editor.LinkCommand);
        }
        
        function test_exec_unlink_creates_UnlinkCommand() {
            assertCommand('unlink', $.telerik.editor.UnlinkCommand);
        }
       
        function test_exec_insertImage_creates_image_command() {
            assertCommand('insertImage', $.telerik.editor.ImageCommand);
        }

        function test_exec_indent_creates_indent_command() {
            assertCommand('indent', $.telerik.editor.IndentCommand);
        }

        function test_exec_outdent_creates_indent_command() {
            assertCommand('outdent', $.telerik.editor.OutdentCommand);
        }
    </script>
</asp:Content>
