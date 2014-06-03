<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ProgressBar Update Elements Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().ProgressBar()
                    .Name("myProgressBar")
                    .Value(10)
                    .UpdateElements("#progressValue")
                    .Render(); %>
    <span id="progressValue" style="color:Blue"></span>
    <br/>
    <input id="change" type="button" value="change"/>
    <% Html.Telerik().ScriptRegistrar()
                     .OnDocumentReady(() => 
                                        {%>
                                            $('#change').click(
                                                                    function()
                                                                    {
                                                                        var pb = $('#myProgressBar');
                                                                        var value = pb.progressbar('option','value');

                                                                        if (value == 100)
                                                                        {
                                                                            value = 10;
                                                                        }
                                                                        else
                                                                        {
                                                                            value += 10;
                                                                        }

                                                                        pb.progressbar('value', value);
                                                                    }
                                                                );
                                        <%}
                               );%>
</asp:Content>