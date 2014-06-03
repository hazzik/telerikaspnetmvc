<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<SelectListItem>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatePicker Basic Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <% Html.jQuery().DatePicker()
                        .Name("dateField")
                        .DateFormat("mm/dd/yy")
                        .Render(); %>
    </p>
    <p>
        <%= Html.DropDownList("ddlFormat", Model)%>
    </p>
    <% Html.Telerik().ScriptRegistrar()
                     .OnDocumentReady(() => 
                                        {%>
                                            $('#ddlFormat').change(
                                                                        function()
                                                                        {
                                                                            var dp = $('#dateField');
                                                                            dp.datepicker('option', 'dateFormat', $(this).val());
                                                                        }
                                                                  );
                                        <%}
                               );%>
</asp:Content>