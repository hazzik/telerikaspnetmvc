<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Slider Events Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Slider()
                    .Name("mySlider")
                    .OnStart(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Start fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnSlide(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Slide fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnChange(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Change fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                              )
                    .OnStop(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Stop fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .Render();%>
    <br/>
    <div id="trace" style="font-family:Courier New;background-color:#eee"></div>
</asp:Content>