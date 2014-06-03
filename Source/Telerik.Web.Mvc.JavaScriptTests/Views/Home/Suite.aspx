<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Suite</title>

    <script type="text/javascript" src='<%= Url.Content("~/Scripts/jsUnit/app/jsUnitCore.js") %>'></script>
    <script type="text/javascript">
        function suite()
        {
            var allTests = new top.jsUnitTestSuite();
            var suite = new top.jsUnitTestSuite();

            suite.addTestPage("AutoComplete");
            suite.addTestPage("Core");
            suite.addTestPage("Calendar");
            suite.addTestPage("ComboBox");
            suite.addTestPage("DatePicker");
            suite.addTestPage("DropDownList");
            suite.addTestPage("Editor");
            suite.addTestPage("Grid");
            suite.addTestPage("Input");
            suite.addTestPage("Menu");
            suite.addTestPage("PanelBar");
            suite.addTestPage("TabStrip");
            suite.addTestPage("TreeView");
            suite.addTestPage("Window");
            
            allTests.addTestSuite(suite);
            return allTests;
        }
    </script>
</head>
<body>
</body>
</html>
