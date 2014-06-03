<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TestPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <script type="text/javascript">
	function test_failing() {
		assertEquals(true, false);
	}
	
	function test_succeeding() {
		assertEquals(true, true);
	}
   </script>

</asp:Content>
