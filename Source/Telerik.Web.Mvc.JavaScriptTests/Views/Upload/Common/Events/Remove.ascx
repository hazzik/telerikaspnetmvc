<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<script type="text/javascript">

    test("remove fired when clicking remove", function() {
        var removeFired = false;
        uploadInstance = createUpload({ onRemove:
            function(e) {
                removeFired = true;
            }
        });

        simulateUpload();
        simulateRemove();

        ok(removeFired);
    });

    test("remove event arguments contain list of files", function() {
        var files = false;
        uploadInstance = createUpload({ onRemove:
            function(e) {
                files = e.files;
            }
        });

        simulateUpload();
        simulateRemove();

        assertSelectedFile(files);
    });

    test("cancelling remove aborts remove operation", function() {
        uploadInstance = createUpload({ onRemove:
            function(e) {
                e.preventDefault();
            }
        });

        var removeCalled = false;
        uploadInstance._submitRemove = function(data, onSuccess) {
            removeCalled = true;
        };

        simulateUpload();
        simulateRemove();

        ok(!removeCalled);
    });

</script>