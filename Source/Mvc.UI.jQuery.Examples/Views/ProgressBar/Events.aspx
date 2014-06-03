<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ProgressBar Events Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().ProgressBar()
                    .Name("myProgressBar")
                    .Value(10)
                    .OnChange(()=>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Value changed: ' + new Date() + '<br/>');
                                        }
                                    <%}
                             )
                    .Render(); %>
    <br/>
    <input id="chanage" type="button" value="change"/>
    <br/>
    <div id="trace" style="font-family:Courier New;background-color:#eee"></div>
    <% Html.Telerik().ScriptRegistrar()
                     .OnDocumentReady(() => 
                                        {%>
                                            $('#chanage').click(
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