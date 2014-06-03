<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tab Events Example
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.jQuery().Tab()
                    .Name("myTab")
                    .Items(parent =>
                                  {
                                      parent.Add()
                                          .Text("Nunc tincidunt")
                                          .Content(() =>
                                                         {%>
                                                             <p>
                                                                 Proin elit arcu, rutrum commodo, vehicula tempus, 
                                                                 commodo a, risus. Curabitur nec arcu. Donec 
                                                                 sollicitudin mi sit amet mauris. Nam elementum 
                                                                 quam ullamcorper ante. Etiam aliquet massa et 
                                                                 lorem. Mauris dapibus lacus auctor risus. Aenean 
                                                                 tempor ullamcorper leo. Vivamus sed magna quis 
                                                                 ligula eleifend adipiscing. Duis orci. Aliquam 
                                                                 sodales tortor vitae ipsum. Aliquam nulla. Duis 
                                                                 aliquam molestie erat. Ut et mauris vel pede 
                                                                 varius sollicitudin. Sed ut dolor nec orci 
                                                                 tincidunt interdum. Phasellus ipsum. Nunc 
                                                                 tristique tempus lectus.
                                                            </p>
                                                         <%}
                                                    );

                                       parent.Add()
                                           .Text("Proin dolor")
                                           .LoadContentFrom(Url.Action("AjaxView1", "Tab"));
                                  }
                           )
                    .OnSelect(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Selected fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnShow(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Show fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnAdd(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Add fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                              )
                    .OnRemove(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Remove fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnEnable(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Enable fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnDisable(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Disable fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .OnLoad(() =>
                                    {%>
                                        function(event, ui)
                                        {
                                            $('#trace').append('Load fired: ' + new Date() + '<br/>');
                                        }
                                    <%}
                            )
                    .Render(); %>
    <br/>
    <input id="add" type="button" value="Add"/>
    <input id="remove" type="button" value="Remove"/>
    <input id="disable" type="button" value="Disable"/>
    <input id="enable" type="button" value="Enable"/>
    <br/>
    <div id="trace" style="font-family:Courier New;background-color:#eee"></div>
    <% Html.Telerik().ScriptRegistrar()
                     .OnDocumentReady(() =>
                                        {%>
                                            $('#add').click(
                                                                    function()
                                                                    {
                                                                        var myTab = $('#myTab');
                                                                        var newNumber = myTab.tabs('length') + 1;

                                                                        myTab.tabs('add', '<%= Url.Action("AjaxView2", "Tab")%>', 'New Tab ' + newNumber);
                                                                    }
                                                          );

                                            $('#remove').click(
                                                                    function()
                                                                    {
                                                                        var myTab = $('#myTab');
                                                                        var index = myTab.tabs('length') - 1;
                                                                        myTab.tabs('remove', index);
                                                                    }
                                                              );

                                            $('#disable').click(
                                                                    function()
                                                                    {
                                                                        var myTab = $('#myTab');
                                                                        var index = myTab.tabs('length') - 1;
                                                                        myTab.tabs('disable', index);
                                                                    }
                                                              );

                                            $('#enable').click(
                                                                    function()
                                                                    {
                                                                        var myTab = $('#myTab');
                                                                        var index = myTab.tabs('length') - 1;
                                                                        myTab.tabs('enable', index);
                                                                    }
                                                              );
                                        <%}
                               );%>
</asp:Content>